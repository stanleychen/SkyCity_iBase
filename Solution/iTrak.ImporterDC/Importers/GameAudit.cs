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
    public class GameAudit
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_GameAudit (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion

        #region ImportOneRecord
        private Guid ImportOneRecord(GameAuditBE gameAuditBE)
        {
            if (string.IsNullOrEmpty(gameAuditBE.Status))
                gameAuditBE.Status = DEFAULT_STATUS;

            List<SqlParameter> paraList = new List<SqlParameter>();
            SqlParameter gameAuditParam = new SqlParameter();
            gameAuditParam.ParameterName = "@GameAuditGUID";
            gameAuditParam.DbType = DbType.Guid;
            gameAuditParam.Direction = ParameterDirection.InputOutput;
            if (gameAuditBE.GameAuditGUID == Guid.Empty) //New Row
            {
                gameAuditParam.Value = DBNull.Value;
            }
            else
            {
                gameAuditParam.Value = gameAuditBE.GameAuditGUID;
            }
            paraList.Add(gameAuditParam);
            paraList.Add(new SqlParameter("@Number", gameAuditBE.Number));
            paraList.Add(new SqlParameter("@PropertyGUID", gameAuditBE.PropertyGUID));
            paraList.Add(new SqlParameter("@ReviewMethod ", gameAuditBE.ReviewMethod));
            paraList.Add(new SqlParameter("@GameAuditStartDateTime", gameAuditBE.GameAuditStartDateTime));
            paraList.Add(new SqlParameter("@GameAuditEndDateTime", gameAuditBE.GameAuditEndDateTime));
            paraList.Add(new SqlParameter("@VCRConsoleNumber", gameAuditBE.VCRConsoleNumber));
            paraList.Add(new SqlParameter("@CreatorUserID", gameAuditBE.CreatorUserID));
            paraList.Add(new SqlParameter("@Closed", gameAuditBE.Closed));
            paraList.Add(new SqlParameter("@AreaAudited", gameAuditBE.AreaAudited));
            paraList.Add(new SqlParameter("@ModifiedBy", gameAuditBE.ModifiedBy));
            paraList.Add(new SqlParameter("@DateCreated", gameAuditBE.DateCreated));
            paraList.Add(new SqlParameter("@SecondAuditor", gameAuditBE.SecondAuditor));
            paraList.Add(new SqlParameter("@Remarks", gameAuditBE.Remarks));
            paraList.Add(new SqlParameter("@LastModifiedDate", gameAuditBE.LastModifiedDate));
            paraList.Add(new SqlParameter("@DepartmentGuid", gameAuditBE.DepartmentGuid));
            paraList.Add(new SqlParameter("@Owner", gameAuditBE.Owner));

            if (gameAuditBE.RequestedByGUID != Guid.Empty)
                paraList.Add(new SqlParameter("@RequestedByGUID", gameAuditBE.RequestedByGUID));
            paraList.Add(new SqlParameter("@Status ", gameAuditBE.Status));
            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_GameAudit]", paraList.ToArray());
            return (Guid)gameAuditParam.Value;
        }
        #endregion

        #region AppendUnmatchedTables
        private void AppendUnmatchedTables(ref StringBuilder sb, int rowID)
        {
            string sourceSQL = "__iTrakImporter_sps_GameAudit_UnMatchedTableData";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@RowID", rowID);
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL, sqlParams))
            {
                int i = 0;
                while (dr.Read())
                {
                    if(i == 0)
                    {
                        sb.Append("--- Unmatched Fields From " + dr["TableName"].ToString() + " ---");
                        sb.Append(System.Environment.NewLine);
                    }
                    if(!string.IsNullOrEmpty(dr["uTableString"].ToString().Trim()))
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
                    if(!string.IsNullOrEmpty(dr["uTableText2Value"].ToString().Trim()))
                    {
                        sb.Append(dr["uTableText2Caption"].ToString() + ": ");
                        sb.Append(dr["uTableText2Value"].ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                     if(!string.IsNullOrEmpty(dr["uTableText3Value"].ToString().Trim()))
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

        #region ImportGameAudits
        private GameAuditBE GetOneGameAudit(IDataReader dr)
        {
            GameAuditBE gameAuditBE = new GameAuditBE();
            gameAuditBE.GameAuditGUID = DataHelper.GetGuid(dr["GameAuditGuid"]);
            gameAuditBE.Number = dr["Number"].ToString();
            gameAuditBE.ModifiedBy = DataHelper.GetUserName(dr["ModifiedBy"].ToString());
            gameAuditBE.Archived = DataHelper.GetBool(dr["Archived"]);
            gameAuditBE.AreaAudited = dr["AreaAudited"].ToString();
            gameAuditBE.Closed = DataHelper.GetBool(dr["Closed"]);
            gameAuditBE.CreatorUserID = DataHelper.GetUserName(dr["CreatorUserID"].ToString());
            gameAuditBE.DateCreated = DataHelper.GetDateTime(dr["DateCreated"]);
            gameAuditBE.GameAuditEndDateTime = DataHelper.GetDateTime(dr["GameAuditEndDateTime"]);
            gameAuditBE.GameAuditStartDateTime = DataHelper.GetDateTime(dr["GameAuditStartDateTime"]);
            gameAuditBE.LastModifiedDate = DataHelper.GetDateTime(dr["LastModifiedDate"]);
            gameAuditBE.ModifiedBy = DataHelper.GetUserName(dr["ModifiedBy"].ToString());
            gameAuditBE.RequestedByGUID = DataHelper.GetGuid(dr["RequestedByGUID"]);
            gameAuditBE.ReviewMethod = dr["ReviewMethod"].ToString();
            gameAuditBE.SecondAuditor = dr["SecondAuditor"].ToString();
            gameAuditBE.Section = dr["Section"].ToString();
            gameAuditBE.Status = dr["Status"].ToString();
            gameAuditBE.VCRConsoleNumber = dr["VCRConsoleNumber"].ToString();
            gameAuditBE.PropertyGUID = (Guid)dr["PropertyGuid"];
            gameAuditBE.DepartmentGuid = (Guid)dr["DepartmentGuid"];
            gameAuditBE.Owner = SqlClientUtility.GetString(dr, "Owner", string.Empty);

            if (gameAuditBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                gameAuditBE.LastModifiedDate = gameAuditBE.DateCreated;
            if (gameAuditBE.DateCreated < DataHelper.SQL_MIN_DATE)
                gameAuditBE.DateCreated = gameAuditBE.LastModifiedDate;

            if (gameAuditBE.DateCreated < DataHelper.SQL_MIN_DATE)
                gameAuditBE.DateCreated = DateTime.Now;
            if (gameAuditBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                gameAuditBE.LastModifiedDate = DateTime.Now;

            StringBuilder sb = new StringBuilder();
            sb.Append(dr["Remarks"].ToString());
            sb.Append(System.Environment.NewLine);

            string text1 = SqlClientUtility.GetString(dr, "Text1", string.Empty);
            string text2 = SqlClientUtility.GetString(dr, "Text2", string.Empty);

            string text1Caption = SqlClientUtility.GetString(dr, "Text1Caption", string.Empty);
            string text2Caption = SqlClientUtility.GetString(dr, "Text2Caption", string.Empty);

            if (!string.IsNullOrEmpty(text1))
            {
                sb.AppendFormat("{0}: {1}", text1Caption, text1);
            }

            if (!string.IsNullOrEmpty(text2))
            {
                sb.AppendFormat("{0}: {1}", text2Caption, text2);
            }

            sb.Append(dr["uString"].ToString());
            this.AppendUnmatchedTables(ref sb, (int)dr["RowID"]);
            gameAuditBE.Remarks = sb.ToString();

            if(DEFAULT_STATUS == "Closed")
                gameAuditBE.Closed = true;
            return gameAuditBE;
        }
        public void ImportGameAudits()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_GameAudit]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {

                try
                {
                    int i = 0;
                    while (dr.Read())
                    {
                        GameAuditBE gameAuditBE = GetOneGameAudit(dr);
                        this.ImportOneRecord(gameAuditBE);
                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing Game Audit"));
                        }
                        if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                        {
                            break;
                        }
                    }
                 
                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed Game Aduit"));
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
        #endregion End of Import GameAduits

    }
}
