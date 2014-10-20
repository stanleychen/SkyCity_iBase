using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
namespace iTrak.Importer.Data
{
    class SubjectBanWatchStatus : BaseDAL
    {
        private string masterquery_subjectBanWatchStatus;
        private SqlDataAdapter adapter_subjectBanWatchStatus;
        private DataSetIXData dsIXData;
        public SubjectBanWatchStatus()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters.BanWatchStatusTableAdapter tableAdapter = new DataSetIXDataTableAdapters.BanWatchStatusTableAdapter();
            adapter_subjectBanWatchStatus = GetAdapter(tableAdapter);
            SubjectBanWatchStatusExtendedTableAdapter extendedAdapter = new SubjectBanWatchStatusExtendedTableAdapter();
            adapter_subjectBanWatchStatus.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            this.masterquery_subjectBanWatchStatus = adapter_subjectBanWatchStatus.SelectCommand.CommandText;
        }

        private void UpdateRow(System.Data.DataRow srcRow, ref System.Data.DataRow dstRow)
        {
            try
            {
                dstRow["SubjectGUID"] = srcRow["SubjectGUID"];
                dstRow["DetailedReportGUID"] = srcRow["DetailedReportGUID"];
                if (srcRow["IsBanned"] != System.DBNull.Value && (bool)srcRow["IsBanned"])
                {
                    dstRow["Status"] = 1;
                }
                else
                {
                    dstRow["Status"] = 0;
                }
                dstRow["Commencement"] = srcRow["BanStartDate"];
                dstRow["EndDate"] = srcRow["BanEndDate"];
                if (srcRow["IsPermanentBanned"] == System.DBNull.Value)
                {
                    dstRow["IsPermanent"] = 0;
                }
                else
                {
                    dstRow["IsPermanent"] = srcRow["IsPermanentBanned"];
                }
                dstRow["TypeOfBan"] = srcRow["TypeOfBan"];
                dstRow["ReasonForBan"] = srcRow["ReasonOfBan"];
            }
            catch
            {
                throw;
            }
        }
        public void ImportOneSubjectBanWatchStatus(System.Data.DataRow srcRow, SqlTransaction trans)
        {
            this.SetTransaction(adapter_subjectBanWatchStatus, trans);

            dsIXData.BanWatchStatus.Clear();
            this.adapter_subjectBanWatchStatus.SelectCommand.CommandText = this.masterquery_subjectBanWatchStatus + " WHERE SubjectGUID in (SELECT ConvertGUID FROM _ConversionTmp WHERE TableName='SubjectProfile' AND ConvertId1 ='" + srcRow["SourceID"] + "')";
            this.adapter_subjectBanWatchStatus.Fill(dsIXData, "BanWatchStatus");
            if (dsIXData.BanWatchStatus.Rows.Count > 0) //update row
            {
                System.Data.DataRow dstRow = dsIXData.BanWatchStatus.Rows[0];
                this.UpdateRow(srcRow, ref dstRow);
            }
            else //import row
            {
                System.Data.DataRow newRow = dsIXData.BanWatchStatus.NewRow();
                newRow["SubjectGUID"] = srcRow["SubjectGUID"];
                this.UpdateRow(srcRow, ref newRow);
                dsIXData.BanWatchStatus.Rows.Add(newRow);
            }
            adapter_subjectBanWatchStatus.Update(dsIXData, "BanWatchStatus");
        }
    }
}
