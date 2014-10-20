using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class RecordAttachmentBE
    {
        #region Fields

        private Guid recordAttachmentGUID;
        private Guid hostRecordGUID;
        private Guid recordGUID;
        private string recordSummary;
        private string recordType;
        private DateTime dateAttached;
        private string attachedByUser;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RecordAttachment class.
        /// </summary>
        public RecordAttachmentBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RecordAttachment class.
        /// </summary>
        public RecordAttachmentBE(Guid recordAttachmentGUID, Guid hostRecordGUID, Guid recordGUID, string recordSummary, string recordType, DateTime dateAttached, string attachedByUser)
        {
            this.recordAttachmentGUID = recordAttachmentGUID;
            this.hostRecordGUID = hostRecordGUID;
            this.recordGUID = recordGUID;
            this.recordSummary = recordSummary;
            this.recordType = recordType;
            this.dateAttached = dateAttached;
            this.attachedByUser = attachedByUser;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the RecordAttachmentGUID value.
        /// </summary>
        public virtual Guid RecordAttachmentGUID
        {
            get { return recordAttachmentGUID; }
            set { recordAttachmentGUID = value; }
        }

        /// <summary>
        /// Gets or sets the HostRecordGUID value.
        /// </summary>
        public virtual Guid HostRecordGUID
        {
            get { return hostRecordGUID; }
            set { hostRecordGUID = value; }
        }

        /// <summary>
        /// Gets or sets the RecordGUID value.
        /// </summary>
        public virtual Guid RecordGUID
        {
            get { return recordGUID; }
            set { recordGUID = value; }
        }

        /// <summary>
        /// Gets or sets the RecordSummary value.
        /// </summary>
        public virtual string RecordSummary
        {
            get { return recordSummary; }
            set { recordSummary = value; }
        }

        /// <summary>
        /// Gets or sets the RecordType value.
        /// </summary>
        public virtual string RecordType
        {
            get { return recordType; }
            set { recordType = value; }
        }

        /// <summary>
        /// Gets or sets the DateAttached value.
        /// </summary>
        public virtual DateTime DateAttached
        {
            get { return dateAttached; }
            set { dateAttached = value; }
        }

        /// <summary>
        /// Gets or sets the AttachedByUser value.
        /// </summary>
        public virtual string AttachedByUser
        {
            get { return attachedByUser; }
            set { attachedByUser = value; }
        }

        #endregion
    }
}
