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
    public class Blotter
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_Blotter (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion


        #region Import Blotter
        public Guid ImportOneRecord(BlotterBE blotterBE)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
            SqlParameter blotterParam = new SqlParameter();
            blotterParam.ParameterName = "@BlotterGUID";
            blotterParam.DbType = DbType.Guid;
            blotterParam.Direction = ParameterDirection.InputOutput;
            if (blotterBE.BlotterGUID == Guid.Empty) //New Row
            {
                blotterParam.Value = DBNull.Value;
            }
            else
            {
                blotterParam.Value = blotterBE.BlotterGUID;
            }
            paraList.Add(blotterParam);
            paraList.Add(new SqlParameter("@Number", blotterBE.Number));
            if (blotterBE.Occured < DataHelper.SQL_MIN_DATE)
            {
                blotterBE.Occured = DateTime.Now;
            }
            paraList.Add(new SqlParameter("@Occured", blotterBE.Occured));
            paraList.Add(new SqlParameter("@BlotterAction", blotterBE.BlotterAction));
            paraList.Add(new SqlParameter("@Subject", blotterBE.Subject));
            paraList.Add(new SqlParameter("@Property", blotterBE.Property));
            if (blotterBE.Created < DataHelper.SQL_MIN_DATE)
            {
                blotterBE.Created = DateTime.Now;
            }
            paraList.Add(new SqlParameter("@Created", blotterBE.Created));
            paraList.Add(new SqlParameter("@Archive", blotterBE.Archive));
            paraList.Add(new SqlParameter("@PrimaryOperator", blotterBE.PrimaryOperator));
            paraList.Add(new SqlParameter("@SecondaryOperator", blotterBE.SecondaryOperator));
            paraList.Add(new SqlParameter("@HighPriority", blotterBE.HighPriority));
            paraList.Add(new SqlParameter("@Status", blotterBE.Status));
            paraList.Add(new SqlParameter("@Sublocation", blotterBE.Sublocation));
            paraList.Add(new SqlParameter("@Location", blotterBE.Location));
            paraList.Add(new SqlParameter("@Exclusive", blotterBE.Exclusive));
            paraList.Add(new SqlParameter("@Synopsis", blotterBE.Synopsis));
            if (blotterBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
            {
                blotterBE.LastModifiedDate = DateTime.Now;
            }
            paraList.Add(new SqlParameter("@LastModifiedDate", blotterBE.LastModifiedDate));
            paraList.Add(new SqlParameter("@Reference", blotterBE.Reference));
            paraList.Add(new SqlParameter("@ModifiedBy", blotterBE.ModifiedBy));
            if (blotterBE.EndTime > DataHelper.SQL_MIN_DATE)
                paraList.Add(new SqlParameter("@EndTime", blotterBE.EndTime));
            if(blotterBE.ClosedTime > DataHelper.SQL_MIN_DATE)
                paraList.Add(new SqlParameter("@ClosedTime", blotterBE.ClosedTime));
            paraList.Add(new SqlParameter("@IsGlobal", blotterBE.IsGlobal));
            paraList.Add(new SqlParameter("@Owner", blotterBE.Owner));
            paraList.Add(new SqlParameter("@DepartmentGuid", blotterBE.DepartmentGuid));

            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_Blotter]", paraList.ToArray());

            return (Guid)blotterParam.Value;
        }
        #endregion

        #region ImportBlotter
        private BlotterBE GetOneBlotter(IDataReader dr)
        {
            BlotterBE blotterBE = new BlotterBE();
            blotterBE.BlotterGUID = DataHelper.GetGuid(dr["BlotterGUID"]);
            blotterBE.Number = dr["Number"].ToString();
            blotterBE.Archive = SqlClientUtility.GetBoolean(dr, "Archive", false);
            blotterBE.ArchiveDate = SqlClientUtility.GetDateTime(dr, "ArchiveDate", DateTime.Now);
            blotterBE.BlotterAction = SqlClientUtility.GetString(dr, "BlotterAction", string.Empty);
            blotterBE.ClosedTime = SqlClientUtility.GetDateTime(dr, "ClosedTime", DateTime.Now);
            blotterBE.Created = SqlClientUtility.GetDateTime(dr, "Created", DateTime.Now);
            blotterBE.DepartmentGuid = SqlClientUtility.GetGuid(dr, "OwnerDepartmentGUID", Guid.Empty);
            blotterBE.EndTime = SqlClientUtility.GetDateTime(dr, "EndTime", DateTime.Now);
            blotterBE.Exclusive = SqlClientUtility.GetString(dr, "Exclusive", string.Empty);
            blotterBE.HighPriority = SqlClientUtility.GetBoolean(dr, "HighPriority", false);
            blotterBE.IsGlobal = SqlClientUtility.GetBoolean(dr, "IsGlobal", false);
            blotterBE.LastModifiedDate = SqlClientUtility.GetDateTime(dr, "LastModifiedDate", DateTime.Now);
            blotterBE.Location = SqlClientUtility.GetString(dr, "Location", string.Empty);
            blotterBE.LockedBySource = SqlClientUtility.GetBoolean(dr, "LockedBySource", false);
            blotterBE.ModifiedBy = SqlClientUtility.GetString(dr, "ModifiedBy", string.Empty);
            blotterBE.Occured = SqlClientUtility.GetDateTime(dr, "Occured", DateTime.Now);
            blotterBE.Owner = SqlClientUtility.GetString(dr, "Owner", string.Empty);
            blotterBE.PrimaryOperator = SqlClientUtility.GetString(dr, "PrimaryOperator", string.Empty);
            blotterBE.Property = SqlClientUtility.GetGuid(dr, "Property", Guid.Empty);
            blotterBE.Reference = SqlClientUtility.GetString(dr, "Reference", string.Empty);
            blotterBE.SecondaryOperator = SqlClientUtility.GetString(dr, "SecondaryOperator", string.Empty);
            blotterBE.SourceID = SqlClientUtility.GetString(dr, "SourceID", string.Empty);
            blotterBE.Status = SqlClientUtility.GetString(dr, "Status", string.Empty);
            blotterBE.Subject = SqlClientUtility.GetString(dr, "Subject", string.Empty);
            blotterBE.Sublocation = SqlClientUtility.GetString(dr, "Sublocation", string.Empty);
            blotterBE.Synopsis = SqlClientUtility.GetString(dr, "Synopsis", string.Empty);
            blotterBE.uString = SqlClientUtility.GetString(dr, "UnmatchedData", string.Empty);

            if (blotterBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                blotterBE.LastModifiedDate = blotterBE.Created;
            if (blotterBE.Created < DataHelper.SQL_MIN_DATE)
                blotterBE.Created = blotterBE.LastModifiedDate;

            if (blotterBE.Created < DataHelper.SQL_MIN_DATE)
                blotterBE.Created = DateTime.Now;
            if (blotterBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                blotterBE.LastModifiedDate = DateTime.Now;

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(blotterBE.Synopsis))
                sb.AppendLine(blotterBE.Synopsis);

            if (!string.IsNullOrEmpty(blotterBE.uString))
                sb.AppendLine(blotterBE.uString);

            blotterBE.Synopsis = sb.ToString();

            blotterBE.Status = DEFAULT_STATUS;

            return blotterBE;
        }
        public void ImportBlotters()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_Blotter]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {

                try
                {
                    int i = 0;
                    while (dr.Read())
                    {
                        BlotterBE blotterBE = GetOneBlotter(dr);
                        this.ImportOneRecord(blotterBE);
                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing Blotter"));
                        }
                        if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                        {
                            break;
                        }
                    }

                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed Blotter"));
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
        #endregion End of Import Blotter

    }
}
