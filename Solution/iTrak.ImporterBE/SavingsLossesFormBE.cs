using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{

    public class SavingsLossesFormBE
    {
        #region Fields

        private Guid savingsLossesItemGUID;
        private Guid savingsLossesFormGUID;
        private string savingsLossesType;
        private string saveOrLoss;
        private decimal savingsLossesValue;
        private string description;
        private bool loss;
        private Guid savingsLossesPrimaryParentGUID;
        private string assignedBy;
        private DateTime assignedDate;
        private DateTime occured;
        private string savingsLossesPrimaryParentType;
        private string savingsLossesFormType;
        private string groupByType;
        private string sourceID;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SavingsLossesForm class.
        /// </summary>
        public SavingsLossesFormBE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SavingsLossesForm class.
        /// </summary>
        public SavingsLossesFormBE(Guid savingsLossesItemGUID, Guid savingsLossesFormGUID, string savingsLossesType, string saveOrLoss, decimal savingsLossesValue, string description, bool loss, Guid savingsLossesPrimaryParentGUID, string assignedBy, DateTime assignedDate, DateTime occured, string savingsLossesPrimaryParentType, string savingsLossesFormType, string groupByType)
        {
            this.savingsLossesItemGUID = savingsLossesItemGUID;
            this.savingsLossesFormGUID = savingsLossesFormGUID;
            this.savingsLossesType = savingsLossesType;
            this.saveOrLoss = saveOrLoss;
            this.savingsLossesValue = savingsLossesValue;
            this.description = description;
            this.loss = loss;
            this.savingsLossesPrimaryParentGUID = savingsLossesPrimaryParentGUID;
            this.assignedBy = assignedBy;
            this.assignedDate = assignedDate;
            this.occured = occured;
            this.savingsLossesPrimaryParentType = savingsLossesPrimaryParentType;
            this.savingsLossesFormType = savingsLossesFormType;
            this.groupByType = groupByType;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the SavingsLossesItemGUID value.
        /// </summary>
        public virtual Guid SavingsLossesItemGUID
        {
            get { return savingsLossesItemGUID; }
            set { savingsLossesItemGUID = value; }
        }

        /// <summary>
        /// Gets or sets the SavingsLossesFormGUID value.
        /// </summary>
        public virtual Guid SavingsLossesFormGUID
        {
            get { return savingsLossesFormGUID; }
            set { savingsLossesFormGUID = value; }
        }

        /// <summary>
        /// Gets or sets the SavingsLossesType value.
        /// </summary>
        public virtual string SavingsLossesType
        {
            get { return savingsLossesType; }
            set { savingsLossesType = value; }
        }

        /// <summary>
        /// Gets or sets the SaveOrLoss value.
        /// </summary>
        public virtual string SaveOrLoss
        {
            get { return saveOrLoss; }
            set { saveOrLoss = value; }
        }

        /// <summary>
        /// Gets or sets the SavingsLossesValue value.
        /// </summary>
        public virtual decimal SavingsLossesValue
        {
            get { return savingsLossesValue; }
            set { savingsLossesValue = value; }
        }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the Loss value.
        /// </summary>
        public virtual bool Loss
        {
            get { return loss; }
            set { loss = value; }
        }

        /// <summary>
        /// Gets or sets the SavingsLossesPrimaryParentGUID value.
        /// </summary>
        public virtual Guid SavingsLossesPrimaryParentGUID
        {
            get { return savingsLossesPrimaryParentGUID; }
            set { savingsLossesPrimaryParentGUID = value; }
        }

        /// <summary>
        /// Gets or sets the AssignedBy value.
        /// </summary>
        public virtual string AssignedBy
        {
            get { return assignedBy; }
            set { assignedBy = value; }
        }

        /// <summary>
        /// Gets or sets the AssignedDate value.
        /// </summary>
        public virtual DateTime AssignedDate
        {
            get { return assignedDate; }
            set { assignedDate = value; }
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
        /// Gets or sets the SavingsLossesPrimaryParentType value.
        /// </summary>
        public virtual string SavingsLossesPrimaryParentType
        {
            get { return savingsLossesPrimaryParentType; }
            set { savingsLossesPrimaryParentType = value; }
        }

        /// <summary>
        /// Gets or sets the SavingsLossesFormType value.
        /// </summary>
        public virtual string SavingsLossesFormType
        {
            get { return savingsLossesFormType; }
            set { savingsLossesFormType = value; }
        }

        /// <summary>
        /// Gets or sets the GroupByType value.
        /// </summary>
        public virtual string GroupByType
        {
            get { return groupByType; }
            set { groupByType = value; }
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
