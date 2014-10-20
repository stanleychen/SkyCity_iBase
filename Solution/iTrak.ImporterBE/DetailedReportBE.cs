using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class DetailedReportBE
    {
        #region Fields

        private Guid detailedReportGUID;
        private string number;
        private Guid blotterGUID;
        private DateTime occured;
        private string status;
        private bool ambulanceOffered;
        private bool ambulanceDeclined;
        private bool firstAidOffered;
        private bool firstAidDeclined;
        private bool taxiFareOffered;
        private bool taxiFareDeclined;
        private string closedBy;
        private DateTime closed;
        private bool archive;
        private DateTime created;
        private string createdBy;
        private string type;
        private string specific;
        private string category;
        private string exclusive;
        private string secondaryOperator;
        private string details;
        private string closingRemarks;
        private DateTime lastModifiedDate;
        private string executiveBrief;
        private string modifiedBy;
        private DateTime archiveDate;
        private bool isGlobal;
        private string owner = string.Empty;
        private Guid departmentGuid;
        private List<MultiNoteBE> _multiNoteList;
        private string _specifics = string.Empty;
        private string text1Caption = string.Empty;
        private string text1 = string.Empty;
        private string text2Caption = string.Empty;
        private string text2 = string.Empty;
        private string text3Caption = string.Empty;
        private string text3 = string.Empty;
        private string text4Caption = string.Empty;
        private string text4 = string.Empty;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DetailedReport class.
        /// </summary>
        public DetailedReportBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DetailedReport class.
        /// </summary>
        public DetailedReportBE(Guid detailedReportGUID, string number, Guid blotterGUID, 
            DateTime occured, string status, bool ambulanceOffered, bool ambulanceDeclined,
            bool firstAidOffered, bool firstAidDeclined, bool taxiFareOffered, 
            bool taxiFareDeclined, string closedBy, DateTime closed, 
            bool archive, DateTime created, string createdBy, string type, string specific, 
            string category, string exclusive, string secondaryOperator, string details, 
            string closingRemarks, DateTime lastModifiedDate, 
            string executiveBrief, string modifiedBy, DateTime archiveDate, bool isGlobal)
        {
            this.detailedReportGUID = detailedReportGUID;
            this.number = number;
            this.blotterGUID = blotterGUID;
            this.occured = occured;
            this.status = status;
            this.ambulanceOffered = ambulanceOffered;
            this.ambulanceDeclined = ambulanceDeclined;
            this.firstAidOffered = firstAidOffered;
            this.firstAidDeclined = firstAidDeclined;
            this.taxiFareOffered = taxiFareOffered;
            this.taxiFareDeclined = taxiFareDeclined;
            this.closedBy = closedBy;
            this.closed = closed;
            this.archive = archive;
            this.created = created;
            this.createdBy = createdBy;
            this.type = type;
            this.specific = specific;
            this.category = category;
            this.exclusive = exclusive;
            this.secondaryOperator = secondaryOperator;
            this.details = details;
            this.closingRemarks = closingRemarks;
            this.lastModifiedDate = lastModifiedDate;
            this.executiveBrief = executiveBrief;
            this.modifiedBy = modifiedBy;
            this.archiveDate = archiveDate;
            this.isGlobal = isGlobal;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the DetailedReportGUID value.
        /// </summary>
        public virtual Guid DetailedReportGUID
        {
            get { return detailedReportGUID; }
            set { detailedReportGUID = value; }
        }

        /// <summary>
        /// Gets or sets the Number value.
        /// </summary>
        public virtual string Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <summary>
        /// Gets or sets the BlotterGUID value.
        /// </summary>
        public virtual Guid BlotterGUID
        {
            get { return blotterGUID; }
            set { blotterGUID = value; }
        }

        /// <summary>
        /// Gets or sets the Occured value.
        /// </summary>
        public virtual DateTime Occured
        {
            get { return occured; }
            set { occured = value; }
        }

        /// <summary>
        /// Gets or sets the Status value.
        /// </summary>
        public virtual string Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets or sets the AmbulanceOffered value.
        /// </summary>
        public virtual bool AmbulanceOffered
        {
            get { return ambulanceOffered; }
            set { ambulanceOffered = value; }
        }

        /// <summary>
        /// Gets or sets the AmbulanceDeclined value.
        /// </summary>
        public virtual bool AmbulanceDeclined
        {
            get { return ambulanceDeclined; }
            set { ambulanceDeclined = value; }
        }

        /// <summary>
        /// Gets or sets the FirstAidOffered value.
        /// </summary>
        public virtual bool FirstAidOffered
        {
            get { return firstAidOffered; }
            set { firstAidOffered = value; }
        }

        /// <summary>
        /// Gets or sets the FirstAidDeclined value.
        /// </summary>
        public virtual bool FirstAidDeclined
        {
            get { return firstAidDeclined; }
            set { firstAidDeclined = value; }
        }

        /// <summary>
        /// Gets or sets the TaxiFareOffered value.
        /// </summary>
        public virtual bool TaxiFareOffered
        {
            get { return taxiFareOffered; }
            set { taxiFareOffered = value; }
        }

        /// <summary>
        /// Gets or sets the TaxiFareDeclined value.
        /// </summary>
        public virtual bool TaxiFareDeclined
        {
            get { return taxiFareDeclined; }
            set { taxiFareDeclined = value; }
        }

        /// <summary>
        /// Gets or sets the ClosedBy value.
        /// </summary>
        public virtual string ClosedBy
        {
            get { return closedBy; }
            set { closedBy = value; }
        }

        /// <summary>
        /// Gets or sets the Closed value.
        /// </summary>
        public virtual DateTime Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        /// <summary>
        /// Gets or sets the Archive value.
        /// </summary>
        public virtual bool Archive
        {
            get { return archive; }
            set { archive = value; }
        }

        /// <summary>
        /// Gets or sets the Created value.
        /// </summary>
        public virtual DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        /// <summary>
        /// Gets or sets the CreatedBy value.
        /// </summary>
        public virtual string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        /// <summary>
        /// Gets or sets the Type value.
        /// </summary>
        public virtual string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Gets or sets the Specific value.
        /// </summary>
        public virtual string Specific
        {
            get { return specific; }
            set { specific = value; }
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
        /// Gets or sets the Exclusive value.
        /// </summary>
        public virtual string Exclusive
        {
            get { return exclusive; }
            set { exclusive = value; }
        }

        /// <summary>
        /// Gets or sets the SecondaryOperator value.
        /// </summary>
        public virtual string SecondaryOperator
        {
            get { return secondaryOperator; }
            set { secondaryOperator = value; }
        }

        /// <summary>
        /// Gets or sets the Details value.
        /// </summary>
        public virtual string Details
        {
            get { return details; }
            set { details = value; }
        }

        /// <summary>
        /// Gets or sets the ClosingRemarks value.
        /// </summary>
        public virtual string ClosingRemarks
        {
            get { return closingRemarks; }
            set { closingRemarks = value; }
        }

        /// <summary>
        /// Gets or sets the LastModifiedDate value.
        /// </summary>
        public virtual DateTime LastModifiedDate
        {
            get { return lastModifiedDate; }
            set { lastModifiedDate = value; }
        }

        /// <summary>
        /// Gets or sets the ExecutiveBrief value.
        /// </summary>
        public virtual string ExecutiveBrief
        {
            get { return executiveBrief; }
            set { executiveBrief = value; }
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
        /// Gets or sets the ArchiveDate value.
        /// </summary>
        public virtual DateTime ArchiveDate
        {
            get { return archiveDate; }
            set { archiveDate = value; }
        }

        /// <summary>
        /// Gets or sets the IsGlobal value.
        /// </summary>
        public virtual bool IsGlobal
        {
            get { return isGlobal; }
            set { isGlobal = value; }
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

        public virtual List<MultiNoteBE> MultiNoteList
        {
            get
            {
                return _multiNoteList;
            }
            set
            {
                this._multiNoteList = value;
            }
        }
        public virtual string Specifics
        {
            get { return _specifics; }
            set { _specifics = value; }
        }

        public virtual string Text1Caption
        {
            get
            {
                return text1Caption;
            }
            set
            {
                text1Caption = value;
            }
        }

        public virtual string Text1
        {
            get
            {
                return text1;
            }
            set
            {
                text1 = value;
            }
        }

        public virtual string Text2Caption
        {
            get
            {
                return text2Caption;
            }
            set
            {
                text2Caption = value;
            }
        }

        public virtual string Text2
        {
            get
            {
                return text2;
            }
            set
            {
                text2 = value;
            }
        }

        public virtual string Text3Caption
        {
            get
            {
                return text3Caption;
            }
            set
            {
                text3Caption = value;
            }
        }

        public virtual string Text3
        {
            get
            {
                return text3;
            }
            set
            {
                text3 = value;
            }
        }

        public virtual string Text4Caption
        {
            get
            {
                return text4Caption;
            }
            set
            {
                text4Caption = value;
            }
        }

        public virtual string Text4
        {
            get
            {
                return text4;
            }
            set
            {
                text4 = value;
            }
        }
        #endregion
    }
}
