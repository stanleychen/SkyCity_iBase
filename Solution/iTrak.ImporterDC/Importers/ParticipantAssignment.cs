using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

using Microsoft.ApplicationBlocks.Data;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;
namespace iTrak.Importer.Data.Importers
{
    public class ParticipantAssignment
    {
        public event EventHandler ImportedRowEvent;
        public event EventHandler ImportCompletedEvent;
        private int _count = 0;
        public int Count
        {
            get
            {
                if (_count == 0)
                    _count = GetSourceRowCount();
                return _count;
            }
        }
        #region GetSourceRowCount
        private int GetSourceRowCount()
        {
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_ParticipantAssignment (nolock)";
            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;

            return rowCount;
        }
        #endregion

        #region Import ParticpantAssigments
        public void ImportOneRecord(ParticipantAssignmentBE pAssignmentBE)
        {
            try
            {
                if (string.IsNullOrEmpty(pAssignmentBE.ParticipantRole))
                {
                    pAssignmentBE.ParticipantRole = "Unknown";
                }
                if (pAssignmentBE.Assigned == null || pAssignmentBE.Assigned == DateTime.MinValue)
                {
                    pAssignmentBE.Assigned = DateTime.Now;
                }
                pAssignmentBE.AssignedBy = DataHelper.GetUserName(pAssignmentBE.AssignedBy);

                SqlParameter[] parameters = new SqlParameter[]
			    {
				    new SqlParameter("@DetailedReportGuid", pAssignmentBE.DetailedReportGuid),
				    new SqlParameter("@ParticipantGuid", pAssignmentBE.ParticipantGuid),
				    new SqlParameter("@Assigned", pAssignmentBE.Assigned),
				    new SqlParameter("@AssignedBy", pAssignmentBE.AssignedBy),
				    new SqlParameter("@ParticipantType", pAssignmentBE.ParticipantType),              
				    new SqlParameter("@ParticipantRole", pAssignmentBE.ParticipantRole),
				    new SqlParameter("@ParticipantNotes", pAssignmentBE.ParticipantNotes),
				    new SqlParameter("@SecondaryRole", pAssignmentBE.SecondaryRole),
				    new SqlParameter("@PoliceContacted", pAssignmentBE.PoliceContacted),
				    new SqlParameter("@TakenFromScene", pAssignmentBE.TakenFromScene),
				    new SqlParameter("@PoliceContactedResult", pAssignmentBE.PoliceContactedResult),
                    new SqlParameter("@IsNew",pAssignmentBE.IsNew)
			    };
                SqlHelper.ExecuteNonQuery(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, "__iTrakImporter_spiu_ParticipantAssignment", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Duplicated Row: IncidentGuid=" + pAssignmentBE.DetailedReportGuid + "; ParticipantGuid=" + pAssignmentBE.ParticipantGuid, ex);       
            }
        }

        #region CombineParticipantAssignments
        private List<ParticipantAssignmentBE> CombineParticipantAssignments(IList<ParticipantAssignmentBE> pList)
        {
            StringBuilder sb = new StringBuilder();
            ParticipantAssignmentBE p = pList[0];
            sb.Append(p.ParticipantNotes);
            if (pList.Count > 1) //process second participant assignments
            {
                if (string.IsNullOrEmpty(p.SecondaryRole))
                {
                    if (!string.IsNullOrEmpty(pList[1].ParticipantRole))
                        p.SecondaryRole = pList[1].ParticipantRole;
                }
                if (p.ParticipantNotes != pList[1].ParticipantNotes)
                {
                    sb.Append(System.Environment.NewLine);
                    sb.Append(pList[1].ParticipantNotes);
                }
            }
            for (int i = 2; i < pList.Count; i++)
            {
                sb.Append(System.Environment.NewLine);
                sb.Append("ParticipantRole: " + pList[i].ParticipantRole);
                sb.Append(System.Environment.NewLine);
                sb.Append("Notes: " + pList[i].ParticipantNotes);
            }

            p.ParticipantNotes = sb.ToString();

            List<ParticipantAssignmentBE> returnList = new List<ParticipantAssignmentBE>();
            returnList.Add(p);

            return returnList;
        }
        #endregion

        #region GetOneParticipantAssignment
        private ParticipantAssignmentBE GetOneParticipantAssignment(IDataReader dr)
        {
            ParticipantAssignmentBE pAssignment = new ParticipantAssignmentBE();
            pAssignment.DetailedReportGuid = DataHelper.GetGuid(dr["DetailedReportGuid"]);
            pAssignment.ParticipantGuid = DataHelper.GetGuid(dr["ParticipantGuid"]);
            pAssignment.ParticipantNotes = dr["ParticipantNotes"].ToString();
            pAssignment.ParticipantRole = dr["ParticipantRole"].ToString();
            pAssignment.ParticipantType = dr["ParticipantType"].ToString();
            pAssignment.PoliceContacted = DataHelper.GetBool(dr["PoliceContacted"]);
            pAssignment.PoliceContactedResult = dr["PoliceContactedResult"].ToString();
            pAssignment.SecondaryRole = dr["SecondaryRole"].ToString();
            pAssignment.TakenFromScene = DataHelper.GetBool(dr["TakenFromScene"]);
            pAssignment.Assigned = DataHelper.GetDateTime(dr["Assigned"]);
            pAssignment.AssignedBy = dr["AssignedBy"].ToString();

            if (string.IsNullOrEmpty(pAssignment.AssignedBy))
                pAssignment.AssignedBy = "Import";
            if (pAssignment.ParticipantType == "Employee")
                pAssignment.ParticipantType = "Personnel";

            if (dr["ParticipantAssignment_DetailedReportGUID"] == DBNull.Value
                || dr["ParticipantAssignment_ParticipantGUID"] == DBNull.Value)
                pAssignment.IsNew = true;
            else
                pAssignment.IsNew = false;

            return pAssignment;
        }
        #endregion

        public void ImportParticpantAssigments()
        {
            string sourceSQL = "__iTrakImporter_sps_ParticipantAssignment";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {


                try
                {
                    int i = 0;
                    List<ParticipantAssignmentBE> pList = new List<ParticipantAssignmentBE>();
                    while (dr.Read())
                    {
                        ParticipantAssignmentBE assignment = GetOneParticipantAssignment(dr);
                        pList.Add(assignment);
                        if (pList.Count > 1)
                        {

                            if (pList[0].UniqueID != pList[1].UniqueID)
                            {
                                this.ImportOneRecord(pList[0]);
                                pList.Remove(pList[0]); //remove from the list from imported
                            }
                            else //combine to one and continue read next one
                            {
                                pList = CombineParticipantAssignments(pList);
                            }
                        }
                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing ParticipantAssignment"));
                        }
                    }
                    if (pList.Count > 0) //imported remaining
                    {
                        this.ImportOneRecord(pList[0]);
                        pList.Remove(pList[0]); //remove from the list from imported
                    }


                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed Participant Assignments"));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {

                }
            }
        }
        #endregion
    }
}
