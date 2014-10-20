using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

using iTrak.Importer.Common;
namespace iTrak.Importer.Data
{
    class BlotterDAL : BaseDAL 
    {
        private string masterquery_blotter;
        SqlDataAdapter adapter_blotter;
        DataSetIXData dsIXData;
        public BlotterDAL()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters.BlotterTableAdapter tableAdapter = new DataSetIXDataTableAdapters.BlotterTableAdapter();
            adapter_blotter = GetAdapter(tableAdapter);
            BlotterExtendedTableAdapter extendedAdapter = new BlotterExtendedTableAdapter();
            adapter_blotter.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            adapter_blotter.UpdateCommand.CommandTimeout = 60 * 30; //30 minutes
            this.masterquery_blotter = adapter_blotter.SelectCommand.CommandText;
        }
        private void UpdateRow(System.Data.DataRow srcRow,ref System.Data.DataRow dstRow)
        {
            try
            {
                dstRow["Occured"] = srcRow["Occured"];
                dstRow["BlotterAction"] = "Import";
                dstRow["Subject"] = srcRow["Subject"];
                dstRow["Property"] = srcRow["Property"];
                dstRow["Created"] = ToDateTime(srcRow["Created"]);
                dstRow["Archive"] = srcRow["Archive"];
                dstRow["PrimaryOperator"] = srcRow["PrimaryOperator"];
                dstRow["SecondaryOperator"] = ToOperator(srcRow["SecondaryOperator"]);
                dstRow["HighPriority"] = srcRow["HighPriority"];
                dstRow["Status"] = srcRow["Status"];
                dstRow["Sublocation"] = srcRow["Sublocation"];
                dstRow["Location"] = srcRow["Location"];
                dstRow["Exclusive"] = srcRow["Exclusive"];
                dstRow["Synopsis"] = srcRow["Synopsis"];
                dstRow["LastModifiedDate"] = ToDateTime(srcRow["LastModifiedDate"]);
                dstRow["Reference"] = srcRow["Reference"];
                dstRow["ModifiedBy"] = srcRow["ModifiedBy"];
                dstRow["ArchiveDate"] = srcRow["ArchiveDate"];
                dstRow["EndTime"] = srcRow["EndTime"];
                dstRow["ClosedTime"] = srcRow["ClosedTime"];
                dstRow["IsGlobal"] = srcRow["IsGlobal"];
                dstRow["SourceModuleID"] = srcRow["SourceModuleID"];
                dstRow["SourceID"] = srcRow["SourceID"];
                dstRow["SourceGUID"] = srcRow["SourceGUID"];
                dstRow["LockedBySource"] = srcRow["LockedBySource"];
            }
            catch
            {
                throw;
            }
        }
        public Guid ImportOneBlotter(System.Data.DataRow srcRow, SqlTransaction trans)
        {
            this.SetTransaction(adapter_blotter, trans);
            dsIXData.Blotter.Clear();

            SqlParameter numberParam = new SqlParameter();
            numberParam.ParameterName = "@Number";
            numberParam.DbType = DbType.String;
            numberParam.Value = DataHelper.GetBlotterNumber(srcRow["Number"].ToString());

            if (!this.adapter_blotter.SelectCommand.Parameters.Contains("@Number"))
                this.adapter_blotter.SelectCommand.Parameters.Add(numberParam);

            this.adapter_blotter.SelectCommand.Parameters["@Number"].Value = DataHelper.GetBlotterNumber(srcRow["Number"].ToString());
            this.adapter_blotter.SelectCommand.CommandText = this.masterquery_blotter + " WHERE Number=@Number";
            this.adapter_blotter.Fill(dsIXData, "Blotter");
            if (dsIXData.Blotter.Rows.Count > 0) //update row
            {
                System.Data.DataRow dstRow = dsIXData.Blotter.Rows[0];
                this.UpdateRow(srcRow, ref dstRow);
            }
            else //import new row
            {
                System.Data.DataRow newRow = dsIXData.Blotter.NewRow();
                newRow["BlotterGUID"] = Guid.NewGuid();
                newRow["Number"] = DataHelper.GetBlotterNumber(srcRow["Number"].ToString());
                this.UpdateRow(srcRow, ref newRow);
                dsIXData.Blotter.Rows.Add(newRow);
            }
            adapter_blotter.Update(dsIXData, "Blotter");
            return (Guid)dsIXData.Blotter.Rows[0]["BlotterGuid"];
        }

        public void CreateDummyBlotter(Guid blotterGUID, SqlTransaction trans)
        {
            try
            {
                this.SetTransaction(adapter_blotter, trans);

                dsIXData.Blotter.Clear();
                this.adapter_blotter.SelectCommand.CommandText = this.masterquery_blotter + " WHERE BlotterGUID='" + blotterGUID + "'";
                this.adapter_blotter.Fill(dsIXData, "Blotter");
                if (dsIXData.Blotter.Rows.Count == 0) //update row
                {
                    System.Threading.Thread.Sleep(2);
                    DateTime date = DateTime.Now;
                    System.Data.DataRow newRow = dsIXData.Blotter.NewRow();
                    newRow["BlotterGUID"] = blotterGUID;
                    newRow["BlotterAction"] = "Import";
                    newRow["Occured"] = DateTime.Now;
                    newRow["Number"] = "DI_" + date.Year + date.Month.ToString() + date.Day.ToString() + date.Hour.ToString() + date.Minute.ToString() + date.Second.ToString() + date.Millisecond.ToString();
                    newRow["Created"] = DateTime.Now;
                    newRow["Property"] = System.Guid.NewGuid();
                    newRow["HighPriority"] = 0;
                    newRow["IsGlobal"] = true;
                    newRow["Archive"] = false;
                    newRow["LastModifiedDate"] = DateTime.Now;
                    newRow["SourceModuleID"] = 0;
                    newRow["LockedBySource"] = 0;
                    newRow["Status"] = "Closed";
                    dsIXData.Blotter.Rows.Add(newRow);
                }
                adapter_blotter.Update(dsIXData, "Blotter");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import bloter", ex);
            }
        }
    }
}
