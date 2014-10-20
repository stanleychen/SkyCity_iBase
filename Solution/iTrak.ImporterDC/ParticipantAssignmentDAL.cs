using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
namespace iTrak.Importer.Data
{
    public class ParticipantAssignmentDAL : BaseDAL
    {
        private string masterquery_participantAssignment;
        private SqlDataAdapter adapter_participantAssignment;
        private DataSetIXData dsIXData;
        public ParticipantAssignmentDAL()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters.ParticipantAssignmentTableAdapter tableAdapter = new DataSetIXDataTableAdapters.ParticipantAssignmentTableAdapter();
            adapter_participantAssignment = GetAdapter(tableAdapter);
            ParticipantAssignmentExtendedTableAdapter extendedAdapter = new ParticipantAssignmentExtendedTableAdapter();
            adapter_participantAssignment.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            this.masterquery_participantAssignment = adapter_participantAssignment.SelectCommand.CommandText;
        }
        private void UpdateRow(System.Data.DataRow srcRow, ref System.Data.DataRow dstRow)
        {
            try
            {
                dstRow["ParticipantGUID"] = srcRow["SubjectGUID"];
                dstRow["DetailedReportGUID"] = srcRow["DetailedReportGUID"];
                dstRow["Assigned"] = DateTime.Now;
                dstRow["AssignedBy"] = "N/A";
                dstRow["ParticipantType"] = "Subject";
                //accused = 7343272C-39CA-475F-B717-1A24CD7EE330
                dstRow["ParticipantRole"] = new Guid("7343272C-39CA-475F-B717-1A24CD7EE330");
                dstRow["PoliceContacted"] = 0;
                dstRow["TakenFromScene"] = 0;
            }
            catch
            {
                throw;
            }
        }
        public void ImportOneParticipantAssignment(System.Data.DataRow srcRow, SqlTransaction trans)
        {
            try
            {
                this.SetTransaction(adapter_participantAssignment, trans);

                dsIXData.ParticipantAssignment.Clear();
                this.adapter_participantAssignment.SelectCommand.CommandText = this.masterquery_participantAssignment + " WHERE ParticipantGUID ='" + srcRow["SubjectGUID"] + "' AND DetailedReportGUID = '" + srcRow["DetailedReportGUID"] + "'";
                this.adapter_participantAssignment.Fill(dsIXData, "ParticipantAssignment");
                if (dsIXData.ParticipantAssignment.Rows.Count > 0) //update row
                {
                    System.Data.DataRow dstRow = dsIXData.ParticipantAssignment.Rows[0];
                    this.UpdateRow(srcRow, ref dstRow);
                }
                else //import row
                {
                    System.Data.DataRow newRow = dsIXData.ParticipantAssignment.NewRow();
                    newRow["ParticipantGUID"] = srcRow["SubjectGUID"];
                    newRow["DetailedReportGUID"] = srcRow["DetailedReportGUID"];
                    this.UpdateRow(srcRow, ref newRow);
                    dsIXData.ParticipantAssignment.Rows.Add(newRow);
                }
                adapter_participantAssignment.Update(dsIXData, "ParticipantAssignment");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to Import ParticipantAssigment. Due to "
                    + ex.Message + ". SQL: " + this.adapter_participantAssignment.SelectCommand.CommandText, ex);
            }
        }
    }
}
