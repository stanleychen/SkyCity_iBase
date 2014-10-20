 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_MiniAudit_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_MiniAudit_UnMatchedTableData]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_MiniAudit_UnMatchedTableData]
	@RowID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  dbo.__iTrakImporter_MiniAudit_UnMatchedTableData.*
	FROM    dbo.__iTrakImporter_MiniAudit (nolock) INNER JOIN
            dbo.__iTrakImporter_MiniAudit_UnMatchedTableData (nolock) ON 
            dbo.__iTrakImporter_MiniAudit.RowID = dbo.__iTrakImporter_MiniAudit_UnMatchedTableData.MiniAuditRowID
	WHERE __iTrakImporter_MiniAudit_UnMatchedTableData.MiniAuditRowID = @RowID
	ORDER BY __iTrakImporter_MiniAudit_UnMatchedTableData.TableName

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

