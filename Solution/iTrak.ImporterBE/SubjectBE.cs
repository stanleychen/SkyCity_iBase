using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class SubjectBE
    {
        #region Fields

        private Guid subjectGUID;
        private int rowID = 0;
        private string subjectSourceID = string.Empty;
        private string firstName;
        private string middleName;
        private string lastName;
        private string gender;
        private DateTime dateOfBirth;
        private int ageRangeLower;
        private int ageRangeUpper;
        private int height;
        private int weight;
        private string hairColour;
        private string eyeColour;
        private string race;
        private DateTime dateCreated;
        private DateTime dateModified;
        private string creatoruserID;
        private Guid propertyGUID;
        private string comment;
        private DateTime lastIncidentDate;
        private int fRSAcSysUserID;
        private string exclusive;
        private Guid bestAssetGUID;
        private string category;
        private string activities;
        private string specifics;
        private string groups;
        private string aliases;
        private string traits;
        private string address;
        private string city;
        private string state;
        private string postalCode;
        private string country;
        private string homePhone;
        private string workPhone;
        private string email;
        private string clientID;
        private string sINNumber;
        private string occupation;
        private string driversLicenseNum;
        private byte[] digitalSignature;
        private int dataProviderType;
        private int subjectId;
        private string modifiedBy;
        private string custom1;
        private string custom2;
        private string companyName;
        private string passportNumber;
        private string businessFaxNumber;
        private string webAddress;
        private string owner = string.Empty;
        private Guid departmentGuid;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SubjectProfile class.
        /// </summary>
        public SubjectBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SubjectProfile class.
        /// </summary>
        public SubjectBE(Guid subjectGUID, string firstName, string middleName, string lastName, 
            string gender, DateTime dateOfBirth, int ageRangeLower, int ageRangeUpper,
            int height, int weight, string hairColour, string eyeColour, string race,
            DateTime dateCreated, DateTime dateModified, string creatoruserID, 
            Guid propertyGUID, string comment, DateTime lastIncidentDate, int fRSAcSysUserID, 
            string exclusive, Guid bestAssetGUID, string category, string activities, string specifics,
            string groups, string aliases, string traits, string address, string city, string state, 
            string postalCode, string country, string homePhone, string workPhone, string email, 
            string clientID, string sINNumber, string occupation, string driversLicenseNum,
            byte[] digitalSignature, int dataProviderType, string modifiedBy, 
            string custom1, string custom2, string companyName, string passportNumber,
            string businessFaxNumber, string webAddress)
        {
            this.subjectGUID = subjectGUID;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.ageRangeLower = ageRangeLower;
            this.ageRangeUpper = ageRangeUpper;
            this.height = height;
            this.weight = weight;
            this.hairColour = hairColour;
            this.eyeColour = eyeColour;
            this.race = race;
            this.dateCreated = dateCreated;
            this.dateModified = dateModified;
            this.creatoruserID = creatoruserID;
            this.propertyGUID = propertyGUID;
            this.comment = comment;
            this.lastIncidentDate = lastIncidentDate;
            this.fRSAcSysUserID = fRSAcSysUserID;
            this.exclusive = exclusive;
            this.bestAssetGUID = bestAssetGUID;
            this.category = category;
            this.activities = activities;
            this.specifics = specifics;
            this.groups = groups;
            this.aliases = aliases;
            this.traits = traits;
            this.address = address;
            this.city = city;
            this.state = state;
            this.postalCode = postalCode;
            this.country = country;
            this.homePhone = homePhone;
            this.workPhone = workPhone;
            this.email = email;
            this.clientID = clientID;
            this.sINNumber = sINNumber;
            this.occupation = occupation;
            this.driversLicenseNum = driversLicenseNum;
            this.digitalSignature = digitalSignature;
            this.dataProviderType = dataProviderType;
            this.modifiedBy = modifiedBy;
            this.custom1 = custom1;
            this.custom2 = custom2;
            this.companyName = companyName;
            this.passportNumber = passportNumber;
            this.businessFaxNumber = businessFaxNumber;
            this.webAddress = webAddress;
        }

        /// <summary>
        /// Initializes a new instance of the SubjectProfile class.
        /// </summary>
        public SubjectBE(Guid subjectGUID, string firstName, string middleName, string lastName, string gender, DateTime dateOfBirth, int ageRangeLower, int ageRangeUpper, int height, int weight, string hairColour, string eyeColour, string race, DateTime dateCreated, DateTime dateModified, string creatoruserID, string password, Guid propertyGUID, string comment, DateTime lastIncidentDate, int fRSAcSysUserID, string exclusive, Guid bestAssetGUID, string category, string activities, string specifics, string groups, string aliases, string traits, string address, string city, string state, string postalCode, string country, string homePhone, string workPhone, string email, string clientID, string sINNumber, string occupation, string driversLicenseNum, DateTime timeStamp, byte[] digitalSignature, int dataProviderType, int subjectId, string modifiedBy, string custom1, string custom2, string companyName, string passportNumber, string businessFaxNumber, string webAddress)
        {
            this.subjectGUID = subjectGUID;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.ageRangeLower = ageRangeLower;
            this.ageRangeUpper = ageRangeUpper;
            this.height = height;
            this.weight = weight;
            this.hairColour = hairColour;
            this.eyeColour = eyeColour;
            this.race = race;
            this.dateCreated = dateCreated;
            this.dateModified = dateModified;
            this.creatoruserID = creatoruserID;
            this.propertyGUID = propertyGUID;
            this.comment = comment;
            this.lastIncidentDate = lastIncidentDate;
            this.fRSAcSysUserID = fRSAcSysUserID;
            this.exclusive = exclusive;
            this.bestAssetGUID = bestAssetGUID;
            this.category = category;
            this.activities = activities;
            this.specifics = specifics;
            this.groups = groups;
            this.aliases = aliases;
            this.traits = traits;
            this.address = address;
            this.city = city;
            this.state = state;
            this.postalCode = postalCode;
            this.country = country;
            this.homePhone = homePhone;
            this.workPhone = workPhone;
            this.email = email;
            this.clientID = clientID;
            this.sINNumber = sINNumber;
            this.occupation = occupation;
            this.driversLicenseNum = driversLicenseNum;
            this.digitalSignature = digitalSignature;
            this.dataProviderType = dataProviderType;
            this.subjectId = subjectId;
            this.modifiedBy = modifiedBy;
            this.custom1 = custom1;
            this.custom2 = custom2;
            this.companyName = companyName;
            this.passportNumber = passportNumber;
            this.businessFaxNumber = businessFaxNumber;
            this.webAddress = webAddress;
        }

        #endregion

        #region Properties
        public int RowID
        {
            get
            {
                return rowID;
            }
            set
            {
                rowID = value;
            }
        }
        /// <summary>
        /// Gets or sets the SubjectGUID value.
        /// </summary>
        public virtual Guid SubjectGUID
        {
            get { return subjectGUID; }
            set { subjectGUID = value; }
        }

        public virtual string SubjectSourceID
        {
            get
            {
                return subjectSourceID;
            }
            set
            {
                subjectSourceID = value;
            }
        }
        /// <summary>
        /// Gets or sets the FirstName value.
        /// </summary>
        public virtual string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        /// <summary>
        /// Gets or sets the MiddleName value.
        /// </summary>
        public virtual string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        /// <summary>
        /// Gets or sets the LastName value.
        /// </summary>
        public virtual string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        /// <summary>
        /// Gets or sets the Gender value.
        /// </summary>
        public virtual string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        /// <summary>
        /// Gets or sets the DateOfBirth value.
        /// </summary>
        public virtual DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        /// <summary>
        /// Gets or sets the AgeRangeLower value.
        /// </summary>
        public virtual int AgeRangeLower
        {
            get { return ageRangeLower; }
            set { ageRangeLower = value; }
        }

        /// <summary>
        /// Gets or sets the AgeRangeUpper value.
        /// </summary>
        public virtual int AgeRangeUpper
        {
            get { return ageRangeUpper; }
            set { ageRangeUpper = value; }
        }

        /// <summary>
        /// Gets or sets the Height value.
        /// </summary>
        public virtual int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Gets or sets the Weight value.
        /// </summary>
        public virtual int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        /// <summary>
        /// Gets or sets the HairColour value.
        /// </summary>
        public virtual string HairColour
        {
            get { return hairColour; }
            set { hairColour = value; }
        }

        /// <summary>
        /// Gets or sets the EyeColour value.
        /// </summary>
        public virtual string EyeColour
        {
            get { return eyeColour; }
            set { eyeColour = value; }
        }

        /// <summary>
        /// Gets or sets the Race value.
        /// </summary>
        public virtual string Race
        {
            get { return race; }
            set { race = value; }
        }

        /// <summary>
        /// Gets or sets the DateCreated value.
        /// </summary>
        public virtual DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        /// <summary>
        /// Gets or sets the DateModified value.
        /// </summary>
        public virtual DateTime DateModified
        {
            get { return dateModified; }
            set { dateModified = value; }
        }

        /// <summary>
        /// Gets or sets the CreatoruserID value.
        /// </summary>
        public virtual string CreatoruserID
        {
            get { return creatoruserID; }
            set { creatoruserID = value; }
        }

        /// Gets or sets the PropertyGUID value.
        /// </summary>
        public virtual Guid PropertyGUID
        {
            get { return propertyGUID; }
            set { propertyGUID = value; }
        }

        /// <summary>
        /// Gets or sets the Comment value.
        /// </summary>
        public virtual string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        /// <summary>
        /// Gets or sets the LastIncidentDate value.
        /// </summary>
        public virtual DateTime LastIncidentDate
        {
            get { return lastIncidentDate; }
            set { lastIncidentDate = value; }
        }

        /// <summary>
        /// Gets or sets the FRSAcSysUserID value.
        /// </summary>
        public virtual int FRSAcSysUserID
        {
            get { return fRSAcSysUserID; }
            set { fRSAcSysUserID = value; }
        }

        /// <summary>
        /// Gets or sets the Exclusive value.
        /// </summary>
        public virtual string Exclusive
        {
            get { return exclusive; }
            set { exclusive = value; }
        }

        /// <summary>
        /// Gets or sets the BestAssetGUID value.
        /// </summary>
        public virtual Guid BestAssetGUID
        {
            get { return bestAssetGUID; }
            set { bestAssetGUID = value; }
        }

        /// <summary>
        /// Gets or sets the Category value.
        /// </summary>
        public virtual string Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// Gets or sets the Activities value.
        /// </summary>
        public virtual string Activities
        {
            get { return activities; }
            set { activities = value; }
        }

        /// <summary>
        /// Gets or sets the Specifics value.
        /// </summary>
        public virtual string Specifics
        {
            get { return specifics; }
            set { specifics = value; }
        }

        /// <summary>
        /// Gets or sets the Groups value.
        /// </summary>
        public virtual string Groups
        {
            get { return groups; }
            set { groups = value; }
        }

        /// <summary>
        /// Gets or sets the Aliases value.
        /// </summary>
        public virtual string Aliases
        {
            get { return aliases; }
            set { aliases = value; }
        }

        /// <summary>
        /// Gets or sets the Traits value.
        /// </summary>
        public virtual string Traits
        {
            get { return traits; }
            set { traits = value; }
        }

        /// <summary>
        /// Gets or sets the Address value.
        /// </summary>
        public virtual string Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Gets or sets the City value.
        /// </summary>
        public virtual string City
        {
            get { return city; }
            set { city = value; }
        }

        /// <summary>
        /// Gets or sets the State value.
        /// </summary>
        public virtual string State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Gets or sets the PostalCode value.
        /// </summary>
        public virtual string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        /// <summary>
        /// Gets or sets the Country value.
        /// </summary>
        public virtual string Country
        {
            get { return country; }
            set { country = value; }
        }

        /// <summary>
        /// Gets or sets the HomePhone value.
        /// </summary>
        public virtual string HomePhone
        {
            get { return homePhone; }
            set { homePhone = value; }
        }

        /// <summary>
        /// Gets or sets the WorkPhone value.
        /// </summary>
        public virtual string WorkPhone
        {
            get { return workPhone; }
            set { workPhone = value; }
        }

        /// <summary>
        /// Gets or sets the Email value.
        /// </summary>
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// Gets or sets the ClientID value.
        /// </summary>
        public virtual string ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        /// <summary>
        /// Gets or sets the SINNumber value.
        /// </summary>
        public virtual string SINNumber
        {
            get { return sINNumber; }
            set { sINNumber = value; }
        }

        /// <summary>
        /// Gets or sets the Occupation value.
        /// </summary>
        public virtual string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        /// <summary>
        /// Gets or sets the DriversLicenseNum value.
        /// </summary>
        public virtual string DriversLicenseNum
        {
            get { return driversLicenseNum; }
            set { driversLicenseNum = value; }
        }
        /// <summary>
        /// Gets or sets the DigitalSignature value.
        /// </summary>
        public virtual byte[] DigitalSignature
        {
            get { return digitalSignature; }
            set { digitalSignature = value; }
        }

        /// <summary>
        /// Gets or sets the DataProviderType value.
        /// </summary>
        public virtual int DataProviderType
        {
            get { return dataProviderType; }
            set { dataProviderType = value; }
        }

        /// <summary>
        /// Gets or sets the SubjectId value.
        /// </summary>
        public virtual int SubjectId
        {
            get { return subjectId; }
            set { subjectId = value; }
        }

        /// <summary>
        /// Gets or sets the ModifiedBy value.
        /// </summary>
        public virtual string ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        /// <summary>
        /// Gets or sets the Custom1 value.
        /// </summary>
        public virtual string Custom1
        {
            get { return custom1; }
            set { custom1 = value; }
        }

        /// <summary>
        /// Gets or sets the Custom2 value.
        /// </summary>
        public virtual string Custom2
        {
            get { return custom2; }
            set { custom2 = value; }
        }

        /// <summary>
        /// Gets or sets the CompanyName value.
        /// </summary>
        public virtual string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        /// <summary>
        /// Gets or sets the PassportNumber value.
        /// </summary>
        public virtual string PassportNumber
        {
            get { return passportNumber; }
            set { passportNumber = value; }
        }

        /// <summary>
        /// Gets or sets the BusinessFaxNumber value.
        /// </summary>
        public virtual string BusinessFaxNumber
        {
            get { return businessFaxNumber; }
            set { businessFaxNumber = value; }
        }

        /// <summary>
        /// Gets or sets the WebAddress value.
        /// </summary>
        public virtual string WebAddress
        {
            get { return webAddress; }
            set { webAddress = value; }
        }

        public virtual string Owner
        {
            get { return owner; }
            set { this.owner = value; }
        }
        public virtual Guid DepartmentGuid
        {
            get { return departmentGuid; }
            set { departmentGuid = value; }
        }
        #endregion
    }
}
