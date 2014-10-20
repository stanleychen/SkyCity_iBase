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
    public class ReturnVerification
    {
        public static void ImportOneReturnVerification(SqlTransaction trans, LostFoundReturnVerificationBE returnBE)
        {
            try
            {
                if (returnBE.ReturnDate < DataHelper.SQL_MIN_DATE)
                    returnBE.ReturnDate = DateTime.Now;
                List<SqlParameter> paraList = new List<SqlParameter>();
                paraList.Add(new SqlParameter("@FoundReportGUID", returnBE.FoundReportGUID));
                if (returnBE.LostReportGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@LostReportGUID", returnBE.LostReportGUID));
                paraList.Add(new SqlParameter("@ReturnDate", returnBE.ReturnDate));
                if (returnBE.EmployeeGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@EmployeeGUID", returnBE.EmployeeGUID));
                paraList.Add(new SqlParameter("@ID1", returnBE.ID1));
                paraList.Add(new SqlParameter("@ID1Number", returnBE.ID1Number));
                paraList.Add(new SqlParameter("@ID2", returnBE.ID2));
                paraList.Add(new SqlParameter("@ID2Number", returnBE.ID2Number));
                paraList.Add(new SqlParameter("@ItemReturned", returnBE.ItemReturned));
                paraList.Add(new SqlParameter("@ItemToBeMailed", returnBE.ItemToBeMailed));
                paraList.Add(new SqlParameter("@DeliveryCost", returnBE.DeliveryCost));
                paraList.Add(new SqlParameter("@DeliveryInvoiceID", returnBE.DeliveryInvoiceID));
                if (returnBE.MailInfoGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@MailInfoGUID", returnBE.MailInfoGUID));
                paraList.Add(new SqlParameter("@RewardOffered", returnBE.RewardOffered));
                paraList.Add(new SqlParameter("@RewardAmount", returnBE.RewardAmount));
                if (returnBE.RewardPaidToGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@RewardPaidToGUID", returnBE.RewardPaidToGUID));
                if (returnBE.PhotoGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@PhotoGUID", returnBE.PhotoGUID));
                paraList.Add(new SqlParameter("@Operator", returnBE.Operator));
                paraList.Add(new SqlParameter("@DateCreated", returnBE.DateCreated));
                paraList.Add(new SqlParameter("@SignString", returnBE.SignString));
                if (returnBE.SignEncryptBytes != null)
                    paraList.Add(new SqlParameter("@SignEncryptBytes", returnBE.SignEncryptBytes));
                if (returnBE.SignEncryptIV != null)
                    paraList.Add(new SqlParameter("@SignEncryptIV", returnBE.SignEncryptIV));
                if(returnBE.ReturnDueDate > DataHelper.SQL_MIN_DATE)
                    paraList.Add(new SqlParameter("@ReturnDueDate", returnBE.ReturnDueDate));
                paraList.Add(new SqlParameter("@IsNew",returnBE.IsNew));
                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "__iTrakImporter_spiu_LostFoundReturnVerification", paraList.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import one return verification", ex);
            }
            finally
            {
            }
        }
    }
}
