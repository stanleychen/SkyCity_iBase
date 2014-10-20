using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class GameAuditBE
    {
        #region Fields

        private Guid gameAuditGUID;
        private Guid propertyGUID;
        private string reviewMethod;
        private DateTime gameAuditStartDateTime;
        private DateTime gameAuditEndDateTime;
        private string vCRConsoleNumber;
        private string creatorUserID;
        private DateTime lastModifiedDate;
        private string password;
        private bool closed;
        private bool archived;
        private string areaAudited;
        private string section;
        private string exclusive;
        private string remarks;
        private string modifiedBy;
        private DateTime dateCreated;
        private string secondAuditor;
        private string number;
        private Guid requestedByGUID;
        private string status;
        private Guid departmentGuid;
        private string owner;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameAudit class.
        /// </summary>
        public GameAuditBE()
        {
        }

      
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the GameAuditGUID value.
        /// </summary>
        public virtual Guid GameAuditGUID
        {
            get { return gameAuditGUID; }
            set { gameAuditGUID = value; }
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
        /// Gets or sets the ReviewMethod value.
        /// </summary>
        public virtual string ReviewMethod
        {
            get { return reviewMethod; }
            set { reviewMethod = value; }
        }

        /// <summary>
        /// Gets or sets the GameAuditStartDateTime value.
        /// </summary>
        public virtual DateTime GameAuditStartDateTime
        {
            get { return gameAuditStartDateTime; }
            set { gameAuditStartDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the GameAuditEndDateTime value.
        /// </summary>
        public virtual DateTime GameAuditEndDateTime
        {
            get { return gameAuditEndDateTime; }
            set { gameAuditEndDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the VCRConsoleNumber value.
        /// </summary>
        public virtual string VCRConsoleNumber
        {
            get { return vCRConsoleNumber; }
            set { vCRConsoleNumber = value; }
        }

        /// <summary>
        /// Gets or sets the CreatorUserID value.
        /// </summary>
        public virtual string CreatorUserID
        {
            get { return creatorUserID; }
            set { creatorUserID = value; }
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
        /// Gets or sets the Password value.
        /// </summary>
        public virtual string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Gets or sets the Closed value.
        /// </summary>
        public virtual bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }

        /// <summary>
        /// Gets or sets the Archived value.
        /// </summary>
        public virtual bool Archived
        {
            get { return archived; }
            set { archived = value; }
        }

        /// <summary>
        /// Gets or sets the AreaAudited value.
        /// </summary>
        public virtual string AreaAudited
        {
            get { return areaAudited; }
            set { areaAudited = value; }
        }

        /// <summary>
        /// Gets or sets the Section value.
        /// </summary>
        public virtual string Section
        {
            get { return section; }
            set { section = value; }
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
        /// Gets or sets the Remarks value.
        /// </summary>
        public virtual string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
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
        /// Gets or sets the DateCreated value.
        /// </summary>
        public virtual DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        /// <summary>
        /// Gets or sets the SecondAuditor value.
        /// </summary>
        public virtual string SecondAuditor
        {
            get { return secondAuditor; }
            set { secondAuditor = value; }
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
        /// Gets or sets the RequestedByGUID value.
        /// </summary>
        public virtual Guid RequestedByGUID
        {
            get { return requestedByGUID; }
            set { requestedByGUID = value; }
        }

        /// <summary>
        /// Gets or sets the Status value.
        /// </summary>
        public virtual string Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual Guid DepartmentGuid
        {
            get
            {
                return departmentGuid;
            }
            set
            {
                departmentGuid = value;
            }
        }

        public virtual string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }
        #endregion
    }
}
