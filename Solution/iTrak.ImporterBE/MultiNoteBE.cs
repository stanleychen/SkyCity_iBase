using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class MultiNoteBE
    {
        private Guid _multiNoteGuid;
        private string _notes;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private string _createdBy;
        private string _modifiedBy;
        private string _masterTable;
        private string _userType;
        private Guid _masterGuid;
        private string _sourceId;
        public Guid MultiNoteGuid
        {
            get
            {
                return _multiNoteGuid;
            }
            set
            {
                _multiNoteGuid = value;
            }
        }

        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                this._notes = value;
            }
        }
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
            set
            {
                this._dateCreated = value;
            }
        }
        public DateTime DateModified
        {
            get
            {
                return _dateModified;
            }
            set
            {
                this._dateModified = value;
            }
        }
        public string CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                this._createdBy = value;
            }
        }
        public string ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }
            set
            {
                this._modifiedBy = value;
            }
        }
        public string MasterTable
        {
            get
            {
                return _masterTable;
            }
            set
            {
                this._masterTable = value;
            }
        }
        public string UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                this._userType = value;
            }
        }
        public Guid MasterGUID
        {
            get
            {
                return _masterGuid;
            }
            set
            {
                this._masterGuid = value;
            }
        }
        public string SourceID
        {
            get
            {
                return _sourceId;
            }
            set
            {
                _sourceId = value;
            }
        }
    }
}
