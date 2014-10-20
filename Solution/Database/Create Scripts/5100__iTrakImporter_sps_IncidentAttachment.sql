 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_IncidentAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_IncidentAttachment]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- =============================================
CREATE PROCEDURE dbo.__iTrakImporter_sps_IncidentAttachment
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  dbo.__iTrakImporter_IncidentAttachment.*, _ConversionTmp_Subject.ConvertGuid AS HostGuid, 
			_ConversionTmp_Media.ConvertGuid AS MediaGuid
	FROM    dbo.__iTrakImporter_IncidentAttachment (nolock) 
			INNER JOIN dbo.__iTrakImporter_SubjectProfile (nolock) ON dbo.__iTrakImporter_IncidentAttachment.HostRowID = dbo.__iTrakImporter_SubjectProfile.RowID 
			INNER JOIN dbo._ConversionTmp _ConversionTmp_Subject (nolock) ON dbo.__iTrakImporter_SubjectProfile.SourceID = _ConversionTmp_Subject.ConvertId1
			LEFT OUTER JOIN (SELECT * FROM dbo._ConversionTmp (nolock) WHERE TABLENAME = 'Media') _ConversionTmp_Media  ON dbo.__iTrakImporter_IncidentAttachment.SourceID = _ConversionTmp_Media.ConvertId1
	WHERE   (_ConversionTmp_Subject.TableName = N'SubjectProfile') 

	ORDER BY __iTrakImporter_SubjectProfile.RowID
	
	
SELECT  atm.*, d.DetailedReportGuid AS HostGuid,media.ConvertGuid AS MediaGuid
FROM    dbo.__iTrakImporter_IncidentAttachment atm (nolock) 
		INNER JOIN dbo.__iTrakImporter_DetailedReport s (nolock) ON atm.HostRowID = s.RowID 
		INNER JOIN dbo.DetailedReport d ON s.IncidentNumber = d.Number
		LEFT OUTER JOIN (SELECT * FROM dbo._ConversionTmp (nolock) WHERE TABLENAME = 'Media') media  
		ON atm.SourceID = media.ConvertId1
		ORDER BY s.RowID
		
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

