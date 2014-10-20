 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_RecordAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_RecordAttachment]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_RecordAttachment]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT dbo.DetailedReport.DetailedReportGUID, dbo.MiniAudit.MiniAuditGUID, dbo.DetailedReport.ModifiedBy, 
		   dbo.RecordAttachment.RecordAttachmentGUID,'MiniAudit' as RecordType
	FROM   dbo.__iTrakImporter_DetailedReport (nolock) INNER JOIN dbo.DetailedReport (nolock)
		   ON dbo.__iTrakImporter_DetailedReport.IncidentNumber = dbo.DetailedReport.Number INNER JOIN  dbo.MiniAudit (nolock) 
		  ON dbo.__iTrakImporter_DetailedReport.GameDisputeNumber = dbo.MiniAudit.Number LEFT OUTER JOIN dbo.RecordAttachment (nolock)
		   ON dbo.MiniAudit.MiniAuditGUID = dbo.RecordAttachment.RecordGUID AND  dbo.DetailedReport.DetailedReportGUID = dbo.RecordAttachment.HostRecordGUID
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

