using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
namespace iTrak.Importer.Data
{
    public class MediaImporter : BaseDAL
    {       
        private string iXDataConnectionString;
        public event EventHandler ImportedNumberOfMedia;
        private List<iTrak.Importer.Entities.MediaSourceBE> _mediaList;
        public MediaImporter(List<iTrak.Importer.Entities.MediaSourceBE> list, string iXDataConnectionString)
        {
            this._mediaList = list;
            this.iXDataConnectionString = iXDataConnectionString;
        }

        #region "Helper Function"
        //private FileInfo[] GetFiles()
        //{
        //    System.IO.DirectoryInfo dirinfo = new System.IO.DirectoryInfo(sourcePath);
        //    return  dirinfo.GetFiles("????_*",SearchOption.TopDirectoryOnly);
        //}
        
        private string GetNumber(string fileName)
        {
           string[] tmpStr = fileName.Split('_');
            string number;
            try
            {
                if (tmpStr[1].IndexOf(".") > 0)
                    number = tmpStr[1].Substring(0, tmpStr[1].IndexOf("."));
                else
                    number = tmpStr[1];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
            }
           return number;
        }
        private DataView GetIncidentIDDataView()
        {
            string sqlText = "SELECT DetailedReportGUID,Number FROM DetailedReport";
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(iXDataConnectionString, CommandType.Text, sqlText);
            return ds.Tables[0].DefaultView;
        }
        private Nullable<System.Guid> FindHostGuid(DataView hostView,string number)
        {
            int idx = hostView.Find(number);

            if (idx > -1)
                return (Guid)hostView[idx]["DetailedReportGUID"];
            else
                return null;

        }
        #endregion

        public int GetNumberOfMedia()
        {
            return _mediaList.Count;
        }

        #region ImportIncidentMedia
        private DataView GetSubjectIDDataView()
        {
            string sqlText = "SELECT SubjectGUID as DetailedReportGUID,ConvertId1 as Number FROM SubjectProfile s INNER JOIN _ConversionTmp c ON s.SubjectGUID = c.ConvertGUID";
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(iXDataConnectionString, CommandType.Text, sqlText);
            return ds.Tables[0].DefaultView;
        }
        public void ImportIncidentMedia()
        {
            System.Data.DataView guidView = GetIncidentIDDataView();
            guidView.Sort = "Number";            
            using (SqlConnection conn = new SqlConnection(iXDataConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    MediaDAL mediaDAL = new MediaDAL();
                    int i = 0;
                    foreach (iTrak.Importer.Entities.MediaSourceBE sourceBE in _mediaList)
                    {
                        string number = "II_" + sourceBE.ID;
                        Nullable<Guid> hostGuid = FindHostGuid(guidView, number);
                        if (hostGuid != null) //import one
                        {
                            mediaDAL.ImportOneMedia(sourceBE.FileFullName, (Guid)hostGuid, trans);
                        }
                        if (this.ImportedNumberOfMedia != null)
                            ImportedNumberOfMedia(++i, null);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message, ex);               
                }
            }
        }
        #endregion

        #region Import Subject Media
        public void ImportSubjectMedia()
        {
            System.Data.DataView guidView = GetSubjectIDDataView();
            guidView.Sort = "Number";
            using (SqlConnection conn = new SqlConnection(iXDataConnectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    MediaDAL mediaDAL = new MediaDAL();
                    int i = 0;
                    foreach (iTrak.Importer.Entities.MediaSourceBE sourceBE in _mediaList)
                    {
                        string number = sourceBE.ID;
                        Nullable<Guid> hostGuid = FindHostGuid(guidView, number);
                        if (hostGuid != null) //import one
                        {
                            mediaDAL.ImportOneMedia(sourceBE.FileFullName, (Guid)hostGuid, trans);
                        }
                        if (this.ImportedNumberOfMedia != null)
                            ImportedNumberOfMedia(++i, null);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message, ex);
                }
            }
        }
        #endregion

    }
}
