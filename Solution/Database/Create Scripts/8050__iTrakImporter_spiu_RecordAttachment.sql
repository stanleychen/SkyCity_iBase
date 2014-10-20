 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_RecordAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_RecordAttachment]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_RecordAttachment]
(
	@RecordAttachmentGUID uniqueidentifier = null output,
	@HostRecordGUID uniqueidentifier,
	@RecordGUID uniqueidentifier,
	@RecordSummary ntext,
	@RecordType nvarchar(50),
	@DateAttached datetime,
	@AttachedByUser nvarchar(50)
)

AS

SET NOCOUNT ON

IF @RecordAttachmentGUID IS NULL 
BEGIN
	SET @RecordAttachmentGUID = NEWID()
	INSERT INTO [RecordAttachment]
	(	[RecordAttachmentGUID],	[HostRecordGUID],[RecordGUID],[RecordSummary],	[RecordType],[DateAttached],
		[AttachedByUser]
	)
	VALUES (@RecordAttachmentGUID,	@HostRecordGUID,@RecordGUID,@RecordSummary,
		@RecordType,@DateAttached,@AttachedByUser)
END
ELSE
BEGIN
	UPDATE [RecordAttachment]
	SET [HostRecordGUID] = @HostRecordGUID,
		[RecordGUID] = @RecordGUID,
		[RecordSummary] = @RecordSummary,
		[RecordType] = @RecordType,
		[DateAttached] = @DateAttached,
		[AttachedByUser] = @AttachedByUser
	WHERE [RecordAttachmentGUID] = @RecordAttachmentGUID
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

