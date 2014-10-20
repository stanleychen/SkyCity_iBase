using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
   public class LostFoundDisposalReportBE
	{
		#region Fields

		private Guid disposalGUID;
		private Guid foundReportGUID;
		private DateTime disposalDate;
		private Guid disposerEmployeeGUID;
		private string durationheld;
		private string dispositionInfo;
		private string dispositionDescription;
		private byte[] signature;
		private string _operator;
		private DateTime dateCreated;
        private bool _isNew = false;
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the LostFoundDisposalReport class.
		/// </summary>
		public LostFoundDisposalReportBE()
		{
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the DisposalGUID value.
		/// </summary>
		public virtual Guid DisposalGUID
		{
			get { return disposalGUID; }
			set { disposalGUID = value; }
		}

		/// <summary>
		/// Gets or sets the FoundReportGUID value.
		/// </summary>
		public virtual Guid FoundReportGUID
		{
			get { return foundReportGUID; }
			set { foundReportGUID = value; }
		}

		/// <summary>
		/// Gets or sets the DisposalDate value.
		/// </summary>
		public virtual DateTime DisposalDate
		{
			get { return disposalDate; }
			set { disposalDate = value; }
		}

		/// <summary>
		/// Gets or sets the DisposerEmployeeGUID value.
		/// </summary>
		public virtual Guid DisposerEmployeeGUID
		{
			get { return disposerEmployeeGUID; }
			set { disposerEmployeeGUID = value; }
		}

		/// <summary>
		/// Gets or sets the Durationheld value.
		/// </summary>
		public virtual string Durationheld
		{
			get { return durationheld; }
			set { durationheld = value; }
		}

		/// <summary>
		/// Gets or sets the DispositionInfo value.
		/// </summary>
		public virtual string DispositionInfo
		{
			get { return dispositionInfo; }
			set { dispositionInfo = value; }
		}

		/// <summary>
		/// Gets or sets the DispositionDescription value.
		/// </summary>
		public virtual string DispositionDescription
		{
			get { return dispositionDescription; }
			set { dispositionDescription = value; }
		}

		/// <summary>
		/// Gets or sets the Signature value.
		/// </summary>
		public virtual byte[] Signature
		{
			get { return signature; }
			set { signature = value; }
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
        public bool IsNew
        {
           get
           {
               return _isNew;
           }
           set
           {
               _isNew = value;
           }
        }
		#endregion
	}
}
