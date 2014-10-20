 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_LostFoundLostReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_LostFoundLostReport]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE dbo.__iTrakImporter_sps_LostFoundLostReport
AS
BEGIN
	SET NOCOUNT ON;

	SELECT dbo.__iTrakImporter_LostFoundLostReport.*, dbo.LostFoundLostReport.LostGUID, dbo.LostFoundReturnVerification.FoundReportGUID
	FROM   dbo.LostFoundReturnVerification (nolock) RIGHT OUTER JOIN dbo.LostFoundLostReport (nolock) 
	ON dbo.LostFoundReturnVerification.LostReportGUID = dbo.LostFoundLostReport.LostGUID RIGHT OUTER JOIN dbo.__iTrakImporter_LostFoundLostReport (nolock)
    ON dbo.LostFoundLostReport.LostUID = dbo.__iTrakImporter_LostFoundLostReport.LostUID

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

