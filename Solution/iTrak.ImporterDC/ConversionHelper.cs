using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;

using Microsoft.ApplicationBlocks.Data;
namespace iTrak.Importer.Data
{
    public class ConversionHelper
    {
        #region Create TempTable
        public static void RunSourceScript(string connectionString)
        {
            RunScript(connectionString, "SourceScript.sql");
        }
        public static void CreateTempTable(string connectionString)
        {
            RunScript(connectionString, "SQLScript.sql");
        }
        private static void RunScript(string connectionString,string fileName)
        {
            StringBuilder sBuilder = new StringBuilder();
            try
            {
                using (System.IO.TextReader textReader = new StreamReader(fileName))
                {
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        if (line.Trim().ToUpper() != "GO")
                        {
                            sBuilder.Append(line + " ");
                        }
                        else
                        {
                            SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sBuilder.ToString());
                            sBuilder = new StringBuilder();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute sql script" + fileName, ex);
            }
        }
        #endregion

        #region TempTable
        public static void DropIncidentTempTable(string connectionString)
        {
            string sqlString = "IF  EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[iXData_DetailedReport]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " +
                               " DROP TABLE [dbo].[iXData_DetailedReport]";
            SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sqlString);
        }
        public static void DropSubjectTempTable(string connectionString)
        {
            string sqlString = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[iXData_SubjectProfile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) " +
                                " drop table [dbo].[iXData_SubjectProfile]";
            SqlHelper.ExecuteNonQuery(connectionString,CommandType.Text,sqlString);
        }
        #endregion

        #region Helper Function
        public static void GetAgeRanges(string s, out int lowerAge, out int upperAge)
        {
            int age = 0;
            lowerAge = 0;
            upperAge = 0;
            if (Int32.TryParse(s, out age) == true)
            {
                if (age < 20)
                {
                    lowerAge = 0;
                    upperAge = 19;
                }
                else if (age < 30)
                {
                    lowerAge = 20;
                    upperAge = 29;
                }
                else if (age < 40)
                {
                    lowerAge = 30;
                    upperAge = 39;
                }
                else if (age < 50)
                {
                    lowerAge = 40;
                    upperAge = 49;
                }
                else if (age < 60)
                {
                    lowerAge = 50;
                    upperAge = 59;
                }
                else if (age < 70)
                {
                    lowerAge = 60;
                    upperAge = 69;
                }
                else if (age < 80)
                {
                    lowerAge = 70;
                    upperAge = 79;
                }
                else 
                {
                    lowerAge = 80;
                    upperAge = 89;
                }

            }
        }
        public static int GetAgeRangeUpper(string s)
        {
            return 0;
        }
        public static System.DateTime ToDateTime(object dbObj)
        {
            DateTime tempDate = System.DateTime.Now;
            if (System.DBNull.Value != dbObj)
            {
                if (DateTime.TryParse(dbObj.ToString(), out tempDate) == false)
                    tempDate = System.DateTime.Now;
            }
            return tempDate;
        }
        public static object ToNullableDateTime(object dbObj)
        {
            object returnValue = System.DBNull.Value;
            if (System.DBNull.Value != dbObj)
            {
                DateTime tempDate = DateTime.Now;
                if (DateTime.TryParse(dbObj.ToString(), out tempDate) == true)
                {
                    returnValue = tempDate;
                }
            }
            return returnValue;
        }
        public static Nullable<System.DateTime> CombineDateTime(DataRow sRow, string dateField, string timeField)
        {
            System.DateTime tempDatetime = System.DateTime.Now;
            try
            {
                if (sRow[dateField] == System.DBNull.Value || sRow[timeField] == System.DBNull.Value)
                    return null;
                string[] dateStrings = sRow[dateField].ToString().Trim().Split(' ');
                string[] timeStrings = sRow[timeField].ToString().Trim().Split(' ');
                string dateTimeString = string.Empty;
                if (timeStrings.Length > 2)
                    dateTimeString = dateStrings[0] + " " + timeStrings[1] + " " + timeStrings[2];
                else
                    dateTimeString = dateStrings[0] + " " + timeStrings[0] + " " + timeStrings[1];

                if (DateTime.TryParse(dateTimeString, out tempDatetime) == false)
                    tempDatetime = System.DateTime.Now;
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError(dateField + ": " + sRow[dateField].ToString() + ", " + timeField + ": " + sRow[timeField] + ". " + ex.Message);
            }
            return tempDatetime;
        }
        public static bool IsNull(object o)
        {
            if (o == System.DBNull.Value || o == null)
                return true;
            else
                return false;
        }
        public static bool IsTrue(object o)
        {
            if (o == System.DBNull.Value)
                return false;
            else
                return (bool)o;
        }
        public static string BoolToYesNo(object o)
        {
            if (IsTrue(o))
                return "Yes";
            else
                return "No";
        }
        public static void AppendUnmatchColumn(ref StringBuilder builder, string caption, object dbObj)
        {
            if (!IsNull(dbObj))
            {
                AppendUnmatchColumn(ref builder, caption, dbObj.ToString());
            }
        }
        public static void AppendUnmatchColumn(ref StringBuilder builder, string caption, string value)
        {
            if (value != null && value != string.Empty && value.Trim() != string.Empty && value.Trim() != "0")
            {
                builder.Append(caption + ": " + value);
                builder.Append(System.Environment.NewLine);
            }
        }
        public static string  GetCreatedBy(DataRow sRow, string fieldName)
        {
            string tempString = sRow[fieldName].ToString();
            if (tempString.Length > 16)
                tempString = tempString.Substring(0, 16);

            return tempString;
        }
        public static string GetLocation(DataRow sRow, string fieldName)
        {
            string subLocation = sRow[fieldName].ToString().Trim();
            if (subLocation.Length > 50)
                subLocation = subLocation.Substring(0, 50);
            return subLocation;
        }
        public static string GetState(DataRow sRow, string fieldName)
        {
            string state = sRow[fieldName].ToString().Trim();
            if (state.Length > 20)
                state = state.Substring(0, 20);
            return state;
        }
        #endregion
    }
}
