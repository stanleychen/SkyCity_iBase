 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spi_RecordAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spi_RecordAttachment]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[__iTrakImporter_spi_RecordAttachment]

AS
	
	INSERT INTO RecordAttachment(RecordAttachmentGUID,HostRecordGUID,RecordGUID,RecordType,DateAttached,AttachedByUser)
	
	SELECT NewID(),dbo.DetailedReport.DetailedReportGUID, dbo.MiniAudit.MiniAuditGUID,'MiniAudit' as RecordType,DetailedReport.LastModifiedDate, dbo.DetailedReport.ModifiedBy 
		   
	FROM   dbo.__iTrakImporter_DetailedReport (nolock) INNER JOIN dbo.DetailedReport (nolock)
		   ON dbo.__iTrakImporter_DetailedReport.IncidentNumber = dbo.DetailedReport.Number INNER JOIN  dbo.MiniAudit (nolock) 
		  ON dbo.__iTrakImporter_DetailedReport.GameDisputeNumber = dbo.MiniAudit.Number LEFT OUTER JOIN dbo.RecordAttachment (nolock)
		   ON dbo.MiniAudit.MiniAuditGUID = dbo.RecordAttachment.RecordGUID AND  dbo.DetailedReport.DetailedReportGUID = dbo.RecordAttachment.HostRecordGUID
		   
		WHERE RecordAttachment.RecordAttachmentGUID IS NULL	    