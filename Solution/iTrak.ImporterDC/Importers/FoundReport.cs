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
    public class FoundReport
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
            string sqlText = "SELECT COUNT(*) FROM __iTrakImporter_LostFoundFoundReport (nolock)";

            int rowCount = (int)SqlHelper.ExecuteScalar(DbHelper.iTrakConnectionString, CommandType.Text, sqlText);
            if (DataHelper.IsTesting() == true && rowCount > DataHelper.TestingRows)
                rowCount = DataHelper.TestingRows;
            return rowCount;
        }
        #endregion

        #region Get One Found
        private LostFoundFoundReportBE GetOneFoundReport(IDataReader dr)
        {
            LostFoundFoundReportBE foundReportBE = new LostFoundFoundReportBE();
            foundReportBE.FoundItemGUID = SqlClientUtility.GetGuid(dr, "FoundItemGUID", Guid.Empty);
            foundReportBE.PropertyGUID = SqlClientUtility.GetGuid(dr, "PropertyGUID", Guid.Empty);
            foundReportBE.FoundUID = SqlClientUtility.GetString(dr, "FoundUID", String.Empty);
            foundReportBE.ItemCategory = SqlClientUtility.GetString(dr, "ItemCategory", String.Empty);
            foundReportBE.ItemValue = SqlClientUtility.GetDecimal(dr, "ItemValue", Decimal.Zero);
            foundReportBE.Colour = SqlClientUtility.GetString(dr, "colour", String.Empty);
            foundReportBE.SerialNumber = SqlClientUtility.GetString(dr, "SerialNumber", String.Empty);
            foundReportBE.Material = SqlClientUtility.GetString(dr, "Material", String.Empty);
            foundReportBE.Manufacturer = SqlClientUtility.GetString(dr, "Manufacturer", String.Empty);
            foundReportBE.AgeYrs = SqlClientUtility.GetInt32(dr, "AgeYrs", 0);
            foundReportBE.AgeMonths = SqlClientUtility.GetInt32(dr, "AgeMonths", 0);
            foundReportBE.Contents = SqlClientUtility.GetString(dr, "Contents", String.Empty);
            foundReportBE.Description = SqlClientUtility.GetString(dr, "Description", String.Empty);
            foundReportBE.KeyWords = SqlClientUtility.GetString(dr, "KeyWords", String.Empty);
            foundReportBE.FoundDateTime = SqlClientUtility.GetDateTime(dr, "FoundDateTime", DateTime.Now);
            foundReportBE.FoundStatus = SqlClientUtility.GetString(dr, "FoundStatus", String.Empty);
            foundReportBE.SpecificLocation = SqlClientUtility.GetString(dr, "SpecificLocation", String.Empty);
            foundReportBE.FoundByContactUID = SqlClientUtility.GetGuid(dr, "FoundByContactUID", Guid.Empty);
            foundReportBE.ReportByContactUID = SqlClientUtility.GetGuid(dr, "ReportByContactUID", Guid.Empty);
            foundReportBE.ReceivedByEmployeeUID = SqlClientUtility.GetGuid(dr, "ReceivedByEmployeeUID", Guid.Empty);
            foundReportBE.StoreLocation = SqlClientUtility.GetString(dr, "StoreLocation", String.Empty);
            foundReportBE.AdditionalInfo = SqlClientUtility.GetString(dr, "AdditionalInfo", String.Empty);
            foundReportBE.HoldUntil = SqlClientUtility.GetDateTime(dr, "HoldUntil", DateTime.MinValue);
            foundReportBE.BestImageGUID = SqlClientUtility.GetGuid(dr, "BestImageGUID", Guid.Empty);
            foundReportBE.Operator = SqlClientUtility.GetString(dr, "Operator", String.Empty);
            foundReportBE.Barcode = SqlClientUtility.GetString(dr, "Barcode", String.Empty);
            foundReportBE.DateCreated = SqlClientUtility.GetDateTime(dr, "DateCreated", DateTime.Now);
            foundReportBE.Department = SqlClientUtility.GetString(dr, "Department", String.Empty);
            foundReportBE.Location = SqlClientUtility.GetString(dr, "Location", String.Empty);
            foundReportBE.Sublocation = SqlClientUtility.GetString(dr, "Sublocation", String.Empty);
            foundReportBE.ModifiedBy = SqlClientUtility.GetString(dr, "ModifiedBy", String.Empty);
            foundReportBE.DateModified = SqlClientUtility.GetDateTime(dr, "DateModified", DateTime.Now);
            foundReportBE.IsDisposed = SqlClientUtility.GetBoolean(dr, "IsDisposed", false);
            foundReportBE.IsReturned = SqlClientUtility.GetBoolean(dr, "IsReturned", false);
            foundReportBE.uString = SqlClientUtility.GetString(dr, "uString", string.Empty);

            if (foundReportBE.uString != string.Empty)
            {
                foundReportBE.Description = foundReportBE.Description.ToString() + System.Environment.NewLine + foundReportBE.uString;
            }

            //set default
            if (foundReportBE.FoundDateTime < DataHelper.SQL_MIN_DATE)
                foundReportBE.FoundDateTime = DateTime.Now;

            if (foundReportBE.DateModified < DataHelper.SQL_MIN_DATE)
                foundReportBE.DateModified = foundReportBE.FoundDateTime;

            if (foundReportBE.DateCreated < DataHelper.SQL_MIN_DATE)
                foundReportBE.DateCreated = foundReportBE.FoundDateTime;

            return foundReportBE;

        }
        #endregion

        #region Get One Return Verification
        private LostFoundReturnVerificationBE GetOneReturnReport(IDataReader dr,LostFoundFoundReportBE foundReportBE)
        {
            LostFoundReturnVerificationBE returnBE = null;
            try
            {
                returnBE = new LostFoundReturnVerificationBE();
                returnBE.FoundReportGUID = SqlClientUtility.GetGuid(dr, "FoundReportGUID", Guid.Empty);
                returnBE.Operator = SqlClientUtility.GetString(dr, "Operator", DEFAULT_OPERATOR);
                returnBE.DateCreated = SqlClientUtility.GetDateTime(dr, "DateCreated", DateTime.Now);
                returnBE.ReturnDate = SqlClientUtility.GetDateTime(dr, "ReturnDate", DateTime.Now);

                if (returnBE.FoundReportGUID == Guid.Empty)
                    returnBE.IsNew = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get one return report", ex);
            }
            finally
            {
            }
            return returnBE;
        }
        #endregion

        #region Get One Disposal
        private LostFoundDisposalReportBE GetOneDisposal(IDataReader dr,LostFoundFoundReportBE foundReportBE)
        {
            LostFoundDisposalReportBE disposalReportBE = new LostFoundDisposalReportBE();
            disposalReportBE.DisposalGUID = SqlClientUtility.GetGuid(dr, "DisposalGUID", Guid.Empty);
            disposalReportBE.FoundReportGUID = SqlClientUtility.GetGuid(dr, "FoundReportGUID", Guid.Empty);
            disposalReportBE.DispositionInfo = SqlClientUtility.GetString(dr, "DispositionInfo", String.Empty);
            disposalReportBE.DispositionDescription = SqlClientUtility.GetString(dr, "DispositionDescription", String.Empty);

            disposalReportBE.Operator = SqlClientUtility.GetString(dr, "Operator", DEFAULT_OPERATOR);
            disposalReportBE.DateCreated = SqlClientUtility.GetDateTime(dr, "DateCreated", DateTime.Now);

            disposalReportBE.DisposalDate = SqlClientUtility.GetDateTime(dr, "ReturnDate", DateTime.Now);

            if (disposalReportBE.DisposalGUID == Guid.Empty)
                disposalReportBE.IsNew = true;

            return disposalReportBE;
        }
        #endregion

        #region Import One Found
        public static Guid ImportOneFoundReport(SqlTransaction trans, LostFoundFoundReportBE foundReportBE)
        {

            Guid foundItemGuid = Guid.Empty;
            SqlParameter keyParam = null;
            try
            {
                List<SqlParameter> paraList = new List<SqlParameter>();
                keyParam = new SqlParameter();
                keyParam.ParameterName = "@FoundItemGUID";
                keyParam.SqlDbType = SqlDbType.UniqueIdentifier;
                keyParam.Direction = ParameterDirection.InputOutput;
                if (foundReportBE.FoundItemGUID == Guid.Empty)
                    keyParam.Value = DBNull.Value;
                else
                    keyParam.Value = foundReportBE.FoundItemGUID;
                paraList.Add(keyParam);
                paraList.Add(new SqlParameter("@PropertyGUID", foundReportBE.PropertyGUID));
                paraList.Add(new SqlParameter("@FoundUID", foundReportBE.FoundUID));
                paraList.Add(new SqlParameter("@ItemCategory", foundReportBE.ItemCategory));
                if(foundReportBE.ItemValue != 0)
                    paraList.Add(new SqlParameter("@ItemValue", foundReportBE.ItemValue));
                paraList.Add(new SqlParameter("@colour", foundReportBE.Colour));
                paraList.Add(new SqlParameter("@SerialNumber", foundReportBE.SerialNumber));
                paraList.Add(new SqlParameter("@Material", foundReportBE.Material));
                paraList.Add(new SqlParameter("@Manufacturer", foundReportBE.Manufacturer));
                paraList.Add(new SqlParameter("@AgeYrs", foundReportBE.AgeYrs));
                paraList.Add(new SqlParameter("@AgeMonths", foundReportBE.AgeMonths));
                if(!string.IsNullOrEmpty(foundReportBE.Contents))
                    paraList.Add(new SqlParameter("@Contents", foundReportBE.Contents));
                if(!string.IsNullOrEmpty(foundReportBE.Description))
                    paraList.Add(new SqlParameter("@Description", foundReportBE.Description));
                if(!string.IsNullOrEmpty(foundReportBE.KeyWords))
                    paraList.Add(new SqlParameter("@KeyWords", foundReportBE.KeyWords));
                paraList.Add(new SqlParameter("@FoundDateTime", foundReportBE.FoundDateTime));
                paraList.Add(new SqlParameter("@FoundStatus", foundReportBE.FoundStatus));
                paraList.Add(new SqlParameter("@SpecificLocation", foundReportBE.SpecificLocation));

                if (foundReportBE.FoundByContactUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@FoundByContactUID", foundReportBE.FoundByContactUID));
                if (foundReportBE.ReportByContactUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@ReportByContactUID", foundReportBE.ReportByContactUID));
                if (foundReportBE.ReceivedByEmployeeUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@ReceivedByEmployeeUID", foundReportBE.ReceivedByEmployeeUID));
                paraList.Add(new SqlParameter("@StoreLocation", foundReportBE.StoreLocation));
                if(!string.IsNullOrEmpty(foundReportBE.AdditionalInfo))
                    paraList.Add(new SqlParameter("@AdditionalInfo", foundReportBE.AdditionalInfo));

                if (foundReportBE.HoldUntil != DateTime.MinValue)
                    paraList.Add(new SqlParameter("@HoldUntil", foundReportBE.HoldUntil));
                if (foundReportBE.BestImageGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@BestImageGUID", foundReportBE.BestImageGUID));
                paraList.Add(new SqlParameter("@Operator", foundReportBE.Operator));
                paraList.Add(new SqlParameter("@Barcode", foundReportBE.Barcode));
                paraList.Add(new SqlParameter("@DateCreated", foundReportBE.DateCreated));
                paraList.Add(new SqlParameter("@Department", foundReportBE.Department));
                paraList.Add(new SqlParameter("@Location", foundReportBE.Location));
                paraList.Add(new SqlParameter("@Sublocation", foundReportBE.Sublocation));
                paraList.Add(new SqlParameter("@ModifiedBy", foundReportBE.ModifiedBy));
                paraList.Add(new SqlParameter("@DateModified", foundReportBE.DateModified));
                paraList.Add(new SqlParameter("@IsNew", foundReportBE.IsNew));

                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "__iTrakImporter_spiu_LostFoundFoundReport", paraList.ToArray());

                foundItemGuid = (Guid)keyParam.Value;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import one found report", ex);
            }
            finally
            {
            }
            return foundItemGuid;
        }
        private static void ImportOneFoundReport(object o)
        {
            object[] objs = o as object[];
            int index = (int)objs[0];
            LostFoundFoundReportBE foundReportBE = objs[1] as LostFoundFoundReportBE;
            LostFoundReturnVerificationBE returnBE = objs[2] as LostFoundReturnVerificationBE;
            LostFoundDisposalReportBE disposalBE = objs[3] as LostFoundDisposalReportBE;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(DbHelper.iTrakConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();

                Guid foundReportGuid = ImportOneFoundReport(trans, foundReportBE);
                if (returnBE != null)
                {
                    returnBE.FoundReportGUID = foundReportGuid;
                    ReturnVerification.ImportOneReturnVerification(trans, returnBE);
                }

                if (disposalBE != null)
                {
                    disposalBE.FoundReportGUID = foundReportGuid;
                    Disposal.ImportOneDisposal(trans, disposalBE);
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

        #region Import Found Report
        public void ImportFoundReports()
        {
            try
            {
                int testingRow = 0;
                if (DataHelper.IsTesting() == true)
                    testingRow = DataHelper.TestingRows;
                string sourceSQL = "[dbo].[__iTrakImporter_sps_LostFoundFoundReport]";
                using (IDataReader dr = SqlHelper.ExecuteReader(DbHelper.iTrakConnectionString, CommandType.StoredProcedure, sourceSQL))
                {
                    int i = 0;
                    _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                    List<int> importedWatchSubjects = new List<int>();

                    List<string> typeOfBanList = new List<string>();
                    #region While Begin
                    while (dr.Read())
                    {
                        LostFoundFoundReportBE foundBE = GetOneFoundReport(dr);
                        LostFoundReturnVerificationBE returnBE = null;
                        if (foundBE.IsReturned)
                            returnBE = GetOneReturnReport(dr, foundBE); //check if it is returned
                        LostFoundDisposalReportBE disposalBE = null;
                        if (foundBE.IsDisposed)
                            disposalBE = GetOneDisposal(dr, foundBE);  //check if it is disposal

                        int index = i % ThreadHelper.NUMBER_OF_THREADS;
                        object[] objs = new object[4];
                        objs[0] = index;
                        objs[1] = foundBE;
                        objs[2] = returnBE;
                        objs[3] = disposalBE;

                        _resetEvents[index] = new ManualResetEvent(false);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ImportOneFoundReport), objs);
                        if (index == ThreadHelper.NUMBER_OF_THREADS - 1)
                        {
                            ThreadHelper.WaitAll(_resetEvents);
                            _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];

                        }
                        if (this.ImportedRowEvent != null)
                        {
                            ImportedRowEvent(++i, new ImportEventArgs("Importing found reports..."));
                        }
                        if (testingRow > 0 && i >= testingRow) //If the number of importing Row over TestingRow, then exit
                        {
                            break;
                        }

                    }
                    ThreadHelper.WaitAll(_resetEvents);

                    if (this.ImportCompletedEvent != null)
                    {
                        ImportCompletedEvent(_count, new ImportEventArgs("Completed found reports"));
                    }
                    #endregion While End
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import found reports", ex);
            }
            finally
            {
            }
        }

        #endregion


    }
}
