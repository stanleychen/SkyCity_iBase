 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_GameAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_GameAudit]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_GameAudit]


AS
BEGIN
	SET NOCOUNT ON;


SELECT     dbo.__iTrakImporter_GameAudit.*, dbo.GameAudit.GameAuditGUID
FROM         dbo.__iTrakImporter_GameAudit (nolock) LEFT OUTER JOIN
                      dbo.GameAudit (nolock) ON dbo.__iTrakImporter_GameAudit.Number = dbo.GameAudit.Number

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

