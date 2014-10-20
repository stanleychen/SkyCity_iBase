using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class MiniAuditViolationBE
    {
        #region Fields

        private Guid miniAuditEmployeeViolationGUID;
        private Guid miniAuditGUID;
        private Guid employeeGUID;
        private Guid supervisorGUID;
        private DateTime violationDateTime;
        private string errorType;
        private string violation;
        private string violationDescription;
        private string remarks;
        private bool isNew;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MiniAuditViolation class.
        /// </summary>
        public MiniAuditViolationBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MiniAuditViolation class.
        /// </summary>
        public MiniAuditViolationBE(Guid miniAuditEmployeeViolationGUID, Guid miniAuditGUID, Guid employeeGUID, Guid supervisorGUID, DateTime violationDateTime, string errorType, string violation, string violationDescription, string remarks)
        {
            this.miniAuditEmployeeViolationGUID = miniAuditEmployeeViolationGUID;
            this.miniAuditGUID = miniAuditGUID;
            this.employeeGUID = employeeGUID;
            this.supervisorGUID = supervisorGUID;
            this.violationDateTime = violationDateTime;
            this.errorType = errorType;
            this.violation = violation;
            this.violationDescription = violationDescription;
            this.remarks = remarks;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the MiniAuditEmployeeViolationGUID value.
        /// </summary>
        public virtual Guid MiniAuditEmployeeViolationGUID
        {
            get { return miniAuditEmployeeViolationGUID; }
            set { miniAuditEmployeeViolationGUID = value; }
        }

        /// <summary>
        /// Gets or sets the MiniAuditGUID value.
        /// </summary>
        public virtual Guid MiniAuditGUID
        {
            get { return miniAuditGUID; }
            set { miniAuditGUID = value; }
        }

        /// <summary>
        /// Gets or sets the EmployeeGUID value.
        /// </summary>
        public virtual Guid EmployeeGUID
        {
            get { return employeeGUID; }
            set { employeeGUID = value; }
        }

        /// <summary>
        /// Gets or sets the SupervisorGUID value.
        /// </summary>
        public virtual Guid SupervisorGUID
        {
            get { return supervisorGUID; }
            set { supervisorGUID = value; }
        }

        /// <summary>
        /// Gets or sets the ViolationDateTime value.
        /// </summary>
        public virtual DateTime ViolationDateTime
        {
            get { return violationDateTime; }
            set { violationDateTime = value; }
        }

        /// <summary>
        /// Gets or sets the ErrorType value.
        /// </summary>
        public virtual string ErrorType
        {
            get { return errorType; }
            set { errorType = value; }
        }

        /// <summary>
        /// Gets or sets the Violation value.
        /// </summary>
        public virtual string Violation
        {
            get { return violation; }
            set { violation = value; }
        }

        /// <summary>
        /// Gets or sets the ViolationDescription value.
        /// </summary>
        public virtual string ViolationDescription
        {
            get { return violationDescription; }
            set { violationDescription = value; }
        }

        /// <summary>
        /// Gets or sets the Remarks value.
        /// </summary>
        public virtual string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public virtual bool IsNew
        {
            get
            {
                return isNew;
            }
            set
            {
                this.isNew = value;
            }
        }
        #endregion
    }
}
