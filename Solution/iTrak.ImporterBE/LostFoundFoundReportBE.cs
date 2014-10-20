using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class LostFoundFoundReportBE
    {
        #region Fields

        private Guid foundItemGUID;
        private Guid propertyGUID;
        private string foundUID;
        private string itemCategory;
        private decimal itemValue;
        private string colour;
        private string serialNumber;
        private string material;
        private string manufacturer;
        private int ageYrs;
        private int ageMonths;
        private string contents;
        private string description;
        private string keyWords;
        private DateTime foundDateTime;
        private string foundStatus;
        private string specificLocation;
        private Guid foundByContactUID;
        private Guid reportByContactUID;
        private Guid receivedByEmployeeUID;
        private string storeLocation;
        private string additionalInfo;
        private DateTime holdUntil;
        private Guid bestImageGUID;
        private string _operator;
        private string barcode;
        private DateTime dateCreated;
        private string department;
        private string location;
        private string sublocation;
        private string modifiedBy;
        private DateTime dateModified;
        private bool _isDisposed = false;
        private bool _isReturned = false;
        private string _uString = string.Empty;
        private bool _isNew = false;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LostFoundFoundReport class.
        /// </summary>
        public LostFoundFoundReportBE()
        {
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the FoundItemGUID value.
        /// </summary>
        public virtual Guid FoundItemGUID
        {
            get { return foundItemGUID; }
            set { foundItemGUID = value; }
        }

        /// <summary>
        /// Gets or sets the PropertyGUID value.
        /// </summary>
        public virtual Guid PropertyGUID
        {
            get { return propertyGUID; }
            set { propertyGUID = value; }
        }

        /// <summary>
        /// Gets or sets the FoundUID value.
        /// </summary>
        public virtual string FoundUID
        {
            get { return foundUID; }
            set { foundUID = value; }
        }

        /// <summary>
        /// Gets or sets the ItemCategory value.
        /// </summary>
        public virtual string ItemCategory
        {
            get { return itemCategory; }
            set { itemCategory = value; }
        }

        /// <summary>
        /// Gets or sets the ItemValue value.
        /// </summary>
        public virtual decimal ItemValue
        {
            get { return itemValue; }
            set { itemValue = value; }
        }

        /// <summary>
        /// Gets or sets the Colour value.
        /// </summary>
        public virtual string Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        /// <summary>
        /// Gets or sets the SerialNumber value.
        /// </summary>
        public virtual string SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; }
        }

        /// <summary>
        /// Gets or sets the Material value.
        /// </summary>
        public virtual string Material
        {
            get { return material; }
            set { material = value; }
        }

        /// <summary>
        /// Gets or sets the Manufacturer value.
        /// </summary>
        public virtual string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        /// <summary>
        /// Gets or sets the AgeYrs value.
        /// </summary>
        public virtual int AgeYrs
        {
            get { return ageYrs; }
            set { ageYrs = value; }
        }

        /// <summary>
        /// Gets or sets the AgeMonths value.
        /// </summary>
        public virtual int AgeMonths
        {
            get { return ageMonths; }
            set { ageMonths = value; }
        }

        /// <summary>
        /// Gets or sets the Contents value.
        /// </summary>
        public virtual string Contents
        {
            get { return contents; }
            set { contents = value; }
        }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the KeyWords value.
        /// </summary>
        public virtual string KeyWords
        {
            get { return keyWords; }
            set { keyWords = value; }
        }

        /// <summary>
        /// Gets or sets the FoundDateTime value.
        /// </summary>
        public virtual DateTime FoundDateTime
        {
            get { return foundDateTime; }
            set { foundDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the FoundStatus value.
        /// </summary>
        public virtual string FoundStatus
        {
            get { return foundStatus; }
            set { foundStatus = value; }
        }

        /// <summary>
        /// Gets or sets the SpecificLocation value.
        /// </summary>
        public virtual string SpecificLocation
        {
            get { return specificLocation; }
            set { specificLocation = value; }
        }

        /// <summary>
        /// Gets or sets the FoundByContactUID value.
        /// </summary>
        public virtual Guid FoundByContactUID
        {
            get { return foundByContactUID; }
            set { foundByContactUID = value; }
        }

        /// <summary>
        /// Gets or sets the ReportByContactUID value.
        /// </summary>
        public virtual Guid ReportByContactUID
        {
            get { return reportByContactUID; }
            set { reportByContactUID = value; }
        }

        /// <summary>
        /// Gets or sets the ReceivedByEmployeeUID value.
        /// </summary>
        public virtual Guid ReceivedByEmployeeUID
        {
            get { return receivedByEmployeeUID; }
            set { receivedByEmployeeUID = value; }
        }

        /// <summary>
        /// Gets or sets the StoreLocation value.
        /// </summary>
        public virtual string StoreLocation
        {
            get { return storeLocation; }
            set { storeLocation = value; }
        }

        /// <summary>
        /// Gets or sets the AdditionalInfo value.
        /// </summary>
        public virtual string AdditionalInfo
        {
            get { return additionalInfo; }
            set { additionalInfo = value; }
        }

        /// <summary>
        /// Gets or sets the HoldUntil value.
        /// </summary>
        public virtual DateTime HoldUntil
        {
            get { return holdUntil; }
            set { holdUntil = value; }
        }

        /// <summary>
        /// Gets or sets the BestImageGUID value.
        /// </summary>
        public virtual Guid BestImageGUID
        {
            get { return bestImageGUID; }
            set { bestImageGUID = value; }
        }

        /// <summary>
        /// Gets or sets the Operator value.
        /// </summary>
        public virtual string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        /// <summary>
        /// Gets or sets the Barcode value.
        /// </summary>
        public virtual string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
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
        /// Gets or sets the Department value.
        /// </summary>
        public virtual string Department
        {
            get { return department; }
            set { department = value; }
        }

        /// <summary>
        /// Gets or sets the Location value.
        /// </summary>
        public virtual string Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Gets or sets the Sublocation value.
        /// </summary>
        public virtual string Sublocation
        {
            get { return sublocation; }
            set { sublocation = value; }
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
        /// Gets or sets the DateModified value.
        /// </summary>
        public virtual DateTime DateModified
        {
            get { return dateModified; }
            set { dateModified = value; }
        }
        public bool IsDisposed
        {
            get
            {
                return _isDisposed;
            }
            set
            {
                _isDisposed = value;
            }
        }
        public bool IsReturned
        {
            get
            {
                return _isReturned;
            }
            set
            {
                _isReturned = value;
            }
        }
        public string uString
        {
            get
            {
                return _uString;
            }
            set
            {
                _uString = value;
            }
        }
        public bool IsNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                _isNew = value;
            }
        }
        #endregion
    }
}
