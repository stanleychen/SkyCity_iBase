 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_MiniAuditViolation]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_MiniAuditViolation]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_MiniAuditViolation]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  dbo.__iTrakImporter_MiniAuditViolation.*, dbo.MiniAudit.MiniAuditGUID,dbo.MiniAuditViolation.MiniAuditEmployeeViolationGUID
	FROM    dbo.__iTrakImporter_MiniAuditViolation (nolock) INNER JOIN
			dbo.__iTrakImporter_MiniAudit (nolock) ON dbo.__iTrakImporter_MiniAuditViolation.MiniAuditRowID = dbo.__iTrakImporter_MiniAudit.RowID
			INNER JOIN dbo.MiniAudit (nolock) ON dbo.__iTrakImporter_MiniAudit.Number = dbo.MiniAudit.Number
			LEFT OUTER JOIN dbo.MiniAuditViolation (nolock) ON dbo.MiniAudit.MiniAuditGUID = dbo.MiniAuditViolation.MiniAuditGUID

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

