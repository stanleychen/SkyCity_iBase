if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_DetailedReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_DetailedReport]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_DetailedReport]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT dbo.__iTrakImporter_DetailedReport.*, dbo.Blotter.BlotterGUID, dbo.DetailedReport.DetailedReportGUID
	FROM   dbo.__iTrakImporter_DetailedReport (nolock) LEFT OUTER JOIN dbo.Blotter (nolock)
	ON dbo.__iTrakImporter_DetailedReport.BlotterNumber = dbo.Blotter.Number LEFT OUTER JOIN  dbo.DetailedReport (nolock)
	ON dbo.__iTrakImporter_DetailedReport.IncidentNumber = dbo.DetailedReport.Number

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

