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
    public class Disposal
    {
        public static void ImportOneDisposal(SqlTransaction trans, LostFoundDisposalReportBE disposalBE)
        {
            try
            {
                List<SqlParameter> paraList = new List<SqlParameter>();
                paraList.Add(new SqlParameter("@DisposalGUID", disposalBE.DisposalGUID));
                paraList.Add(new SqlParameter("@FoundReportGUID", disposalBE.FoundReportGUID));
                paraList.Add(new SqlParameter("@DisposalDate", disposalBE.DisposalDate));
                if (disposalBE.DisposerEmployeeGUID != Guid.Empty)
                    paraList.Add(new SqlParameter("@DisposerEmployeeGUID", disposalBE.DisposerEmployeeGUID));
                paraList.Add(new SqlParameter("@Durationheld", disposalBE.Durationheld));
                paraList.Add(new SqlParameter("@DispositionInfo", disposalBE.DispositionInfo));
                paraList.Add(new SqlParameter("@DispositionDescription", disposalBE.DispositionDescription));
             
                paraList.Add(new SqlParameter("@Operator", disposalBE.Operator));
                paraList.Add(new SqlParameter("@DateCreated", disposalBE.DateCreated));

                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "__iTrakImporter_spiu_LostFoundDisposalReport", paraList.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to import disposal", ex);
            }
            finally
            {
            }
        }
    }
}
