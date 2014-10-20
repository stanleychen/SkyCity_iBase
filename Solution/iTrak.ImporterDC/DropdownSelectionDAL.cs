using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
namespace iTrak.Importer.Data
{
    public class DropdownSelectionDAL : BaseDAL
    {
        private string masterquery_dropdownSelection;
        private SqlDataAdapter adapter_dropdownSelection;
        private DataSetIXData dsIXData;
        public DropdownSelectionDAL()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters.DropdownSelectionTableAdapter tableAdapter = new DataSetIXDataTableAdapters.DropdownSelectionTableAdapter();
            adapter_dropdownSelection = GetAdapter(tableAdapter);
            DropdownSelectionExtendedTableAdapter extendedAdapter = new DropdownSelectionExtendedTableAdapter();
            adapter_dropdownSelection.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            this.masterquery_dropdownSelection = adapter_dropdownSelection.SelectCommand.CommandText;
        }
        
        public void ImportOneDropdownSelection(string selectionType,string selectionText, SqlTransaction trans)
        {
            this.SetTransaction(adapter_dropdownSelection, trans);
            
            dsIXData.DropdownSelection.Clear();
            this.adapter_dropdownSelection.SelectCommand.CommandText = this.masterquery_dropdownSelection + " WHERE SelectionType ='" + selectionType + "' AND selectionText = '" + selectionText + "'";
            this.adapter_dropdownSelection.Fill(dsIXData, "DropdownSelection");
            if (dsIXData.DropdownSelection.Rows.Count == 0) //Add New Row
            {
                System.Data.DataRow newRow = dsIXData.DropdownSelection.NewRow();
                newRow["SelectionGUID"] = Guid.NewGuid();
                newRow["SelectionType"] = selectionType;
                newRow["SelectionText"] = selectionText;
                newRow["DateModified"] = DateTime.Now;
                newRow["Hidden"] = 0;
                newRow["ParentGuid"] = System.DBNull.Value;
                newRow["PropertyGUID"] = System.DBNull.Value;
                newRow["SelectionCode"] = System.DBNull.Value;
                newRow["Lock"] = 0;
                newRow["SelectionDescription"] = System.DBNull.Value;
                dsIXData.DropdownSelection.Rows.Add(newRow);
            }
            adapter_dropdownSelection.Update(dsIXData, "DropdownSelection");
        }
    }
}
