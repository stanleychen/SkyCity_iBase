using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;
namespace iTrak.Importer.Data.Importers
{
    public class MultiNotes
    {

        private static ManualResetEvent[] _resetEvents;
        public event EventHandler ImportedRowEvent;
        private const string DEFAULT_STATUS = "Open";
        private const string ERROR_TYPE = "No Error";
        public event EventHandler ImportCompletedEvent;
        private string _sourceKey = string.Empty;

        #region Count
        private int _count = -1;
        public int Count
        {
            get
            {
                if (_count == -1) _count = GetSourceRowCount();

                return _count;
            }
        }
        #endregion

        #region GetSourceRowCount
        private int GetSourceRowCount()
        {
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_MultiNotes (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;
            return rowCount;
        }
        #endregion
   

        #region GetOneMultiNote
  
        private MultiNoteBE GetOneMultiNote(IDataReader dr)
        {
            MultiNoteBE multiNoteBE = new MultiNoteBE();
            multiNoteBE.MasterTable = SqlClientUtility.GetString(dr, "MasterTable", String.Empty); ;
            multiNoteBE.MultiNoteGuid = SqlClientUtility.GetGuid(dr, "MultiNoteGuid", Guid.Empty);
            multiNoteBE.MasterGUID = SqlClientUtility.GetGuid(dr, "HostGuid", Guid.Empty);
            multiNoteBE.SourceID = SqlClientUtility.GetString(dr, "SourceID", string.Empty);

            StringBuilder sb = new StringBuilder();
            string note = string.Empty;
            note = SqlClientUtility.GetString(dr, "uTableString", string.Empty);
            sb.AppendLine(note);

            string note1Caption = SqlClientUtility.GetString(dr, "uTableText1Caption", string.Empty);
            string note1Value = SqlClientUtility.GetString(dr, "uTableText1Value", string.Empty);
            if(!string.IsNullOrEmpty(note1Caption) && !string.IsNullOrEmpty(note1Value))
                sb.AppendFormat("{0}: {1}", note1Caption, note1Value);
            
            sb.AppendLine();

            string note2Caption = SqlClientUtility.GetString(dr, "uTableText2Caption", string.Empty);
            string note2Value = SqlClientUtility.GetString(dr, "uTableText2Value", string.Empty);
            if (!string.IsNullOrEmpty(note2Caption) && !string.IsNullOrEmpty(note2Value))
                sb.AppendFormat("{0}: {1}", note2Caption, note2Value);

            sb.AppendLine();

            string note3Caption = SqlClientUtility.GetString(dr, "uTableText3Caption", string.Empty);
            string note3Value = SqlClientUtility.GetString(dr, "uTableText3Value", string.Empty);
            if (!string.IsNullOrEmpty(note3Caption) && !string.IsNullOrEmpty(note3Value))
                sb.AppendFormat("{0}: {1}", note3Caption, note3Value);

            string note4Caption = SqlClientUtility.GetString(dr, "uTableText4Caption", string.Empty);
            string note4Value = SqlClientUtility.GetString(dr, "uTableText4Value", string.Empty);
            if (!string.IsNullOrEmpty(note3Caption) && !string.IsNullOrEmpty(note3Value))
                sb.AppendFormat("{0}: {1}", note4Caption, note4Value);

            sb.AppendLine();

            multiNoteBE.Notes = sb.ToString();
            multiNoteBE.UserType = "CREATED_FROM_MULTINOTE_CONTROL";
            if (string.IsNullOrEmpty(multiNoteBE.CreatedBy))
                multiNoteBE.CreatedBy = "Administrator";
            if (string.IsNullOrEmpty(multiNoteBE.ModifiedBy))
                multiNoteBE.ModifiedBy = "Administrator";

            multiNoteBE.DateCreated = DateTime.Now;
            multiNoteBE.DateModified = DateTime.Now;

            return multiNoteBE;
        }
        #endregion


        #region Import MultiNotes

        #region Import One Notes
        private static void ImportOneNote(object o)
        {
            try
            {
                object[] objs = o as object[];
                int index = (int)objs[0];
                MultiNoteBE multiNote = objs[1] as MultiNoteBE;

                List<SqlParameter> paraList = new List<SqlParameter>();
                SqlParameter noteParam = new SqlParameter();
                noteParam.ParameterName = "@MultiNoteGuid";
                noteParam.DbType = DbType.Guid;
                noteParam.Direction = ParameterDirection.InputOutput;
                if (multiNote.MultiNoteGuid == Guid.Empty) //New Row
                {
                    noteParam.Value = DBNull.Value;
                }
                else
                {
                    noteParam.Value = multiNote.MultiNoteGuid;
                }
                paraList.Add(noteParam);

                paraList.Add(new SqlParameter("@SourceID", multiNote.SourceID));
                paraList.Add(new SqlParameter("@Notes", multiNote.Notes));
                paraList.Add(new SqlParameter("@DateCreated", multiNote.DateCreated));
                paraList.Add(new SqlParameter("@DateModified", multiNote.DateModified));
                paraList.Add(new SqlParameter("@CreatedBy", multiNote.CreatedBy));

                paraList.Add(new SqlParameter("@ModifiedBy", multiNote.ModifiedBy));
                paraList.Add(new SqlParameter("@MasterTable", multiNote.MasterTable));
                paraList.Add(new SqlParameter("@UserType", multiNote.UserType));
                paraList.Add(new SqlParameter("@MasterGUID", multiNote.MasterGUID));

                SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_MultiNotes]", paraList.ToArray());
                _resetEvents[index].Set();
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError("MultiNotes.ImportOneNote", ex);
                throw new Exception("Failed to import MultiNotes", ex);
            }
            finally
            {
            }

        }
        #endregion

        #region Import Multi Notes
        private void ImportMultiNotes(IDataReader dr)
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;
            int tempHostRowID = 0;
            int i = 0;
            _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS_MEDIA];

            #region While Begin
            while (dr.Read())
            {

                MultiNoteBE noteBE = GetOneMultiNote(dr);

                int index = i % ThreadHelper.NUMBER_OF_THREADS_MEDIA;
                object[] objs = new object[2];
                objs[0] = index;
                objs[1] = noteBE;

                _resetEvents[index] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneNote), objs);
                if (index == ThreadHelper.NUMBER_OF_THREADS_MEDIA - 1)
                {
                    ThreadHelper.WaitAll(_resetEvents);
                    _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS_MEDIA];

                }
                if (this.ImportedRowEvent != null)
                {
                    ImportedRowEvent(++i, new ImportEventArgs("Importing MultiNotes..."));
                }
                if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                {
                    break;
                }

            }
            ThreadHelper.WaitAll(_resetEvents);
          
            #endregion While End

        }
        public void ImportMultiNotes()
        {
            string sourceSQL = "[dbo].[__iTrakImporter_sps_MultiNotes]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                ImportMultiNotes(dr); //The first one is the subject result set
               
                if (this.ImportCompletedEvent != null)
                {
                    ImportCompletedEvent(_count, new ImportEventArgs("Completed MultiNotes"));
                }
            }
        }
        #endregion

        #endregion

    }
}
