using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
namespace iTrak.Importer.Data.Importers
{
    public class Property
    {
        private static void CreateTempTable()
        {
            string dropSQL = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_PropertyMappings]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " +
                                " drop table [dbo].[__iTrakImporter_PropertyMappings] ";

            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.Text, dropSQL);

            string createSQL = " CREATE TABLE [dbo].[__iTrakImporter_PropertyMappings] ( " +
                                " [SourcePropertyCode] [varchar] (255) NOT NULL , " +
                                " [iTrakPropertyName] [varchar] (255) NOT NULL , " +
                                " [iTrakDepartmentName] [varchar] (255) NOT NULL , " +
                                " [Description] [varchar] (255) NULL,  " +
                                " [CurrentImporting] [bit]  NULL  " +
                                " ) ON [PRIMARY] " +
                                " ALTER TABLE [dbo].[__iTrakImporter_PropertyMappings] ADD  " +
                                " CONSTRAINT [PK___iTrakImporter_PropertyMappings] PRIMARY KEY  CLUSTERED  " +
                                " ( [SourcePropertyCode] )  ON [PRIMARY] ";

            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.Text, createSQL);

        }
        private static void ValidatePropertyMapping(SqlTransaction trans, DataTable sourcePropertyTable)
        {
            string sqlString = "SELECT * FROM SystemProperty WHERE IsPropertyGroup = 0";
            DataSet dsiTrakProperties = SqlHelper.ExecuteDataset(trans, CommandType.Text, sqlString);
            DataView iTrakPropertyView = dsiTrakProperties.Tables[0].DefaultView;
            List<string> propertyList = new List<string>();
            foreach (DataRow row in sourcePropertyTable.Rows)
            {
                string propertyCode = row["SourcePropertyCode"].ToString();
                if (propertyList.Contains(propertyCode))
                    throw new Exception(string.Format("The property code \"{0}\" is duplicated.", propertyCode));
                else
                    propertyList.Add(propertyCode);

                string propertyName = row["iTrakPropertyName"].ToString();
                iTrakPropertyView.RowFilter = string.Format("PropertyName = '{0}'", propertyName.Replace("'", "''"));
                if (iTrakPropertyView.Count == 0)
                {
                    string msg = string.Format("The property \"{0}\" is required in iTrak database in order to continue the conversion.", propertyName);
                    throw new Exception(msg);
                }
            }
        }
        private static void ValidateDepartmentMapping(SqlTransaction trans, DataTable sourcePropertyTable)
        {
            string sqlString = "SELECT * FROM SystemDepartment";
            DataSet dsDepartments = SqlHelper.ExecuteDataset(trans, CommandType.Text, sqlString);
            DataView departmentView = dsDepartments.Tables[0].DefaultView;
            foreach (DataRow row in sourcePropertyTable.Rows)
            {
                string departmentName = row["iTrakDepartmentName"].ToString();
                departmentView.RowFilter = string.Format("DepartmentName = '{0}'", departmentName.Replace("'", "''"));
                if (departmentView.Count == 0)
                {
                    string msg = string.Format("The department \"{0}\" is required in iTrak database in order to continue the conversion.", departmentName);
                    throw new Exception(msg);
                }
            }
        }

        public static void ImportPropertyMappings(string appPath)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            string xmpFile = Path.Combine(appPath, "PropertyMappings.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(xmpFile, XmlReadMode.Auto);
            try
            {
                CreateTempTable(); //create property temp table

                conn = new SqlConnection(DbHelper.iTrakConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                ValidatePropertyMapping(trans, ds.Tables[0]); //Failed to convert if the property is not in iTrak database

                ValidateDepartmentMapping(trans, ds.Tables[0]); //Failed to convert if Department is not in Trak Database

                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM __iTrakImporter_PropertyMappings");

                string insertSQL = "INSERT INTO __iTrakImporter_PropertyMappings(SourcePropertyCode,iTrakPropertyName,iTrakDepartmentName, Description,CurrentImporting) ";
                insertSQL += " VALUES(@SourcePropertyCode,@iTrakPropertyName,@iTrakDepartmentName,@Description,@CurrentImporting)";

                int i = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    List<SqlParameter> paraList = new List<SqlParameter>();
                    paraList.Add(new SqlParameter("@SourcePropertyCode", row["SourcePropertyCode"].ToString()));
                    paraList.Add(new SqlParameter("@iTrakPropertyName", row["iTrakPropertyName"].ToString()));
                    paraList.Add(new SqlParameter("@iTrakDepartmentName", row["iTrakDepartmentName"].ToString()));
                    paraList.Add(new SqlParameter("@Description", row["Description"].ToString()));
                    if (i == 0)
                        paraList.Add(new SqlParameter("@CurrentImporting", 1)); //set first row to default
                    else
                        paraList.Add(new SqlParameter("@CurrentImporting", 0));
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, insertSQL, paraList.ToArray());
                    i++;
                }
                trans.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
    }
}
