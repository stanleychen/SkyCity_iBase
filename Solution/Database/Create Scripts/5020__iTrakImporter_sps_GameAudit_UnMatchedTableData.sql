if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_GameAudit_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_GameAudit_UnMatchedTableData]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[__iTrakImporter_sps_GameAudit_UnMatchedTableData]
	@RowID int
AS
BEGIN
	SET NOCOUNT ON;

SELECT __iTrakImporter_GameAudit_UnMatchedTableData.* FROM 
  dbo.__iTrakImporter_GameAudit_UnMatchedTableData (nolock) INNER JOIN  dbo.__iTrakImporter_GameAudit (nolock)
 ON dbo.__iTrakImporter_GameAudit_UnMatchedTableData.GameAuditRowID = dbo.__iTrakImporter_GameAudit.RowID
WHERE 	__iTrakImporter_GameAudit_UnMatchedTableData.GameAuditRowID = @RowID

END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

 