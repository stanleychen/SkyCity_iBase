using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class SubjectBanBE
    {
        #region Fields

        private Guid subjectGUID;
        private Guid detailedReportGUID;
        private int sequenceNumber;
        private DateTime commencement;
        private bool isPermanent;
        private DateTime endDate;
        private bool subjectCharged;
        private bool letterSent;
        private bool compulsiveGambler;
        private int recordType;
        private string typeOfBan;
        private string identificationUsed;
        private string reasonForBan;
        private byte[] selfExclusiveReport;
        private Guid removedBanIncidentGuid;
        private Guid originalBanIncidentGuid;
        private bool removed;
        private DateTime removalDate;
        private bool isNew = false;
        private int subjectRowID = 0;
        private int incidentRowID = 0;
        private bool hasParticipantAssignment = false;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SubjectBan class.
        /// </summary>
        public SubjectBanBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SubjectBan class.
        /// </summary>
        public SubjectBanBE(Guid subjectGUID, Guid detailedReportGUID, DateTime commencement, bool isPermanent, DateTime endDate, bool subjectCharged, bool letterSent, bool compulsiveGambler, int recordType, string typeOfBan, string identificationUsed, string reasonForBan, byte[] selfExclusiveReport, Guid removedBanIncidentGuid, Guid originalBanIncidentGuid, bool removed, DateTime removalDate)
        {
            this.subjectGUID = subjectGUID;
            this.detailedReportGUID = detailedReportGUID;
            this.commencement = commencement;
            this.isPermanent = isPermanent;
            this.endDate = endDate;
            this.subjectCharged = subjectCharged;
            this.letterSent = letterSent;
            this.compulsiveGambler = compulsiveGambler;
            this.recordType = recordType;
            this.typeOfBan = typeOfBan;
            this.identificationUsed = identificationUsed;
            this.reasonForBan = reasonForBan;
            this.selfExclusiveReport = selfExclusiveReport;
            this.removedBanIncidentGuid = removedBanIncidentGuid;
            this.originalBanIncidentGuid = originalBanIncidentGuid;
            this.removed = removed;
            this.removalDate = removalDate;
        }

        /// <summary>
        /// Initializes a new instance of the SubjectBan class.
        /// </summary>
        public SubjectBanBE(Guid subjectGUID, Guid detailedReportGUID, int sequenceNumber, DateTime commencement, bool isPermanent, DateTime endDate, bool subjectCharged, bool letterSent, bool compulsiveGambler, int recordType, string typeOfBan, string identificationUsed, string reasonForBan, byte[] selfExclusiveReport, Guid removedBanIncidentGuid, Guid originalBanIncidentGuid, bool removed, DateTime removalDate)
        {
            this.subjectGUID = subjectGUID;
            this.detailedReportGUID = detailedReportGUID;
            this.sequenceNumber = sequenceNumber;
            this.commencement = commencement;
            this.isPermanent = isPermanent;
            this.endDate = endDate;
            this.subjectCharged = subjectCharged;
            this.letterSent = letterSent;
            this.compulsiveGambler = compulsiveGambler;
            this.recordType = recordType;
            this.typeOfBan = typeOfBan;
            this.identificationUsed = identificationUsed;
            this.reasonForBan = reasonForBan;
            this.selfExclusiveReport = selfExclusiveReport;
            this.removedBanIncidentGuid = removedBanIncidentGuid;
            this.originalBanIncidentGuid = originalBanIncidentGuid;
            this.removed = removed;
            this.removalDate = removalDate;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the SubjectGUID value.
        /// </summary>
        public virtual Guid SubjectGUID
        {
            get { return subjectGUID; }
            set { subjectGUID = value; }
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
        /// Gets or sets the SequenceNumber value.
        /// </summary>
        public virtual int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        /// <summary>
        /// Gets or sets the Commencement value.
        /// </summary>
        public virtual DateTime Commencement
        {
            get { return commencement; }
            set { commencement = value; }
        }

        /// <summary>
        /// Gets or sets the IsPermanent value.
        /// </summary>
        public virtual bool IsPermanent
        {
            get { return isPermanent; }
            set { isPermanent = value; }
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
        /// Gets or sets the SubjectCharged value.
        /// </summary>
        public virtual bool SubjectCharged
        {
            get { return subjectCharged; }
            set { subjectCharged = value; }
        }

        /// <summary>
        /// Gets or sets the LetterSent value.
        /// </summary>
        public virtual bool LetterSent
        {
            get { return letterSent; }
            set { letterSent = value; }
        }

        /// <summary>
        /// Gets or sets the CompulsiveGambler value.
        /// </summary>
        public virtual bool CompulsiveGambler
        {
            get { return compulsiveGambler; }
            set { compulsiveGambler = value; }
        }

        /// <summary>
        /// Gets or sets the RecordType value.
        /// </summary>
        public virtual int RecordType
        {
            get { return recordType; }
            set { recordType = value; }
        }

        /// <summary>
        /// Gets or sets the TypeOfBan value.
        /// </summary>
        public virtual string TypeOfBan
        {
            get { return typeOfBan; }
            set { typeOfBan = value; }
        }

        /// <summary>
        /// Gets or sets the IdentificationUsed value.
        /// </summary>
        public virtual string IdentificationUsed
        {
            get { return identificationUsed; }
            set { identificationUsed = value; }
        }

        /// <summary>
        /// Gets or sets the ReasonForBan value.
        /// </summary>
        public virtual string ReasonForBan
        {
            get { return reasonForBan; }
            set { reasonForBan = value; }
        }

        /// <summary>
        /// Gets or sets the SelfExclusiveReport value.
        /// </summary>
        public virtual byte[] SelfExclusiveReport
        {
            get { return selfExclusiveReport; }
            set { selfExclusiveReport = value; }
        }

        /// <summary>
        /// Gets or sets the RemovedBanIncidentGuid value.
        /// </summary>
        public virtual Guid RemovedBanIncidentGuid
        {
            get { return removedBanIncidentGuid; }
            set { removedBanIncidentGuid = value; }
        }

        /// <summary>
        /// Gets or sets the OriginalBanIncidentGuid value.
        /// </summary>
        public virtual Guid OriginalBanIncidentGuid
        {
            get { return originalBanIncidentGuid; }
            set { originalBanIncidentGuid = value; }
        }

        /// <summary>
        /// Gets or sets the Removed value.
        /// </summary>
        public virtual bool Removed
        {
            get { return removed; }
            set { removed = value; }
        }

        /// <summary>
        /// Gets or sets the RemovalDate value.
        /// </summary>
        public virtual DateTime RemovalDate
        {
            get { return removalDate; }
            set { removalDate = value; }
        }

        public bool IsNew
        {
            get
            {
                return isNew;
            }
            set
            {
                isNew = value;
            }
        }
        public int IncidentRowID
        {
            get
            {
                return incidentRowID;
            }
            set
            {
                incidentRowID = value;
            }
        }
        public int SubjectRowID
        {
            get
            {
                return subjectRowID;
            }
            set
            {
                subjectRowID = value;
            }
        }
        public string UnqiueID
        {
            get
            {
                return subjectRowID.ToString() + "_" + incidentRowID.ToString();
            }
        }
        public bool HasParticipantAssignment
        {
            get
            {
                return hasParticipantAssignment;
            }
            set
            {
                hasParticipantAssignment = value;
            }
        }
        #endregion
    }
}
