 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_LostFoundFoundReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_LostFoundFoundReport]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE dbo.__iTrakImporter_sps_LostFoundFoundReport
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  dbo.__iTrakImporter_LostFoundFoundReport.*, dbo.LostFoundFoundReport.FoundItemGUID,
	LostFoundDisposalReport.DisposalGUID,LostFoundReturnVerification.FoundReportGuid
	FROM    dbo.__iTrakImporter_LostFoundFoundReport (nolock) LEFT OUTER JOIN dbo.LostFoundFoundReport (nolock) 
	ON dbo.__iTrakImporter_LostFoundFoundReport.FoundUID = dbo.LostFoundFoundReport.FoundUID
	LEFT OUTER JOIN dbo.LostFoundDisposalReport (nolock) 
	ON LostFoundFoundReport.FoundItemGUID = LostFoundDisposalReport.FoundReportGUID
	LEFT OUTER JOIN LostFoundReturnVerification (nolock) 
	ON LostFoundFoundReport.FoundItemGUID = LostFoundReturnVerification.FoundReportGuid
	
END
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

