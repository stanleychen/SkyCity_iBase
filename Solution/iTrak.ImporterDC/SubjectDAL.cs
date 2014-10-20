using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace iTrak.Importer.Data
{
    public class SubjectDAL : BaseDAL
    {
        private string masterquery_subject;
        private SqlDataAdapter adapter_subject;
        private DataSetIXData dsIXData;
        private ConversionTmpDAL tmpDAL;
        public SubjectDAL()
        {
            dsIXData = new DataSetIXData();
            DataSetIXDataTableAdapters.SubjectProfileTableAdapter tableAdapter = new DataSetIXDataTableAdapters.SubjectProfileTableAdapter();
            adapter_subject = GetAdapter(tableAdapter);
            SubjectExtendedTableAdapter extendedAdapter = new  SubjectExtendedTableAdapter();
            adapter_subject.SelectCommand = extendedAdapter.SqlCommandCollection[0];
            adapter_subject.SelectCommand.CommandTimeout = 60 * 10; //10 minutes;
            adapter_subject.InsertCommand.CommandTimeout = 60 * 20; // 20 miniutes;
            adapter_subject.UpdateCommand.CommandTimeout = 60 * 30; //30 minutes
            this.masterquery_subject = adapter_subject.SelectCommand.CommandText;
            tmpDAL = new ConversionTmpDAL();
        }

        private void UpdateRow(System.Data.DataRow srcRow, ref System.Data.DataRow dstRow)
        {
            try
            {
                dstRow["FirstName"] = ToNameString(srcRow["FirstName"]);
                dstRow["MiddleName"] = ToNameString(srcRow["MiddleName"]);
                dstRow["LastName"] = ToNameString(srcRow["LastName"]);
                dstRow["Gender"] = ToGender(srcRow["Gender"]);
                dstRow["DateOfBirth"] = srcRow["DateOfBirth"];
                dstRow["AgeRangeLower"] = srcRow["AgeRangeLower"];
                dstRow["AgeRangeUpper"] = srcRow["AgeRangeUpper"];
                dstRow["Height"] = srcRow["Height"];
                dstRow["Weight"] = srcRow["Weight"];
                dstRow["HairColour"] =ToHairColor( srcRow["HairColour"]);
                dstRow["EyeColour"] = ToEyeColor(srcRow["EyeColour"]);
                dstRow["Race"] = ToRace(srcRow["Race"]);
                dstRow["DateCreated"] = ToDateTime(srcRow["DateCreated"]);
                dstRow["DateModified"] = ToDateTime(srcRow["DateModified"]);
                dstRow["CreatoruserID"] = ToUserID(srcRow["CreatoruserID"]);
                dstRow["PropertyGUID"] = srcRow["PropertyGUID"];
                dstRow["Comment"] = srcRow["Comment"];
                dstRow["LastIncidentDate"] = System.DBNull.Value;
                dstRow["Category"] = "Default";
                dstRow["Activities"] = srcRow["Activities"];
                dstRow["Specifics"] = srcRow["Specifics"];
                dstRow["Groups"] = srcRow["Groups"];
                dstRow["Aliases"] = srcRow["Aliases"];
                dstRow["Traits"] = srcRow["Traits"];
                dstRow["Address"] = srcRow["Address"];
                dstRow["City"] = srcRow["City"];
                dstRow["State"] = srcRow["State"];
                dstRow["PostalCode"] = srcRow["PostalCode"];
                dstRow["Country"] = srcRow["Country"];
                dstRow["HomePhone"] = srcRow["HomePhone"];
                dstRow["WorkPhone"] = srcRow["WorkPhone"];
                dstRow["Email"] = srcRow["Email"];
                dstRow["ClientID"] = srcRow["ClientID"];
                dstRow["SINNumber"] = srcRow["SINNumber"];
                dstRow["Occupation"] = srcRow["Occupation"];
                dstRow["DriversLicenseNum"] = srcRow["DriversLicenseNum"];
                dstRow["ModifiedBy"] = srcRow["ModifiedBy"];
                dstRow["Custom1"] = srcRow["Custom1"];
                dstRow["Custom2"] = srcRow["Custom2"];
                dstRow["CompanyName"] = srcRow["CompanyName"];
                dstRow["PassportNumber"] = srcRow["PassportNumber"];
                dstRow["BusinessFaxNumber"] = srcRow["BusinessFaxNumber"];
                dstRow["FRSAcSysUserID"] = 0;
                dstRow["DataProviderType"] = 0;
                dstRow["WebAddress"] = srcRow["WebAddress"];
            }
            catch
            {
                throw;
            }
        }
        public Guid ImportOneSubject(Guid subjectGuid, System.Data.DataRow srcRow, SqlTransaction trans)
        {
            this.SetTransaction(adapter_subject, trans);
            dsIXData.SubjectProfile.Clear();
            if (subjectGuid == Guid.Empty) //no more subject before. Create a new one
            {
                subjectGuid = Guid.NewGuid();
                System.Data.DataRow newRow = dsIXData.SubjectProfile.NewRow();
                newRow["SubjectGUID"] = subjectGuid;
                tmpDAL.AddOneConversionTmp(subjectGuid, trans, "SubjectProfile", srcRow["SourceID"].ToString());
                this.UpdateRow(srcRow, ref newRow);
                dsIXData.SubjectProfile.Rows.Add(newRow);

            }
            else //receive existing subject profile to update
            {
                this.adapter_subject.SelectCommand.CommandText = this.masterquery_subject + " WHERE SubjectGUID='" + subjectGuid.ToString() + "'";
                this.adapter_subject.Fill(dsIXData, "SubjectProfile");
                System.Data.DataRow dstRow = dsIXData.SubjectProfile.Rows[0];
                this.UpdateRow(srcRow, ref dstRow);
            }
           
            adapter_subject.Update(dsIXData, "SubjectProfile");
            return (Guid)dsIXData.SubjectProfile.Rows[0]["SubjectGuid"];
        }

    }
}
