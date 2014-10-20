using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;

namespace iTrak.Importer.Data.Importers
{
    public class MiniAudit
    {
        private static ManualResetEvent[] _resetEvents;
        public event EventHandler ImportedRowEvent;
        private const string DEFAULT_STATUS = "Closed";
        private const string ERROR_TYPE = "No Error";
        public event EventHandler ImportCompletedEvent;

        private int _count = -1;
        public int Count
        {
            get
            {
                if (_count == -1) _count = GetSourceRowCount();

                return _count;
            }
        }
        public MiniAudit()
        {
            
        }
        #region GetSourceRowCount
        private int GetSourceRowCount()
        {
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_MiniAudit (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;
            return rowCount;
        }
        #endregion

        #region Receive Data

        #region AppendUnmatchedTables
        private void AppendUnmatchedTables(SqlTransaction sqlTran, ref StringBuilder sb, int rowID)
        {
            string sourceSQL = "[dbo].[__iTrakImporter_sps_MiniAudit_UnMatchedTableData]";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@RowID", rowID);
            using (IDataReader dr = SqlHelper.ExecuteReader(sqlTran, CommandType.StoredProcedure, sourceSQL, sqlParams))
            {
                string tempTableName = string.Empty;
                while (dr.Read())
                {
                    if (tempTableName != dr["TableName"].ToString())
                    {
                        sb.Append("--- Unmatched Fields From " + dr["TableName"].ToString() + " ---");
                        sb.Append(System.Environment.NewLine);
                    }
                    if (!string.IsNullOrEmpty(dr["uTableString"].ToString().Trim()))
                    {
                        sb.Append(dr["uTableString"].ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    #region Un matched Text Fields
                    if (!string.IsNullOrEmpty(dr["uTableText1Value"].ToString().Trim()))
                    {
                        sb.Append(dr["uTableText1Caption"].ToString() + ": ");
                        sb.Append(dr["uTableText1Value"].ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    if (!string.IsNullOrEmpty(dr["uTableText2Value"].ToString().Trim()))
                    {
                        sb.Append(dr["uTableText2Caption"].ToString() + ": ");
                        sb.Append(dr["uTableText2Value"].ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    if (!string.IsNullOrEmpty(dr["uTableText3Value"].ToString().Trim()))
                    {
                        sb.Append(dr["uTableText3Caption"].ToString() + ": ");
                        sb.Append(dr["uTableText3Value"].ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region GetOneMiniAudit
        private MiniAuditBE GetOneMiniAudit(SqlTransaction trans, IDataReader dr)
        {
            MiniAuditBE miniAuditBE = new MiniAuditBE();
            miniAuditBE.MiniAuditGUID = DataHelper.GetGuid(dr["MiniAuditGUID"]);
            miniAuditBE.Number = dr["Number"].ToString();
            miniAuditBE.ModifiedBy = DataHelper.GetUserName(dr["ModifiedBy"].ToString());
            miniAuditBE.Archived = false;
            miniAuditBE.Closed = DataHelper.GetBool(dr["Closed"]);
            miniAuditBE.CreatedBy = DataHelper.GetUserName(dr["CreatedBy"].ToString());
            miniAuditBE.DateCreated = DataHelper.GetDateTime(dr["DateCreated"]);
            miniAuditBE.Exclusive = string.Empty;
            miniAuditBE.MiniAuditStartDateTime = DataHelper.GetDateTime(dr["MiniAuditStartDateTime"]);
            miniAuditBE.MiniAuditEndDateTime = DataHelper.GetDateTime(dr["MiniAuditEndDateTime"]);
            miniAuditBE.LastModifiedDate = DataHelper.GetDateTime(dr["LastModifiedDate"]);
            miniAuditBE.ModifiedBy = DataHelper.GetUserName(dr["ModifiedBy"].ToString());
            miniAuditBE.RequestedByGUID = DataHelper.GetGuid(dr["RequestedByGUID"]);
            miniAuditBE.ReviewMethod = dr["ReviewMethod"].ToString();
            miniAuditBE.SecondAuditor = dr["SecondAuditor"].ToString();
            miniAuditBE.Section = dr["Section"].ToString();
            miniAuditBE.Status = dr["Status"].ToString();
            miniAuditBE.VCRConsoleNumber = dr["VCRConsoleNumber"].ToString();
            miniAuditBE.PropertyGUID = (Guid)dr["PropertyGUID"];
            miniAuditBE.Area = dr["Area"].ToString();
            miniAuditBE.SubjectGUID = DataHelper.GetGuid(dr["SubjectGuid"]);

            if (miniAuditBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                miniAuditBE.LastModifiedDate = miniAuditBE.DateCreated;

            if (miniAuditBE.DateCreated < DataHelper.SQL_MIN_DATE)
                miniAuditBE.DateCreated = miniAuditBE.LastModifiedDate;

            if (miniAuditBE.DateCreated < DataHelper.SQL_MIN_DATE)
                miniAuditBE.DateCreated = DateTime.Now;

            if (miniAuditBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                miniAuditBE.LastModifiedDate = DateTime.Now;

            StringBuilder sb = new StringBuilder();
            sb.Append(dr["Remarks"].ToString());
            sb.Append(System.Environment.NewLine);
            sb.Append(dr["uString"].ToString());
            this.AppendUnmatchedTables(trans, ref sb, (int)dr["RowID"]);
            miniAuditBE.Remarks = sb.ToString();

            if (DEFAULT_STATUS == "Closed")
                miniAuditBE.Closed = true;
            return miniAuditBE;
        }
        #endregion

        #endregion

        #region GetParameters
        private static List<SqlParameter> GetParameters(MiniAuditBE miniAuditBE, out SqlParameter miniAuditParam)
        {
            if (string.IsNullOrEmpty(miniAuditBE.Status))
                miniAuditBE.Status = DEFAULT_STATUS;
            List<SqlParameter> paraList = new List<SqlParameter>();
            miniAuditParam = new SqlParameter();
            miniAuditParam.ParameterName = "@MiniAuditGUID";
            miniAuditParam.DbType = DbType.Guid;
            miniAuditParam.Direction = ParameterDirection.InputOutput;
            if (miniAuditBE.MiniAuditGUID == Guid.Empty) //New Row
            {
                miniAuditParam.Value = DBNull.Value;
            }
            else
            {
                miniAuditParam.Value = miniAuditBE.MiniAuditGUID;
            }
            paraList.Add(miniAuditParam);
            paraList.Add(new SqlParameter("@Number", miniAuditBE.Number));
            paraList.Add(new SqlParameter("@PropertyGUID", miniAuditBE.PropertyGUID));
            paraList.Add(new SqlParameter("@ReviewMethod ", miniAuditBE.ReviewMethod));
            paraList.Add(new SqlParameter("@MiniAuditStartDateTime", miniAuditBE.MiniAuditStartDateTime));
            paraList.Add(new SqlParameter("@MiniAuditEndDateTime", miniAuditBE.MiniAuditEndDateTime));
            paraList.Add(new SqlParameter("@VCRConsoleNumber", miniAuditBE.VCRConsoleNumber));
            paraList.Add(new SqlParameter("@CreatedBy", miniAuditBE.CreatedBy));
            paraList.Add(new SqlParameter("@Closed", miniAuditBE.Closed));
            paraList.Add(new SqlParameter("@Area", miniAuditBE.Area));
            paraList.Add(new SqlParameter("@ModifiedBy", miniAuditBE.ModifiedBy));
            paraList.Add(new SqlParameter("@DateCreated", miniAuditBE.DateCreated));
            paraList.Add(new SqlParameter("@SecondAuditor", miniAuditBE.SecondAuditor));
            paraList.Add(new SqlParameter("@Section", miniAuditBE.Section));
            paraList.Add(new SqlParameter("@Remarks", miniAuditBE.Remarks));
            paraList.Add(new SqlParameter("@LastModifiedDate", miniAuditBE.LastModifiedDate));
            if (miniAuditBE.RequestedByGUID != Guid.Empty)
                paraList.Add(new SqlParameter("@RequestedByGUID", miniAuditBE.RequestedByGUID));
            if(miniAuditBE.SubjectGUID !=Guid.Empty)
                paraList.Add(new SqlParameter("@SubjectGUID", miniAuditBE.SubjectGUID));
            paraList.Add(new SqlParameter("@Status ", miniAuditBE.Status));

            return paraList;
        }
        #endregion

        #region ImportMiniAudits2

        #region ImportOneRecord2
        private static void ImportOneMiniAudit2(object o)
        {
            object[] objs = o as object[];
            int index = (int)objs[0];
            try
            {
                MiniAuditBE miniAuditBE = objs[1] as MiniAuditBE;

                SqlParameter miniAuditParam = null;
                List<SqlParameter> paraList = GetParameters(miniAuditBE, out miniAuditParam);
                SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_MiniAudit]", paraList.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import one game dispute", ex);
            }
            finally
            {
                _resetEvents[index].Set();
            }
        }
        #endregion

        #region ImportMiniAudits2
        public void ImportMiniAudits2()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;
            string sourceSQL = "[dbo].[__iTrakImporter_sps_MiniAudit]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.iTrakConnectionString))
                {
                    conn.Open();
                    SqlTransaction trans = null;
                    try
                    {
                        trans = conn.BeginTransaction();
                        int i = 0;
                        _resetEvents = new ManualResetEvent[_count];
                        while (dr.Read())
                        {
                            MiniAuditBE miniAuditBE = GetOneMiniAudit(trans, dr);
                            int index = i % ThreadHelper.NUMBER_OF_THREADS;
                            object[] objs = new object[2];
                            objs[0] = index;
                            objs[1] = miniAuditBE;
                            _resetEvents[index] = new ManualResetEvent(false);
                            ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneMiniAudit2), objs);
                            if (index == ThreadHelper.NUMBER_OF_THREADS - 1)
                            {
                                ThreadHelper.WaitAll(_resetEvents);
                                _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                            }
                            if (this.ImportedRowEvent != null)
                            {
                                ImportedRowEvent(++i, new ImportEventArgs("Importing Game Dispute"));
                            }
                            if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                            {
                                break;
                            }                          
                        }
                        ThreadHelper.WaitAll(_resetEvents);
                        trans.Commit();
                        conn.Close();

                        if (this.ImportCompletedEvent != null)
                        {
                            ImportCompletedEvent(_count, new ImportEventArgs("Completed Game Dispute"));
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to import Mini Audits", ex);
                    }
                    finally
                    {
                        if (conn != null) 
                            conn.Close();      
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Import MiniAudits

        #region ImportOneRecord
        private Guid ImportOneRecord(SqlTransaction trans, MiniAuditBE miniAuditBE)
        {
            SqlParameter miniAuditParam = null;
            List<SqlParameter> paraList = GetParameters(miniAuditBE, out miniAuditParam);

            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_MiniAudit]", paraList.ToArray());
            return (Guid)miniAuditParam.Value;
        }
        #endregion

        #region ImportMiniAudits
        public void ImportMiniAudits()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;
            string sourceSQL = "[dbo].[__iTrakImporter_sps_MiniAudit]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.iTrakConnectionString))
                {
                    conn.Open();
                    SqlTransaction trans  = null;
                    try
                    {
                        trans = conn.BeginTransaction();
                        int i = 0;
                        _resetEvents = new ManualResetEvent[_count];
                        while (dr.Read())
                        {
                            MiniAuditBE miniAuditBE = GetOneMiniAudit(trans, dr);
                            Guid miniAuditGuid = this.ImportOneRecord(trans, miniAuditBE);
                            if (this.ImportedRowEvent != null)
                            {
                                ImportedRowEvent(++i, new ImportEventArgs("Importing Mini Audit"));
                            }
                            if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                            {
                                break;
                            }
                        }
                        trans.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        if(trans != null)
                            trans.Rollback();
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {
                    }
                }
            }
        }
        #endregion

        #endregion End of Import GameAduits

    }
}
