using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
   public class LostFoundLostReportBE
	{
		#region Fields

		private Guid lostGUID;
		private string lostUID;
		private string itemCategory;
		private string colour;
		private string serialNumber;
		private decimal itemValue;
		private string material;
		private string manufacturer;
		private int ageYrs;
		private int ageMonths;
		private string keyWords;
		private Guid lostPropertyGUID;
		private string lostRoomNumber;
		private DateTime whenLost;
		private bool reportedAsStolen;
		private string contents;
		private string description;
		private Guid reportByContactGUID;
		private bool owner;
		private Guid alternateContactGUID;
		private bool hotelGuest;
		private Guid guestPropertyGUID;
		private string guestRoom;
		private bool policeReportFiled;
		private string policeReportNumber;
		private string policeReportOfficer;
		private string policeReportLocation;
		private Guid insuredByCompanyGUID;
		private bool followUp;
		private DateTime followUpDate;
		private string notes;
		private string _operator;
		private DateTime dateCreated;
		private string lostLocation;
		private string sublocation;
		private string modifiedBy;
		private DateTime dateModified;
        private bool _isReturned = false;
        private string _uString = string.Empty;
        private string _uText1Caption = string.Empty;
        private string _uText1Value = string.Empty;
		#endregion

		/// <summary>
		/// Initializes a new instance of the LostFoundLostReport class.
		/// </summary>
		public LostFoundLostReportBE()
		{
		}

		#region Properties
		/// <summary>
		/// Gets or sets the LostGUID value.
		/// </summary>
		public virtual Guid LostGUID
		{
			get { return lostGUID; }
			set { lostGUID = value; }
		}

		/// <summary>
		/// Gets or sets the LostUID value.
		/// </summary>
		public virtual string LostUID
		{
			get { return lostUID; }
			set { lostUID = value; }
		}

		/// <summary>
		/// Gets or sets the ItemCategory value.
		/// </summary>
		public virtual string ItemCategory
		{
			get { return itemCategory; }
			set { itemCategory = value; }
		}

		/// <summary>
		/// Gets or sets the Colour value.
		/// </summary>
		public virtual string Colour
		{
			get { return colour; }
			set { colour = value; }
		}

		/// <summary>
		/// Gets or sets the SerialNumber value.
		/// </summary>
		public virtual string SerialNumber
		{
			get { return serialNumber; }
			set { serialNumber = value; }
		}

		/// <summary>
		/// Gets or sets the ItemValue value.
		/// </summary>
		public virtual decimal ItemValue
		{
			get { return itemValue; }
			set { itemValue = value; }
		}

		/// <summary>
		/// Gets or sets the Material value.
		/// </summary>
		public virtual string Material
		{
			get { return material; }
			set { material = value; }
		}

		/// <summary>
		/// Gets or sets the Manufacturer value.
		/// </summary>
		public virtual string Manufacturer
		{
			get { return manufacturer; }
			set { manufacturer = value; }
		}

		/// <summary>
		/// Gets or sets the AgeYrs value.
		/// </summary>
		public virtual int AgeYrs
		{
			get { return ageYrs; }
			set { ageYrs = value; }
		}

		/// <summary>
		/// Gets or sets the AgeMonths value.
		/// </summary>
		public virtual int AgeMonths
		{
			get { return ageMonths; }
			set { ageMonths = value; }
		}

		/// <summary>
		/// Gets or sets the KeyWords value.
		/// </summary>
		public virtual string KeyWords
		{
			get { return keyWords; }
			set { keyWords = value; }
		}

		/// <summary>
		/// Gets or sets the LostPropertyGUID value.
		/// </summary>
		public virtual Guid LostPropertyGUID
		{
			get { return lostPropertyGUID; }
			set { lostPropertyGUID = value; }
		}

		/// <summary>
		/// Gets or sets the LostRoomNumber value.
		/// </summary>
		public virtual string LostRoomNumber
		{
			get { return lostRoomNumber; }
			set { lostRoomNumber = value; }
		}

		/// <summary>
		/// Gets or sets the WhenLost value.
		/// </summary>
		public virtual DateTime WhenLost
		{
			get { return whenLost; }
			set { whenLost = value; }
		}

		/// <summary>
		/// Gets or sets the ReportedAsStolen value.
		/// </summary>
		public virtual bool ReportedAsStolen
		{
			get { return reportedAsStolen; }
			set { reportedAsStolen = value; }
		}

		/// <summary>
		/// Gets or sets the Contents value.
		/// </summary>
		public virtual string Contents
		{
			get { return contents; }
			set { contents = value; }
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
		/// Gets or sets the ReportByContactGUID value.
		/// </summary>
		public virtual Guid ReportByContactGUID
		{
			get { return reportByContactGUID; }
			set { reportByContactGUID = value; }
		}

		/// <summary>
		/// Gets or sets the Owner value.
		/// </summary>
		public virtual bool Owner
		{
			get { return owner; }
			set { owner = value; }
		}

		/// <summary>
		/// Gets or sets the AlternateContactGUID value.
		/// </summary>
		public virtual Guid AlternateContactGUID
		{
			get { return alternateContactGUID; }
			set { alternateContactGUID = value; }
		}

		/// <summary>
		/// Gets or sets the HotelGuest value.
		/// </summary>
		public virtual bool HotelGuest
		{
			get { return hotelGuest; }
			set { hotelGuest = value; }
		}

		/// <summary>
		/// Gets or sets the GuestPropertyGUID value.
		/// </summary>
		public virtual Guid GuestPropertyGUID
		{
			get { return guestPropertyGUID; }
			set { guestPropertyGUID = value; }
		}

		/// <summary>
		/// Gets or sets the GuestRoom value.
		/// </summary>
		public virtual string GuestRoom
		{
			get { return guestRoom; }
			set { guestRoom = value; }
		}

		/// <summary>
		/// Gets or sets the PoliceReportFiled value.
		/// </summary>
		public virtual bool PoliceReportFiled
		{
			get { return policeReportFiled; }
			set { policeReportFiled = value; }
		}

		/// <summary>
		/// Gets or sets the PoliceReportNumber value.
		/// </summary>
		public virtual string PoliceReportNumber
		{
			get { return policeReportNumber; }
			set { policeReportNumber = value; }
		}

		/// <summary>
		/// Gets or sets the PoliceReportOfficer value.
		/// </summary>
		public virtual string PoliceReportOfficer
		{
			get { return policeReportOfficer; }
			set { policeReportOfficer = value; }
		}

		/// <summary>
		/// Gets or sets the PoliceReportLocation value.
		/// </summary>
		public virtual string PoliceReportLocation
		{
			get { return policeReportLocation; }
			set { policeReportLocation = value; }
		}

		/// <summary>
		/// Gets or sets the InsuredByCompanyGUID value.
		/// </summary>
		public virtual Guid InsuredByCompanyGUID
		{
			get { return insuredByCompanyGUID; }
			set { insuredByCompanyGUID = value; }
		}

		/// <summary>
		/// Gets or sets the FollowUp value.
		/// </summary>
		public virtual bool FollowUp
		{
			get { return followUp; }
			set { followUp = value; }
		}

		/// <summary>
		/// Gets or sets the FollowUpDate value.
		/// </summary>
		public virtual DateTime FollowUpDate
		{
			get { return followUpDate; }
			set { followUpDate = value; }
		}

		/// <summary>
		/// Gets or sets the Notes value.
		/// </summary>
		public virtual string Notes
		{
			get { return notes; }
			set { notes = value; }
		}

		/// <summary>
		/// Gets or sets the Operator value.
		/// </summary>
		public virtual string Operator
		{
			get { return _operator; }
			set { _operator = value; }
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
		/// Gets or sets the LostLocation value.
		/// </summary>
		public virtual string LostLocation
		{
			get { return lostLocation; }
			set { lostLocation = value; }
		}

		/// <summary>
		/// Gets or sets the Sublocation value.
		/// </summary>
		public virtual string Sublocation
		{
			get { return sublocation; }
			set { sublocation = value; }
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
		/// Gets or sets the DateModified value.
		/// </summary>
		public virtual DateTime DateModified
		{
			get { return dateModified; }
			set { dateModified = value; }
		}

       public bool IsReturned
       {
           get
           {
               return _isReturned;
           }
           set
           {
               _isReturned = value;
           }
       }

       public string UString
       {
           get
           {
               return _uString;
           }
           set
           {
               _uString = value;
           }
       }
       public string UText1Caption
       {
           get
           {
               return _uText1Caption;
           }
           set
           {
               _uText1Caption = value;
           }
       }
       public string UText1Value
       {
           get
           {
               return _uText1Value;
           }
           set
           {
               _uText1Value = value;
           }
       }
	   #endregion
	}
}
