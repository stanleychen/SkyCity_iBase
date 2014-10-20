using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class VehicleBE
    {
        #region Fields
        private int _rowID;
        private Guid _vehicleGuid = Guid.Empty;
        private string _sourceKey = string.Empty;
		private string _license;
		private string _country;
		private string _state;
		private string _condition;
		private string _color;
		private string _vIN;
		private string _odometer;
		private string _make;
		private string _model;
		private string _vehicleType;
		private string _year;
        private Guid _subjectGuid = Guid.Empty;
        private Guid _bestImageGuid = Guid.Empty;
		private string _note;
		private DateTime _dateCreated;
		private DateTime _dateModified;
		private string _createdBy;
		private string _modifiedBy;
		private string _incidentNumber;
		private string _uString;
		private string _uText1Caption;
		private string _uText1Value;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the __iTrakImporter_Vehicle class.
		/// </summary>
		public VehicleBE()
		{
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the RowID value.
		/// </summary>
		public virtual int RowID
		{
			get { return _rowID; }
			set { _rowID = value; }
		}
        public virtual Guid VehicleGuid
        {
            get {return _vehicleGuid;}
            set { _vehicleGuid = value; }
        }
        public virtual string SourceKey
        {
            get { return _sourceKey; }
            set { _sourceKey = value; }
        }
		/// <summary>
		/// Gets or sets the License value.
		/// </summary>
		public virtual string License
		{
			get { return _license; }
			set { _license = value; }
		}

		/// <summary>
		/// Gets or sets the Country value.
		/// </summary>
		public virtual string Country
		{
			get { return _country; }
			set { _country = value; }
		}

		/// <summary>
		/// Gets or sets the State value.
		/// </summary>
		public virtual string State
		{
			get { return _state; }
			set { _state = value; }
		}

		/// <summary>
		/// Gets or sets the Condition value.
		/// </summary>
		public virtual string Condition
		{
			get { return _condition; }
			set { _condition = value; }
		}

		/// <summary>
		/// Gets or sets the Color value.
		/// </summary>
		public virtual string Color
		{
			get { return _color; }
			set { _color = value; }
		}

		/// <summary>
		/// Gets or sets the VIN value.
		/// </summary>
		public virtual string VIN
		{
			get { return _vIN; }
			set { _vIN = value; }
		}

		/// <summary>
		/// Gets or sets the Odometer value.
		/// </summary>
		public virtual string Odometer
		{
			get { return _odometer; }
			set { _odometer = value; }
		}

		/// <summary>
		/// Gets or sets the Make value.
		/// </summary>
		public virtual string Make
		{
			get { return _make; }
			set { _make = value; }
		}

		/// <summary>
		/// Gets or sets the Model value.
		/// </summary>
		public virtual string Model
		{
			get { return _model; }
			set { _model = value; }
		}

		/// <summary>
		/// Gets or sets the VehicleType value.
		/// </summary>
		public virtual string VehicleType
		{
			get { return _vehicleType; }
			set { _vehicleType = value; }
		}

		/// <summary>
		/// Gets or sets the Year value.
		/// </summary>
		public virtual string Year
		{
			get { return _year; }
			set { _year = value; }
		}
        public virtual Guid SubjectGuid
        {
            get
            {
                return _subjectGuid;
            }
            set
            {
                _subjectGuid = value;
            }
        }
        public virtual Guid BestImageGuid
        {
            get { return _bestImageGuid; }
            set { _bestImageGuid = value; }
        }
		/// <summary>
		/// Gets or sets the Note value.
		/// </summary>
		public virtual string Note
		{
			get { return _note; }
			set { _note = value; }
		}

		/// <summary>
		/// Gets or sets the DateCreated value.
		/// </summary>
		public virtual DateTime DateCreated
		{
			get { return _dateCreated; }
			set { _dateCreated = value; }
		}

		/// <summary>
		/// Gets or sets the DateModified value.
		/// </summary>
		public virtual DateTime DateModified
		{
			get { return _dateModified; }
			set { _dateModified = value; }
		}

		/// <summary>
		/// Gets or sets the CreatedBy value.
		/// </summary>
		public virtual string CreatedBy
		{
			get { return _createdBy; }
			set { _createdBy = value; }
		}

		/// <summary>
		/// Gets or sets the ModifiedBy value.
		/// </summary>
		public virtual string ModifiedBy
		{
			get { return _modifiedBy; }
			set { _modifiedBy = value; }
		}

		/// <summary>
		/// Gets or sets the IncidentNumber value.
		/// </summary>
		public virtual string IncidentNumber
		{
			get { return _incidentNumber; }
			set { _incidentNumber = value; }
		}

		/// <summary>
		/// Gets or sets the UString value.
		/// </summary>
		public virtual string UString
		{
			get { return _uString; }
			set { _uString = value; }
		}

		/// <summary>
		/// Gets or sets the UText1Caption value.
		/// </summary>
		public virtual string UText1Caption
		{
			get { return _uText1Caption; }
			set { _uText1Caption = value; }
		}

		/// <summary>
		/// Gets or sets the UText1Value value.
		/// </summary>
		public virtual string UText1Value
		{
			get { return _uText1Value; }
			set { _uText1Value = value; }
		}

		#endregion
	}
}
