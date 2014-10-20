using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace iTrak.Importer.Data
{
    public class ConversionTmpDAL :BaseDAL
    {
        private string masterquery_ConversionTmp;
        SqlDataAdapter adapter_ConversionTmp;
        DataSetIXData dsIXData;
        public ConversionTmpDAL()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters._ConversionTmpTableAdapter tableAdapter = new DataSetIXDataTableAdapters._ConversionTmpTableAdapter();
            adapter_ConversionTmp = GetAdapter(tableAdapter);
            BlotterExtendedTableAdapter extendedAdapter = new BlotterExtendedTableAdapter();
            adapter_ConversionTmp.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            this.masterquery_ConversionTmp = adapter_ConversionTmp.SelectCommand.CommandText;
        }

        public void AddOneConversionTmp(System.Guid convertGUID, SqlTransaction trans, string tableName, string convertID1)
        {
            this.SetTransaction(adapter_ConversionTmp, trans);
            dsIXData._ConversionTmp.Clear();
            System.Data.DataRow newRow = dsIXData._ConversionTmp.NewRow();
            newRow["ConvertGuid"] = convertGUID;
            newRow["TableName"] = tableName;
            newRow["ConvertId1"] = convertID1;
            dsIXData._ConversionTmp.Rows.Add(newRow);
            adapter_ConversionTmp.Update(dsIXData, "_ConversionTmp");
        }
    }
}
