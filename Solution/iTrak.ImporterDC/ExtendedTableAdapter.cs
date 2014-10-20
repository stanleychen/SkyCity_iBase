using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
namespace iTrak.Importer.Data
{
    #region Blotter
    public  class BlotterExtendedTableAdapter : DataSetIXDataTableAdapters.BlotterTableAdapter 
    {
        public BlotterExtendedTableAdapter() : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion
    
    #region Incident
    public class DetailedReportExtendedTableAdapter : DataSetIXDataTableAdapters.DetailedReportTableAdapter
    {
        public DetailedReportExtendedTableAdapter() : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion

    #region Subject
    public class SubjectExtendedTableAdapter : DataSetIXDataTableAdapters.SubjectProfileTableAdapter
    {
        public SubjectExtendedTableAdapter()
            : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion

    #region SubjectBan
    public class SubjectBanExtendedTableAdapter : DataSetIXDataTableAdapters.SubjectBanTableAdapter
    {
        public SubjectBanExtendedTableAdapter()
            : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion

    #region SubjectBanWatchStatus
    public class SubjectBanWatchStatusExtendedTableAdapter : DataSetIXDataTableAdapters.BanWatchStatusTableAdapter
    {
        public SubjectBanWatchStatusExtendedTableAdapter()
            : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion

    #region ParticipantAssignment
    public class ParticipantAssignmentExtendedTableAdapter : DataSetIXDataTableAdapters.ParticipantAssignmentTableAdapter
    {
        public ParticipantAssignmentExtendedTableAdapter()
            : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion

    #region DropdownSelection
    public class DropdownSelectionExtendedTableAdapter : DataSetIXDataTableAdapters.DropdownSelectionTableAdapter
    {
        public DropdownSelectionExtendedTableAdapter()
            : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion

    #region Conversion Temp
    public class ConversionTmpExtendedTableAdapter : DataSetIXDataTableAdapters._ConversionTmpTableAdapter
    {
        public ConversionTmpExtendedTableAdapter()
            : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion
  
    #region Media
    public class IncidentAttachmentExtendedTableAdapter : DataSetIXDataTableAdapters.IncidentAttachmentTableAdapter
    {
        public IncidentAttachmentExtendedTableAdapter() : base()
        {
        }
        public SqlCommand[] SqlCommandCollection
        {
            get
            {
                return this.CommandCollection;
            }
        }
    }
    #endregion

}
