using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
namespace iTrak.Importer.Data
{
    class SubjectBanDAL : BaseDAL
    {
        private string masterquery_subjectBan;
        private SqlDataAdapter adapter_subjectBan;
        private DataSetIXData dsIXData;
        public SubjectBanDAL()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters.SubjectBanTableAdapter tableAdapter = new DataSetIXDataTableAdapters.SubjectBanTableAdapter();
            adapter_subjectBan = GetAdapter(tableAdapter);
            SubjectBanExtendedTableAdapter extendedAdapter = new SubjectBanExtendedTableAdapter();
            adapter_subjectBan.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            this.masterquery_subjectBan = adapter_subjectBan.SelectCommand.CommandText;
        }
        private void UpdateRow(System.Data.DataRow srcRow, ref System.Data.DataRow dstRow)
        {
            try
            {
                dstRow["SubjectGUID"] = srcRow["SubjectGUID"];
                dstRow["DetailedReportGUID"] = srcRow["DetailedReportGUID"];
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
                if (srcRow["SubjectCharged"] == System.DBNull.Value)
                {
                    dstRow["SubjectCharged"] = 0;
                }
                else
                {
                    dstRow["SubjectCharged"] = srcRow["SubjectCharged"];
                }
                if (srcRow["LetterSent"] == System.DBNull.Value)
                {
                    dstRow["LetterSent"] = 0;
                }
                else
                {
                    dstRow["LetterSent"] = srcRow["LetterSent"];
                }
                if (srcRow["CompulsiveGambler"] == System.DBNull.Value)
                {
                    dstRow["CompulsiveGambler"] = 0;
                }
                else
                {
                    dstRow["CompulsiveGambler"] = srcRow["CompulsiveGambler"];
                }
                if (srcRow["IsBanned"] != System.DBNull.Value && (bool)srcRow["IsBanned"])
                {
                    dstRow["RecordType"] = 1;
                }
                else
                {
                    dstRow["RecordType"] = 0;
                }
                dstRow["TypeOfBan"] = srcRow["TypeOfBan"];
                dstRow["IdentificationUsed"] = "Unknown";
                dstRow["ReasonForBan"] = srcRow["ReasonOfBan"];
            }
            catch
            {
                throw;
            }
        }
        public void ImportOneSubjectBan(System.Data.DataRow srcRow, SqlTransaction trans)
        {
            this.SetTransaction(adapter_subjectBan, trans);

            dsIXData.SubjectBan.Clear();
            this.adapter_subjectBan.SelectCommand.CommandText = this.masterquery_subjectBan + " WHERE SubjectGUID ='" + srcRow["SubjectGUID"] + "' AND DetailedReportGUID='" + srcRow["DetailedReportGUID"] + "'";
            this.adapter_subjectBan.Fill(dsIXData, "SubjectBan");
            if (dsIXData.SubjectBan.Rows.Count > 0) //update row
            {
                System.Data.DataRow dstRow = dsIXData.SubjectBan.Rows[0];
                this.UpdateRow(srcRow, ref dstRow);
            }
            else //import row
            {
                System.Data.DataRow newRow = dsIXData.SubjectBan.NewRow();
                newRow["SubjectGUID"] = srcRow["SubjectGUID"];
                this.UpdateRow(srcRow, ref newRow);
                dsIXData.SubjectBan.Rows.Add(newRow);
            }
            adapter_subjectBan.Update(dsIXData, "SubjectBan");
        }
    }
}
