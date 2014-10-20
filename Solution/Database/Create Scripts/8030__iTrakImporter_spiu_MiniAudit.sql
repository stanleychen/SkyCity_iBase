 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_MiniAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_MiniAudit]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_MiniAudit]
(
	@MiniAuditGUID uniqueidentifier = null output,
	@PropertyGUID uniqueidentifier,
	@MiniAuditStartDateTime datetime,
	@MiniAuditEndDateTime datetime,
	@ReviewMethod nvarchar(50),
	@VCRConsoleNumber nvarchar(50),
	@CreatedBy nvarchar(50),
	@DateCreated datetime,
	@Closed bit = 0,
	@Archived bit = 0,
	@SecondAuditor nvarchar(50),
	@Area nvarchar(100),
	@Section nvarchar(100),
	@Exclusive ntext = NULL,
	@Remarks ntext,
	@ModifiedBy nvarchar(16),
	@LastModifiedDate datetime,
	@Number nvarchar(50),
	@RequestedByGUID uniqueidentifier = null,
	@Status nvarchar(50),
	@SubjectGUID uniqueidentifier = null
)

AS

SET NOCOUNT ON

IF @MiniAuditGUID IS NULL 
BEGIN 
	SET @MiniAuditGUID = NewID()
	INSERT INTO [MiniAudit]
	(
		[MiniAuditGUID],[PropertyGUID],[MiniAuditStartDateTime],[MiniAuditEndDateTime],
		[ReviewMethod],[VCRConsoleNumber],[CreatedBy],[DateCreated],[Closed],[Archived],
		[SecondAuditor],[Area],[Section],[Exclusive],[Remarks],	[ModifiedBy],
		[LastModifiedDate],	[Number],[RequestedByGUID],	[Status],[SubjectGUID]
	)
	VALUES
	(
		@MiniAuditGUID,	@PropertyGUID,	@MiniAuditStartDateTime,@MiniAuditEndDateTime,
		@ReviewMethod,@VCRConsoleNumber,@CreatedBy,@DateCreated,@Closed,@Archived,
		@SecondAuditor,@Area,@Section,@Exclusive,@Remarks,@ModifiedBy,@LastModifiedDate,
		@Number,@RequestedByGUID,@Status,@SubjectGUID
	)
END 
ELSE
BEGIN

	UPDATE [MiniAudit]
	SET [PropertyGUID] = @PropertyGUID,
		[MiniAuditStartDateTime] = @MiniAuditStartDateTime,
		[MiniAuditEndDateTime] = @MiniAuditEndDateTime,
		[ReviewMethod] = @ReviewMethod,
		[VCRConsoleNumber] = @VCRConsoleNumber,
		[CreatedBy] = @CreatedBy,
		[DateCreated] = @DateCreated,
		[Closed] = @Closed,
		[Archived] = @Archived,
		[SecondAuditor] = @SecondAuditor,
		[Area] = @Area,
		[Section] = @Section,
		[Exclusive] = @Exclusive,
		[Remarks] = @Remarks,
		[ModifiedBy] = @ModifiedBy,
		[LastModifiedDate] = @LastModifiedDate,
		[Number] = @Number,
		[RequestedByGUID] = @RequestedByGUID,
		[Status] = @Status,
		[SubjectGUID] = @SubjectGUID
	WHERE [MiniAuditGUID] = @MiniAuditGUID
	
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

