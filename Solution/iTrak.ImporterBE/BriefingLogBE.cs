using System;

namespace iTrak.Importer.Entities
{
    public class BriefingLogBE
    {
        #region Fields

        private Guid briefingLogGUID;
        private Guid detailedReportGUID;
        private DateTime startDate;
        private DateTime endDate;
        private int distributionType;
        private DateTime created;
        private string createdBy;
        private string topic;
        private string details;
        private DateTime lastModifiedDate;
        private DateTime timeStamp;
        private string modifiedBy;
        private string moduleName;
        private string briefingLogType;
        private string briefingLogSubType;
        private string text1Caption;
        private string text1;
        private string text2Caption;
        private string text2;
        private string text3Caption;
        private string text3;
        private string text4Caption;
        private string text4;
        private string text5Caption;
        private string text5;
        private string text6Caption;
        private string text6;
        private string text7Caption;
        private string text7;
        private string text8Caption;
        private string text8;
        private string text9Caption;
        private string text9;
        private string text10Caption;
        private string text10;
        private string ustring;
        private string sourceID;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BriefingLog class.
        /// </summary>
        public BriefingLogBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BriefingLog class.
        /// </summary>
        public BriefingLogBE(Guid briefingLogGUID, Guid detailedReportGUID, DateTime startDate, DateTime endDate, int distributionType, DateTime created, string createdBy, string topic, string details, DateTime lastModifiedDate, DateTime timeStamp, string modifiedBy, string moduleName)
        {
            this.briefingLogGUID = briefingLogGUID;
            this.detailedReportGUID = detailedReportGUID;
            this.startDate = startDate;
            this.endDate = endDate;
            this.distributionType = distributionType;
            this.created = created;
            this.createdBy = createdBy;
            this.topic = topic;
            this.details = details;
            this.lastModifiedDate = lastModifiedDate;
            this.timeStamp = timeStamp;
            this.modifiedBy = modifiedBy;
            this.moduleName = moduleName;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the BriefingLogGUID value.
        /// </summary>
        public virtual Guid BriefingLogGUID
        {
            get { return briefingLogGUID; }
            set { briefingLogGUID = value; }
        }

        /// <summary>
        /// Gets or sets the DetailedReportGUID value.
        /// </summary>
        public virtual Guid DetailedReportGUID
        {
            get { return detailedReportGUID; }
            set { detailedReportGUID = value; }
        }

        /// <summary>
        /// Gets or sets the StartDate value.
        /// </summary>
        public virtual DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        /// <summary>
        /// Gets or sets the EndDate value.
        /// </summary>
        public virtual DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        /// <summary>
        /// Gets or sets the DistributionType value.
        /// </summary>
        public virtual int DistributionType
        {
            get { return distributionType; }
            set { distributionType = value; }
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
        /// Gets or sets the Topic value.
        /// </summary>
        public virtual string Topic
        {
            get { return topic; }
            set { topic = value; }
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
        /// Gets or sets the LastModifiedDate value.
        /// </summary>
        public virtual DateTime LastModifiedDate
        {
            get { return lastModifiedDate; }
            set { lastModifiedDate = value; }
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
        /// Gets or sets the ModifiedBy value.
        /// </summary>
        public virtual string ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        /// <summary>
        /// Gets or sets the ModuleName value.
        /// </summary>
        public virtual string ModuleName
        {
            get { return moduleName; }
            set { moduleName = value; }
        }

        public virtual string BriefingLogType
        {
            get
            {
                return briefingLogType;
            }
            set
            {
                briefingLogType = value;
            }
        }

        public virtual string BriefingLogSubType
        {
            get
            {
                return briefingLogSubType;
            }
            set
            {
                briefingLogSubType = value;
            }
        }

        public virtual string Text1Caption
        {
            get { return text1Caption; }
            set { text1Caption = value; }
        }
        public virtual string Text1
        {
            get { return text1; }
            set { text1 = value; }
        }

        public virtual string Text2Caption
        {
            get { return text2Caption; }
            set { text2Caption = value; }
        }
        public virtual string Text2
        {
            get { return text2; }
            set { text2 = value; }
        }

        public virtual string Text3Caption
        {
            get { return text3Caption; }
            set { text3Caption = value; }
        }
        public virtual string Text3
        {
            get { return text3; }
            set { text3 = value; }
        }

        public virtual string Text4Caption
        {
            get { return text4Caption; }
            set { text4Caption = value; }
        }

        public virtual string Text4
        {
            get { return text4; }
            set { text4 = value; }
        }

        public virtual string Text5Caption
        {
            get { return text5Caption; }
            set { text5Caption = value; }
        }

        public virtual string Text5
        {
            get { return text5; }
            set { text5 = value; }
        }

        public virtual string Text6Caption
        {
            get { return text6Caption; }
            set { text6Caption = value; }
        }

        public virtual string Text6
        {
            get { return text6; }
            set { text6 = value; }
        }

        public virtual string Text7Caption
        {
            get { return text7Caption; }
            set { text7Caption = value; }
        }

        public virtual string Text7
        {
            get { return text7; }
            set { text7 = value; }
        }

        public virtual string Text8Caption
        {
            get { return text8Caption; }
            set { text8Caption = value; }
        }

        public virtual string Text8
        {
            get { return text8; }
            set { text8 = value; }
        }

        public virtual string Text9Caption
        {
            get { return text9Caption; }
            set { text9Caption = value; }
        }

        public virtual string Text9
        {
            get { return text9; }
            set { text9 = value; }
        }

        public virtual string Text10Caption
        {
            get { return text10Caption; }
            set { text10Caption = value; }
        }

        public virtual string Text10
        {
            get { return text10; }
            set { text10 = value; }
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
        #endregion
    }
}
