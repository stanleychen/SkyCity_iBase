using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class MiniAuditBE
    {
        #region Fields

        private Guid miniAuditGUID;
        private Guid propertyGUID;
        private DateTime miniAuditStartDateTime;
        private DateTime miniAuditEndDateTime;
        private string reviewMethod;
        private string vCRConsoleNumber;
        private string createdBy;
        private DateTime dateCreated;
        private bool closed;
        private bool archived;
        private string secondAuditor;
        private string area;
        private string section;
        private string exclusive;
        private string remarks;
        private string modifiedBy;
        private DateTime lastModifiedDate;
        private string number;
        private Guid requestedByGUID;
        private string status;
        private Guid subjectGUID;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MiniAudit class.
        /// </summary>
        public MiniAuditBE()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the MiniAuditGUID value.
        /// </summary>
        public virtual Guid MiniAuditGUID
        {
            get { return miniAuditGUID; }
            set { miniAuditGUID = value; }
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
        /// Gets or sets the MiniAuditStartDateTime value.
        /// </summary>
        public virtual DateTime MiniAuditStartDateTime
        {
            get { return miniAuditStartDateTime; }
            set { miniAuditStartDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the MiniAuditEndDateTime value.
        /// </summary>
        public virtual DateTime MiniAuditEndDateTime
        {
            get { return miniAuditEndDateTime; }
            set { miniAuditEndDateTime = value; }
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
        /// Gets or sets the VCRConsoleNumber value.
        /// </summary>
        public virtual string VCRConsoleNumber
        {
            get { return vCRConsoleNumber; }
            set { vCRConsoleNumber = value; }
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
        /// Gets or sets the DateCreated value.
        /// </summary>
        public virtual DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
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
        /// Gets or sets the SecondAuditor value.
        /// </summary>
        public virtual string SecondAuditor
        {
            get { return secondAuditor; }
            set { secondAuditor = value; }
        }

        /// <summary>
        /// Gets or sets the Area value.
        /// </summary>
        public virtual string Area
        {
            get { return area; }
            set { area = value; }
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
        /// Gets or sets the LastModifiedDate value.
        /// </summary>
        public virtual DateTime LastModifiedDate
        {
            get { return lastModifiedDate; }
            set { lastModifiedDate = value; }
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

        /// <summary>
        /// Gets or sets the SubjectGUID value.
        /// </summary>
        public virtual Guid SubjectGUID
        {
            get { return subjectGUID; }
            set { subjectGUID = value; }
        }

        #endregion
    }
}
