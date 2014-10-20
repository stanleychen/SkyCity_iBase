using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;

namespace iTrak.Importer.Data.Importers
{
    public class BriefingLog
    {
        private const string DEFAULT_STATUS = "Closed";
        public event EventHandler ImportedRowEvent;
        public event EventHandler ImportCompletedEvent;
        private int _count = 0;
        public int Count
        {
            get
            {
                if (_count == 0)
                    _count = GetSourceRowCount();

                return _count;
            }
        }
        #region GetSourceRowCount
        private int GetSourceRowCount()
        {
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_BriefingLog (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion

        #region ImportOneRecord
        private Guid ImportOneRecord(BriefingLogBE briefingLogBE)
        {


            List<SqlParameter> paraList = new List<SqlParameter>();
            SqlParameter briefingLogParam = new SqlParameter();
            briefingLogParam.ParameterName = "@BriefingLogGUID";
            briefingLogParam.DbType = DbType.Guid;
            briefingLogParam.Direction = ParameterDirection.InputOutput;
            if (briefingLogBE.BriefingLogGUID == Guid.Empty) //New Row
            {
                briefingLogParam.Value = DBNull.Value;
            }
            else
            {
                briefingLogParam.Value = briefingLogBE.BriefingLogGUID;
            }
            paraList.Add(briefingLogParam);
            paraList.Add(new SqlParameter("@DetailedReportGUID", briefingLogBE.DetailedReportGUID));
            paraList.Add(new SqlParameter("@StartDate", briefingLogBE.StartDate));
            paraList.Add(new SqlParameter("@EndDate", briefingLogBE.EndDate));
            paraList.Add(new SqlParameter("@DistributionType", briefingLogBE.DistributionType));
            paraList.Add(new SqlParameter("@Created", briefingLogBE.Created));
            paraList.Add(new SqlParameter("@CreatedBy", briefingLogBE.CreatedBy));
            paraList.Add(new SqlParameter("@Topic", briefingLogBE.Topic));
            paraList.Add(new SqlParameter("@Details", briefingLogBE.Details));
            paraList.Add(new SqlParameter("@LastModifiedDate", briefingLogBE.LastModifiedDate));
            paraList.Add(new SqlParameter("@ModifiedBy", briefingLogBE.ModifiedBy));
            paraList.Add(new SqlParameter("@ModuleName", briefingLogBE.ModuleName));
            paraList.Add(new SqlParameter("@BriefingLogType", briefingLogBE.BriefingLogType));
            paraList.Add(new SqlParameter("@BriefingLogSubType", briefingLogBE.BriefingLogSubType));
            paraList.Add(new SqlParameter("@SourceID", briefingLogBE.SourceID));
         
            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_BriefingLog]", paraList.ToArray());
            return (Guid)briefingLogParam.Value;
        }
        #endregion

        #region AppendUnmatchedTables
        //private void AppendUnmatchedTables(SqlTransaction sqlTran, ref StringBuilder sb, int rowID)
        //{
        //    string sourceSQL = "__iTrakImporter_sps_GameAudit_UnMatchedTableData";
        //    SqlParameter[] sqlParams = new SqlParameter[1];
        //    sqlParams[0] = new SqlParameter("@RowID", rowID);
        //    using (IDataReader dr = SqlHelper.ExecuteReader(sqlTran, CommandType.StoredProcedure, sourceSQL, sqlParams))
        //    {
        //        int i = 0;
        //        while (dr.Read())
        //        {
        //            if(i == 0)
        //            {
        //                sb.Append("--- Unmatched Fields From " + dr["TableName"].ToString() + " ---");
        //                sb.Append(System.Environment.NewLine);
        //            }
        //            if(!string.IsNullOrEmpty(dr["uTableString"].ToString().Trim()))
        //            {
        //                sb.Append(dr["uTableString"].ToString());
        //                sb.Append(System.Environment.NewLine);
        //            }
        //            #region Un matched Text Fields
        //            if (!string.IsNullOrEmpty(dr["uTableText1Value"].ToString().Trim()))
        //            {
        //                sb.Append(dr["uTableText1Caption"].ToString() + ": ");
        //                sb.Append(dr["uTableText1Value"].ToString());
        //                sb.Append(System.Environment.NewLine);
        //            }
        //            if(!string.IsNullOrEmpty(dr["uTableText2Value"].ToString().Trim()))
        //            {
        //                sb.Append(dr["uTableText2Caption"].ToString() + ": ");
        //                sb.Append(dr["uTableText2Value"].ToString());
        //                sb.Append(System.Environment.NewLine);
        //            }
        //             if(!string.IsNullOrEmpty(dr["uTableText3Value"].ToString().Trim()))
        //            {
        //                sb.Append(dr["uTableText3Caption"].ToString() + ": ");
        //                sb.Append(dr["uTableText3Value"].ToString());
        //                sb.Append(System.Environment.NewLine);
        //            }
        //            #endregion
        //        }
        //    }
        //}
        #endregion

        #region GetOneBriefingLog
        private BriefingLogBE GetOneBriefingLog(IDataReader dr)
        {
            BriefingLogBE briefingLogBE = new BriefingLogBE();
            briefingLogBE.BriefingLogGUID = DataHelper.GetGuid(dr["BriefingLogGUID"]);
            briefingLogBE.Created = DataHelper.GetDateTime(dr["Created"]);
            briefingLogBE.CreatedBy = SqlClientUtility.GetString(dr, "CreatedBy", string.Empty);
            //briefingLogBE.DetailedReportGUID = SqlClientUtility.GetGuid(dr, "DetailedReportGUID", Guid.Empty);
            briefingLogBE.Details = SqlClientUtility.GetString(dr, "Details", string.Empty);
            briefingLogBE.DistributionType = SqlClientUtility.GetInt32(dr, "DistributionType", 0);
            briefingLogBE.EndDate = SqlClientUtility.GetDateTime(dr, "EndDate", DateTime.Now);
            briefingLogBE.LastModifiedDate = SqlClientUtility.GetDateTime(dr, "LastModifiedDate", DateTime.Now);
            briefingLogBE.ModifiedBy = SqlClientUtility.GetString(dr, "ModifiedBy", string.Empty);
            briefingLogBE.ModuleName = SqlClientUtility.GetString(dr, "ModuleName", string.Empty);
            briefingLogBE.StartDate = SqlClientUtility.GetDateTime(dr, "StartDate", DateTime.Now);
            briefingLogBE.Topic = SqlClientUtility.GetString(dr, "Topic", string.Empty);

            briefingLogBE.BriefingLogType = SqlClientUtility.GetString(dr, "BriefingLogType", string.Empty);
            briefingLogBE.BriefingLogSubType = SqlClientUtility.GetString(dr, "BriefingLogSubType", string.Empty);

            briefingLogBE.Text1Caption = SqlClientUtility.GetString(dr, "Text1Caption", string.Empty);
            briefingLogBE.Text1 = SqlClientUtility.GetString(dr, "Text1", string.Empty);

            briefingLogBE.Text2Caption = SqlClientUtility.GetString(dr, "Text2Caption", string.Empty);
            briefingLogBE.Text2 = SqlClientUtility.GetString(dr, "Text2", string.Empty);

            briefingLogBE.Text3Caption = SqlClientUtility.GetString(dr, "Text3Caption", string.Empty);
            briefingLogBE.Text3 = SqlClientUtility.GetString(dr, "Text3", string.Empty);

            briefingLogBE.Text4Caption = SqlClientUtility.GetString(dr, "Text4Caption", string.Empty);
            briefingLogBE.Text4 = SqlClientUtility.GetString(dr, "Text4", string.Empty);

            briefingLogBE.Text5Caption = SqlClientUtility.GetString(dr, "Text5Caption", string.Empty);
            briefingLogBE.Text5 = SqlClientUtility.GetString(dr, "Text5", string.Empty);

            briefingLogBE.Text6Caption = SqlClientUtility.GetString(dr, "Text6Caption", string.Empty);
            briefingLogBE.Text6 = SqlClientUtility.GetString(dr, "Text6", string.Empty);

            briefingLogBE.Text7Caption = SqlClientUtility.GetString(dr, "Text7Caption", string.Empty);
            briefingLogBE.Text7 = SqlClientUtility.GetString(dr, "Text7", string.Empty);

            briefingLogBE.Text8Caption = SqlClientUtility.GetString(dr, "Text8Caption", string.Empty);
            briefingLogBE.Text8 = SqlClientUtility.GetString(dr, "Text8", string.Empty);

            briefingLogBE.Text9Caption = SqlClientUtility.GetString(dr, "Text9Caption", string.Empty);
            briefingLogBE.Text9 = SqlClientUtility.GetString(dr, "Text9", string.Empty);

            briefingLogBE.Text10Caption = SqlClientUtility.GetString(dr, "Text10Caption", string.Empty);
            briefingLogBE.Text10 = SqlClientUtility.GetString(dr, "Text10", string.Empty);

            briefingLogBE.uString = SqlClientUtility.GetString(dr, "uString", string.Empty);
            briefingLogBE.SourceID = SqlClientUtility.GetString(dr, "SourceID", string.Empty);
           

            if (briefingLogBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                briefingLogBE.LastModifiedDate = briefingLogBE.Created;
            if (briefingLogBE.Created < DataHelper.SQL_MIN_DATE)
                briefingLogBE.Created = briefingLogBE.LastModifiedDate;

            if (briefingLogBE.Created < DataHelper.SQL_MIN_DATE)
                briefingLogBE.Created = DateTime.Now;
            if (briefingLogBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                briefingLogBE.LastModifiedDate = DateTime.Now;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(briefingLogBE.Details);

            if (!string.IsNullOrEmpty(briefingLogBE.Text1))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text1Caption, briefingLogBE.Text1);

            if (!string.IsNullOrEmpty(briefingLogBE.Text2))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text2Caption, briefingLogBE.Text2);

            if (!string.IsNullOrEmpty(briefingLogBE.Text3))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text3Caption, briefingLogBE.Text3);

            if (!string.IsNullOrEmpty(briefingLogBE.Text4))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text4Caption, briefingLogBE.Text4);

            if (!string.IsNullOrEmpty(briefingLogBE.Text5))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text5Caption, briefingLogBE.Text5);

            if (!string.IsNullOrEmpty(briefingLogBE.Text6))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text6Caption, briefingLogBE.Text6);

            if (!string.IsNullOrEmpty(briefingLogBE.Text7))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text7Caption, briefingLogBE.Text7);

            if (!string.IsNullOrEmpty(briefingLogBE.Text8))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text8Caption, briefingLogBE.Text8);

            if (!string.IsNullOrEmpty(briefingLogBE.Text9))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text9Caption, briefingLogBE.Text9);

            if (!string.IsNullOrEmpty(briefingLogBE.Text10))
                sb.AppendFormat("{0}: {1}", briefingLogBE.Text10Caption, briefingLogBE.Text10);

            if(!string.IsNullOrEmpty(briefingLogBE.uString))
                sb.AppendLine(briefingLogBE.uString);

            briefingLogBE.Details = sb.ToString();

            return briefingLogBE;
        }
        #endregion

        #region ImportBriefingLogs
        public void ImportBriefingLogs()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_BriefingLog]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                try
                {
                    int i = 0;
                    while (dr.Read())
                    {
                        BriefingLogBE briefingLogBE = GetOneBriefingLog(dr);
                        this.ImportOneRecord(briefingLogBE);
                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing BriefingLog"));
                        }
                        if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                        {
                            break;
                        }
                    }

                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed BriefingLog"));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {

                }
            }
        }
        #endregion
    }
}
