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
    public class Vehicle
    {
        private const string DEFAULT_CREATOR = "Importer";
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_Vehicle (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion


        #region Import Vehicles

        #region AppendUnmatchedTables
        private void AppendUnmatchedTables(ref StringBuilder sb, int rowID)
        {
            string sourceSQL = "[dbo].[__iTrakImporter_sps_Vehicle_UnMatchedTableData]";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@RowID", rowID);
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL, sqlParams))
            {
                string tempTableName = string.Empty;
                while (dr.Read())
                {
                    if (tempTableName != dr["TableName"].ToString())
                    {
                        sb.Append(System.Environment.NewLine);
                        sb.Append(System.Environment.NewLine);
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


        private Guid ImportOneRecord(VehicleBE vehicleBE)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
            SqlParameter vehicleGuidPara = new SqlParameter();
            vehicleGuidPara.ParameterName = "@VehicleGUID";            
            vehicleGuidPara.DbType = DbType.Guid;
            vehicleGuidPara.Direction = ParameterDirection.InputOutput;
            if (vehicleBE.VehicleGuid == Guid.Empty) //New Row
            {
                vehicleGuidPara.Value = DBNull.Value;
            }
            else
            {
                vehicleGuidPara.Value = vehicleBE.VehicleGuid;
            }
            paraList.Add(vehicleGuidPara);
            paraList.Add(new SqlParameter("@SourceKey", vehicleBE.SourceKey));
            if(vehicleBE.BestImageGuid != Guid.Empty)
                paraList.Add(new SqlParameter("@BestImageGUID", vehicleBE.BestImageGuid));

            paraList.Add(new SqlParameter("@License", vehicleBE.License));
            paraList.Add(new SqlParameter("@Country", vehicleBE.Country));
            paraList.Add(new SqlParameter("@State", vehicleBE.State));
            paraList.Add(new SqlParameter("@Condition", vehicleBE.Condition));
            paraList.Add(new SqlParameter("@Color", vehicleBE.Color));
            paraList.Add(new SqlParameter("@VIN", vehicleBE.VIN));
            paraList.Add(new SqlParameter("@Odometer", vehicleBE.Odometer));
            paraList.Add(new SqlParameter("@Make", vehicleBE.Make));
            paraList.Add(new SqlParameter("@Model", vehicleBE.Model));
            paraList.Add(new SqlParameter("@Year", vehicleBE.Year));
            paraList.Add(new SqlParameter("@VehicleType", vehicleBE.VehicleType));
            if(vehicleBE.SubjectGuid != Guid.Empty)
                paraList.Add(new SqlParameter("@Owner", vehicleBE.SubjectGuid));
            paraList.Add(new SqlParameter("@Note", vehicleBE.Note));
            if (vehicleBE.DateCreated < DataHelper.SQL_MIN_DATE)
                vehicleBE.DateCreated = DateTime.Now;
            paraList.Add(new SqlParameter("@DateCreated", vehicleBE.DateCreated));
            if (vehicleBE.DateModified < DataHelper.SQL_MIN_DATE)
                vehicleBE.DateModified = DateTime.Now;
            paraList.Add(new SqlParameter("@DateModified", vehicleBE.DateModified));
            paraList.Add(new SqlParameter("@CreatedBy", vehicleBE.CreatedBy));
            paraList.Add(new SqlParameter("@ModifiedBy", vehicleBE.ModifiedBy));
            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_Vehicles]", paraList.ToArray());
            return (Guid)vehicleGuidPara.Value;
        }
        public void ImportVehicles()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_Vehicles]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.iTrakConnectionString))
                {
                    conn.Open();
              
                    try
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            #region Build Entity
                            VehicleBE vehicleBE = new VehicleBE();
                            vehicleBE.VehicleGuid = DataHelper.GetGuid(dr["iTrakVehicleGuid"]);
                            vehicleBE.SourceKey = dr["SourceKey"].ToString();
                            vehicleBE.Color = dr["Color"].ToString();
                            vehicleBE.Condition = dr["Condition"].ToString();
                            vehicleBE.Country = dr["Country"].ToString();
                            vehicleBE.CreatedBy = DataHelper.GetUserName(dr["CreatedBy"].ToString());
                            vehicleBE.DateCreated = DataHelper.GetDateTime(dr["DateCreated"]);
                            vehicleBE.DateModified = DataHelper.GetDateTime(dr["DateModified"]);
                            vehicleBE.IncidentNumber = dr["IncidentNumber"].ToString();
                            vehicleBE.License = dr["License"].ToString();
                            vehicleBE.Make = dr["Make"].ToString();
                            vehicleBE.Model = dr["Model"].ToString();
                            vehicleBE.ModifiedBy = DataHelper.GetUserName(dr["ModifiedBy"].ToString());                            
                            vehicleBE.Odometer = dr["Odometer"].ToString();
                            vehicleBE.RowID = (int)dr["RowID"];
                            vehicleBE.State = dr["State"].ToString();
                            vehicleBE.SubjectGuid = DataHelper.GetGuid(dr["iTrakSubjectGuid"]);
                            vehicleBE.VehicleType = dr["VehicleType"].ToString();
                            vehicleBE.VIN = dr["VIN"].ToString();
                            vehicleBE.Year = dr["Year"].ToString();

                            vehicleBE.Note = dr["Note"].ToString();
                            vehicleBE.UText1Value = dr["UText1Value"].ToString().Trim();
                            vehicleBE.UText1Caption = dr["uText1Caption"].ToString();
                            vehicleBE.UString = dr["UString"].ToString().Trim();

                            if (string.IsNullOrEmpty(vehicleBE.CreatedBy))
                                vehicleBE.CreatedBy = DEFAULT_CREATOR;

                            if (vehicleBE.UText1Value != string.Empty)
                                vehicleBE.Note += System.Environment.NewLine + vehicleBE.UText1Caption + ":" + vehicleBE.UText1Value;

                            if (vehicleBE.UString != string.Empty)
                                vehicleBE.Note += System.Environment.NewLine + vehicleBE.UString;

                            StringBuilder sb = new StringBuilder();
                            this.AppendUnmatchedTables(ref sb, (int)dr["RowID"]);
                            vehicleBE.Note += sb.ToString();
                            #endregion
                            //Import one Participant Assignment
                            this.ImportOneRecord(vehicleBE);
                            if (this.ImportedRowEvent != null)
                            {
                                ImportedRowEvent(++i, new ImportEventArgs("Importing Vehicles"));
                            }
                            if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                            {
                                break;
                            }
                        }
            
                        conn.Close();

                        if (this.ImportCompletedEvent != null)
                        {
                            ImportCompletedEvent(_count, new ImportEventArgs("Completed Vehicles"));
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
        }
        #endregion

    }
}
