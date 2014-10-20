using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;

using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;

namespace iTrak.Importer.Data.Importers
{
    public class DetailedReport
    {
        private const string DEFAULT_INCIDENT_STATUS = "Open";
        private const string DEFAULT_BLOTTER_STATUS = "Closed";
        public event EventHandler ImportedRowEvent;
        public event EventHandler ImportCompletedEvent;
        private static ManualResetEvent[] _resetEvents;

        private SortedList<string, Guid> _importedBlotters = null;
        public SortedList<string, Guid> ImportedBlotters
        {
            get
            {
                return _importedBlotters;
            }
        }
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_DetailedReport (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion

        #region ImportOneRecord
        private Guid ImportOneRecord(DetailedReportBE detailedReportBE)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
            SqlParameter incidentParam = new SqlParameter();
            incidentParam.ParameterName = "@DetailedReportGUID";
            incidentParam.DbType = DbType.Guid;
            incidentParam.Direction = ParameterDirection.InputOutput;
            if (detailedReportBE.DetailedReportGUID == Guid.Empty) //New Row
            {
                incidentParam.Value = DBNull.Value;
            }
            else
            {
                incidentParam.Value = detailedReportBE.DetailedReportGUID;
            }
            paraList.Add(incidentParam);
            paraList.Add(new SqlParameter("@Number", detailedReportBE.Number));
            paraList.Add(new SqlParameter("@BlotterGUID", detailedReportBE.BlotterGUID));
            paraList.Add(new SqlParameter("@Occured", detailedReportBE.Occured));
            paraList.Add(new SqlParameter("@Status", detailedReportBE.Status));
            paraList.Add(new SqlParameter("@AmbulanceOffered", detailedReportBE.AmbulanceOffered));
            paraList.Add(new SqlParameter("@AmbulanceDeclined", detailedReportBE.AmbulanceDeclined));
            paraList.Add(new SqlParameter("@FirstAidOffered", detailedReportBE.FirstAidOffered));
            paraList.Add(new SqlParameter("@FirstAidDeclined", detailedReportBE.FirstAidDeclined));
            paraList.Add(new SqlParameter("@TaxiFareOffered", detailedReportBE.TaxiFareOffered));
            paraList.Add(new SqlParameter("@TaxiFareDeclined", detailedReportBE.TaxiFareDeclined));
            paraList.Add(new SqlParameter("@ClosedBy", detailedReportBE.ClosedBy));
            if(detailedReportBE.Closed > DataHelper.SQL_MIN_DATE && detailedReportBE.Status == "Closed")
                paraList.Add(new SqlParameter("@Closed", detailedReportBE.Closed));
            paraList.Add(new SqlParameter("@Archive", detailedReportBE.Archive));
            if (detailedReportBE.Created < DataHelper.SQL_MIN_DATE)
                detailedReportBE.Created = DateTime.Now;
            paraList.Add(new SqlParameter("@Created", detailedReportBE.Created));
            paraList.Add(new SqlParameter("@CreatedBy", detailedReportBE.CreatedBy));
            paraList.Add(new SqlParameter("@Type", detailedReportBE.Type));
            paraList.Add(new SqlParameter("@Specific", detailedReportBE.Specific));
            paraList.Add(new SqlParameter("@Category", detailedReportBE.Category));
            paraList.Add(new SqlParameter("@Exclusive", detailedReportBE.Exclusive));
            paraList.Add(new SqlParameter("@SecondaryOperator", detailedReportBE.SecondaryOperator));
            paraList.Add(new SqlParameter("@Details", detailedReportBE.Details));
            paraList.Add(new SqlParameter("@ClosingRemarks", detailedReportBE.ClosingRemarks));
            paraList.Add(new SqlParameter("@LastModifiedDate", detailedReportBE.LastModifiedDate));
            paraList.Add(new SqlParameter("@ExecutiveBrief", detailedReportBE.ExecutiveBrief));
            paraList.Add(new SqlParameter("@ModifiedBy", detailedReportBE.ModifiedBy));
            paraList.Add(new SqlParameter("@Specifics", detailedReportBE.Specifics));
            if(detailedReportBE.ArchiveDate > DataHelper.SQL_MIN_DATE)
                paraList.Add(new SqlParameter("@ArchiveDate", detailedReportBE.ArchiveDate));

            paraList.Add(new SqlParameter("@IsGlobal", detailedReportBE.IsGlobal));
            paraList.Add(new SqlParameter("@Owner", detailedReportBE.Owner));
            paraList.Add(new SqlParameter("@DepartmentGuid", detailedReportBE.DepartmentGuid));

            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_DetailedReport]", paraList.ToArray());

            return (Guid)incidentParam.Value;
        }
        #endregion


        #region Import Incidents
        #region Get One Blotter
        private BlotterBE GetOneBlotter(IDataReader dr)
        {
            BlotterBE blotterBE = new BlotterBE();
            blotterBE.Number = dr["BlotterNumber"].ToString();
            blotterBE.Archive = false;
            blotterBE.ArchiveDate = DataHelper.GetDateTime(dr["ArchiveDate"]);
            blotterBE.BlotterAction = "Import";
            blotterBE.BlotterGUID = DataHelper.GetGuid(dr["BlotterGuid"]);
            blotterBE.ClosedTime = DateTime.Now;
            blotterBE.Created = DataHelper.GetDateTime(dr["Created"]);
            blotterBE.EndTime = DataHelper.GetDateTime(dr["EndTime"]);
            blotterBE.Exclusive = string.Empty;
            blotterBE.HighPriority = false;
            blotterBE.IsGlobal = false;
            blotterBE.LastModifiedDate = DataHelper.GetDateTime(dr["LastModifiedDate"]);
            blotterBE.Location = dr["Location"].ToString();
            blotterBE.LockedBySource = false;
            blotterBE.ModifiedBy = DataHelper.GetUserName(dr["ModifiedBy"].ToString());
            blotterBE.Occured = DataHelper.GetDateTime(dr["Occured"]);
            blotterBE.PrimaryOperator = DataHelper.GetUserName(dr["CreatedBy"].ToString());
            blotterBE.Property = DataHelper.GetGuid(dr["PropertyGuid"]);
            blotterBE.Reference = DataHelper.GetUserName(dr["Reference"].ToString()); 
            blotterBE.SecondaryOperator = DataHelper.GetUserName(dr["SecondaryOperator"].ToString());
            blotterBE.Status = dr["Status"].ToString();
            blotterBE.Subject = string.Empty;
            blotterBE.Sublocation = dr["SubLocation"].ToString();
            blotterBE.Synopsis = dr["Synopsis"].ToString();
            blotterBE.DepartmentGuid = DataHelper.GetGuid(dr["DepartmentGuid"]);


            StringBuilder sb = new StringBuilder();
            sb.Append(dr["Synopsis"].ToString());
            sb.Append(System.Environment.NewLine);
            sb.Append(dr["uString"].ToString());
            this.AppendUnmatchedTables(ref sb, (int)dr["RowID"]);
            blotterBE.Synopsis = sb.ToString();


            #region Set Default value for null
            if (string.IsNullOrEmpty(blotterBE.Status))
                blotterBE.Status = DEFAULT_BLOTTER_STATUS;

            if (string.IsNullOrEmpty(blotterBE.PrimaryOperator))
                blotterBE.PrimaryOperator = blotterBE.ModifiedBy;

            if (string.IsNullOrEmpty(blotterBE.ModifiedBy))
                blotterBE.ModifiedBy = blotterBE.PrimaryOperator;

            if (blotterBE.Created < DataHelper.SQL_MIN_DATE)
                blotterBE.Created = blotterBE.LastModifiedDate;

            if (blotterBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                blotterBE.LastModifiedDate = blotterBE.Created;

            if (blotterBE.Occured < DataHelper.SQL_MIN_DATE)
                blotterBE.Occured = blotterBE.Created;
            #endregion

            if (string.IsNullOrEmpty(blotterBE.Owner))
                blotterBE.Owner = "Importer";

            blotterBE.Status = "Closed"; //default status

            if (string.IsNullOrEmpty(blotterBE.PrimaryOperator))
            {
                blotterBE.PrimaryOperator = "Importer";
            }
            return blotterBE;
        }
        #endregion
        #region GetOneIncident
        private DetailedReportBE GetOneIncident(Guid blotterGuid,IDataReader dr)
        {
            DetailedReportBE detailedReportBE = new DetailedReportBE();
            detailedReportBE.AmbulanceDeclined = false;
            detailedReportBE.AmbulanceOffered = false;
            detailedReportBE.Archive = false;
            detailedReportBE.BlotterGUID = blotterGuid;
            detailedReportBE.Category = dr["Category"].ToString();
            detailedReportBE.Closed = DataHelper.GetDateTime(dr["Closed"]);
            detailedReportBE.ClosedBy = dr["ClosedBy"].ToString();
            detailedReportBE.ClosingRemarks = dr["ClosingRemarks"].ToString();
            detailedReportBE.Created = DataHelper.GetDateTime(dr["Created"]);
            detailedReportBE.CreatedBy = DataHelper.GetUserName(dr["CreatedBy"].ToString());
            detailedReportBE.DetailedReportGUID = DataHelper.GetGuid(dr["DetailedReportGUID"]);
            detailedReportBE.FirstAidDeclined = false;
            detailedReportBE.FirstAidOffered = false;
            detailedReportBE.IsGlobal = false;
            detailedReportBE.LastModifiedDate = DataHelper.GetDateTime(dr["LastModifiedDate"]);
            detailedReportBE.ModifiedBy = DataHelper.GetUserName(dr["ModifiedBy"].ToString());
            detailedReportBE.Number = dr["IncidentNumber"].ToString();
            detailedReportBE.Occured = DataHelper.GetDateTime(dr["Occured"]);           
            detailedReportBE.SecondaryOperator = DataHelper.GetUserName(dr["SecondaryOperator"].ToString());
            detailedReportBE.Specific = dr["Specific"].ToString();
            detailedReportBE.Status = dr["Status"].ToString();
            detailedReportBE.TaxiFareDeclined = false;
            detailedReportBE.TaxiFareOffered = false;
            detailedReportBE.Type = dr["Type"].ToString();
            detailedReportBE.Specifics = SqlClientUtility.GetString(dr, "Specifics", string.Empty);
            detailedReportBE.DepartmentGuid = DataHelper.GetGuid(dr["DepartmentGuid"]);
            detailedReportBE.Text1Caption = SqlClientUtility.GetString(dr, "uText1Caption", string.Empty);
            detailedReportBE.Text1 = SqlClientUtility.GetString(dr, "uText1Value", string.Empty);

            detailedReportBE.Text2Caption = SqlClientUtility.GetString(dr, "uText2Caption", string.Empty);
            detailedReportBE.Text2 = SqlClientUtility.GetString(dr, "uText2Value", string.Empty);

            detailedReportBE.Text3Caption = SqlClientUtility.GetString(dr, "uText3Caption", string.Empty);
            detailedReportBE.Text3 = SqlClientUtility.GetString(dr, "uText3Value", string.Empty);

            detailedReportBE.Text4Caption = SqlClientUtility.GetString(dr, "uText4Caption", string.Empty);
            detailedReportBE.Text4 = SqlClientUtility.GetString(dr, "uText4Value", string.Empty);

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine(dr["Details"].ToString());

            //if (!string.IsNullOrEmpty(detailedReportBE.Text1))
            //{
            //    sb.AppendFormat("{0}: {1}", detailedReportBE.Text1Caption, detailedReportBE.Text1);
            //}

            //if (!string.IsNullOrEmpty(detailedReportBE.Text2))
            //{
            //    sb.AppendFormat("{0}: {1}", detailedReportBE.Text2Caption, detailedReportBE.Text2);
            //}

            //if (!string.IsNullOrEmpty(detailedReportBE.Text3))
            //{
            //    sb.AppendFormat("{0}: {1}", detailedReportBE.Text3Caption, detailedReportBE.Text3);
            //}

            //if (!string.IsNullOrEmpty(detailedReportBE.Text4))
            //{
            //    sb.AppendFormat("{0}: {1}", detailedReportBE.Text4Caption, detailedReportBE.Text4);
            //}

            //sb.AppendLine(dr["uString"].ToString());
            //this.AppendUnmatchedTables(ref sb, (int)dr["RowID"]);
            //detailedReportBE.Details = sb.ToString();
            detailedReportBE.Details = string.Empty;

            if (string.IsNullOrEmpty(detailedReportBE.Status))
                detailedReportBE.Status = DEFAULT_INCIDENT_STATUS;

            #region set default values
            if (detailedReportBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                detailedReportBE.Created = detailedReportBE.Created;
            if (detailedReportBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                detailedReportBE.LastModifiedDate = detailedReportBE.Created;
            if (string.IsNullOrEmpty(detailedReportBE.CreatedBy))
                detailedReportBE.CreatedBy = detailedReportBE.ModifiedBy;
            if (string.IsNullOrEmpty(detailedReportBE.ModifiedBy))
                detailedReportBE.ModifiedBy = detailedReportBE.CreatedBy;
            if (detailedReportBE.Occured < DataHelper.SQL_MIN_DATE)
                detailedReportBE.Occured = detailedReportBE.Created;

            if (detailedReportBE.LastModifiedDate < DataHelper.SQL_MIN_DATE)
                detailedReportBE.LastModifiedDate = DateTime.Now;
            #endregion

            if (string.IsNullOrEmpty(detailedReportBE.Owner))
                detailedReportBE.Owner = "Importer";

            if (string.IsNullOrEmpty(detailedReportBE.CreatedBy))
                detailedReportBE.CreatedBy = "Importer";

            detailedReportBE.SecondaryOperator = detailedReportBE.CreatedBy;
            detailedReportBE.CreatedBy = "Administrator";
            return detailedReportBE;
        }
        #endregion

        #region AppendUnmatchedTables
        private void AppendUnmatchedTables(ref StringBuilder sb, int rowID)
        {
            string sourceSQL = "[dbo].[__iTrakImporter_sps_iTrakImporter_DetailedReport_UnMatchedTableData]";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@RowID", rowID);
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL, sqlParams))
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

        #region ImportIncidents
        public void ImportIncidents()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_DetailedReport]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.iTrakConnectionString))
                {
                    conn.Open();                                 
                    try
                    {
                        int i = 0;
                        Blotter blotter = new Blotter();
                        _importedBlotters = new SortedList<string, Guid>();
                        while (dr.Read())
                        {
                            #region Build Entity
                            Guid blotterGuid = Guid.Empty;
                            BlotterBE blotterBE = GetOneBlotter(dr);
                            if (!_importedBlotters.ContainsKey(blotterBE.Number))
                            {
                                blotterGuid = blotter.ImportOneRecord(blotterBE);
                                _importedBlotters.Add(blotterBE.Number, blotterGuid);
                            }
                            else
                            {
                                blotterGuid = _importedBlotters[blotterBE.Number];
                            }
                            DetailedReportBE detailedReportBE = GetOneIncident(blotterGuid, dr);
                            this.ImportOneRecord(detailedReportBE);                                
                            #endregion

                            if (this.ImportedRowEvent != null)
                            {
                                ImportedRowEvent(++i, new ImportEventArgs("Importing Incidents"));
                            }
                            if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                            {
                                break;
                            }
                        }
                      
                        conn.Close();

                        if (this.ImportCompletedEvent != null)
                        {
                            ImportCompletedEvent(_count, new ImportEventArgs("Completed Incident"));
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

        #endregion End of Import Incidents

        #region ImportIncidents2
        private static void ImportOneIncident2(object o)
        {
            //SqlConnection conn  = null;
            //SqlTransaction trans = null;
            int index = 0;
            try
            {
                object[] objs = o as object[];
                index = (int)objs[0];
                BlotterBE blotterBE = objs[1] as BlotterBE;
                DetailedReportBE detailedReportBE = objs[2] as DetailedReportBE;
                DetailedReport incident = objs[3] as DetailedReport;
                Blotter blotter = objs[4] as Blotter;
                //conn = new SqlConnection(DbHelper.iTrakConnectionString);
                //conn.Open();
                //trans = conn.BeginTransaction();
                Guid blotterGuid = Guid.Empty;
                if (!incident.ImportedBlotters.ContainsKey(blotterBE.Number))
                {
                    blotterGuid = blotter.ImportOneRecord(blotterBE);
                    lock (incident.ImportedBlotters)
                    {
                        incident.ImportedBlotters.Add(blotterBE.Number, blotterGuid);
                    }
                }
                else
                {
                    blotterGuid = incident.ImportedBlotters[blotterBE.Number];
                }
                detailedReportBE.BlotterGUID = blotterGuid;
                incident.ImportOneRecord(detailedReportBE);

                //trans.Commit();
                //conn.Close();
            }
            catch (Exception ex)
            {
                //if(trans != null)
                //    trans.Rollback();
                throw new Exception("Failed to import Incident", ex);
            }
            finally
            {
                _resetEvents[index].Set();
                //if(conn != null)
                //    conn.Close();
            }
        }
        public void ImportIncidents2()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_DetailedReport]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {

                try
                {
                    int i = 0;
                    Blotter blotter = new Blotter();
                    _importedBlotters = new SortedList<string, Guid>();
                    _resetEvents = new ManualResetEvent[_count];
                    while (dr.Read())
                    {
                        //if(dr["IncidentNumber"].ToString() != "~IN000069QTN")
                        //   continue;

                        #region Build Entity
                        Guid blotterGuid = Guid.Empty;
                        BlotterBE blotterBE = GetOneBlotter(dr);

                        DetailedReportBE detailedReportBE = GetOneIncident(blotterGuid, dr);

                        int index = i % ThreadHelper.NUMBER_OF_THREADS;
                        object[] objs = new object[5];

                        objs[0] = index;
                        objs[1] = blotterBE;
                        objs[2] = detailedReportBE;
                        objs[3] = this;
                        objs[4] = blotter;

                        _resetEvents[index] = new ManualResetEvent(false);

                        ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneIncident2), objs);
                        if (index == ThreadHelper.NUMBER_OF_THREADS - 1)
                        {
                            ThreadHelper.WaitAll(_resetEvents);
                            _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                        }

                        #endregion

                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing Incidents"));
                        }
                        if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                        {
                            break;
                        }
                    }
                    ThreadHelper.WaitAll(_resetEvents);

                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed Incident"));
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
        #endregion ImportIncidents2



    }
}
