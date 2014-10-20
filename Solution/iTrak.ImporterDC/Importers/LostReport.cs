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

    public class LostReport
    {
        private const string DEFAULT_OPERATOR = "Administrator";
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_LostFoundLostReport (nolock)";

            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;
            return rowCount;
        }
        #endregion

        #region Get One LostReport
        private LostFoundLostReportBE GetOneLostReport(IDataReader dr)
        {
            LostFoundLostReportBE lostReportBE = new LostFoundLostReportBE();
            lostReportBE.LostGUID = SqlClientUtility.GetGuid(dr, "LostGUID", Guid.Empty);
            lostReportBE.LostUID = SqlClientUtility.GetString(dr, "LostUID", String.Empty);
            lostReportBE.ItemCategory = SqlClientUtility.GetString(dr, "ItemCategory", String.Empty);
            lostReportBE.Colour = SqlClientUtility.GetString(dr, "Colour", String.Empty);
            lostReportBE.SerialNumber = SqlClientUtility.GetString(dr, "SerialNumber", String.Empty);
            lostReportBE.ItemValue = SqlClientUtility.GetDecimal(dr, "ItemValue", Decimal.Zero);
            lostReportBE.Material = SqlClientUtility.GetString(dr, "Material", String.Empty);
            lostReportBE.Manufacturer = SqlClientUtility.GetString(dr, "Manufacturer", String.Empty);
            lostReportBE.AgeYrs = SqlClientUtility.GetInt32(dr, "AgeYrs", 0);
            lostReportBE.AgeMonths = SqlClientUtility.GetInt32(dr, "AgeMonths", 0);
            lostReportBE.KeyWords = SqlClientUtility.GetString(dr, "KeyWords", String.Empty);
            lostReportBE.LostPropertyGUID = SqlClientUtility.GetGuid(dr, "LostPropertyGUID", Guid.Empty);
            lostReportBE.LostRoomNumber = SqlClientUtility.GetString(dr, "LostRoomNumber", String.Empty);
            lostReportBE.WhenLost = SqlClientUtility.GetDateTime(dr, "whenLost", DateTime.Now);
            lostReportBE.ReportedAsStolen = SqlClientUtility.GetBoolean(dr, "ReportedAsStolen", false);
            lostReportBE.Contents = SqlClientUtility.GetString(dr, "Contents", String.Empty);
            lostReportBE.Description = SqlClientUtility.GetString(dr, "Description", String.Empty);
            lostReportBE.ReportByContactGUID = SqlClientUtility.GetGuid(dr, "ReportByContactGUID", Guid.Empty);
            lostReportBE.Owner = SqlClientUtility.GetBoolean(dr, "Owner", false);
            lostReportBE.AlternateContactGUID = SqlClientUtility.GetGuid(dr, "AlternateContactGUID", Guid.Empty);
            lostReportBE.HotelGuest = SqlClientUtility.GetBoolean(dr, "HotelGuest", false);
            lostReportBE.GuestPropertyGUID = SqlClientUtility.GetGuid(dr, "GuestPropertyGUID", Guid.Empty);
            lostReportBE.GuestRoom = SqlClientUtility.GetString(dr, "GuestRoom", String.Empty);
            lostReportBE.PoliceReportFiled = SqlClientUtility.GetBoolean(dr, "PoliceReportFiled", false);
            lostReportBE.PoliceReportNumber = SqlClientUtility.GetString(dr, "PoliceReportNumber", String.Empty);
            lostReportBE.PoliceReportOfficer = SqlClientUtility.GetString(dr, "PoliceReportOfficer", String.Empty);
            lostReportBE.PoliceReportLocation = SqlClientUtility.GetString(dr, "PoliceReportLocation", String.Empty);
            lostReportBE.InsuredByCompanyGUID = SqlClientUtility.GetGuid(dr, "InsuredByCompanyGUID", Guid.Empty);
            lostReportBE.FollowUp = SqlClientUtility.GetBoolean(dr, "FollowUp", false);
            lostReportBE.FollowUpDate = SqlClientUtility.GetDateTime(dr, "FollowUpDate", DateTime.Now);
            lostReportBE.Notes = SqlClientUtility.GetString(dr, "Notes", String.Empty);
            lostReportBE.Operator = SqlClientUtility.GetString(dr, "Operator", String.Empty);
            lostReportBE.DateCreated = SqlClientUtility.GetDateTime(dr, "DateCreated", DateTime.Now);
            lostReportBE.LostLocation = SqlClientUtility.GetString(dr, "LostLocation", String.Empty);
            lostReportBE.Sublocation = SqlClientUtility.GetString(dr, "Sublocation", String.Empty);
            lostReportBE.ModifiedBy = SqlClientUtility.GetString(dr, "ModifiedBy", String.Empty);
            lostReportBE.DateModified = SqlClientUtility.GetDateTime(dr, "DateModified", DateTime.Now);
            lostReportBE.IsReturned = SqlClientUtility.GetBoolean(dr, "IsReturned", false);
            lostReportBE.UString = SqlClientUtility.GetString(dr, "uString", string.Empty);
            lostReportBE.UText1Caption = SqlClientUtility.GetString(dr, "uText1Caption", string.Empty);
            lostReportBE.UText1Value = SqlClientUtility.GetString(dr, "uText1Value", string.Empty);

            if (lostReportBE.UString != string.Empty)
            {
                lostReportBE.Description = lostReportBE.Description.ToString() + System.Environment.NewLine + lostReportBE.UString;
            }

            if (lostReportBE.UText1Caption != string.Empty || lostReportBE.UText1Value != string.Empty)
            {
                lostReportBE.Description = lostReportBE.Description.ToString() + System.Environment.NewLine + lostReportBE.UText1Caption + ": " + lostReportBE.UText1Value;
            }
            return lostReportBE;
        }
        #endregion

        #region Get One FoundReport
        /// <summary>
        /// Get one found report from the lost information
        /// </summary>
        /// <param name="lostReportBE"></param>
        /// <returns></returns>
        private LostFoundFoundReportBE GetOneFoundReport(IDataReader dr, LostFoundLostReportBE lostReportBE)
        {
            LostFoundFoundReportBE foundBE = new LostFoundFoundReportBE();
            foundBE.FoundItemGUID = SqlClientUtility.GetGuid(dr, "FoundReportGUID", Guid.Empty);
            if (foundBE.FoundItemGUID == Guid.Empty)
                foundBE.IsNew = true;
            foundBE.FoundDateTime = lostReportBE.WhenLost;
            foundBE.DateCreated = lostReportBE.DateCreated;
            foundBE.DateModified = lostReportBE.DateModified;
            foundBE.ModifiedBy = lostReportBE.ModifiedBy;
            foundBE.Operator = lostReportBE.Operator;
            foundBE.FoundUID = lostReportBE.LostUID;

            return foundBE;
        }
        #endregion

        #region Get One Return Report
        /// <summary>
        /// Get one Retuned report from the lost information
        /// </summary>
        /// <param name="lostReportBE"></param>
        /// <returns></returns>
        private LostFoundReturnVerificationBE GetOneReturnedReport(IDataReader dr, LostFoundLostReportBE lostReportBE)
        {
            LostFoundReturnVerificationBE retunedReportBE = new LostFoundReturnVerificationBE();
            retunedReportBE.FoundReportGUID = SqlClientUtility.GetGuid(dr, "FoundReportGUID", Guid.Empty);
            if (retunedReportBE.FoundReportGUID == Guid.Empty)
                retunedReportBE.IsNew = true;
            retunedReportBE.DateCreated = lostReportBE.DateCreated;
            retunedReportBE.Operator = lostReportBE.Operator;
            retunedReportBE.ReturnDate = lostReportBE.WhenLost;

            return retunedReportBE;
        }
        #endregion

        #region Import one Lost Report
        private static Guid ImportOneLostReport(SqlTransaction trans, LostFoundLostReportBE lostBE)
        {
            List<SqlParameter> paraList = new List<SqlParameter>();
            SqlParameter keyParam = new SqlParameter();
            keyParam.ParameterName = "@LostGUID";
            keyParam.SqlDbType = SqlDbType.UniqueIdentifier;
            keyParam.Direction = ParameterDirection.InputOutput;
            if (lostBE.LostGUID == Guid.Empty)
                keyParam.Value = DBNull.Value;
            else
                keyParam.Value = lostBE.LostGUID;
            paraList.Add(keyParam);
			
			paraList.Add(new SqlParameter("@LostUID", lostBE.LostUID));
			paraList.Add(new SqlParameter("@ItemCategory", lostBE.ItemCategory));
			paraList.Add(new SqlParameter("@Colour", lostBE.Colour));
			paraList.Add(new SqlParameter("@SerialNumber", lostBE.SerialNumber));
			paraList.Add(new SqlParameter("@ItemValue", lostBE.ItemValue));
			paraList.Add(new SqlParameter("@Material", lostBE.Material));
			paraList.Add(new SqlParameter("@Manufacturer", lostBE.Manufacturer));
			paraList.Add(new SqlParameter("@AgeYrs", lostBE.AgeYrs));
			paraList.Add(new SqlParameter("@AgeMonths", lostBE.AgeMonths));
			paraList.Add(new SqlParameter("@KeyWords", lostBE.KeyWords));
            if(lostBE.LostPropertyGUID != Guid.Empty)
			    paraList.Add(new SqlParameter("@LostPropertyGUID", lostBE.LostPropertyGUID));
			paraList.Add(new SqlParameter("@LostRoomNumber", lostBE.LostRoomNumber));
			paraList.Add(new SqlParameter("@whenLost", lostBE.WhenLost));
			paraList.Add(new SqlParameter("@ReportedAsStolen", lostBE.ReportedAsStolen));
			paraList.Add(new SqlParameter("@Contents", lostBE.Contents));
			paraList.Add(new SqlParameter("@Description", lostBE.Description));
            if(lostBE.ReportByContactGUID != Guid.Empty)
			    paraList.Add(new SqlParameter("@ReportByContactGUID", lostBE.ReportByContactGUID));
			paraList.Add(new SqlParameter("@Owner", lostBE.Owner));
            if(lostBE.AlternateContactGUID != Guid.Empty)
			    paraList.Add(new SqlParameter("@AlternateContactGUID", lostBE.AlternateContactGUID));
			paraList.Add(new SqlParameter("@HotelGuest", lostBE.HotelGuest));
            if(lostBE.GuestPropertyGUID != Guid.Empty)
			    paraList.Add(new SqlParameter("@GuestPropertyGUID", lostBE.GuestPropertyGUID));
			paraList.Add(new SqlParameter("@GuestRoom", lostBE.GuestRoom));
			paraList.Add(new SqlParameter("@PoliceReportFiled", lostBE.PoliceReportFiled));
			paraList.Add(new SqlParameter("@PoliceReportNumber", lostBE.PoliceReportNumber));
			paraList.Add(new SqlParameter("@PoliceReportOfficer", lostBE.PoliceReportOfficer));
			paraList.Add(new SqlParameter("@PoliceReportLocation", lostBE.PoliceReportLocation));
            if(lostBE.InsuredByCompanyGUID != Guid.Empty)
			    paraList.Add(new SqlParameter("@InsuredByCompanyGUID", lostBE.InsuredByCompanyGUID));
			paraList.Add(new SqlParameter("@FollowUp", lostBE.FollowUp));
			paraList.Add(new SqlParameter("@FollowUpDate", lostBE.FollowUpDate));
			paraList.Add(new SqlParameter("@Notes", lostBE.Notes));
			paraList.Add(new SqlParameter("@Operator", lostBE.Operator));
			paraList.Add(new SqlParameter("@DateCreated", lostBE.DateCreated));
			paraList.Add(new SqlParameter("@LostLocation", lostBE.LostLocation));
			paraList.Add(new SqlParameter("@Sublocation", lostBE.Sublocation));
			paraList.Add(new SqlParameter("@ModifiedBy", lostBE.ModifiedBy));
            paraList.Add(new SqlParameter("@DateModified", lostBE.DateModified));

            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "__iTrakImporter_spiu_LostFoundLostReport", paraList.ToArray());

            return (Guid)keyParam.Value;
        }
        private static void ImportOneLostReport(object o)
        {
            object[] objs = o as object[];
            int index = (int)objs[0];

            LostFoundLostReportBE lostBE = objs[1] as LostFoundLostReportBE;
            LostFoundFoundReportBE foundBE = objs[2] as LostFoundFoundReportBE;
            LostFoundReturnVerificationBE returnBE = objs[3] as LostFoundReturnVerificationBE;

            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(DbHelper.iTrakConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                Guid lostGuid = ImportOneLostReport(trans, lostBE);
                if (returnBE != null)
                {
                    returnBE.LostReportGUID = lostGuid;
                    if (foundBE != null)
                    {
                        Guid foundGuid = FoundReport.ImportOneFoundReport(trans, foundBE);
                        if (returnBE.FoundReportGUID == Guid.Empty)
                            returnBE.FoundReportGUID = foundGuid;
                    }
                    ReturnVerification.ImportOneReturnVerification(trans, returnBE);
                }

                trans.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception("Failed to import one found", ex);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
               
                _resetEvents[index].Set();
            }
        }
        #endregion

        #region Import Lost Reports
        public void ImportLostReports()
        {
            int testingRow = 0;
            if (DataHelper.IsTesting() == true)
                testingRow = DataHelper.TestingRows;
            string sourceSQL = "[dbo].[__iTrakImporter_sps_LostFoundLostReport]";
            using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
            {
                int i = 0;
                _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                List<int> importedWatchSubjects = new List<int>();

                List<string> typeOfBanList = new List<string>();
                #region While Begin
                while (dr.Read())
                {
                    LostFoundLostReportBE lostBE = GetOneLostReport(dr);
                    
                    LostFoundReturnVerificationBE returnBE = null;
                    LostFoundFoundReportBE foundReportBE = null;

                    if (lostBE.IsReturned)
                    {
                        foundReportBE = GetOneFoundReport(dr,lostBE);//Create one found for the return verification
                        returnBE = GetOneReturnedReport(dr,lostBE); //check if it is returned
                    }

                    int index = i % ThreadHelper.NUMBER_OF_THREADS;
                    object[] objs = new object[4];
                    objs[0] = index;
                    objs[1] = lostBE;
                    objs[2] = foundReportBE;
                    objs[3] = returnBE;

                    _resetEvents[index] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneLostReport), objs);
                    if (index == ThreadHelper.NUMBER_OF_THREADS - 1)
                    {
                        ThreadHelper.WaitAll(_resetEvents);
                        _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];

                    }
                    if (this.ImportedRowEvent != null)
                    {
                        ImportedRowEvent(++i, new ImportEventArgs("Importing lost reports..."));
                    }
                    if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                    {
                        break;
                    }

                }
                ThreadHelper.WaitAll(_resetEvents);

                if (this.ImportCompletedEvent != null)
                {
                    ImportCompletedEvent(_count, new ImportEventArgs("Completed lost reports"));
                }
                #endregion While End

            }
        }

        #endregion

    }
}
