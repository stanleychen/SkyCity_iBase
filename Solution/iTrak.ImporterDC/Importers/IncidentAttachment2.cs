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
    public class IncidentAttachment2
    {

        public event EventHandler ImportedRowEvent;
        private const string DEFAULT_STATUS = "Open";
        private const string ERROR_TYPE = "No Error";
        public event EventHandler ImportCompletedEvent;

        private string _sourceConnection = string.Empty;
        public IncidentAttachment2()
        {
            _sourceConnection = ConfigurationManager.ConnectionStrings["SourceDB"].ConnectionString;

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
                throw new Exception("Failed to build media", ex);
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
            else if (mediaType == "doc" || mediaType == "xls" || mediaType == "pdf" || mediaType == "txt" || mediaType == "db")
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
                    isImageType = true;
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
            mediaBE.OriginalFilename = SqlClientUtility.GetString(dr, "OriginalFilename", String.Empty);
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

            //Get Media From Source
            IDataReader sdr = SqlHelper.ExecuteReader(_sourceConnection, CommandType.Text,
                "SELECT Attachment FROM tblAttachments WHERE AttachmentID  = '" + mediaBE.SourceID + "'");

            if (sdr.Read())
            {
                mediaBE.AttachedmentData = SqlClientUtility.GetBytes(sdr, "Attachment", null);
            }
            sdr.Close();

            if (IsImage(mediaBE.AttachedType, mediaBE.AttachedmentData))
            {
                if(BuildiTrakImage(ref mediaBE) == false)
                    BuildiTrakMedia(ref mediaBE);
            }
            else
                BuildiTrakMedia(ref mediaBE);

            return mediaBE;
        }
        #endregion

        #endregion

        #region Import Media

        #region Import One Media
        private static void ImportOneMedia(SqlTransaction trans,IncidentAttachmentBE mediaBE)
        {
            try
            {

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

                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_IncidentAttachment]", paraList.ToArray());
                
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import Import Media", ex);
            }
            finally
            {
            }

        }
        #endregion

        #region ImportMedia
        private void ImportMedia(SqlTransaction trans, IDataReader dr, string hostType)
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;
            int tempHostRowID = 0;
            int i = 0;
            
            #region While Begin
            while (dr.Read())
            {
                IncidentAttachmentBE mediaBE = GetOneMedia(dr, hostType);

                int hostRowID = (int)dr["HostRowID"];
                if (IsImage(mediaBE.AttachedType,mediaBE.AttachedmentData) && hostRowID != tempHostRowID)
                {
                    mediaBE.IsBestAsset = true;
                }
                tempHostRowID = hostRowID;

                ImportOneMedia(trans,mediaBE);

                if (this.ImportedRowEvent != null)
                {
                    ImportedRowEvent(++i, new ImportEventArgs("Importing Media..."));
                }
                if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                {
                    break;
                }

            }
          
            #endregion While End

        }
        public void ImportMedia()
        {
         
            string sourceSQL = "[dbo].[__iTrakImporter_sps_IncidentAttachment]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                SqlConnection conn = null;
                SqlTransaction trans = null;
                try
                {
                    conn = new SqlConnection(DbHelper.iTrakConnectionString);
                    conn.Open();
                    trans = conn.BeginTransaction();

                    ImportMedia(trans,dr, "Subject"); //The first one is the subject result set
                    dr.NextResult(); //Incident Resultset
                    ImportMedia(trans,dr, "Incident");

                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed Media"));
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        #endregion

        #endregion

    }
}
