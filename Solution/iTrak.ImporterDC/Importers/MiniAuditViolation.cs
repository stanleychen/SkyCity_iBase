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
    public class MiniAuditViolation
    {
        private static ManualResetEvent[] _resetEvents;
        public event EventHandler ImportedRowEvent;
        private const string DEFAULT_STATUS = "Open";
        private const string ERROR_TYPE = "No Error";
        public event EventHandler ImportCompletedEvent;
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_MiniAuditViolation (nolock)";
           
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;
            return rowCount;
        }
        #endregion


        #region Import Violations

        #region ImportOneViolation
        private static void ImportOneViolation(object o)
        {
            try
            {
                object[] objs = o as object[];
                int index = (int)objs[0];
                MiniAuditViolationBE miniAuditViolationBE = objs[1] as MiniAuditViolationBE;

                if (string.IsNullOrEmpty(miniAuditViolationBE.ErrorType))
                    miniAuditViolationBE.ErrorType = ERROR_TYPE;

                List<SqlParameter> paraList = new List<SqlParameter>();
                SqlParameter miniAuditViolationParam = new SqlParameter();
                miniAuditViolationParam.ParameterName = "@MiniAuditEmployeeViolationGUID";
                miniAuditViolationParam.DbType = DbType.Guid;
                miniAuditViolationParam.Direction = ParameterDirection.InputOutput;
                if (miniAuditViolationBE.MiniAuditEmployeeViolationGUID == Guid.Empty) //New Row
                {
                    miniAuditViolationParam.Value = DBNull.Value;
                }
                else
                {
                    miniAuditViolationParam.Value = miniAuditViolationBE.MiniAuditEmployeeViolationGUID;
                }
                paraList.Add(miniAuditViolationParam);
                paraList.Add(new SqlParameter("@MiniAuditGUID", miniAuditViolationBE.MiniAuditGUID));
                if (miniAuditViolationBE.EmployeeGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@EmployeeGUID", miniAuditViolationBE.EmployeeGUID));
                if (miniAuditViolationBE.SupervisorGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@SupervisorGUID", miniAuditViolationBE.SupervisorGUID));
                paraList.Add(new SqlParameter("@ViolationDateTime", miniAuditViolationBE.ViolationDateTime));
                paraList.Add(new SqlParameter("@ErrorType", miniAuditViolationBE.ErrorType));
                paraList.Add(new SqlParameter("@Violation", miniAuditViolationBE.Violation));
                paraList.Add(new SqlParameter("@ViolationDescription", miniAuditViolationBE.ViolationDescription));
                paraList.Add(new SqlParameter("@Remarks", miniAuditViolationBE.Remarks));
                SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_MiniAuditViolation]", paraList.ToArray());
                _resetEvents[index].Set();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import Import Violation", ex);
            }
            finally
            {
            }
            
        }
        #endregion

        #region ImportViolations
        public void ImportMiniAuditViolations()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;
            string sourceSQL = "[dbo].[__iTrakImporter_sps_MiniAuditViolation]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                int i = 0;
                _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                #region While Begin
                while (dr.Read())
                {
                    MiniAuditViolationBE miniViolationBE = new MiniAuditViolationBE();
                    miniViolationBE.MiniAuditEmployeeViolationGUID = DataHelper.GetGuid(dr["MiniAuditEmployeeViolationGUID"]);
                    miniViolationBE.EmployeeGUID = DataHelper.GetGuid(dr["EmployeeGUID"]);
                    miniViolationBE.MiniAuditGUID = DataHelper.GetGuid(dr["MiniAuditGUID"]);
                    miniViolationBE.Remarks = dr["Remarks"].ToString();
                    miniViolationBE.SupervisorGUID = DataHelper.GetGuid(dr["SupervisorGUID"]);
                    miniViolationBE.Violation = dr["Violation"].ToString();
                    miniViolationBE.ViolationDateTime = DataHelper.GetDateTime(dr["ViolationDateTime"]);
                    miniViolationBE.ViolationDescription = dr["ViolationDescription"].ToString();
                    miniViolationBE.ErrorType = dr["ErrorType"].ToString();

                    int index = i % ThreadHelper.NUMBER_OF_THREADS;
                    object[] objs = new object[2];
                    objs[0] = index;
                    objs[1] = miniViolationBE;

                    _resetEvents[index] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneViolation), objs);
                    if (index == ThreadHelper.NUMBER_OF_THREADS - 1)
                    {
                        ThreadHelper.WaitAll(_resetEvents);
                        _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];

                    }
                    if (this.ImportedRowEvent != null)
                    {
                        ImportedRowEvent(++i, new ImportEventArgs("Importing Game Dispute Violation..."));
                    }
                    if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                    {
                        break;
                    }

                }
                ThreadHelper.WaitAll(_resetEvents);

                if (this.ImportCompletedEvent != null)
                {
                    ImportCompletedEvent(_count, new ImportEventArgs("Completed Game Dispute Violations"));
                }
                #endregion While End

            }  
        }
      
        #endregion

        #endregion
    }
}
