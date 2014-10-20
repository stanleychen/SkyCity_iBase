 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_GameAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_GameAudit]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_GameAudit]
(
	@GameAuditGUID uniqueidentifier = null output,
	@Number nvarchar(50),
	@PropertyGUID uniqueidentifier,
	@ReviewMethod nvarchar(50),
	@GameAuditStartDateTime datetime,
	@GameAuditEndDateTime datetime,
	@VCRConsoleNumber nvarchar(50) = null,
	@CreatorUserID nvarchar(50),
	@LastModifiedDate datetime,	
	@Closed bit = 0,
	@Archived bit = 0 ,
	@AreaAudited nvarchar(100) = null,
	@Section nvarchar(100) = null,	
	@Remarks ntext = null,
	@ModifiedBy nvarchar(16),	
	@DateCreated datetime,
	@SecondAuditor nvarchar(50),	
	@RequestedByGUID uniqueidentifier = null,
	@Status nvarchar(50) = null
)

AS

SET NOCOUNT ON

IF @GameAuditGUID IS NULL 
BEGIN
	SET @GameAuditGUID = NewID()
	INSERT INTO [GameAudit]
	( [GameAuditGUID],[PropertyGUID],[ReviewMethod],[GameAuditStartDateTime],
	[GameAuditEndDateTime],	[VCRConsoleNumber],	[CreatorUserID],[LastModifiedDate],
	[Closed],[Archived],[AreaAudited],[Section],[Remarks],[ModifiedBy],	[DateCreated],
	[SecondAuditor],[Number],[RequestedByGUID],	[Status]
	)
	VALUES
	(
		@GameAuditGUID,	@PropertyGUID,@ReviewMethod,@GameAuditStartDateTime,
		@GameAuditEndDateTime,@VCRConsoleNumber,@CreatorUserID,@LastModifiedDate,
		@Closed,@Archived,@AreaAudited,@Section,@Remarks,@ModifiedBy,
		@DateCreated,@SecondAuditor,@Number,@RequestedByGUID,@Status
	)
END
ELSE
BEGIN

	UPDATE [GameAudit]
	SET [PropertyGUID] = @PropertyGUID,
		[ReviewMethod] = @ReviewMethod,
		[GameAuditStartDateTime] = @GameAuditStartDateTime,
		[GameAuditEndDateTime] = @GameAuditEndDateTime,
		[VCRConsoleNumber] = @VCRConsoleNumber,
		[CreatorUserID] = @CreatorUserID,
		[LastModifiedDate] = @LastModifiedDate,
		[Closed] = @Closed,
		[Archived] = @Archived,
		[AreaAudited] = @AreaAudited,
		[Section] = @Section,
		[Remarks] = @Remarks,
		[ModifiedBy] = @ModifiedBy,
		[DateCreated] = @DateCreated,
		[SecondAuditor] = @SecondAuditor,
		[Number] = @Number,
		[RequestedByGUID] = @RequestedByGUID,
		[Status] = @Status
	WHERE [GameAuditGUID] = @GameAuditGUID
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

