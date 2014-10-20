using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class BanWatchStatusBE
    {
        #region Fields

        private Guid subjectGUID;
        private int status;
        private DateTime commencement;
        private DateTime endDate;
        private bool isPermanent;
        private string typeOfBan;
        private string reasonForBan;
        private Guid detailedReportGUID;
        private bool isNew = false;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BanWatchStatu class.
        /// </summary>
        public BanWatchStatusBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BanWatchStatu class.
        /// </summary>
        public BanWatchStatusBE(Guid subjectGUID, int status, DateTime commencement, DateTime endDate, bool isPermanent, string typeOfBan, string reasonForBan, Guid detailedReportGUID)
        {
            this.subjectGUID = subjectGUID;
            this.status = status;
            this.commencement = commencement;
            this.endDate = endDate;
            this.isPermanent = isPermanent;
            this.typeOfBan = typeOfBan;
            this.reasonForBan = reasonForBan;
            this.detailedReportGUID = detailedReportGUID;
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
        /// Gets or sets the Status value.
        /// </summary>
        public virtual int Status
        {
            get { return status; }
            set { status = value; }
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
        /// Gets or sets the EndDate value.
        /// </summary>
        public virtual DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
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
        /// Gets or sets the TypeOfBan value.
        /// </summary>
        public virtual string TypeOfBan
        {
            get { return typeOfBan; }
            set { typeOfBan = value; }
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
        /// Gets or sets the DetailedReportGUID value.
        /// </summary>
        public virtual Guid DetailedReportGUID
        {
            get { return detailedReportGUID; }
            set { detailedReportGUID = value; }
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

        #endregion
    }
}
