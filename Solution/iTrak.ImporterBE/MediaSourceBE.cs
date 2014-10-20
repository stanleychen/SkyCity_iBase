using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class MediaSourceBE
    {
        private string _id = string.Empty;
        private string _fileName = string.Empty;
        private string _fileLocation = string.Empty;
        private string _title = string.Empty;
        private DateTime _attachedDate = DateTime.Now;
        private string _attachedBy = string.Empty;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        public string FileLocation
        {
            get
            {
                return _fileLocation;
            }
            set
            {
                _fileLocation = value;
            }
        }
        public string FileFullName
        {
            get
            {
                return _fileLocation + @"\" + _fileName;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public DateTime AttachedDate
        {
            get
            {
                return _attachedDate;
            }
            set
            {
                _attachedDate = value;
            }
        }
        public string AttachedBy
        {
            get
            {
                return _attachedBy;
            }
            set
            {
                _attachedBy = value;
            }
        }
    
    }
}
