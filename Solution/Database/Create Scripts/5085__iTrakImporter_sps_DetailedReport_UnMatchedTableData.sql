 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_iTrakImporter_DetailedReport_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_iTrakImporter_DetailedReport_UnMatchedTableData]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_iTrakImporter_DetailedReport_UnMatchedTableData]
	@RowID int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  dbo.__iTrakImporter_DetailedReport_UnMatchedTableData.*
	FROM    dbo.__iTrakImporter_DetailedReport (nolock) INNER JOIN
            dbo.__iTrakImporter_DetailedReport_UnMatchedTableData (nolock) ON 
            dbo.__iTrakImporter_DetailedReport.RowID = dbo.__iTrakImporter_DetailedReport_UnMatchedTableData.IncidentRowID
	WHERE __iTrakImporter_DetailedReport_UnMatchedTableData.IncidentRowID = @RowID
	ORDER BY __iTrakImporter_DetailedReport_UnMatchedTableData.TableName

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

