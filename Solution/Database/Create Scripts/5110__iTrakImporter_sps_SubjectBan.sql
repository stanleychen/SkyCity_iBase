 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_SubjectBan]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_SubjectBan]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE dbo.__iTrakImporter_sps_SubjectBan
AS
BEGIN

	SET NOCOUNT ON;

	SELECT source.*, dest.DetailedReportGuid as BanIncidentGuid, dest.SubjectGuid as BanSubjectGuid,
	bw.SubjectGuid as BanwatchSubjectGuid,
	pa.ParticipantGuid, pa.DetailedReportGuid as PDetailedReportGuid FROM
	(

		SELECT   dbo.__iTrakImporter_SubjectBan.*, dbo.DetailedReport.DetailedReportGUID, dbo._ConversionTmp.ConvertGuid AS SubjectGuid
		FROM     dbo.__iTrakImporter_DetailedReport (nolock)  
				INNER JOIN dbo.__iTrakImporter_SubjectBan (nolock) 
				INNER JOIN dbo.__iTrakImporter_SubjectProfile (nolock) ON dbo.__iTrakImporter_SubjectBan.SubjectRowID = dbo.__iTrakImporter_SubjectProfile.RowID 
				ON  dbo.__iTrakImporter_DetailedReport.RowID = dbo.__iTrakImporter_SubjectBan.IncidentRowID 
				INNER JOIN dbo.DetailedReport (nolock) 
				 ON dbo.__iTrakImporter_DetailedReport.IncidentNumber = dbo.DetailedReport.Number 
				INNER JOIN dbo._ConversionTmp (nolock)  
				ON dbo.__iTrakImporter_SubjectProfile.SourceID = dbo._ConversionTmp.ConvertId1
		WHERE     (dbo._ConversionTmp.TableName = N'SubjectProfile')

	) source

	LEFT OUTER JOIN SUBJECTBAN dest (nolock) ON source.DetailedReportGuid = dest.DetailedReportGuid
	AND source.SubjectGuid = dest.SubjectGuid
	
	LEFT OUTER JOIN BANWATCHSTATUS bw (nolock) on source.SubjectGuid = bw.SubjectGuid

	LEFT OUTER  JOIN PARTICIPANTASSIGNMENT pa (nolock) on Source.SubjectGuid =pa.ParticipantGuid AND Source.DetailedReportGuid = pa.DetailedReportGuid

	ORDER BY source.SubjectRowID,source.IncidentRowID


END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

