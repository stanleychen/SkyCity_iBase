using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;

using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;
namespace iTrak.Importer.Data.Importers
{
    public class Employee
    {
        private const string DEFAULT_RACE = "Unknown";
        private const string DEFAULT_GENDER = "Unknown";
        private const string DEFAULT_CATEGORY = "Default";
        private static ManualResetEvent[] _resetEvents;
        public event EventHandler ImportedRowEvent;
        public event EventHandler ImportCompletedEvent;
        private int _count = 0;
        public int Count
        {
            get
            {
                if (_count == 0)
                    _count = GetSourceRowCount();
                return _count;
            }
        }

        #region GetSourceRowCount
        private int GetSourceRowCount()
        {
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_Employee (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion

        #region Import Employee

        #region Build iTrakEmployee
        private void BuildiTrakEmployee(ref EmployeeBE employeeBE)
        {
            employeeBE.DateOfBirth = new DateTime(employeeBE.DateOfBirth.Year, employeeBE.DateOfBirth.Month, employeeBE.DateOfBirth.Day);
         
        }
        #endregion

        #region GetOneEmployee
        private EmployeeBE GetOneEmployee(IDataReader dr)
        {
            EmployeeBE employeeBE = new EmployeeBE();
            employeeBE.RowID = SqlClientUtility.GetInt32(dr, "RowID", 0);
            employeeBE.EmployeeGUID = SqlClientUtility.GetGuid(dr, "EmployeeGUID", Guid.Empty);
            employeeBE.PropertyGUID = SqlClientUtility.GetGuid(dr, "PropertyGUID", Guid.Empty);
            employeeBE.FirstName = SqlClientUtility.GetString(dr, "FirstName", String.Empty);
            employeeBE.MiddleName = SqlClientUtility.GetString(dr, "MiddleName", String.Empty);
            employeeBE.LastName = SqlClientUtility.GetString(dr, "LastName", String.Empty);
            employeeBE.StreetAddress = SqlClientUtility.GetString(dr, "StreetAddress", String.Empty);
            employeeBE.City = SqlClientUtility.GetString(dr, "City", String.Empty);
            employeeBE.State = SqlClientUtility.GetString(dr, "State", String.Empty);
            employeeBE.ZipCode = SqlClientUtility.GetString(dr, "ZipCode", String.Empty);
            employeeBE.PhoneNumber = SqlClientUtility.GetString(dr, "PhoneNumber", String.Empty);
            employeeBE.SocialSecurityNumber = SqlClientUtility.GetString(dr, "SocialSecurityNumber", String.Empty);
            employeeBE.DateOfBirth = SqlClientUtility.GetDateTime(dr, "DateOfBirth", DateTime.Now);
            employeeBE.EmergencyNotify = SqlClientUtility.GetString(dr, "EmergencyNotify", String.Empty);
            employeeBE.LogonName = SqlClientUtility.GetString(dr, "LogonName", String.Empty);
            employeeBE.EmployeeID = SqlClientUtility.GetString(dr, "EmployeeID", String.Empty);
            employeeBE.SupervisorLevel = SqlClientUtility.GetInt32(dr, "SupervisorLevel", 0);
            employeeBE.DateHired = SqlClientUtility.GetDateTime(dr, "DateHired", DateTime.Now);
            employeeBE.DateFired = SqlClientUtility.GetDateTime(dr, "DateFired", DateTime.Now);
            employeeBE.DateOfSeniority = SqlClientUtility.GetDateTime(dr, "DateOfSeniority", DateTime.Now);
            employeeBE.GamingCardNumber = SqlClientUtility.GetString(dr, "GamingCardNumber", String.Empty);
            employeeBE.GamingCardExpiryDate = SqlClientUtility.GetDateTime(dr, "GamingCardExpiryDate", DateTime.Now);
            employeeBE.MondayOff = SqlClientUtility.GetBoolean(dr, "MondayOff", false);
            employeeBE.TuesdayOff = SqlClientUtility.GetBoolean(dr, "TuesdayOff", false);
            employeeBE.WednesdayOff = SqlClientUtility.GetBoolean(dr, "WednesdayOff", false);
            employeeBE.ThursdayOff = SqlClientUtility.GetBoolean(dr, "ThursdayOff", false);
            employeeBE.FridayOff = SqlClientUtility.GetBoolean(dr, "FridayOff", false);
            employeeBE.SaturdayOff = SqlClientUtility.GetBoolean(dr, "SaturdayOff", false);
            employeeBE.SundayOff = SqlClientUtility.GetBoolean(dr, "SundayOff", false);
            employeeBE.GamingRelated = SqlClientUtility.GetBoolean(dr, "GamingRelated", false);
            employeeBE.Country = SqlClientUtility.GetString(dr, "Country", String.Empty);
            employeeBE.Department = SqlClientUtility.GetString(dr, "Department", String.Empty);
            employeeBE.JobPosition = SqlClientUtility.GetString(dr, "JobPosition", String.Empty);
            employeeBE.Shift = SqlClientUtility.GetString(dr, "Shift", String.Empty);
            employeeBE.Email = SqlClientUtility.GetString(dr, "Email", String.Empty);
            employeeBE.Exclusive = SqlClientUtility.GetString(dr, "Exclusive", String.Empty);
            employeeBE.Password = SqlClientUtility.GetString(dr, "Password", String.Empty);
            employeeBE.OtherSkills = SqlClientUtility.GetString(dr, "OtherSkills", String.Empty);
            //employeeBE.TimeStamp = SqlClientUtility.GetDateTime(dr, "TimeStamp", DateTime.Now);
            employeeBE.DateCreated = SqlClientUtility.GetDateTime(dr, "DateCreated", DateTime.Now);
            employeeBE.DateModified = SqlClientUtility.GetDateTime(dr, "DateModified", DateTime.Now);
            employeeBE.SupervisorGUID = SqlClientUtility.GetGuid(dr, "SupervisorGUID", Guid.Empty);
            employeeBE.Division = SqlClientUtility.GetString(dr, "Division", String.Empty);
            employeeBE.Height = SqlClientUtility.GetInt32(dr, "Height", 0);
            employeeBE.Weight = SqlClientUtility.GetInt32(dr, "Weight", 0);
            employeeBE.ShiftStart = SqlClientUtility.GetDateTime(dr, "ShiftStart", DateTime.Now);
            employeeBE.ShiftEnd = SqlClientUtility.GetDateTime(dr, "ShiftEnd", DateTime.Now);
            employeeBE.CellPhoneNumber = SqlClientUtility.GetString(dr, "CellPhoneNumber", String.Empty);
            employeeBE.Gender = SqlClientUtility.GetString(dr, "Gender", String.Empty);
            employeeBE.PrimaryLanguageSpoken = SqlClientUtility.GetString(dr, "PrimaryLanguageSpoken", String.Empty);
            employeeBE.MaritalStatus = SqlClientUtility.GetString(dr, "MaritalStatus", String.Empty);
            employeeBE.BestAssetGUID = SqlClientUtility.GetGuid(dr, "BestAssetGUID", Guid.Empty);
            employeeBE.ModifiedBy = SqlClientUtility.GetString(dr, "ModifiedBy", String.Empty);
            employeeBE.BusinessPhoneNumber = SqlClientUtility.GetString(dr, "BusinessPhoneNumber", String.Empty);
            employeeBE.BusinessFaxNumber = SqlClientUtility.GetString(dr, "BusinessFaxNumber", String.Empty);
            employeeBE.MailCode = SqlClientUtility.GetString(dr, "MailCode", String.Empty);
            
            employeeBE.LockForAPI = SqlClientUtility.GetBoolean(dr, "LockForAPI", false);
            employeeBE.Custom1 = SqlClientUtility.GetString(dr, "Custom1", String.Empty);
            employeeBE.Custom2 = SqlClientUtility.GetString(dr, "Custom2", String.Empty);
            employeeBE.IsGlobal = SqlClientUtility.GetBoolean(dr, "IsGlobal", false);
            employeeBE.DriversLicenseNum = SqlClientUtility.GetString(dr, "DriversLicenseNum", String.Empty);
            employeeBE.HairColour = SqlClientUtility.GetString(dr, "HairColour", String.Empty);
            employeeBE.EyeColour = SqlClientUtility.GetString(dr, "EyeColour", String.Empty);
            employeeBE.PassportNumber = SqlClientUtility.GetString(dr, "PassportNumber", String.Empty);
            employeeBE.WebAddress = SqlClientUtility.GetString(dr, "WebAddress", String.Empty);
            employeeBE.DepartmentGuid = DataHelper.GetGuid(dr["DepartmentGuid"]);
            employeeBE.EmployeeSourceID = SqlClientUtility.GetString(dr, "SourceID", string.Empty);
            employeeBE.CreatedBy = SqlClientUtility.GetString(dr, "CreatedBy", string.Empty);

            StringBuilder sb = new StringBuilder();
            sb.Append(dr["Comments"].ToString());
            sb.Append(System.Environment.NewLine);
            sb.Append(dr["uString"].ToString());
            employeeBE.Comments = sb.ToString();

            BuildiTrakEmployee(ref employeeBE);

            return employeeBE;
        }
        #endregion

        #region ImportOneRecord
        public Guid ImportOneRecord(EmployeeBE employeeBE)
        {
            Guid newGuid = Guid.Empty;
            try
            {
                List<SqlParameter> paraList = new List<SqlParameter>();
                SqlParameter empParam = new SqlParameter();
                empParam.ParameterName = "@EmployeeGUID";
                empParam.DbType = DbType.Guid;
                empParam.Direction = ParameterDirection.InputOutput;
                if (employeeBE.EmployeeGUID == Guid.Empty) //New Row
                {
                    empParam.Value = DBNull.Value;
                }
                else
                {
                    empParam.Value = employeeBE.EmployeeGUID;
                }
                paraList.Add(empParam);
                //if (employeeBE.EmployeeGUID != Guid.Empty)
                //    paraList.Add(new SqlParameter("@EmployeeGUID", employeeBE.EmployeeGUID));
                paraList.Add(new SqlParameter("@EmployeeSourceID", employeeBE.EmployeeSourceID));


                paraList.Add(new SqlParameter("@FirstName", employeeBE.FirstName));
                paraList.Add(new SqlParameter("@MiddleName", employeeBE.MiddleName));
                paraList.Add(new SqlParameter("@LastName", employeeBE.LastName));
                paraList.Add(new SqlParameter("@StreetAddress", employeeBE.StreetAddress));
                paraList.Add(new SqlParameter("@City", employeeBE.City));
                paraList.Add(new SqlParameter("@State", employeeBE.State));
                paraList.Add(new SqlParameter("@ZipCode", employeeBE.ZipCode));
                paraList.Add(new SqlParameter("@PhoneNumber", employeeBE.PhoneNumber));
                paraList.Add(new SqlParameter("@SocialSecurityNumber", employeeBE.SocialSecurityNumber));
                paraList.Add(new SqlParameter("@DateOfBirth", employeeBE.DateOfBirth));
                paraList.Add(new SqlParameter("@EmergencyNotify", employeeBE.EmergencyNotify));
                paraList.Add(new SqlParameter("@LogonName", employeeBE.LogonName));
                paraList.Add(new SqlParameter("@employeeID", employeeBE.EmployeeID));
                paraList.Add(new SqlParameter("@SupervisorLevel", employeeBE.SupervisorLevel));
                paraList.Add(new SqlParameter("@DateHired", employeeBE.DateHired));
                if (employeeBE.PropertyGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@PropertyGUID", employeeBE.PropertyGUID));
                paraList.Add(new SqlParameter("@DateFired", employeeBE.DateFired));
                paraList.Add(new SqlParameter("@DateOfSeniority", employeeBE.DateOfSeniority));
                paraList.Add(new SqlParameter("@GamingCardNumber", employeeBE.GamingCardNumber));
                paraList.Add(new SqlParameter("@GamingCardExpiryDate", employeeBE.GamingCardExpiryDate));
                paraList.Add(new SqlParameter("@MondayOff", employeeBE.MondayOff));
                paraList.Add(new SqlParameter("@TuesdayOff", employeeBE.TuesdayOff));
                paraList.Add(new SqlParameter("@WednesdayOff", employeeBE.WednesdayOff));
                paraList.Add(new SqlParameter("@ThursdayOff", employeeBE.ThursdayOff));
                paraList.Add(new SqlParameter("@FridayOff", employeeBE.FridayOff));
                paraList.Add(new SqlParameter("@SaturdayOff", employeeBE.SaturdayOff));
                paraList.Add(new SqlParameter("@SundayOff", employeeBE.SundayOff));
                paraList.Add(new SqlParameter("@GamingRelated", employeeBE.GamingRelated));
                paraList.Add(new SqlParameter("@Country", employeeBE.Country));
                paraList.Add(new SqlParameter("@Department", employeeBE.Department));
                paraList.Add(new SqlParameter("@JobPosition", employeeBE.JobPosition));
                paraList.Add(new SqlParameter("@Shift", employeeBE.Shift));
                paraList.Add(new SqlParameter("@Email", employeeBE.Email));
                paraList.Add(new SqlParameter("@Exclusive", employeeBE.Exclusive));
                paraList.Add(new SqlParameter("@OtherSkills", employeeBE.OtherSkills));
                paraList.Add(new SqlParameter("@DateCreated", employeeBE.DateCreated));
                paraList.Add(new SqlParameter("@DateModified", employeeBE.DateModified));
                paraList.Add(new SqlParameter("@SupervisorGUID", employeeBE.SupervisorGUID));
                paraList.Add(new SqlParameter("@Division", employeeBE.Division));
                paraList.Add(new SqlParameter("@Height", employeeBE.Height));
                paraList.Add(new SqlParameter("@Weight", employeeBE.Weight));
                paraList.Add(new SqlParameter("@ShiftStart", employeeBE.ShiftStart));
                paraList.Add(new SqlParameter("@ShiftEnd", employeeBE.ShiftEnd));
                paraList.Add(new SqlParameter("@CellPhoneNumber", employeeBE.CellPhoneNumber));
                paraList.Add(new SqlParameter("@Gender", employeeBE.Gender));
                paraList.Add(new SqlParameter("@PrimaryLanguageSpoken", employeeBE.PrimaryLanguageSpoken));
                paraList.Add(new SqlParameter("@MaritalStatus", employeeBE.MaritalStatus));
                paraList.Add(new SqlParameter("@BestAssetGUID", employeeBE.BestAssetGUID));
                paraList.Add(new SqlParameter("@ModifiedBy", employeeBE.ModifiedBy));
                paraList.Add(new SqlParameter("@BusinessPhoneNumber", employeeBE.BusinessPhoneNumber));
                paraList.Add(new SqlParameter("@BusinessFaxNumber", employeeBE.BusinessFaxNumber));
                paraList.Add(new SqlParameter("@MailCode", employeeBE.MailCode));
                paraList.Add(new SqlParameter("@Comments", employeeBE.Comments));
                paraList.Add(new SqlParameter("@Custom1", employeeBE.Custom1));
                paraList.Add(new SqlParameter("@Custom2", employeeBE.Custom2));
                paraList.Add(new SqlParameter("@IsGlobal", employeeBE.IsGlobal));
                paraList.Add(new SqlParameter("@DriversLicenseNum", employeeBE.DriversLicenseNum));
                paraList.Add(new SqlParameter("@HairColour", employeeBE.HairColour));
                paraList.Add(new SqlParameter("@EyeColour", employeeBE.EyeColour));
                paraList.Add(new SqlParameter("@PassportNumber", employeeBE.PassportNumber));
                paraList.Add(new SqlParameter("@WebAddress", employeeBE.WebAddress));
                paraList.Add(new SqlParameter("@DepartmentGuid", employeeBE.DepartmentGuid));
                paraList.Add(new SqlParameter("@CreatedBy", employeeBE.CreatedBy));

                SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_Employee]", paraList.ToArray());

                newGuid = (Guid)empParam.Value;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import personnel", ex);
            }
            return newGuid;
        }
        #endregion

        #region Import Employees
        public void ImportEmployee()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_Employee]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                using (SqlConnection conn = new SqlConnection(DbHelper.iTrakConnectionString))
                {
                    conn.Open();
                               
                    try
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            #region Build Entity
                            
                            EmployeeBE employee = GetOneEmployee(dr);
                            this.ImportOneRecord(employee);                                
                            #endregion

                            if (this.ImportedRowEvent != null)
                            {
                                ImportedRowEvent(++i, new ImportEventArgs("Importing Employee"));
                            }
                            if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                            {
                                break;
                            }
                        }
                        conn.Close();
                        if (this.ImportCompletedEvent != null)
                        {
                            ImportCompletedEvent(_count, new ImportEventArgs("Completed Subjects"));
                        }

                    }
                    catch (Exception ex)
                    {
                        if (conn != null)
                            conn.Close();
                      
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {

                    }
                }
            }
        }
        #endregion 

        #endregion

        #region ImportEmployee
        private static void ImportOneEmployee2(object o)
        {
            object[] objs = o as object[];
            int index = (int)objs[0];
            Employee employee = objs[1] as Employee;
            EmployeeBE employeeBE = objs[2] as EmployeeBE;
            SqlConnection conn = null;     
            try
            {
                conn = new SqlConnection(DbHelper.iTrakConnectionString);
                conn.Open();
          
                employee.ImportOneRecord(employeeBE);
            
                conn.Close();
            }
            catch (Exception ex)
            {
                  throw new Exception("Failed to import one Employee", ex);
            }
            finally
            {
                _resetEvents[index].Set();
                if (conn != null)
                    conn.Close();
            }

        }
        public void ImportEmployee2()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_Employee]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {

                try
                {
                    int i = 0;
                    _resetEvents = new ManualResetEvent[_count];
                    while (dr.Read())
                    {
                        #region Build Entity

                        EmployeeBE employeeBE = GetOneEmployee(dr);

                        int index = i % ThreadHelper.NUMBER_OF_THREADS;
                        object[] objs = new object[3];

                        objs[0] = index;
                        objs[1] = this;
                        objs[2] = employeeBE;

                        _resetEvents[index] = new ManualResetEvent(false);

                        ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneEmployee2), objs);
                        if (index == ThreadHelper.NUMBER_OF_THREADS - 1)
                        {
                            ThreadHelper.WaitAll(_resetEvents);
                            _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                        }

                        #endregion

                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing Employees"));
                        }
                        if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                        {
                            break;
                        }
                    }
                    ThreadHelper.WaitAll(_resetEvents);

                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed Employees"));
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {

                }
            }
        }
        #endregion 

    }
}
