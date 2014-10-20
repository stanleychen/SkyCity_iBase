using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
   
	public class LostFoundReturnVerificationBE
	{
		#region Fields

		private Guid foundReportGUID;
		private Guid lostReportGUID;
		private DateTime returnDate;
		private Guid employeeGUID;
		private string iD1;
		private string iD1Number;
		private string iD2;
		private string iD2Number;
		private byte itemReturned;
		private bool itemToBeMailed;
		private bool deliveryCost;
		private string deliveryInvoiceID;
		private Guid mailInfoGUID;
		private bool rewardOffered;
		private decimal rewardAmount;
		private Guid rewardPaidToGUID;
		private Guid photoGUID;
		private string _operator;
		private DateTime dateCreated;
		private string signString;
		private byte[] signEncryptBytes;
		private byte[] signEncryptIV;
		private DateTime returnDueDate;
        private bool isNew = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LostFoundReturnVerification class.
		/// </summary>
		public LostFoundReturnVerificationBE()
		{
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the FoundReportGUID value.
		/// </summary>
		public virtual Guid FoundReportGUID
		{
			get { return foundReportGUID; }
			set { foundReportGUID = value; }
		}

		/// <summary>
		/// Gets or sets the LostReportGUID value.
		/// </summary>
		public virtual Guid LostReportGUID
		{
			get { return lostReportGUID; }
			set { lostReportGUID = value; }
		}

		/// <summary>
		/// Gets or sets the ReturnDate value.
		/// </summary>
		public virtual DateTime ReturnDate
		{
			get { return returnDate; }
			set { returnDate = value; }
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
		/// Gets or sets the ID1 value.
		/// </summary>
		public virtual string ID1
		{
			get { return iD1; }
			set { iD1 = value; }
		}

		/// <summary>
		/// Gets or sets the ID1Number value.
		/// </summary>
		public virtual string ID1Number
		{
			get { return iD1Number; }
			set { iD1Number = value; }
		}

		/// <summary>
		/// Gets or sets the ID2 value.
		/// </summary>
		public virtual string ID2
		{
			get { return iD2; }
			set { iD2 = value; }
		}

		/// <summary>
		/// Gets or sets the ID2Number value.
		/// </summary>
		public virtual string ID2Number
		{
			get { return iD2Number; }
			set { iD2Number = value; }
		}

		/// <summary>
		/// Gets or sets the ItemReturned value.
		/// </summary>
		public virtual byte ItemReturned
		{
			get { return itemReturned; }
			set { itemReturned = value; }
		}

		/// <summary>
		/// Gets or sets the ItemToBeMailed value.
		/// </summary>
		public virtual bool ItemToBeMailed
		{
			get { return itemToBeMailed; }
			set { itemToBeMailed = value; }
		}

		/// <summary>
		/// Gets or sets the DeliveryCost value.
		/// </summary>
		public virtual bool DeliveryCost
		{
			get { return deliveryCost; }
			set { deliveryCost = value; }
		}

		/// <summary>
		/// Gets or sets the DeliveryInvoiceID value.
		/// </summary>
		public virtual string DeliveryInvoiceID
		{
			get { return deliveryInvoiceID; }
			set { deliveryInvoiceID = value; }
		}

		/// <summary>
		/// Gets or sets the MailInfoGUID value.
		/// </summary>
		public virtual Guid MailInfoGUID
		{
			get { return mailInfoGUID; }
			set { mailInfoGUID = value; }
		}

		/// <summary>
		/// Gets or sets the RewardOffered value.
		/// </summary>
		public virtual bool RewardOffered
		{
			get { return rewardOffered; }
			set { rewardOffered = value; }
		}

		/// <summary>
		/// Gets or sets the RewardAmount value.
		/// </summary>
		public virtual decimal RewardAmount
		{
			get { return rewardAmount; }
			set { rewardAmount = value; }
		}

		/// <summary>
		/// Gets or sets the RewardPaidToGUID value.
		/// </summary>
		public virtual Guid RewardPaidToGUID
		{
			get { return rewardPaidToGUID; }
			set { rewardPaidToGUID = value; }
		}

		/// <summary>
		/// Gets or sets the PhotoGUID value.
		/// </summary>
		public virtual Guid PhotoGUID
		{
			get { return photoGUID; }
			set { photoGUID = value; }
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
		/// Gets or sets the SignString value.
		/// </summary>
		public virtual string SignString
		{
			get { return signString; }
			set { signString = value; }
		}

		/// <summary>
		/// Gets or sets the SignEncryptBytes value.
		/// </summary>
		public virtual byte[] SignEncryptBytes
		{
			get { return signEncryptBytes; }
			set { signEncryptBytes = value; }
		}

		/// <summary>
		/// Gets or sets the SignEncryptIV value.
		/// </summary>
		public virtual byte[] SignEncryptIV
		{
			get { return signEncryptIV; }
			set { signEncryptIV = value; }
		}

		/// <summary>
		/// Gets or sets the ReturnDueDate value.
		/// </summary>
		public virtual DateTime ReturnDueDate
		{
			get { return returnDueDate; }
			set { returnDueDate = value; }
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
