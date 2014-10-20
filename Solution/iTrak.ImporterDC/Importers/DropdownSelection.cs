using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace iTrak.Importer.Data.Importers
{
    public class DropdownSelection
    {
        public static void AddOneDropdownSelection(string type,string text)
        {
            string insertSQL = "IF NOT EXISTS(SELECT * FROM DropdownSelection WHERE SelectionType = SelectionType AND SelectionText=@SelectionText) ";
            insertSQL += "INSERT INTO DropdownSelection (SelectionGUID, SelectionType, SelectionText, Hidden,ParentGUID,lock) ";
            insertSQL += " VALUES (NewID(), @SelectionType, @SelectionText, 0, NULL,0) ";

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@SelectionType", type));
            paraList.Add(new SqlParameter("@SelectionText", text));

            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.Text, insertSQL, paraList.ToArray());
        }
    }
}
