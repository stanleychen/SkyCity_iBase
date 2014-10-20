using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

using iTrak.Importer.Common;
using iTrak.Importer.Entities;
namespace iTrak.Importer.Data
{
    public class SubjectImporter
    {
        private string sourceConnectionString;
        private string iXDataConnectionString;
        public event EventHandler ImportedRowNumber;
        public SubjectImporter(string sourceConnectionString, string iXDataConnectionString)
        {
            this.sourceConnectionString = sourceConnectionString;
            this.iXDataConnectionString = iXDataConnectionString;
        }

        #region Data Reader
        private System.Data.DataRow GetOneSubjectRow(IDataReader dr)
        {
            DataSetIXData.iXData_SubjectProfileDataTable subject = new DataSetIXData.iXData_SubjectProfileDataTable();
            System.Data.DataRow row = subject.NewRow();
            //row["SubjectGUID"] = dr["SubjectGUID"];
            row["FirstName"] = dr["FirstName"];
            row["MiddleName"] = dr["MiddleName"];
            row["LastName"] = dr["LastName"];
            row["Gender"] = dr["Gender"];
            row["DateOfBirth"] = dr["DateOfBirth"];
            row["AgeRangeLower"] = dr["AgeRangeLower"];
            row["AgeRangeUpper"] = dr["AgeRangeUpper"];
            row["Height"] = dr["Height"];
            row["Weight"] = dr["Weight"];
            row["HairColour"] = dr["HairColour"];
            row["EyeColour"] = dr["EyeColour"];
            row["Race"] = dr["Race"];
            row["DateCreated"] = dr["DateCreated"];
            row["DateModified"] = dr["DateModified"];
            row["CreatoruserID"] = dr["CreatoruserID"];
            row["PropertyGUID"] = dr["PropertyGUID"];
            row["Comment"] = dr["Comment"] + System.Environment.NewLine + dr["UnmatchedData"];
            row["LastIncidentDate"] = dr["LastIncidentDate"];
            row["Category"] = "Default";
            row["Activities"] = dr["Activities"];
            row["Specifics"] = dr["Specifics"];
            row["Groups"] = dr["Groups"];
            row["Aliases"] = dr["Aliases"];
            row["Traits"] = dr["Traits"];
            row["Address"] = dr["Address"];
            row["City"] = dr["City"];
            row["State"] = dr["State"];
            row["PostalCode"] = dr["PostalCode"];
            row["Country"] = dr["Country"];
            row["HomePhone"] = dr["HomePhone"];
            row["WorkPhone"] = dr["WorkPhone"];
            row["Email"] = dr["Email"];
            row["ClientID"] = dr["ClientID"];
            row["SINNumber"] = dr["SINNumber"];
            row["Occupation"] = dr["Occupation"];
            row["DriversLicenseNum"] = dr["DriversLicenseNum"];
            row["ModifiedBy"] = dr["ModifiedBy"];
            row["Custom1"] = dr["Custom1"];
            row["Custom2"] = dr["Custom2"];
            row["CompanyName"] = dr["CompanyName"];
            row["PassportNumber"] = dr["PassportNumber"];
            row["BusinessFaxNumber"] = dr["BusinessFaxNumber"];
            row["FRSAcSysUserID"] = 0;
            row["DataProviderType"] = 0;
            row["WebAddress"] = dr["WebAddress"];
            row["SourceID"] = dr["SourceID"];
            row["IncidentNumber"] = dr["IncidentNumber"];
            row["IsBanned"] = dr["IsBanned"];
            row["BanStartDate"] = dr["BanStartDate"];
            row["BanEndDate"] = dr["BanEndDate"];
            row["BanIdentificationUsed"] = dr["BanIdentificationUsed"];
            row["TypeOfBan"] = dr["TypeOfBan"];
            row["ReasonOfBan"] = dr["ReasonOfBan"];
            row["IsPermanentBanned"] = dr["IsPermanentBanned"];
            row["SubjectCharged"] = dr["SubjectCharged"];
            row["LetterSent"] = dr["LetterSent"];
            row["CompulsiveGambler"] = dr["CompulsiveGambler"];
            row["RecordType"] = dr["RecordType"];
            row["Status"] = dr["Status"];
            row["DetailedReportGUID"] = dr["DetailedReportGUID"];
            return row;
        }

     
        #endregion

        public int GetSourceRowCount()
        {
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_SubjectProfile";
            int rowCount = (int)SqlHelper.ExecuteScalar(sourceConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        public void ImportSubject()
        {
            string topNumberOfRows = string.Empty;
            if (DataHelper.IsTesting() == true)
                topNumberOfRows = "Top " + DataHelper.TestingRows;
            string sqlString = string.Format("SELECT ss.*,s.SubjectGuid as iTrakSubjectGUID FROM __iTrakImporter_SubjectProfile ss  (nolock) ",topNumberOfRows);
            sqlString += " left outer join _ConversionTmp  ct (nolock) on ss.SourceID = ct.ConvertId1 ";
            sqlString += " left outer join SubjectProfile s (nolock)  on ct.ConvertGUID = s.SubjectGUID ";
            sqlString += " WHERE ct.TableName='SubjectProfile' or ct.ConvertGuid is null";
            using (IDataReader dr = SqlHelper.ExecuteReader(sourceConnectionString, CommandType.Text, sqlString))
            {
                using (SqlConnection conn = new SqlConnection(iXDataConnectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    try
                    {
                        SubjectDAL subjectDAL = new SubjectDAL();
                        SubjectBanDAL subjectBanDAL = new SubjectBanDAL();
                        SubjectBanWatchStatus subjectBanWatchStatus = new SubjectBanWatchStatus();
                        BlotterDAL blotterDAL = new BlotterDAL();
                        IncidentDAL incidentDAL = new IncidentDAL();
                        ParticipantAssignmentDAL participantAssignmentDAL = new ParticipantAssignmentDAL();
                        DropdownSelectionDAL dropdownSelectionDAL = new DropdownSelectionDAL();
                        int i = 0;
                        while (dr.Read())
                        {
                            #region Importing Data
                            //get one subject
                            Guid subjectGuid = Guid.Empty;
                            if (dr["iTrakSubjectGuid"] != System.DBNull.Value)
                                subjectGuid = (Guid)dr["iTrakSubjectGuid"];
                            System.Data.DataRow subjectRow = this.GetOneSubjectRow(dr);
                            //Import one blotter
                            subjectDAL.ImportOneSubject(subjectGuid,subjectRow, trans);
                            if (dr["IsBanned"] != System.DBNull.Value && (bool)dr["IsBanned"])
                            {
                                //Create a dummy incident if there no any incident
                                if (subjectRow["DetailedReportGUID"] == System.DBNull.Value) 
                                {
                                    Guid blotterGUID = (Guid)subjectRow["SubjectGUID"];
                                    Guid IncidentGUID = (Guid)subjectRow["SubjectGUID"];
                                    blotterDAL.CreateDummyBlotter(blotterGUID, trans);
                                    incidentDAL.CreateDummyIncident(IncidentGUID, blotterGUID, trans);
                                    subjectRow["DetailedReportGUID"] = IncidentGUID;
                                }
                                subjectBanDAL.ImportOneSubjectBan(subjectRow, trans);
                                subjectBanWatchStatus.ImportOneSubjectBanWatchStatus(subjectRow, trans);
                                participantAssignmentDAL.ImportOneParticipantAssignment(subjectRow, trans);
                                string reasonForBan = subjectRow["ReasonOfBan"].ToString();
                                if (reasonForBan != string.Empty)
                                    dropdownSelectionDAL.ImportOneDropdownSelection("ReasonForBan", reasonForBan, trans);
                                string identificationUsed = "Unknown";
                                if (identificationUsed != string.Empty)
                                    dropdownSelectionDAL.ImportOneDropdownSelection("IdentificationUsed", identificationUsed, trans);
                            }
                            //Import one incident
                            ImportEventArgs eventArgs = new ImportEventArgs();
                            eventArgs.Message = "Importing Subject";
                          
                            if (this.ImportedRowNumber != null)
                                ImportedRowNumber(++i, eventArgs);

                            #endregion
                        }
                        trans.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {

                    }
                    conn.Close();
                }
            }
        }

    }
}
