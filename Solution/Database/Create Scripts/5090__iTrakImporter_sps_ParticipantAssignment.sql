 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_ParticipantAssignment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_ParticipantAssignment]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_ParticipantAssignment]
AS
BEGIN
	SET NOCOUNT ON;

SELECT * FROM (
	SELECT  convert(varchar(50),dbo.DetailedReport.DetailedReportGUID) DetailedReportGuid, 
		convert(varchar(50),dbo._ConversionTmp.ConvertGuid) AS  ParticipantGUID, 
		dbo.ParticipantAssignment.ParticipantGUID AS ParticipantAssignment_ParticipantGUID, 
		dbo.ParticipantAssignment.DetailedReportGUID AS ParticipantAssignment_DetailedReportGUID,
		dbo.__iTrakImporter_ParticipantAssignment.*
							  
		FROM         dbo.DetailedReport (nolock) INNER JOIN
							  dbo.__iTrakImporter_DetailedReport (nolock)  ON dbo.DetailedReport.Number = dbo.__iTrakImporter_DetailedReport.IncidentNumber 
	    INNER JOIN  dbo.__iTrakImporter_ParticipantAssignment (nolock) ON  dbo.__iTrakImporter_DetailedReport.RowID = dbo.__iTrakImporter_ParticipantAssignment.IncidentRowID 
	    INNER JOIN  dbo.__iTrakImporter_SubjectProfile (nolock)  ON dbo.__iTrakImporter_ParticipantAssignment.SubjectRowID = dbo.__iTrakImporter_SubjectProfile.RowID
	    INNER JOIN  dbo._ConversionTmp (nolock) ON dbo.__iTrakImporter_SubjectProfile.SourceID = dbo._ConversionTmp.ConvertId1 LEFT OUTER JOIN
							  dbo.ParticipantAssignment (nolock)  ON dbo._ConversionTmp.ConvertGuid = dbo.ParticipantAssignment.ParticipantGUID AND 
							  dbo.DetailedReport.DetailedReportGUID = dbo.ParticipantAssignment.DetailedReportGUID
		WHERE     (dbo._ConversionTmp.TableName = N'SubjectProfile') 

) p order by DetailedReportGuid,ParticipantGUID
	

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

