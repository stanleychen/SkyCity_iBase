using System;
using System.Collections.Generic;
using System.Text;

namespace iTrak.Importer.Entities
{
    public class ImportEventArgs : EventArgs
    {
        private string _message = string.Empty;
        private int _count = 0;
        private int _rowNumber = 0;
        #region Constructor
        public ImportEventArgs()
        {
        }
        public ImportEventArgs(string message)
        {
            this._message = message;
        }
        #endregion
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }
        public int RowNumber
        {
            get
            {
                return _rowNumber;
            }
            set
            {
                _rowNumber = value;
            }
        }
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }
    }
}
