using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{

    public class IncidentAttachmentBE
    {
        #region Fields

        private Guid incidentAttachment;
        private Guid detailedReportGUID;
        private string sourceID = string.Empty;
        private DateTime attached;
        private byte[] attachedmentData;
        private string originalFilename;
        private string attachedBy;
        private byte[] thumbnail;
        private string attachedType;
        private long attachmentSize;
        private bool linked;
        private byte[] digitalSignature;
        private int dataProviderType;
        private bool deleted;
        private DateTime lastModifiedDate;
        private DateTime serverCreateDate;
        private bool isBestAsset = false;
        private string hostType = string.Empty;
        private string mediaTitle = string.Empty;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the IncidentAttachment class.
        /// </summary>
        public IncidentAttachmentBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the IncidentAttachment class.
        /// </summary>
        public IncidentAttachmentBE(Guid incidentAttachment, Guid detailedReportGUID, DateTime attached, byte[] attachedmentData, string originalFilename, string attachedBy, byte[] thumbnail, string attachedType, long attachmentSize, bool linked, byte[] digitalSignature, int dataProviderType, bool deleted, DateTime lastModifiedDate, DateTime serverCreateDate)
        {
            this.incidentAttachment = incidentAttachment;
            this.detailedReportGUID = detailedReportGUID;
            this.attached = attached;
            this.attachedmentData = attachedmentData;
            this.originalFilename = originalFilename;
            this.attachedBy = attachedBy;
            this.thumbnail = thumbnail;
            this.attachedType = attachedType;
            this.attachmentSize = attachmentSize;
            this.linked = linked;
            this.digitalSignature = digitalSignature;
            this.dataProviderType = dataProviderType;
            this.deleted = deleted;
            this.lastModifiedDate = lastModifiedDate;
            this.serverCreateDate = serverCreateDate;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the IncidentAttachment value.
        /// </summary>
        public virtual Guid IncidentAttachment
        {
            get { return incidentAttachment; }
            set { incidentAttachment = value; }
        }

        /// <summary>
        /// Gets or sets the DetailedReportGUID value.
        /// </summary>
        public virtual Guid DetailedReportGUID
        {
            get { return detailedReportGUID; }
            set { detailedReportGUID = value; }
        }

        public virtual string SourceID
        {
            get
            {
                return sourceID;
            }
            set
            {
                sourceID = value;
            }
        }
        /// <summary>
        /// Gets or sets the Attached value.
        /// </summary>
        public virtual DateTime Attached
        {
            get { return attached; }
            set { attached = value; }
        }

        /// <summary>
        /// Gets or sets the AttachedmentData value.
        /// </summary>
        public virtual byte[] AttachedmentData
        {
            get { return attachedmentData; }
            set { attachedmentData = value; }
        }

        /// <summary>
        /// Gets or sets the OriginalFilename value.
        /// </summary>
        public virtual string OriginalFilename
        {
            get { return originalFilename; }
            set { originalFilename = value; }
        }

        /// <summary>
        /// Gets or sets the AttachedBy value.
        /// </summary>
        public virtual string AttachedBy
        {
            get { return attachedBy; }
            set { attachedBy = value; }
        }

        /// <summary>
        /// Gets or sets the Thumbnail value.
        /// </summary>
        public virtual byte[] Thumbnail
        {
            get { return thumbnail; }
            set { thumbnail = value; }
        }

        /// <summary>
        /// Gets or sets the AttachedType value.
        /// </summary>
        public virtual string AttachedType
        {
            get { return attachedType; }
            set { attachedType = value; }
        }

        /// <summary>
        /// Gets or sets the AttachmentSize value.
        /// </summary>
        public virtual long AttachmentSize
        {
            get { return attachmentSize; }
            set { attachmentSize = value; }
        }

        /// <summary>
        /// Gets or sets the Linked value.
        /// </summary>
        public virtual bool Linked
        {
            get { return linked; }
            set { linked = value; }
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
        /// Gets or sets the Deleted value.
        /// </summary>
        public virtual bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
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
        /// Gets or sets the ServerCreateDate value.
        /// </summary>
        public virtual DateTime ServerCreateDate
        {
            get { return serverCreateDate; }
            set { serverCreateDate = value; }
        }

        public bool IsBestAsset
        {
            get
            {
                return this.isBestAsset;
            }
            set
            {
                this.isBestAsset = value;
            }
        }

        public string HostType
        {
            get
            {
                return hostType;
            }
            set
            {
                hostType = value;
            }
        }

        public string MediaTitle
        {
            get { return mediaTitle; }
            set { mediaTitle = value; }
        }
        #endregion
    }
}
