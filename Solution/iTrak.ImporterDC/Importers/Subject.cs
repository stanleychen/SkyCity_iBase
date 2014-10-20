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
using System.Data.SqlTypes;
namespace iTrak.Importer.Data.Importers
{
    public class Subject
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_SubjectProfile (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion

        #region Import Subjects

        #region Build iTrakEmployee
        private void BuildiTrakSubject(ref SubjectBE subjectBE)
        {
            if (string.IsNullOrEmpty(subjectBE.Race))
                subjectBE.Race = DEFAULT_RACE;
            if (string.IsNullOrEmpty(subjectBE.Gender))
                subjectBE.Gender = DEFAULT_GENDER;
            if (string.IsNullOrEmpty(subjectBE.Category))
                subjectBE.Category = DEFAULT_CATEGORY;
            subjectBE.DateOfBirth = new DateTime(subjectBE.DateOfBirth.Year, subjectBE.DateOfBirth.Month, subjectBE.DateOfBirth.Day);

            if(string.IsNullOrEmpty(subjectBE.Owner))
                subjectBE.Owner = "Importer";

            int lowerAge = 0;
            int upperAge = 0;
            DataHelper.GetAgeRanges(subjectBE.DateOfBirth,out lowerAge,out upperAge);
            subjectBE.AgeRangeLower = lowerAge;
            subjectBE.AgeRangeUpper = upperAge;
        }
        #endregion

        #region GetOneSubject
        private SubjectBE GetOneSubject(IDataReader dr)
        {
            SubjectBE subjectBE = new SubjectBE();
            subjectBE.RowID = SqlClientUtility.GetInt32(dr, "RowID", 0);
            subjectBE.SubjectGUID = SqlClientUtility.GetGuid(dr, "SubjectGUID", Guid.Empty);
            subjectBE.SubjectSourceID = SqlClientUtility.GetString(dr, "SourceID", String.Empty);
            subjectBE.FirstName = SqlClientUtility.GetString(dr, "FirstName", String.Empty);
            subjectBE.MiddleName = SqlClientUtility.GetString(dr, "MiddleName", String.Empty);
            subjectBE.LastName = SqlClientUtility.GetString(dr, "LastName", String.Empty);
            subjectBE.Gender = SqlClientUtility.GetString(dr, "Gender", String.Empty);
            subjectBE.DateOfBirth = SqlClientUtility.GetDateTime(dr, "DateOfBirth", DateTime.Now);
            subjectBE.AgeRangeLower = SqlClientUtility.GetInt32(dr, "AgeRangeLower", 0);
            subjectBE.AgeRangeUpper = SqlClientUtility.GetInt32(dr, "AgeRangeUpper", 0);
            subjectBE.Height = SqlClientUtility.GetInt32(dr, "Height", 0);
            subjectBE.Weight = SqlClientUtility.GetInt32(dr, "Weight", 0);
            subjectBE.HairColour = SqlClientUtility.GetString(dr, "HairColour", String.Empty);
            subjectBE.EyeColour = SqlClientUtility.GetString(dr, "EyeColour", String.Empty);
            subjectBE.Race = SqlClientUtility.GetString(dr, "Race", String.Empty);
            subjectBE.DateCreated = SqlClientUtility.GetDateTime(dr, "DateCreated", DateTime.Now);
            subjectBE.DateModified = SqlClientUtility.GetDateTime(dr, "DateModified", DateTime.Now);
            subjectBE.CreatoruserID = SqlClientUtility.GetString(dr, "CreatoruserID", String.Empty);
            subjectBE.PropertyGUID = SqlClientUtility.GetGuid(dr, "PropertyGUID", Guid.Empty);
            
            subjectBE.LastIncidentDate = SqlClientUtility.GetDateTime(dr, "LastIncidentDate", DateTime.Now);
            subjectBE.BestAssetGUID = SqlClientUtility.GetGuid(dr, "BestAssetGUID", Guid.Empty);
            subjectBE.Category = SqlClientUtility.GetString(dr, "Category", String.Empty);
            subjectBE.Activities = SqlClientUtility.GetString(dr, "Activities", String.Empty);
            subjectBE.Specifics = SqlClientUtility.GetString(dr, "Specifics", String.Empty);
            subjectBE.Groups = SqlClientUtility.GetString(dr, "Groups", String.Empty);
            subjectBE.Aliases = SqlClientUtility.GetString(dr, "Aliases", String.Empty);
            subjectBE.Traits = SqlClientUtility.GetString(dr, "Traits", String.Empty);
            subjectBE.Address = SqlClientUtility.GetString(dr, "Address", String.Empty);
            subjectBE.City = SqlClientUtility.GetString(dr, "City", String.Empty);
            subjectBE.State = SqlClientUtility.GetString(dr, "State", String.Empty);
            subjectBE.PostalCode = SqlClientUtility.GetString(dr, "PostalCode", String.Empty);
            subjectBE.Country = SqlClientUtility.GetString(dr, "Country", String.Empty);
            subjectBE.HomePhone = SqlClientUtility.GetString(dr, "HomePhone", String.Empty);
            subjectBE.WorkPhone = SqlClientUtility.GetString(dr, "WorkPhone", String.Empty);
            subjectBE.Email = SqlClientUtility.GetString(dr, "Email", String.Empty);
            subjectBE.ClientID = SqlClientUtility.GetString(dr, "ClientID", String.Empty);
            subjectBE.SINNumber = SqlClientUtility.GetString(dr, "SINNumber", String.Empty);
            subjectBE.Occupation = SqlClientUtility.GetString(dr, "Occupation", String.Empty);
            subjectBE.DriversLicenseNum = SqlClientUtility.GetString(dr, "DriversLicenseNum", String.Empty);
            subjectBE.ModifiedBy = SqlClientUtility.GetString(dr, "ModifiedBy", String.Empty);
            subjectBE.Custom1 = SqlClientUtility.GetString(dr, "Custom1", String.Empty);
            subjectBE.Custom2 = SqlClientUtility.GetString(dr, "Custom2", String.Empty);
            subjectBE.CompanyName = SqlClientUtility.GetString(dr, "CompanyName", String.Empty);
            subjectBE.PassportNumber = SqlClientUtility.GetString(dr, "PassportNumber", String.Empty);
            subjectBE.BusinessFaxNumber = SqlClientUtility.GetString(dr, "BusinessFaxNumber", String.Empty);
            subjectBE.WebAddress = SqlClientUtility.GetString(dr, "WebAddress", String.Empty);
            subjectBE.DepartmentGuid = DataHelper.GetGuid(dr["DepartmentGuid"]);

            StringBuilder sb = new StringBuilder();
            sb.Append(dr["Comment"].ToString());
            sb.Append(System.Environment.NewLine);
            sb.Append(dr["uString"].ToString());
            subjectBE.Comment = sb.ToString();


            BuildiTrakSubject(ref subjectBE);

            return subjectBE;
        }
        #endregion

        #region ImportOneRecord
        public Guid ImportOneRecord(SubjectBE subjectBE)
        {
            try
            {
                List<SqlParameter> paraList = new List<SqlParameter>();
                SqlParameter subjectParam = new SqlParameter();
                subjectParam.ParameterName = "@SubjectGUID";
                subjectParam.DbType = DbType.Guid;
                subjectParam.Direction = ParameterDirection.InputOutput;
                if (subjectBE.SubjectGUID == Guid.Empty) //New Row
                {
                    subjectParam.Value = DBNull.Value;
                }
                else
                {
                    subjectParam.Value = subjectBE.SubjectGUID;
                }
                paraList.Add(subjectParam);
                paraList.Add(new SqlParameter("@SubjectSourceID", subjectBE.SubjectSourceID));
                paraList.Add(new SqlParameter("@FirstName", subjectBE.FirstName));
                paraList.Add(new SqlParameter("@MiddleName", subjectBE.MiddleName));
                paraList.Add(new SqlParameter("@LastName", subjectBE.LastName));
                paraList.Add(new SqlParameter("@Gender", subjectBE.Gender));
                if (subjectBE.DateOfBirth < DateTime.Now.AddDays(-1) && subjectBE.DateOfBirth > SqlDateTime.MinValue)
                    paraList.Add(new SqlParameter("@DateOfBirth", subjectBE.DateOfBirth));
                else
                    paraList.Add(new SqlParameter("@DateOfBirth", System.DBNull.Value));

                paraList.Add(new SqlParameter("@AgeRangeLower", subjectBE.AgeRangeLower));
                paraList.Add(new SqlParameter("@AgeRangeUpper", subjectBE.AgeRangeUpper));
                paraList.Add(new SqlParameter("@Height", subjectBE.Height));
                paraList.Add(new SqlParameter("@Weight", subjectBE.Weight));
                paraList.Add(new SqlParameter("@HairColour", subjectBE.HairColour));
                paraList.Add(new SqlParameter("@EyeColour", subjectBE.EyeColour));
                paraList.Add(new SqlParameter("@Race", subjectBE.Race));
                paraList.Add(new SqlParameter("@DateCreated", subjectBE.DateCreated));
                paraList.Add(new SqlParameter("@DateModified", subjectBE.DateModified));
                paraList.Add(new SqlParameter("@CreatoruserID", subjectBE.CreatoruserID));
                if (subjectBE.PropertyGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@PropertyGUID", subjectBE.PropertyGUID));
                paraList.Add(new SqlParameter("@Comment", subjectBE.Comment));
                paraList.Add(new SqlParameter("@LastIncidentDate", subjectBE.LastIncidentDate));
                paraList.Add(new SqlParameter("@Exclusive", subjectBE.Exclusive));
                if (subjectBE.BestAssetGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@BestAssetGUID", subjectBE.BestAssetGUID));
                paraList.Add(new SqlParameter("@Category", subjectBE.Category));
                paraList.Add(new SqlParameter("@Activities", subjectBE.Activities));
                paraList.Add(new SqlParameter("@Specifics", subjectBE.Specifics));
                paraList.Add(new SqlParameter("@Groups", subjectBE.Groups));
                paraList.Add(new SqlParameter("@Aliases", subjectBE.Aliases));
                paraList.Add(new SqlParameter("@Traits", subjectBE.Traits));
                paraList.Add(new SqlParameter("@Address", subjectBE.Address));
                paraList.Add(new SqlParameter("@City", subjectBE.City));
                paraList.Add(new SqlParameter("@State", subjectBE.State));
                paraList.Add(new SqlParameter("@PostalCode", subjectBE.PostalCode));
                paraList.Add(new SqlParameter("@Country", subjectBE.Country));
                paraList.Add(new SqlParameter("@HomePhone", subjectBE.HomePhone));
                paraList.Add(new SqlParameter("@WorkPhone", subjectBE.WorkPhone));
                paraList.Add(new SqlParameter("@Email", subjectBE.Email));
                paraList.Add(new SqlParameter("@ClientID", subjectBE.ClientID));
                paraList.Add(new SqlParameter("@SINNumber", subjectBE.SINNumber));
                paraList.Add(new SqlParameter("@Occupation", subjectBE.Occupation));
                paraList.Add(new SqlParameter("@DriversLicenseNum", subjectBE.DriversLicenseNum));
                paraList.Add(new SqlParameter("@ModifiedBy", subjectBE.ModifiedBy));
                paraList.Add(new SqlParameter("@Custom1", subjectBE.Custom1));
                paraList.Add(new SqlParameter("@Custom2", subjectBE.Custom2));
                paraList.Add(new SqlParameter("@CompanyName", subjectBE.CompanyName));
                paraList.Add(new SqlParameter("@PassportNumber", subjectBE.PassportNumber));
                paraList.Add(new SqlParameter("@BusinessFaxNumber", subjectBE.BusinessFaxNumber));
                paraList.Add(new SqlParameter("@WebAddress", subjectBE.WebAddress));
                paraList.Add(new SqlParameter("@Owner", subjectBE.Owner));
                paraList.Add(new SqlParameter("@DepartmentGuid", subjectBE.DepartmentGuid));

                SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_SubjectProfile]", paraList.ToArray());

                return (Guid)subjectParam.Value;
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error(ex.Message, ex);
                throw new Exception("Failed to import one subject", ex);
            }
        }
        #endregion

        #region ImportSubjects
        public void ImportSubjects()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_SubjectProfile]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                //using (SqlConnection conn = new SqlConnection(DbHelper.iTrakConnectionString))
                //{
                //    conn.Open();
                    //SqlTransaction trans = conn.BeginTransaction();                    
                    try
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            #region Build Entity
                            
                            SubjectBE subjectBE = GetOneSubject(dr);
                            this.ImportOneRecord(subjectBE);                                
                            #endregion

                            if (this.ImportedRowEvent != null)
                            {
                                ImportedRowEvent(++i, new ImportEventArgs("Importing Subjects"));
                            }
                            if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                            {
                                break;
                            }
                        }
                        //trans.Commit();
                        //conn.Close();


                        if (this.ImportCompletedEvent != null)
                        {
                            ImportCompletedEvent(_count, new ImportEventArgs("Completed Subjects"));
                        }

                    }
                    catch (Exception ex)
                    {
                        //trans.Rollback();
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {

                    }
                //}
            }
        }
        #endregion 

        #endregion

        #region ImportSubjects2
        private static void ImportOneSubject2(object o)
        {
            object[] objs = o as object[];
            int index = (int)objs[0];
            Subject subject = objs[1] as Subject;
            SubjectBE subjectBE = objs[2] as SubjectBE;
            //SqlConnection conn = null;
        
            try
            {
                //conn = new SqlConnection(DbHelper.iTrakConnectionString);
                //conn.Open();
            
                subject.ImportOneRecord(subjectBE);

              
                //conn.Close();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Failed to import one subject", ex);
            }
            finally
            {
                _resetEvents[index].Set();
                //if (conn != null)
                //    conn.Close();
            }

        }
        public void ImportSubjects2()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;

            string sourceSQL = "[dbo].[__iTrakImporter_sps_SubjectProfile]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {

                try
                {
                    int i = 0;
                    _resetEvents = new ManualResetEvent[_count];
                    while (dr.Read())
                    {
                        #region Build Entity

                        SubjectBE subjectBE = GetOneSubject(dr);

                        int index = i % ThreadHelper.NUMBER_OF_THREADS;
                        object[] objs = new object[3];

                        objs[0] = index;
                        objs[1] = this;
                        objs[2] = subjectBE;

                        _resetEvents[index] = new ManualResetEvent(false);

                        ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneSubject2), objs);
                        if (index == ThreadHelper.NUMBER_OF_THREADS - 1)
                        {
                            ThreadHelper.WaitAll(_resetEvents);
                            _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                        }

                        #endregion

                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing Subjects"));
                        }
                        if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                        {
                            break;
                        }
                    }
                    ThreadHelper.WaitAll(_resetEvents);
                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed Subjects"));
                    }

                }
                catch (Exception ex)
                {
                    CCGeneral.LogAndDisplay.Error(ex.Message, ex);
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
