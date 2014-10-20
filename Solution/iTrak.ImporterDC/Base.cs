using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
namespace iTrak.Importer.Data
{
    public abstract class BaseDAL
    {
        protected SqlDataAdapter GetAdapter(object tableAdapter)
        {
            Type tableAdapterType = tableAdapter.GetType();
            SqlDataAdapter adapter = (SqlDataAdapter)tableAdapterType.GetProperty("Adapter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(tableAdapter, null);            
            return adapter;
        }
        protected void SetTransaction(SqlDataAdapter adapter, SqlTransaction trans)
        {
            adapter.SelectCommand.Connection = trans.Connection;
            adapter.SelectCommand.Transaction = trans;
            adapter.InsertCommand.Connection = trans.Connection;
            adapter.InsertCommand.Transaction = trans;
            adapter.UpdateCommand.Connection = trans.Connection;
            adapter.UpdateCommand.Transaction = trans;
            adapter.DeleteCommand.Connection = trans.Connection;
            adapter.DeleteCommand.Transaction = trans;
        }
        #region "Data Helper Function"
        protected System.DateTime ToDateTime(object dbObj)
        {
            if (System.DBNull.Value == dbObj)
                return System.DateTime.Now;
            else
                return (System.DateTime)dbObj;
        }
        protected string ToOperator(object dbObj)
        {
            if (System.DBNull.Value == dbObj)
            {
                return "N/A";
            }
            else
            {
                if (dbObj.ToString().Length > 16)
                    return dbObj.ToString().Substring(0, 16);
                else
                    return dbObj.ToString();
            }
        }

        protected string ToStatus(object dbObj)
        {
            if (System.DBNull.Value == dbObj)
            {
                return "Open";
            }
            else
            {
                if (dbObj.ToString().Trim().ToLower() == "closed")
                    return "Closed";
                else
                    return dbObj.ToString();
            }
        }
        protected string ToNameString(object dbObj)
        {
            if (System.DBNull.Value == dbObj)
            {
                return string.Empty;
            }
            else
            {
                return dbObj.ToString();
            }
        }
        protected string ToGender(object dbObj)
        {
            if (System.DBNull.Value == dbObj || string.Empty == dbObj.ToString().Trim())
            {
                return "Unknown";
            }
            else
            {
                return dbObj.ToString();
            }
        }
        protected string ToRace(object dbObj)
        {
            if (System.DBNull.Value == dbObj || string.Empty == dbObj.ToString().Trim())
            {
                return "Unknown";
            }
            else
            {
                return dbObj.ToString();
            }
        }
        protected string ToEyeColor(object dbObj)
        {
            if (System.DBNull.Value == dbObj || string.Empty == dbObj.ToString().Trim())
            {
                return "Unknown";
            }
            else
            {
                return dbObj.ToString();
            }
        }
        protected string ToHairColor(object dbObj)
        {
            if (System.DBNull.Value == dbObj || string.Empty == dbObj.ToString().Trim())
            {
                return "Unknown";
            }
            else
            {
                return dbObj.ToString();
            }
        }
        protected string ToUserID(object dbObj)
        {
            if (System.DBNull.Value == dbObj || string.Empty == dbObj.ToString().Trim())
            {
                return "Importer";
            }
            else
            {
                return dbObj.ToString();
            }
        }
        #endregion
    }
}
