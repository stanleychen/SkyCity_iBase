using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class EmployeeBE
    {
        #region Fields

        private int rowId;
        private Guid employeeGUID;
        private Guid propertyGUID;
        private string employeeSourceID;
        private string firstName;
        private string middleName;
        private string lastName;
        private string streetAddress;
        private string city;
        private string state;
        private string zipCode;
        private string phoneNumber;
        private string socialSecurityNumber;
        private DateTime dateOfBirth;
        private string emergencyNotify;
        private string logonName;
        private string employeeID;
        private int supervisorLevel;
        private DateTime dateHired;
        private DateTime dateFired;
        private DateTime dateOfSeniority;
        private string gamingCardNumber;
        private DateTime gamingCardExpiryDate;
        private bool mondayOff;
        private bool tuesdayOff;
        private bool wednesdayOff;
        private bool thursdayOff;
        private bool fridayOff;
        private bool saturdayOff;
        private bool sundayOff;
        private bool gamingRelated;
        private string country;
        private string department;
        private string jobPosition;
        private string shift;
        private string email;
        private string exclusive;
        private string password;
        private string otherSkills;
        private DateTime timeStamp;
        private DateTime dateCreated;
        private DateTime dateModified;
        private Guid supervisorGUID;
        private string division;
        private int height;
        private int weight;
        private DateTime shiftStart;
        private DateTime shiftEnd;
        private string cellPhoneNumber;
        private string gender;
        private string primaryLanguageSpoken;
        private string maritalStatus;
        private Guid bestAssetGUID;
        private string modifiedBy;
        private string businessPhoneNumber;
        private string businessFaxNumber;
        private string mailCode;
        private string comments;
        private bool lockForAPI;
        private string custom1;
        private string custom2;
        private bool isGlobal;
        private string driversLicenseNum;
        private string hairColour;
        private string eyeColour;
        private string passportNumber;
        private string webAddress;
        private Guid departmentGuid;
        private string createdby;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        public EmployeeBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        public EmployeeBE(Guid employeeGUID, Guid propertyGUID, string firstName, string middleName, string lastName, string streetAddress, string city, string state, string zipCode, string phoneNumber, string socialSecurityNumber, DateTime dateOfBirth, string emergencyNotify, string logonName, string employeeID, int supervisorLevel, DateTime dateHired, DateTime dateFired, DateTime dateOfSeniority, string gamingCardNumber, DateTime gamingCardExpiryDate, bool mondayOff, bool tuesdayOff, bool wednesdayOff, bool thursdayOff, bool fridayOff, bool saturdayOff, bool sundayOff, bool gamingRelated, string country, string department, string jobPosition, string shift, string email, string exclusive, string password, string otherSkills, DateTime timeStamp, DateTime dateCreated, DateTime dateModified, Guid supervisorGUID, string division, int height, int weight, DateTime shiftStart, DateTime shiftEnd, string cellPhoneNumber, string gender, string primaryLanguageSpoken, string maritalStatus, Guid bestAssetGUID, string modifiedBy, string businessPhoneNumber, string businessFaxNumber, string mailCode, string comments, bool lockForAPI, string custom1, string custom2, bool isGlobal, string driversLicenseNum, string hairColour, string eyeColour, string passportNumber, string webAddress)
        {
            this.employeeGUID = employeeGUID;
            this.propertyGUID = propertyGUID;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.streetAddress = streetAddress;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
            this.phoneNumber = phoneNumber;
            this.socialSecurityNumber = socialSecurityNumber;
            this.dateOfBirth = dateOfBirth;
            this.emergencyNotify = emergencyNotify;
            this.logonName = logonName;
            this.employeeID = employeeID;
            this.supervisorLevel = supervisorLevel;
            this.dateHired = dateHired;
            this.dateFired = dateFired;
            this.dateOfSeniority = dateOfSeniority;
            this.gamingCardNumber = gamingCardNumber;
            this.gamingCardExpiryDate = gamingCardExpiryDate;
            this.mondayOff = mondayOff;
            this.tuesdayOff = tuesdayOff;
            this.wednesdayOff = wednesdayOff;
            this.thursdayOff = thursdayOff;
            this.fridayOff = fridayOff;
            this.saturdayOff = saturdayOff;
            this.sundayOff = sundayOff;
            this.gamingRelated = gamingRelated;
            this.country = country;
            this.department = department;
            this.jobPosition = jobPosition;
            this.shift = shift;
            this.email = email;
            this.exclusive = exclusive;
            this.password = password;
            this.otherSkills = otherSkills;
            this.timeStamp = timeStamp;
            this.dateCreated = dateCreated;
            this.dateModified = dateModified;
            this.supervisorGUID = supervisorGUID;
            this.division = division;
            this.height = height;
            this.weight = weight;
            this.shiftStart = shiftStart;
            this.shiftEnd = shiftEnd;
            this.cellPhoneNumber = cellPhoneNumber;
            this.gender = gender;
            this.primaryLanguageSpoken = primaryLanguageSpoken;
            this.maritalStatus = maritalStatus;
            this.bestAssetGUID = bestAssetGUID;
            this.modifiedBy = modifiedBy;
            this.businessPhoneNumber = businessPhoneNumber;
            this.businessFaxNumber = businessFaxNumber;
            this.mailCode = mailCode;
            this.comments = comments;
            this.lockForAPI = lockForAPI;
            this.custom1 = custom1;
            this.custom2 = custom2;
            this.isGlobal = isGlobal;
            this.driversLicenseNum = driversLicenseNum;
            this.hairColour = hairColour;
            this.eyeColour = eyeColour;
            this.passportNumber = passportNumber;
            this.webAddress = webAddress;
        }

        #endregion

        #region Properties
        public virtual int RowID
        {
            get
            {
                return rowId;
            }
            set
            {
                rowId = value;
            }
        }
        /// <summary>
        /// Gets or sets the EmployeeGUID value.
        /// </summary>
        public virtual Guid EmployeeGUID
        {
            get { return employeeGUID; }
            set { employeeGUID = value; }
        }

        /// <summary>
        /// Gets or sets the PropertyGUID value.
        /// </summary>
        public virtual Guid PropertyGUID
        {
            get { return propertyGUID; }
            set { propertyGUID = value; }
        }

        public virtual string EmployeeSourceID
        {
            get
            {
                return employeeSourceID;
            }
            set
            {
                this.employeeSourceID = value;
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
        /// Gets or sets the StreetAddress value.
        /// </summary>
        public virtual string StreetAddress
        {
            get { return streetAddress; }
            set { streetAddress = value; }
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
        /// Gets or sets the ZipCode value.
        /// </summary>
        public virtual string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        /// <summary>
        /// Gets or sets the PhoneNumber value.
        /// </summary>
        public virtual string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        /// <summary>
        /// Gets or sets the SocialSecurityNumber value.
        /// </summary>
        public virtual string SocialSecurityNumber
        {
            get { return socialSecurityNumber; }
            set { socialSecurityNumber = value; }
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
        /// Gets or sets the EmergencyNotify value.
        /// </summary>
        public virtual string EmergencyNotify
        {
            get { return emergencyNotify; }
            set { emergencyNotify = value; }
        }

        /// <summary>
        /// Gets or sets the LogonName value.
        /// </summary>
        public virtual string LogonName
        {
            get { return logonName; }
            set { logonName = value; }
        }

        /// <summary>
        /// Gets or sets the EmployeeID value.
        /// </summary>
        public virtual string EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        /// <summary>
        /// Gets or sets the SupervisorLevel value.
        /// </summary>
        public virtual int SupervisorLevel
        {
            get { return supervisorLevel; }
            set { supervisorLevel = value; }
        }

        /// <summary>
        /// Gets or sets the DateHired value.
        /// </summary>
        public virtual DateTime DateHired
        {
            get { return dateHired; }
            set { dateHired = value; }
        }

        /// <summary>
        /// Gets or sets the DateFired value.
        /// </summary>
        public virtual DateTime DateFired
        {
            get { return dateFired; }
            set { dateFired = value; }
        }

        /// <summary>
        /// Gets or sets the DateOfSeniority value.
        /// </summary>
        public virtual DateTime DateOfSeniority
        {
            get { return dateOfSeniority; }
            set { dateOfSeniority = value; }
        }

        /// <summary>
        /// Gets or sets the GamingCardNumber value.
        /// </summary>
        public virtual string GamingCardNumber
        {
            get { return gamingCardNumber; }
            set { gamingCardNumber = value; }
        }

        /// <summary>
        /// Gets or sets the GamingCardExpiryDate value.
        /// </summary>
        public virtual DateTime GamingCardExpiryDate
        {
            get { return gamingCardExpiryDate; }
            set { gamingCardExpiryDate = value; }
        }

        /// <summary>
        /// Gets or sets the MondayOff value.
        /// </summary>
        public virtual bool MondayOff
        {
            get { return mondayOff; }
            set { mondayOff = value; }
        }

        /// <summary>
        /// Gets or sets the TuesdayOff value.
        /// </summary>
        public virtual bool TuesdayOff
        {
            get { return tuesdayOff; }
            set { tuesdayOff = value; }
        }

        /// <summary>
        /// Gets or sets the WednesdayOff value.
        /// </summary>
        public virtual bool WednesdayOff
        {
            get { return wednesdayOff; }
            set { wednesdayOff = value; }
        }

        /// <summary>
        /// Gets or sets the ThursdayOff value.
        /// </summary>
        public virtual bool ThursdayOff
        {
            get { return thursdayOff; }
            set { thursdayOff = value; }
        }

        /// <summary>
        /// Gets or sets the FridayOff value.
        /// </summary>
        public virtual bool FridayOff
        {
            get { return fridayOff; }
            set { fridayOff = value; }
        }

        /// <summary>
        /// Gets or sets the SaturdayOff value.
        /// </summary>
        public virtual bool SaturdayOff
        {
            get { return saturdayOff; }
            set { saturdayOff = value; }
        }

        /// <summary>
        /// Gets or sets the SundayOff value.
        /// </summary>
        public virtual bool SundayOff
        {
            get { return sundayOff; }
            set { sundayOff = value; }
        }

        /// <summary>
        /// Gets or sets the GamingRelated value.
        /// </summary>
        public virtual bool GamingRelated
        {
            get { return gamingRelated; }
            set { gamingRelated = value; }
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
        /// Gets or sets the Department value.
        /// </summary>
        public virtual string Department
        {
            get { return department; }
            set { department = value; }
        }

        /// <summary>
        /// Gets or sets the JobPosition value.
        /// </summary>
        public virtual string JobPosition
        {
            get { return jobPosition; }
            set { jobPosition = value; }
        }

        /// <summary>
        /// Gets or sets the Shift value.
        /// </summary>
        public virtual string Shift
        {
            get { return shift; }
            set { shift = value; }
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
        /// Gets or sets the Exclusive value.
        /// </summary>
        public virtual string Exclusive
        {
            get { return exclusive; }
            set { exclusive = value; }
        }

        /// <summary>
        /// Gets or sets the Password value.
        /// </summary>
        public virtual string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Gets or sets the OtherSkills value.
        /// </summary>
        public virtual string OtherSkills
        {
            get { return otherSkills; }
            set { otherSkills = value; }
        }

        /// <summary>
        /// Gets or sets the TimeStamp value.
        /// </summary>
        public virtual DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
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
        /// Gets or sets the SupervisorGUID value.
        /// </summary>
        public virtual Guid SupervisorGUID
        {
            get { return supervisorGUID; }
            set { supervisorGUID = value; }
        }

        /// <summary>
        /// Gets or sets the Division value.
        /// </summary>
        public virtual string Division
        {
            get { return division; }
            set { division = value; }
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
        /// Gets or sets the ShiftStart value.
        /// </summary>
        public virtual DateTime ShiftStart
        {
            get { return shiftStart; }
            set { shiftStart = value; }
        }

        /// <summary>
        /// Gets or sets the ShiftEnd value.
        /// </summary>
        public virtual DateTime ShiftEnd
        {
            get { return shiftEnd; }
            set { shiftEnd = value; }
        }

        /// <summary>
        /// Gets or sets the CellPhoneNumber value.
        /// </summary>
        public virtual string CellPhoneNumber
        {
            get { return cellPhoneNumber; }
            set { cellPhoneNumber = value; }
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
        /// Gets or sets the PrimaryLanguageSpoken value.
        /// </summary>
        public virtual string PrimaryLanguageSpoken
        {
            get { return primaryLanguageSpoken; }
            set { primaryLanguageSpoken = value; }
        }

        /// <summary>
        /// Gets or sets the MaritalStatus value.
        /// </summary>
        public virtual string MaritalStatus
        {
            get { return maritalStatus; }
            set { maritalStatus = value; }
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
        /// Gets or sets the ModifiedBy value.
        /// </summary>
        public virtual string ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        /// <summary>
        /// Gets or sets the BusinessPhoneNumber value.
        /// </summary>
        public virtual string BusinessPhoneNumber
        {
            get { return businessPhoneNumber; }
            set { businessPhoneNumber = value; }
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
        /// Gets or sets the MailCode value.
        /// </summary>
        public virtual string MailCode
        {
            get { return mailCode; }
            set { mailCode = value; }
        }

        /// <summary>
        /// Gets or sets the Comments value.
        /// </summary>
        public virtual string Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        /// <summary>
        /// Gets or sets the LockForAPI value.
        /// </summary>
        public virtual bool LockForAPI
        {
            get { return lockForAPI; }
            set { lockForAPI = value; }
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
        /// Gets or sets the IsGlobal value.
        /// </summary>
        public virtual bool IsGlobal
        {
            get { return isGlobal; }
            set { isGlobal = value; }
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
        /// Gets or sets the PassportNumber value.
        /// </summary>
        public virtual string PassportNumber
        {
            get { return passportNumber; }
            set { passportNumber = value; }
        }

        /// <summary>
        /// Gets or sets the WebAddress value.
        /// </summary>
        public virtual string WebAddress
        {
            get { return webAddress; }
            set { webAddress = value; }
        }

        public virtual Guid DepartmentGuid
        {
            get { return departmentGuid; }
            set { departmentGuid = value; }
        }

        public virtual string CreatedBy
        {
            get
            {
                return createdby;
            }
            set
            {
                createdby = value;
            }
        }
        #endregion
    }
}

