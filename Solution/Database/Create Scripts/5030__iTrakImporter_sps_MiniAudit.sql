 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_MiniAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_MiniAudit]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_MiniAudit]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  dbo.__iTrakImporter_MiniAudit.*, dbo.MiniAudit.MiniAuditGUID
	FROM  dbo.__iTrakImporter_MiniAudit (nolock) LEFT OUTER JOIN
		 dbo.MiniAudit (nolock) ON dbo.__iTrakImporter_MiniAudit.Number = dbo.MiniAudit.Number

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

