using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class ParticipantAssignmentBE
    {

        #region Fields
        private Guid _detailedReportGuid = Guid.Empty;
        private Guid _participantGuid = Guid.Empty;
        private DateTime _assigned;
        private string _assignedBy;
        private string _participantType;
        private string _participantRole;
        private string _participantNotes;
        private string _secondaryRole;
        private bool _policeContacted;
        private bool _takenFromScene;
        private string _policeContactedResult;
        private bool _isNew = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ParticipantAssigmentBE class.
        /// </summary>
        public ParticipantAssignmentBE()
        {
        }

        #endregion

        #region Properties
        public string UniqueID
        {
            get
            {
               return this.DetailedReportGuid.ToString().ToLower() + "_" + this.ParticipantGuid.ToString().ToLower();
            }
        }
        public Guid DetailedReportGuid
        {
            get
            {
                return _detailedReportGuid;
            }
            set
            {
                _detailedReportGuid = value;
            }
        }
        public Guid ParticipantGuid
        {
            get
            {
                return _participantGuid;
            }
            set
            {
                _participantGuid = value;
            }
        }
        /// <summary>
        /// Gets or sets the Assigned value.
        /// </summary>
        public virtual DateTime Assigned
        {
            get { return _assigned; }
            set { _assigned = value; }
        }

        /// <summary>
        /// Gets or sets the AssignedBy value.
        /// </summary>
        public virtual string AssignedBy
        {
            get { return _assignedBy; }
            set { _assignedBy = value; }
        }

        /// <summary>
        /// Gets or sets the ParticipantType value.
        /// </summary>
        public virtual string ParticipantType
        {
            get { return _participantType; }
            set { _participantType = value; }
        }

        /// <summary>
        /// Gets or sets the ParticipantRole value.
        /// </summary>
        public virtual string ParticipantRole
        {
            get { return _participantRole; }
            set { _participantRole = value; }
        }

        /// <summary>
        /// Gets or sets the ParticipantNotes value.
        /// </summary>
        public virtual string ParticipantNotes
        {
            get { return _participantNotes; }
            set { _participantNotes = value; }
        }

        /// <summary>
        /// Gets or sets the SecondaryRole value.
        /// </summary>
        public virtual string SecondaryRole
        {
            get { return _secondaryRole; }
            set { _secondaryRole = value; }
        }

        /// <summary>
        /// Gets or sets the PoliceContacted value.
        /// </summary>
        public virtual bool PoliceContacted
        {
            get { return _policeContacted; }
            set { _policeContacted = value; }
        }

        /// <summary>
        /// Gets or sets the TakenFromScene value.
        /// </summary>
        public virtual bool TakenFromScene
        {
            get { return _takenFromScene; }
            set { _takenFromScene = value; }
        }

        /// <summary>
        /// Gets or sets the PoliceContactedResult value.
        /// </summary>
        public virtual string PoliceContactedResult
        {
            get { return _policeContactedResult; }
            set { _policeContactedResult = value; }
        }
        public virtual bool IsNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                this._isNew = value;
            }
        }
        #endregion
    }
}
