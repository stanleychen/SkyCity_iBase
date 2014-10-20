using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Common;
namespace iTrak.Importer.Data
{
    public class IncidentImporter : BaseDAL
    {
        private string sourceConnectionString;
        private string iXDataConnectionString;
        public event EventHandler ImportedRowNumber;
        public IncidentImporter(string sourceConnectionString,string iXDataConnectionString)
        {
            this.sourceConnectionString = sourceConnectionString;
            this.iXDataConnectionString = iXDataConnectionString;
        }

        #region Read Data from sources
        private System.Data.DataRow ReadOneBlotter(IDataReader dr)
        {
            DataSetIXData.BlotterDataTable blotter = new DataSetIXData.BlotterDataTable();
            System.Data.DataRow row = blotter.NewRow();
            row["Number"] = dr["Number"];
            row["Occured"] = dr["Occured"];
            row["BlotterAction"] = "Import";
            row["Subject"] = "Import";
            row["Property"] = dr["PropertyGUID"];
            row["Created"] = dr["Created"];
            row["Archive"] = 0;
            row["PrimaryOperator"] = ToOperator(dr["CreatedBy"]);
            row["SecondaryOperator"] = dr["SecondaryOperator"];
            row["HighPriority"] = 0;
            row["Status"] = "Closed"; //default to closed
            row["Sublocation"] = dr["Sublocation"];
            row["Location"] = dr["Location"];
            row["Synopsis"] = dr["Synopsis"];
            row["LastModifiedDate"] = ToDateTime(dr["LastModifiedDate"]);
            row["Reference"] = System.DBNull.Value;
            row["ModifiedBy"] = ToOperator(dr["ModifiedBy"]);
            row["ArchiveDate"] = System.DBNull.Value;
            row["EndTime"] = dr["EndTime"];
            row["ClosedTime"] = System.DBNull.Value;
            row["IsGlobal"] = 0;
            row["SourceModuleID"] = System.DBNull.Value;
            row["SourceID"] = System.DBNull.Value;
            row["SourceGUID"] = System.DBNull.Value;
            row["LockedBySource"] = System.DBNull.Value;

            return row;
        }

        private System.Data.DataRow ReadOneIncident(IDataReader dr)
        {
            DataSetIXData.DetailedReportDataTable incidentTable = new DataSetIXData.DetailedReportDataTable();
            System.Data.DataRow row = incidentTable.NewRow();
            row["Number"] = dr["Number"];
            row["Occured"] = dr["Occured"];
            row["Status"] = ToStatus(dr["Status"]);
            row["AmbulanceOffered"] = 0;
            row["AmbulanceDeclined"] = 0;
            row["FirstAidOffered"] = 0;
            row["FirstAidDeclined"] = 0;
            row["TaxiFareOffered"] = 0;
            row["TaxiFareDeclined"] = 0;
            row["ClosedBy"] = dr["ClosedBy"];
            row["Closed"] = dr["Closed"];
            row["Archive"] = 0;
            row["Created"] = dr["Created"];
            row["CreatedBy"] = ToOperator(dr["CreatedBy"]);
            row["Type"] = dr["Type"];
            row["Specific"] = dr["Specific"];
            row["Category"] = dr["Category"];
            row["SecondaryOperator"] = dr["SecondaryOperator"];
            row["Details"] = dr["Details"] + System.Environment.NewLine + dr["UnmatchedData"];
            row["ClosingRemarks"] = dr["ClosingRemarks"];
            row["LastModifiedDate"] = dr["LastModifiedDate"];
            row["ExecutiveBrief"] = dr["ExecutiveBrief"];
            row["ModifiedBy"] = ToOperator(dr["ModifiedBy"]);
            row["ArchiveDate"] = dr["ArchiveDate"];
         
            return row;
        }
        #endregion

        public int GetSourceRowCount()
        {
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_DetailedReport";
            int rowCount = (int)SqlHelper.ExecuteScalar(sourceConnectionString,CommandType.Text, sqlText);

            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        public void ImportIncident()
        {
            string topNumberOfRows = string.Empty;
            if(DataHelper.IsTesting() == true)
                topNumberOfRows = "Top " + DataHelper.TestingRows;

            string getSource =  string.Format("SELECT {0} * FROM __iTrakImporter_DetailedReport",topNumberOfRows);
            using (IDataReader dr = SqlHelper.ExecuteReader(sourceConnectionString,CommandType.Text, getSource))
            {
                using (SqlConnection conn = new SqlConnection(iXDataConnectionString))
                {
                    conn.Open();                 
                    SqlTransaction trans = conn.BeginTransaction();                    
                    try
                    {
                        BlotterDAL blotterDAL = new BlotterDAL();
                        IncidentDAL IncidentDAL = new IncidentDAL();
                        int i = 0;
                        while (dr.Read())
                        {
                            //get one blotter
                            System.Data.DataRow blotterRow = this.ReadOneBlotter(dr);
                            //get one incident
                            System.Data.DataRow incidentRow = this.ReadOneIncident(dr);
                            //Import one blotter
                            Guid blotterGuid = blotterDAL.ImportOneBlotter(blotterRow, trans);
                            //Import one incident
                            IncidentDAL.ImportOneIncident(blotterGuid,incidentRow, trans);
                            if (this.ImportedRowNumber != null)
                                ImportedRowNumber(++i, null);
                        }
                        trans.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {
                        
                    }
                    conn.Close();
                }
               
            }
        }
    }
}
