using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;
namespace iTrak.Importer.Data.Importers
{
    public class IncidentAttachment
    {

        private static ManualResetEvent[] _resetEvents;
        public event EventHandler ImportedRowEvent;
        private const string DEFAULT_STATUS = "Open";
        private const string ERROR_TYPE = "No Error";
        public event EventHandler ImportCompletedEvent;
        private string _sourceKey = string.Empty;
        private string _mediaFolder = string.Empty;
        private string _sourceConnection = string.Empty;
        public IncidentAttachment()
        {
            //_sourceConnection = ConfigurationManager.ConnectionStrings["Cambridge"].ConnectionString;

            string folder = string.Empty;
            //if (ConfigurationManager.AppSettings["MediaLocation"] != null)
            //{
            //    folder = ConfigurationManager.AppSettings["MediaLocation"];
            //    if (!folder.EndsWith(@"\"))
            //        folder += @"\";
            //}
            this._mediaFolder = folder;
        }
        public IncidentAttachment(string sourceKey)
        {
            _sourceConnection = ConfigurationManager.ConnectionStrings[sourceKey].ConnectionString;
            this._sourceKey = sourceKey;
        }

        #region Count
        private int _count = -1;
        public int Count
        {
            get
            {
                if (_count == -1) _count = GetSourceRowCount();

                return _count;
            }
        }
        #endregion

        #region GetSourceRowCount
        private int GetSourceRowCount()
        {
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_IncidentAttachment (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;
            return rowCount;
        }
        #endregion

        #region Get One Media

        #region BuildiTrakImage
        private byte[] GetMediaFromFile(string fileFullName, string attachedType)
        {
            byte[] media = null;
            try
            {
                if (attachedType == "jpg")
                {
                    if (File.Exists(fileFullName))
                    {
                        Bitmap bitmap = new Bitmap(fileFullName);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            media = ms.ToArray();
                        }
                    }
                }
                else
                {
                    if (File.Exists(fileFullName))
                    {
                        using (FileStream fileStream = File.OpenRead(fileFullName))
                        {
                            using (MemoryStream memStream = new MemoryStream())
                            {
                                memStream.SetLength(fileStream.Length);
                                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
                                media = memStream.ToArray();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Failed to GetMediaFromFile. FileName: {0}", fileFullName);
                CCGeneral.Logging.LogError(msg, ex);
            }
            return media;
        }
        private bool BuildiTrakImage(ref IncidentAttachmentBE mediaBE)
        {
            const int thumb_width = 200;
            const int thumb_height = 200;
            MemoryStream sourceMS = null;
            MemoryStream destMS = null;
            bool success = true;
            try
            {
                if (mediaBE.Attached < DataHelper.SQL_MIN_DATE)
                    mediaBE.Attached = DateTime.Now;
                if (mediaBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                    mediaBE.LastModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(mediaBE.AttachedBy))
                    mediaBE.AttachedBy = "Administrator";

                string attachType = "jpg";

                if (mediaBE.OriginalFilename.IndexOf(".") == -1)
                    mediaBE.OriginalFilename = mediaBE.OriginalFilename + "." + attachType;


                if (mediaBE.AttachedmentData == null)
                    return true;

                sourceMS = new MemoryStream(mediaBE.AttachedmentData);
                Bitmap bitmap = new Bitmap(sourceMS);

                destMS = new System.IO.MemoryStream();
                iView.iTrak.iTrakCommon.ImageHelper.SaveJpeg(bitmap, destMS, 70);
               
                long attachmentSize = destMS.Length;

                Size thumbnailSize = new Size(thumb_width, thumb_height);
                Bitmap thumbnail = iView.iTrak.iTrakCommon.ImageHelper.GetThumbnail(bitmap, thumbnailSize, true);
                System.IO.MemoryStream thumbnailMemoryStream = new System.IO.MemoryStream();
                thumbnail.Save(thumbnailMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                mediaBE.AttachmentSize = attachmentSize;
                mediaBE.AttachedType = attachType;
                mediaBE.AttachedmentData = iView.iTrak.iTrakCommon.ImageHelper.EncryptBytes(destMS.ToArray());
                mediaBE.Thumbnail = iView.iTrak.iTrakCommon.ImageHelper.EncryptBytes(thumbnailMemoryStream.ToArray());


            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError("Failed to build Image SourceID: " + mediaBE.SourceID, ex);
                success = false;
            }
            finally
            {
                if(destMS != null) 
                    destMS.Close();
                if (sourceMS != null)
                    sourceMS.Close();
            }
            return success;
        }
        #endregion 

        private void BuildiTrakMedia(ref IncidentAttachmentBE mediaBE)
        {
            try
            {
                if (mediaBE.Attached < DataHelper.SQL_MIN_DATE)
                    mediaBE.Attached = DateTime.Now;
                if (mediaBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                    mediaBE.LastModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(mediaBE.AttachedBy))
                    mediaBE.AttachedBy = "Administrator";

                long attachmentSize = mediaBE.AttachedmentData.Length;
                mediaBE.AttachedmentData = iView.iTrak.iTrakCommon.ImageHelper.EncryptBytes(mediaBE.AttachedmentData);

                mediaBE.AttachmentSize = attachmentSize;
                if (mediaBE.OriginalFilename.IndexOf(".") == -1)
                    mediaBE.OriginalFilename = mediaBE.OriginalFilename + "." + mediaBE.AttachedType;

            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError("Failed to build Image SourceID: " + mediaBE.SourceID, ex);
            }
            finally
            {
             
            }
        }


        #region GetOneMedia
        private static bool IsImage(string mediaType, byte[] AttachedmentData)
        {
            if(mediaType == null)
                mediaType = string.Empty;
            mediaType = mediaType.ToLower();
            bool isImageType = false;
            if (mediaType == "jpg" || mediaType == "gif" || mediaType == "bmp" || mediaType == "jpeg" || mediaType == "tif")
                isImageType = true;
            else if (mediaType == "rtf" || mediaType == "pef" || mediaType == "shs" || mediaType == "dat" || string.IsNullOrEmpty(mediaType) ||
               mediaType == "xlsx" || mediaType == "doc" || mediaType == "xls" || mediaType == "pdf" || mediaType == "txt" || mediaType == "db")
            {
                isImageType = false;
            }
            else //unknown type
            {
                MemoryStream msources = null;
                try
                {
                    msources = new MemoryStream(AttachedmentData);
                    Bitmap bitmap = new Bitmap(msources);
                }
                catch
                {
                    isImageType = false;
                }
                finally
                {
                    if (msources != null)
                    {
                        msources.Close();
                        msources.Dispose();
                    }
                }
            }
            return isImageType;
        }
        private IncidentAttachmentBE GetOneMedia(IDataReader dr, string hostType)
        {
            IncidentAttachmentBE mediaBE = new IncidentAttachmentBE();
            mediaBE.HostType = hostType; // "Subject";
            mediaBE.IncidentAttachment = SqlClientUtility.GetGuid(dr, "MediaGuid", Guid.Empty);
            mediaBE.DetailedReportGUID = SqlClientUtility.GetGuid(dr, "HostGuid", Guid.Empty);
            mediaBE.SourceID = SqlClientUtility.GetString(dr, "SourceID", string.Empty);
            mediaBE.Attached = SqlClientUtility.GetDateTime(dr, "Attached", DateTime.Now);
            mediaBE.AttachedmentData = SqlClientUtility.GetBytes(dr, "AttachedmentData", null);
            mediaBE.OriginalFilename = GetMediaFileFullName(dr);
            mediaBE.AttachedBy = SqlClientUtility.GetString(dr, "AttachedBy", String.Empty);
            mediaBE.Thumbnail = SqlClientUtility.GetBytes(dr, "Thumbnail", null);
            mediaBE.AttachedType = DataHelper.GetMediaType(SqlClientUtility.GetString(dr, "AttachedType", String.Empty));
            mediaBE.AttachmentSize = SqlClientUtility.GetInt64(dr, "AttachmentSize", 0);
            mediaBE.Linked = SqlClientUtility.GetBoolean(dr, "Linked", false);
            mediaBE.DigitalSignature = SqlClientUtility.GetBytes(dr, "DigitalSignature", null);
            mediaBE.DataProviderType = SqlClientUtility.GetInt32(dr, "DataProviderType", 0);
            mediaBE.LastModifiedDate = SqlClientUtility.GetDateTime(dr, "LastModifiedDate", DateTime.Now);
            mediaBE.ServerCreateDate = SqlClientUtility.GetDateTime(dr, "ServerCreateDate", DateTime.Now);
            mediaBE.MediaTitle = SqlClientUtility.GetString(dr, "Title", string.Empty);

            string sourceID = mediaBE.SourceID;
            string dataColumnName = "Attachment";
            string keyColumnName = "AttachmentID";

            //if (IsGuid(sourceID))
            //{
            //    string sql = string.Format("SELECT {0} FROM tblAttachments (nolock) WHERE {1}  = '{2}'", dataColumnName, keyColumnName, sourceID);
            //    IDataReader sdr = SqlHelper.ExecuteReader(_sourceConnection, CommandType.Text, sql);

            //    if (sdr.Read())
            //    {
            //        mediaBE.AttachedmentData = SqlClientUtility.GetBytes(sdr, dataColumnName, null);
            //    }
            //    sdr.Close();
            //}


            string attachType = string.Empty;
            if (mediaBE.AttachedmentData == null && !string.IsNullOrEmpty(mediaBE.OriginalFilename)) //Get Media from file
            {
               
                if (mediaBE.OriginalFilename.ToLower().EndsWith(".jpg"))
                {
                    attachType = "jpg";
                }
                else
                {
                    attachType = mediaBE.OriginalFilename.Substring(mediaBE.OriginalFilename.LastIndexOf(".") + 1);
                }
                mediaBE.AttachedmentData = GetMediaFromFile(mediaBE.OriginalFilename, attachType);
            }
            if (string.IsNullOrEmpty(mediaBE.AttachedType))
            {
                mediaBE.AttachedType = attachType;
            }
            if (IsImage(mediaBE.AttachedType, mediaBE.AttachedmentData))
            {
                if (BuildiTrakImage(ref mediaBE) == false)
                {
                    BuildiTrakMedia(ref mediaBE);
                }
            }
            else
                BuildiTrakMedia(ref mediaBE);

            return mediaBE;
        }

        private bool IsGuid(string sourceId)
        {
            bool isGuid = false;
            try
            {
                Guid test = new Guid(sourceId);
                isGuid = true;
            }
            catch
            {
                isGuid = false;
            }
            return isGuid;
        }
        #endregion

        #endregion

        #region Import Media

        #region Import One Media
        private static void ImportOneMedia(object o)
        {
            try
            {
                object[] objs = o as object[];
                int index = (int)objs[0];
                IncidentAttachmentBE mediaBE = objs[1] as IncidentAttachmentBE;

                List<SqlParameter> paraList = new List<SqlParameter>();
                SqlParameter mediaParam = new SqlParameter();
                mediaParam.ParameterName = "@IncidentAttachment";
                mediaParam.DbType = DbType.Guid;
                mediaParam.Direction = ParameterDirection.InputOutput;
                if (mediaBE.IncidentAttachment == Guid.Empty) //New Row
                {
                    mediaParam.Value = DBNull.Value;
                }
                else
                {
                    mediaParam.Value = mediaBE.IncidentAttachment;
                }
                paraList.Add(mediaParam);
                
				paraList.Add(new SqlParameter("@DetailedReportGUID", mediaBE.DetailedReportGUID));
                paraList.Add(new SqlParameter("@SourceID", mediaBE.SourceID));
                paraList.Add(new SqlParameter("@Attached", mediaBE.Attached));
                paraList.Add(new SqlParameter("@AttachedmentData", mediaBE.AttachedmentData));
                paraList.Add(new SqlParameter("@OriginalFilename", mediaBE.OriginalFilename));
                paraList.Add(new SqlParameter("@AttachedBy", mediaBE.AttachedBy));
                if(mediaBE.Thumbnail != null)
                    paraList.Add(new SqlParameter("@Thumbnail", mediaBE.Thumbnail));
                paraList.Add(new SqlParameter("@AttachedType", mediaBE.AttachedType));
                paraList.Add(new SqlParameter("@AttachmentSize", mediaBE.AttachmentSize));

                paraList.Add(new SqlParameter("@Linked", mediaBE.Linked));
                if(mediaBE.DigitalSignature != null)
                    paraList.Add(new SqlParameter("@DigitalSignature", mediaBE.DigitalSignature));
                paraList.Add(new SqlParameter("@DataProviderType", mediaBE.DataProviderType));
                paraList.Add(new SqlParameter("@Deleted", mediaBE.Deleted));
                paraList.Add(new SqlParameter("@LastModifiedDate", mediaBE.LastModifiedDate));
                paraList.Add(new SqlParameter("@ServerCreateDate", mediaBE.ServerCreateDate));
                paraList.Add(new SqlParameter("@HostType",mediaBE.HostType));
                paraList.Add(new SqlParameter("@IsBestAsset",mediaBE.IsBestAsset));
                if(!string.IsNullOrEmpty(mediaBE.MediaTitle))
                    paraList.Add(new SqlParameter("@MediaTitle", mediaBE.MediaTitle));

                SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_IncidentAttachment]", paraList.ToArray());
                _resetEvents[index].Set();
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError("IncidentAttachment.ImportOneMedia", ex);
                throw new Exception("Failed to import Import Media", ex);
            }
            finally
            {
            }

        }
        #endregion

        #region ImportMedia
        private string GetMediaFileFullName(IDataReader dr)
        {
            
            string fileName = SqlClientUtility.GetString(dr, "OriginalFilename", string.Empty);

            if (!string.IsNullOrEmpty(_mediaFolder))
                fileName = string.Format("{0}{1}", _mediaFolder, fileName);


            return fileName;
        }
        private void ImportMedia(IDataReader dr, string hostType)
        {

            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;
            int tempHostRowID = 0;
            int i = 0;
            _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS_MEDIA];
            #region While Begin
            while (dr.Read())
            {
                //string sourceId = SqlClientUtility.GetString(dr, "SourceID", string.Empty);

                //if (!IsGuid(sourceId))
                //{
                //    if (dr["AttachedmentData"] == System.DBNull.Value)
                //    {
                //        string fileName = GetMediaFileFullName(dr);
                //        if (string.IsNullOrEmpty(fileName) ||
                //            !System.IO.File.Exists(fileName))
                //            continue;
                //    }
                //}

                IncidentAttachmentBE mediaBE = GetOneMedia(dr, hostType);
                if (mediaBE.AttachedmentData == null)
                    continue;

                int hostRowID = (int)dr["HostRowID"];
                if (IsImage(mediaBE.AttachedType, mediaBE.AttachedmentData) && hostRowID != tempHostRowID)
                {
                    mediaBE.IsBestAsset = true;
                }
                tempHostRowID = hostRowID;

                int index = i % ThreadHelper.NUMBER_OF_THREADS_MEDIA;
                object[] objs = new object[2];
                objs[0] = index;
                objs[1] = mediaBE;

                _resetEvents[index] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneMedia), objs);
                if (index == ThreadHelper.NUMBER_OF_THREADS_MEDIA - 1)
                {
                    ThreadHelper.WaitAll(_resetEvents);
                    _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS_MEDIA];

                }
                if (this.ImportedRowEvent != null)
                {
                    ImportedRowEvent(++i, new ImportEventArgs("Importing Media..."));
                }
                if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                {
                    break;
                }

            }
            ThreadHelper.WaitAll(_resetEvents);

            #endregion While End


        }
        public void ImportMedia()
        {
            string sourceSQL = "[dbo].[__iTrakImporter_sps_IncidentAttachment]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                ImportMedia(dr, "Subject"); //The first one is the subject result set
                dr.NextResult(); //Incident Resultset
                ImportMedia(dr, "Incident");
                dr.NextResult(); //BriefingLog Resultset
                ImportMedia(dr, "BriefingLog");
                dr.NextResult(); //GameAudit Resultset
                ImportMedia(dr, "GameAudit");
                dr.NextResult(); //Employee Resultset
                ImportMedia(dr, "Employee");

                if (this.ImportCompletedEvent != null)
                {
                    ImportCompletedEvent(_count, new ImportEventArgs("Completed Media"));
                }
            }
        }
        #endregion

        #endregion

    }
}
