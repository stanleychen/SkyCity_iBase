using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

using Microsoft.ApplicationBlocks.Data;

namespace iTrak.Importer.Data
{
    class MediaDAL : BaseDAL 
    {
        private string masterquery_Media;
        SqlDataAdapter adapter_Media;
        DataSetIXData dsIXData;       
        public MediaDAL()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters.IncidentAttachmentTableAdapter tableAdapter = new iTrak.Importer.Data.DataSetIXDataTableAdapters.IncidentAttachmentTableAdapter();
            adapter_Media = GetAdapter(tableAdapter);
            IncidentAttachmentExtendedTableAdapter extendedAdapter = new IncidentAttachmentExtendedTableAdapter();
            adapter_Media.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            this.masterquery_Media = adapter_Media.SelectCommand.CommandText;
            adapter_Media.InsertCommand.CommandTimeout = 10 * 60;
            adapter_Media.UpdateCommand.CommandTimeout = 10 * 60;
            adapter_Media.SelectCommand.CommandTimeout = 10 * 60;
        }
        #region "Update  Media DataRow"
        private void UpdateImage(ref System.Data.DataRow newRow, Guid hostGUID, System.Drawing.Bitmap bitmap, string fileFullName)
        {
            const int thumb_width = 200;
            const int thumb_height = 200;
            try
            {
                string fileName = System.IO.Path.GetFileName(fileFullName);
                string user_name = "Administrator";
                DateTime entryTime = DateTime.Now;

                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                iView.iTrak.iTrakCommon.ImageHelper.SaveJpeg(bitmap, memoryStream, 70);
                string attachType = "jpg";
                long attachmentSize = memoryStream.Length;

                Size thumbnailSize = new Size(thumb_width, thumb_height);
                Bitmap thumbnail = iView.iTrak.iTrakCommon.ImageHelper.GetThumbnail(bitmap, thumbnailSize, true);
                System.IO.MemoryStream thumbnailMemoryStream = new System.IO.MemoryStream();
                thumbnail.Save(thumbnailMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Add to dataset which contain full image data
          
                newRow["DetailedReportGUID"] = hostGUID;
                newRow["Attached"] = entryTime;
                newRow["AttachedmentData"] = iView.iTrak.iTrakCommon.ImageHelper.EncryptBytes(memoryStream.ToArray());
                newRow["Thumbnail"] = iView.iTrak.iTrakCommon.ImageHelper.EncryptBytes(thumbnailMemoryStream.ToArray());
                newRow["OriginalFilename"] = fileName.Trim();
                newRow["AttachedBy"] = user_name;
                newRow["AttachedType"] = attachType;
                newRow["AttachmentSize"] = attachmentSize;
                newRow["Linked"] = false;
                newRow["DataProviderType"] = 0;
                newRow["Deleted"] = false;
                //newRow["LastModifiedDate"] = iView.iTrak.iTrakCommon.Database.GetDatabaseDateTime();
                newRow["ServerCreateDate"] = newRow["LastModifiedDate"];
               
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add media", ex);          
            }
        }

        private void UpdateMedia(ref System.Data.DataRow newRow, Guid hostGUID, string fileFullName)
        {         
            FileStream fs = null;
            BinaryReader binReader = null;
            try
            {
                string user_name = "Administrator";
                DateTime entryTime = DateTime.Now;
                fs = File.Open(fileFullName, FileMode.Open);
                binReader = new BinaryReader(fs);
                byte[] bytes = new byte[fs.Length];
                bytes = binReader.ReadBytes((int)fs.Length);

                string attachType = System.IO.Path.GetExtension(fileFullName);
                if (attachType.Length > 1)
                    attachType = attachType.Substring(1);
                string fileName = System.IO.Path.GetFileName(fileFullName);
                long attachmentSize = fs.Length;
                // Add to dataset which contain full image data
                newRow["DetailedReportGUID"] = hostGUID;
                newRow["Attached"] = entryTime;
                newRow["AttachedmentData"] = iView.iTrak.iTrakCommon.ImageHelper.EncryptBytes(bytes);         
                newRow["OriginalFilename"] = fileName.Trim();
                newRow["AttachedBy"] = user_name;
                newRow["AttachedType"] = attachType;
                newRow["AttachmentSize"] = attachmentSize;
                newRow["Linked"] = false;
                newRow["DataProviderType"] = 0;
                newRow["Deleted"] = false;
                //newRow["LastModifiedDate"] = iView.iTrak.iTrakCommon.Database.GetDatabaseDateTime();
                newRow["ServerCreateDate"] = newRow["LastModifiedDate"];
              
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add media", ex);
            }
            finally
            {
                if(fs != null)
                    fs.Close();
                if (binReader != null)
                    binReader.Close();
            }
        }
        #endregion
        private bool IsMediaAdded(string fileFullName, System.Guid hostGUID, SqlTransaction trans)
        {
            string fileName = System.IO.Path.GetFileName(fileFullName);
            string sqlText = "Select count(OriginalFilename) From IncidentAttachment WHERE DetailedReportGUID = '{" + hostGUID.ToString() + "}' AND OriginalFilename = '" + fileName + "'";
            int count = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, sqlText);
            if (count > 0)
                return true;
            else
                return false;
        }
       
        public void ImportOneMedia(string fileFullName, System.Guid hostGUID, SqlTransaction trans)
        {
            this.SetTransaction(adapter_Media, trans);
            this.dsIXData.IncidentAttachment.Clear();
            string type = System.IO.Path.GetExtension(fileFullName).ToLower();        
            if (!IsMediaAdded(fileFullName, hostGUID, trans))
            {
                System.Data.DataRow newRow = this.dsIXData.IncidentAttachment.NewRow();
                newRow["IncidentAttachment"] = System.Guid.NewGuid();
                if (type == ".jpg" || type == ".gif" || type == ".bmp" || type == ".jpeg" || type == ".tif")
                {
                    Bitmap bmp = new Bitmap(fileFullName);
                    this.UpdateImage(ref newRow, hostGUID, bmp, fileFullName);
                }
                else //update media
                {
                    UpdateMedia(ref newRow, hostGUID, fileFullName);
                }
                this.dsIXData.IncidentAttachment.Rows.Add(newRow);
            }
            this.adapter_Media.Update(this.dsIXData,"IncidentAttachment");
        }
    }
}
