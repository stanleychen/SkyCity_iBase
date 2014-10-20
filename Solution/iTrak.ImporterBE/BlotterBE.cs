using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class BlotterBE
    {
        #region Fields

        private Guid blotterGUID;
        private string number;
        private DateTime occured;
        private string blotterAction;
        private string subject;
        private Guid property;
        private DateTime created;
        private bool archive;
        private string primaryOperator;
        private string secondaryOperator;
        private bool highPriority;
        private string status;
        private string sublocation;
        private string location;
        private string exclusive;
        private string synopsis;
        private DateTime lastModifiedDate;
        private string reference;
        private string modifiedBy;
        private DateTime archiveDate;
        private DateTime endTime;
        private DateTime closedTime;
        private bool isGlobal;
        private int sourceModuleID;
        private string sourceID;
        private Guid sourceGUID;
        private bool lockedBySource;
        private string owner = string.Empty;
        private Guid departmentGuid;
        private string ustring = string.Empty;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Blotter class.
        /// </summary>
        public BlotterBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Blotter class.
        /// </summary>
        public BlotterBE(Guid blotterGUID, string number, DateTime occured, string blotterAction, 
            string subject, Guid property, DateTime created, bool archive,
            string primaryOperator, string secondaryOperator, bool highPriority,
            string status, string sublocation, string location, string exclusive,
            string synopsis, DateTime lastModifiedDate, string reference, 
            string modifiedBy, DateTime archiveDate, DateTime endTime, DateTime closedTime, bool isGlobal, int sourceModuleID, string sourceID, Guid sourceGUID, bool lockedBySource)
        {
            this.blotterGUID = blotterGUID;
            this.number = number;
            this.occured = occured;
            this.blotterAction = blotterAction;
            this.subject = subject;
            this.property = property;
            this.created = created;
            this.archive = archive;
            this.primaryOperator = primaryOperator;
            this.secondaryOperator = secondaryOperator;
            this.highPriority = highPriority;
            this.status = status;
            this.sublocation = sublocation;
            this.location = location;
            this.exclusive = exclusive;
            this.synopsis = synopsis;
            this.lastModifiedDate = lastModifiedDate;
            this.reference = reference;
            this.modifiedBy = modifiedBy;
            this.archiveDate = archiveDate;
            this.endTime = endTime;
            this.closedTime = closedTime;
            this.isGlobal = isGlobal;
            this.sourceModuleID = sourceModuleID;
            this.sourceID = sourceID;
            this.sourceGUID = sourceGUID;
            this.lockedBySource = lockedBySource;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the BlotterGUID value.
        /// </summary>
        public virtual Guid BlotterGUID
        {
            get { return blotterGUID; }
            set { blotterGUID = value; }
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
        /// Gets or sets the Occured value.
        /// </summary>
        public virtual DateTime Occured
        {
            get { return occured; }
            set { occured = value; }
        }

        /// <summary>
        /// Gets or sets the BlotterAction value.
        /// </summary>
        public virtual string BlotterAction
        {
            get { return blotterAction; }
            set { blotterAction = value; }
        }

        /// <summary>
        /// Gets or sets the Subject value.
        /// </summary>
        public virtual string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        /// <summary>
        /// Gets or sets the Property value.
        /// </summary>
        public virtual Guid Property
        {
            get { return property; }
            set { property = value; }
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
        /// Gets or sets the Archive value.
        /// </summary>
        public virtual bool Archive
        {
            get { return archive; }
            set { archive = value; }
        }

        /// <summary>
        /// Gets or sets the PrimaryOperator value.
        /// </summary>
        public virtual string PrimaryOperator
        {
            get { return primaryOperator; }
            set { primaryOperator = value; }
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
        /// Gets or sets the HighPriority value.
        /// </summary>
        public virtual bool HighPriority
        {
            get { return highPriority; }
            set { highPriority = value; }
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
        /// Gets or sets the Sublocation value.
        /// </summary>
        public virtual string Sublocation
        {
            get { return sublocation; }
            set { sublocation = value; }
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
        /// Gets or sets the Exclusive value.
        /// </summary>
        public virtual string Exclusive
        {
            get { return exclusive; }
            set { exclusive = value; }
        }

        /// <summary>
        /// Gets or sets the Synopsis value.
        /// </summary>
        public virtual string Synopsis
        {
            get { return synopsis; }
            set { synopsis = value; }
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
        /// Gets or sets the Reference value.
        /// </summary>
        public virtual string Reference
        {
            get { return reference; }
            set { reference = value; }
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
        /// Gets or sets the EndTime value.
        /// </summary>
        public virtual DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// Gets or sets the ClosedTime value.
        /// </summary>
        public virtual DateTime ClosedTime
        {
            get { return closedTime; }
            set { closedTime = value; }
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
        /// Gets or sets the SourceModuleID value.
        /// </summary>
        public virtual int SourceModuleID
        {
            get { return sourceModuleID; }
            set { sourceModuleID = value; }
        }

        /// <summary>
        /// Gets or sets the SourceID value.
        /// </summary>
        public virtual string SourceID
        {
            get { return sourceID; }
            set { sourceID = value; }
        }

        /// <summary>
        /// Gets or sets the SourceGUID value.
        /// </summary>
        public virtual Guid SourceGUID
        {
            get { return sourceGUID; }
            set { sourceGUID = value; }
        }

        /// <summary>
        /// Gets or sets the LockedBySource value.
        /// </summary>
        public virtual bool LockedBySource
        {
            get { return lockedBySource; }
            set { lockedBySource = value; }
        }

        public virtual string Owner
        {
            get { return owner; }
            set { this.owner = value; }
        }
        public virtual Guid DepartmentGuid
        {
            get { return this.departmentGuid; }
            set { this.departmentGuid = value; }
        }

        public virtual string uString
        {
            get
            {
                return ustring;
            }
            set
            {
                ustring = value;
            }
        }
        #endregion
    }
}
