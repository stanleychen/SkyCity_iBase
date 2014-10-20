using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

using iTrak.Importer.Common;
namespace iTrak.Importer.Data
{
    class IncidentDAL : BaseDAL
    {
        private string masterquery_Incident;
        SqlDataAdapter adapter_Incident;
        DataSetIXData dsIXData;
        public IncidentDAL()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters.DetailedReportTableAdapter tableAdapter = new iTrak.Importer.Data.DataSetIXDataTableAdapters.DetailedReportTableAdapter();
            adapter_Incident = GetAdapter(tableAdapter);
            DetailedReportExtendedTableAdapter extendedAdapter = new DetailedReportExtendedTableAdapter();
            adapter_Incident.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            adapter_Incident.UpdateCommand.CommandTimeout = 60 * 30; //30 minutes
            this.masterquery_Incident = adapter_Incident.SelectCommand.CommandText;
        }
        private void UpdateRow(System.Data.DataRow srcRow, ref System.Data.DataRow dstRow)
        {
            try
            {
                dstRow["Occured"] = srcRow["Occured"];               
                dstRow["Status"] = srcRow["Status"];               
                dstRow["ClosedBy"] = srcRow["ClosedBy"];
                dstRow["Closed"] = srcRow["Closed"];                
                dstRow["Created"] = ToDateTime(srcRow["Created"]);
                dstRow["CreatedBy"] = srcRow["CreatedBy"];
                dstRow["Type"] = srcRow["Type"];
                dstRow["Specific"] = srcRow["Specific"];
                dstRow["Category"] = srcRow["Category"];
                dstRow["SecondaryOperator"] = ToOperator(srcRow["SecondaryOperator"]);
                dstRow["Details"] = srcRow["Details"];
                dstRow["ClosingRemarks"] = srcRow["ClosingRemarks"];
                dstRow["LastModifiedDate"] = ToDateTime(srcRow["LastModifiedDate"]);
                dstRow["ExecutiveBrief"] = srcRow["ExecutiveBrief"];
                dstRow["ModifiedBy"] = srcRow["ModifiedBy"];
                dstRow["ArchiveDate"] = srcRow["ArchiveDate"];

                #region Bit Fields
                dstRow["Archive"] = 0;
                dstRow["AmbulanceOffered"] = 0;
                dstRow["AmbulanceDeclined"] = 0;
                dstRow["FirstAidOffered"] = 0;
                dstRow["FirstAidDeclined"] = 0;
                dstRow["TaxiFareOffered"] = 0;
                dstRow["TaxiFareDeclined"] = 0;
                dstRow["IsGlobal"] = 0;
                #endregion
            }
            catch
            {
                throw;
            }
        }
        public void ImportOneIncident(Guid blotterGuid,System.Data.DataRow srcRow, SqlTransaction trans)
        {
            this.SetTransaction(adapter_Incident, trans);

            dsIXData.DetailedReport.Clear();

            SqlParameter numberParam = new SqlParameter();
            numberParam.ParameterName = "@Number";
            numberParam.DbType = DbType.String;
            if(!this.adapter_Incident.SelectCommand.Parameters.Contains("@Number"))
                this.adapter_Incident.SelectCommand.Parameters.Add(numberParam);

            this.adapter_Incident.SelectCommand.Parameters["@Number"].Value = DataHelper.GetIncidentNumber(srcRow["Number"].ToString());
            this.adapter_Incident.SelectCommand.CommandText = this.masterquery_Incident + " WHERE Number=@Number";
            this.adapter_Incident.Fill(dsIXData, "DetailedReport");
            if (dsIXData.DetailedReport.Rows.Count > 0) //update row
            {
                System.Data.DataRow dstRow = dsIXData.DetailedReport.Rows[0];
                this.UpdateRow(srcRow, ref dstRow);
            }
            else //import new Row
            {
                System.Data.DataRow newRow = this.dsIXData.DetailedReport.NewRow();
                newRow["DetailedReportGUID"] = Guid.NewGuid();
                newRow["BlotterGUID"] = blotterGuid;
                newRow["Number"] = DataHelper.GetIncidentNumber(srcRow["Number"].ToString());
                this.UpdateRow(srcRow, ref newRow);
                dsIXData.DetailedReport.Rows.Add(newRow);
            }
        
            adapter_Incident.Update(dsIXData, "DetailedReport");
        }
        public void CreateDummyIncident(System.Guid incidentGUID, System.Guid blotterGUID,SqlTransaction trans)
        {
            this.SetTransaction(adapter_Incident, trans);

            dsIXData.DetailedReport.Clear();
            this.adapter_Incident.SelectCommand.CommandText = this.masterquery_Incident + " WHERE DetailedReportGUID='" + incidentGUID + "'";
            this.adapter_Incident.Fill(dsIXData, "DetailedReport");
            if (dsIXData.DetailedReport.Rows.Count == 0) //update row
            {
                System.Threading.Thread.Sleep(2);
                DateTime date = DateTime.Now;
                System.Data.DataRow newRow = this.dsIXData.DetailedReport.NewRow();
                newRow["DetailedReportGUID"] = incidentGUID;
                newRow["Number"] = "II_" + date.Year + date.Month.ToString() + date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
                newRow["BlotterGUID"] = blotterGUID;
                newRow["Occured"] = DateTime.Now;
                newRow["AmbulanceOffered"] = 0;
                newRow["AmbulanceDeclined"] = 0;
                newRow["FirstAidOffered"] = 0;
                newRow["FirstAidDeclined"] = 0;
                newRow["TaxiFareOffered"] = 0;
                newRow["TaxiFareDeclined"] = 0;
                newRow["Created"] = DateTime.Now;
                newRow["CreatedBy"] = "N/A";
                newRow["IsGlobal"] = true;
                dsIXData.DetailedReport.Rows.Add(newRow);
            }

            adapter_Incident.Update(dsIXData, "DetailedReport");


        }
    }
}
