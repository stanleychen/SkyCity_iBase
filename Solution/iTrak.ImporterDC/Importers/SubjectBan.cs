using System;
using System.Collections.Generic;
using System.Text;

using System.Threading;
using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;


namespace iTrak.Importer.Data.Importers
{
    public class SubjectBan
    {
        private const string DEFAULT_IDENTIFICATION_USED = null;
        private const string DEFAULT_REASON_FOR_BAN = null;
        private const string DEFAULT_TYPE_OF_BAN = "Voluntary";
        private const string DEFAULT_PARTICIPANT_ROLE = "Suspect/Offender";
        private const string DEFAULT_CREATED_BY = "Administrator";
        private static ManualResetEvent[] _resetEvents;
        public event EventHandler ImportedRowEvent;
        public event EventHandler ImportCompletedEvent;
    
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_SubjectBan (nolock)";

            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;
            return rowCount;
        }
        #endregion

        #region Get one ban

        #region GetOneBanWatchStatusBE
        private BanWatchStatusBE GetOneBanWatchStatusBE(IDataReader dr)
        {
            BanWatchStatusBE banWatchStatusBE = new BanWatchStatusBE();
            banWatchStatusBE.SubjectGUID = SqlClientUtility.GetGuid(dr, "SubjectGUID", Guid.Empty);
            banWatchStatusBE.Status = 1;
            banWatchStatusBE.Commencement = SqlClientUtility.GetDateTime(dr, "Commencement", DateTime.Now);
            banWatchStatusBE.EndDate = SqlClientUtility.GetDateTime(dr, "EndDate", DateTime.MinValue);
            banWatchStatusBE.IsPermanent = SqlClientUtility.GetBoolean(dr, "IsPermanent", false);
            banWatchStatusBE.TypeOfBan = SqlClientUtility.GetString(dr, "TypeOfBan", String.Empty);
            banWatchStatusBE.ReasonForBan = SqlClientUtility.GetString(dr, "ReasonForBan", String.Empty);
            banWatchStatusBE.DetailedReportGUID = SqlClientUtility.GetGuid(dr, "DetailedReportGUID", Guid.Empty);

            if (dr["BanwatchSubjectGuid"] == DBNull.Value)
                banWatchStatusBE.IsNew = true;

            if (banWatchStatusBE.EndDate == DateTime.MinValue)
            {
                banWatchStatusBE.IsPermanent = true;
                banWatchStatusBE.EndDate = DateTime.Now.AddMonths(1);
            }

            if (string.IsNullOrEmpty(banWatchStatusBE.TypeOfBan))
                banWatchStatusBE.TypeOfBan = DEFAULT_TYPE_OF_BAN;

            return banWatchStatusBE;
        }
        #endregion

        #region GetOneSubjectBanBE
        private SubjectBanBE GetOneSubjectBanBE(IDataReader dr)
        {
            SubjectBanBE subjectBanBE = new SubjectBanBE();
            subjectBanBE.SubjectGUID = SqlClientUtility.GetGuid(dr, "SubjectGUID", Guid.Empty);
            subjectBanBE.DetailedReportGUID = SqlClientUtility.GetGuid(dr, "DetailedReportGUID", Guid.Empty);
            subjectBanBE.Commencement = SqlClientUtility.GetDateTime(dr, "Commencement", DateTime.Now);
            subjectBanBE.IsPermanent = SqlClientUtility.GetBoolean(dr, "IsPermanent", false);
            subjectBanBE.EndDate = SqlClientUtility.GetDateTime(dr, "EndDate", DateTime.MinValue);
            subjectBanBE.SubjectCharged = SqlClientUtility.GetBoolean(dr, "SubjectCharged", false);
            subjectBanBE.LetterSent = SqlClientUtility.GetBoolean(dr, "LetterSent", false);
            subjectBanBE.CompulsiveGambler = SqlClientUtility.GetBoolean(dr, "CompulsiveGambler", false);
            subjectBanBE.RecordType = SqlClientUtility.GetInt32(dr, "RecordType", 0);
            subjectBanBE.TypeOfBan = SqlClientUtility.GetString(dr, "TypeOfBan", String.Empty);
            subjectBanBE.IdentificationUsed = SqlClientUtility.GetString(dr, "IdentificationUsed", String.Empty);
            subjectBanBE.ReasonForBan = SqlClientUtility.GetString(dr, "ReasonForBan", String.Empty);
            subjectBanBE.SelfExclusiveReport = SqlClientUtility.GetBytes(dr, "SelfExclusiveReport", null);
            subjectBanBE.RemovedBanIncidentGuid = SqlClientUtility.GetGuid(dr, "RemovedBanIncidentGuid", Guid.Empty);
            subjectBanBE.OriginalBanIncidentGuid = SqlClientUtility.GetGuid(dr, "OriginalBanIncidentGuid", Guid.Empty);
            subjectBanBE.Removed = SqlClientUtility.GetBoolean(dr, "Removed", false);
            subjectBanBE.RemovalDate = SqlClientUtility.GetDateTime(dr, "RemovalDate", DateTime.MinValue);
            subjectBanBE.IncidentRowID = (int)dr["IncidentRowID"];
            subjectBanBE.SubjectRowID = (int)dr["SubjectRowID"];

            if (dr["BanIncidentGuid"] == DBNull.Value || dr["BanSubjectGuid"] == DBNull.Value)
                subjectBanBE.IsNew = true;
            else
                subjectBanBE.IsNew = false;

            if (dr["ParticipantGuid"] == DBNull.Value || dr["PDetailedReportGuid"] == DBNull.Value)
            {
                subjectBanBE.HasParticipantAssignment = false;
            }
            else
            {
                subjectBanBE.HasParticipantAssignment = true;
            }
            //set default value
            if (subjectBanBE.EndDate == DateTime.MinValue)
            {
                subjectBanBE.IsPermanent = true;
                subjectBanBE.EndDate = DateTime.Now.AddMonths(1);
            }

            if (string.IsNullOrEmpty(subjectBanBE.TypeOfBan))
                subjectBanBE.TypeOfBan = DEFAULT_TYPE_OF_BAN;

            if (string.IsNullOrEmpty(subjectBanBE.IdentificationUsed))
                subjectBanBE.IdentificationUsed = DEFAULT_IDENTIFICATION_USED;

            if (string.IsNullOrEmpty(subjectBanBE.ReasonForBan))
                subjectBanBE.ReasonForBan = DEFAULT_REASON_FOR_BAN;
            return subjectBanBE;
        }
        #endregion

        #endregion

        #region Import one Ban
        private static void ImportOneWatch(SqlTransaction trans, BanWatchStatusBE watchBE)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
        	paraList.Add(new SqlParameter("@SubjectGUID", watchBE.SubjectGUID));
			paraList.Add(new SqlParameter("@Status", watchBE.Status));
			paraList.Add(new SqlParameter("@Commencement", watchBE.Commencement));
			paraList.Add(new SqlParameter("@EndDate", watchBE.EndDate));
			paraList.Add(new SqlParameter("@IsPermanent", watchBE.IsPermanent));
			paraList.Add(new SqlParameter("@TypeOfBan", watchBE.TypeOfBan));
			paraList.Add(new SqlParameter("@ReasonForBan", watchBE.ReasonForBan));
			paraList.Add(new SqlParameter("@DetailedReportGUID", watchBE.DetailedReportGUID));
            paraList.Add(new SqlParameter("@IsNew",watchBE.IsNew));

            SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_BanWatchStatus]", paraList.ToArray());
        }
        #region ImportOneBan
        private static void ImportOneAssignment(SqlTransaction trans, SubjectBanBE banBE)
        {
            ParticipantAssignmentBE p = new ParticipantAssignmentBE();
            p.ParticipantGuid = banBE.SubjectGUID;
            p.DetailedReportGuid = banBE.DetailedReportGUID;
            p.Assigned = banBE.Commencement;
            p.AssignedBy = DEFAULT_CREATED_BY;
            p.ParticipantType = "Subject";
            p.ParticipantRole = DEFAULT_PARTICIPANT_ROLE;
            p.IsNew = true;

            ParticipantAssignment assignment = new ParticipantAssignment();
            assignment.ImportOneRecord(p);

        }
        private static void ImportOneBan(object o)
        {
            object[] objs = o as object[];
            int index = (int)objs[0];
            SubjectBanBE banBE = objs[1] as SubjectBanBE;
            BanWatchStatusBE watchBE = objs[2] as BanWatchStatusBE;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(DbHelper.iTrakConnectionString);
                conn.Open();
                trans =  conn.BeginTransaction();
        
                if (!banBE.HasParticipantAssignment)
                {
                    ImportOneAssignment(trans, banBE);
                }
                List<SqlParameter> paraList = new List<SqlParameter>();
                paraList.Add(new SqlParameter("@SubjectGUID", banBE.SubjectGUID));
                paraList.Add(new SqlParameter("@DetailedReportGUID", banBE.DetailedReportGUID));
                paraList.Add(new SqlParameter("@Commencement", banBE.Commencement));
                paraList.Add(new SqlParameter("@IsPermanent", banBE.IsPermanent));
                paraList.Add(new SqlParameter("@EndDate", banBE.EndDate));
                paraList.Add(new SqlParameter("@SubjectCharged", banBE.SubjectCharged));
                paraList.Add(new SqlParameter("@LetterSent", banBE.LetterSent));
                paraList.Add(new SqlParameter("@CompulsiveGambler", banBE.CompulsiveGambler));
                paraList.Add(new SqlParameter("@RecordType", banBE.RecordType));
                paraList.Add(new SqlParameter("@TypeOfBan", banBE.TypeOfBan));
                paraList.Add(new SqlParameter("@IdentificationUsed", banBE.IdentificationUsed));
                paraList.Add(new SqlParameter("@ReasonForBan", banBE.ReasonForBan));
                if (banBE.SelfExclusiveReport != null)
                    paraList.Add(new SqlParameter("@SelfExclusiveReport", banBE.SelfExclusiveReport));
                if (banBE.OriginalBanIncidentGuid != Guid.Empty)
                    paraList.Add(new SqlParameter("@RemovedBanIncidentGuid", banBE.RemovedBanIncidentGuid));
                if (banBE.OriginalBanIncidentGuid != Guid.Empty)
                    paraList.Add(new SqlParameter("@OriginalBanIncidentGuid", banBE.OriginalBanIncidentGuid));
                paraList.Add(new SqlParameter("@Removed", banBE.Removed));
                if (banBE.RemovalDate > DataHelper.SQL_MIN_DATE)
                    paraList.Add(new SqlParameter("@RemovalDate", banBE.RemovalDate));

                paraList.Add(new SqlParameter("@IsNew", banBE.IsNew));

                SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "[dbo].[__iTrakImporter_spiu_SubjectBan]", paraList.ToArray());

                if (watchBE != null)
                    ImportOneWatch(trans, watchBE);

                trans.Commit();
                conn.Close();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception("Failed to import subject ban", ex);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
                _resetEvents[index].Set();
            }
        }
        #endregion


        #endregion

        #region Import Subject Bans
        public void ImportSubjectBans()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;
            string sourceSQL = "[dbo].[__iTrakImporter_sps_SubjectBan]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                int i = 0;
                _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                List<int> importedWatchSubjects = new List<int>();

                List<string> typeOfBanList = new List<string>();
                #region While Begin
                while (dr.Read())
                {

                    SubjectBanBE banBE = GetOneSubjectBanBE(dr);
                    BanWatchStatusBE watchBE = null;
                    if (!importedWatchSubjects.Contains(banBE.SubjectRowID))
                    {
                        importedWatchSubjects.Add(banBE.SubjectRowID);
                        watchBE = GetOneBanWatchStatusBE(dr);
                    }
                    int index = i % ThreadHelper.NUMBER_OF_THREADS;
                    object[] objs = new object[3];
                    objs[0] = index;
                    objs[1] = banBE;
                    objs[2] = watchBE;

                    if (!typeOfBanList.Contains(banBE.TypeOfBan))
                    {
                        typeOfBanList.Add(banBE.TypeOfBan);
                        DropdownSelection.AddOneDropdownSelection("TypeOfBan",banBE.TypeOfBan);
                    }
                    _resetEvents[index] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneBan), objs);
                    if (index == ThreadHelper.NUMBER_OF_THREADS - 1)
                    {
                        ThreadHelper.WaitAll(_resetEvents);
                        _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];

                    }
                    if (this.ImportedRowEvent != null)
                    {
                        ImportedRowEvent(++i, new ImportEventArgs("Importing subject bans..."));
                    }
                    if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                    {
                        break;
                    }

                }
                ThreadHelper.WaitAll(_resetEvents);

                if (this.ImportCompletedEvent != null)
                {
                    ImportCompletedEvent(_count, new ImportEventArgs("Completed Subject Bans"));
                }
                #endregion While End

            }
        }

        #endregion


    }
}
