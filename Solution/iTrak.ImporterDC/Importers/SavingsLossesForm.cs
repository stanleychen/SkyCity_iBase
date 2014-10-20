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
    public class SavingsLossesForm
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
            string sqlText = "SELECT COUNT(*) FROM dbo.__iTrakImporter_SavingsLossesForm (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion

        #region ImportOneRecord
        private Guid ImportOneRecord(SavingsLossesFormBE savingLossBE)
        {


            List<SqlParameter> paraList = new List<SqlParameter>();
            SqlParameter savingLossParam = new SqlParameter();
            savingLossParam.ParameterName = "@SavingsLossesItemGUID";
            savingLossParam.DbType = DbType.Guid;
            savingLossParam.Direction = ParameterDirection.InputOutput;
            if (savingLossBE.SavingsLossesItemGUID == Guid.Empty) //New Row
            {
                savingLossParam.Value = DBNull.Value;
            }
            else
            {
                savingLossParam.Value = savingLossBE.SavingsLossesItemGUID;
            }
            paraList.Add(savingLossParam);
            paraList.Add(new SqlParameter("@AssignedBy", savingLossBE.AssignedBy));
            paraList.Add(new SqlParameter("@AssignedDate", savingLossBE.AssignedDate));
            paraList.Add(new SqlParameter("@Description", savingLossBE.Description));
            paraList.Add(new SqlParameter("@GroupByType", savingLossBE.GroupByType));
            paraList.Add(new SqlParameter("@Loss", savingLossBE.Loss));
            paraList.Add(new SqlParameter("@Occured", savingLossBE.Occured));
            paraList.Add(new SqlParameter("@SaveOrLoss", savingLossBE.SaveOrLoss));
            paraList.Add(new SqlParameter("@SavingsLossesFormGUID", savingLossBE.SavingsLossesFormGUID));
            paraList.Add(new SqlParameter("@SavingsLossesFormType", savingLossBE.SavingsLossesFormType));
            paraList.Add(new SqlParameter("@SavingsLossesPrimaryParentGUID", savingLossBE.SavingsLossesPrimaryParentGUID));
            paraList.Add(new SqlParameter("@SavingsLossesPrimaryParentType", savingLossBE.SavingsLossesPrimaryParentType));
            paraList.Add(new SqlParameter("@SavingsLossesType", savingLossBE.SavingsLossesType));
            paraList.Add(new SqlParameter("@SavingsLossesValue", savingLossBE.SavingsLossesValue));
            paraList.Add(new SqlParameter("@SourceID", savingLossBE.SourceID));

            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_SavingsLossesForm]", paraList.ToArray());
            return (Guid)savingLossParam.Value;
        }
        #endregion

        #region Get Saving & Losses
        private SavingsLossesFormBE GetOneSavingLoss(IDataReader dr)
        {
            SavingsLossesFormBE savingLossBE = new SavingsLossesFormBE();
            savingLossBE.SavingsLossesItemGUID = DataHelper.GetGuid(dr["ItemGuid"]);
            savingLossBE.AssignedBy = SqlClientUtility.GetString(dr, "AssignedBy", string.Empty);
            savingLossBE.AssignedDate = SqlClientUtility.GetDateTime(dr, "AssignedDate", DateTime.Now);
            savingLossBE.Description = SqlClientUtility.GetString(dr, "Description", string.Empty);
            savingLossBE.GroupByType = SqlClientUtility.GetString(dr, "GroupByType", string.Empty);
            savingLossBE.Loss = SqlClientUtility.GetBoolean(dr, "Loss", false);
            savingLossBE.Occured = SqlClientUtility.GetDateTime(dr, "Occured", DateTime.Now);
            savingLossBE.SaveOrLoss = SqlClientUtility.GetString(dr, "SaveOrLoss", string.Empty);
            savingLossBE.SavingsLossesFormGUID = SqlClientUtility.GetGuid(dr, "SavingsLossesFormGUID", Guid.Empty);
            savingLossBE.SavingsLossesFormType = SqlClientUtility.GetString(dr, "SavingsLossesFormType", string.Empty);
            savingLossBE.SavingsLossesPrimaryParentGUID = SqlClientUtility.GetGuid(dr, "HostGuid", Guid.Empty);
            savingLossBE.SavingsLossesPrimaryParentType = SqlClientUtility.GetString(dr, "HostType", string.Empty);
            savingLossBE.SavingsLossesType = SqlClientUtility.GetString(dr, "SavingsLossesType", string.Empty);
            savingLossBE.SavingsLossesValue = SqlClientUtility.GetDecimal(dr, "SavingsLossesValue", 0);
            savingLossBE.SourceID = SqlClientUtility.GetString(dr, "SourceID", string.Empty);
          
            if (savingLossBE.AssignedDate < DataHelper.SQL_MIN_DATE)
                savingLossBE.AssignedDate = savingLossBE.Occured;
            if (string.IsNullOrEmpty(savingLossBE.AssignedBy))
                savingLossBE.AssignedBy = "Administrator";
           

            return savingLossBE;
        }
        #endregion

        #region Import Saving & Losses
        public void ImportSavingLosses()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "dbo.__iTrakImporter_sps_SavingsLossesForm";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                try
                {
                    int i = 0;
                    while (dr.Read())
                    {
                        SavingsLossesFormBE savingLossBE = GetOneSavingLoss(dr);
                        this.ImportOneRecord(savingLossBE);
                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing Savings & Losses"));
                        }
                        if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                        {
                            break;
                        }
                    }

                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed Savings & Losses"));
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
