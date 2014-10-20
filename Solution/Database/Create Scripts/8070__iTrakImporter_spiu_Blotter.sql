 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_Blotter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_Blotter]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_Blotter]
(
	@BlotterGUID uniqueidentifier = null output,
	@Number nvarchar(50),
	@Occured datetime = null,
	@BlotterAction nvarchar(50),
	@Subject nvarchar(50),
	@Property uniqueidentifier = null,
	@Created datetime = null,
	@Archive bit = 0,
	@PrimaryOperator nvarchar(16),
	@SecondaryOperator nvarchar(16),
	@HighPriority bit = 0,
	@Status nvarchar(50),
	@Sublocation nvarchar(50) = null,
	@Location nvarchar(100) = null,
	@Exclusive ntext = null,
	@Synopsis ntext = null,
	@LastModifiedDate datetime = null,
	@Reference nvarchar(50) = null,
	@ModifiedBy nvarchar(16) = null,
	@ArchiveDate datetime = null,
	@EndTime datetime = null,
	@ClosedTime datetime = null,
	@IsGlobal bit = 0,
	@SourceModuleID int = 0,
	@SourceID nvarchar(50) = null,
	@SourceGUID uniqueidentifier = null,
	@LockedBySource bit = 0
)

AS

SET NOCOUNT ON
IF @BlotterGUID IS NULL
BEGIN
	SET @BlotterGUID = NEWID()
	INSERT INTO [Blotter]
	(
		[BlotterGUID],[Number],[Occured],[BlotterAction],[Subject],[Property],[Created],
		[Archive],[PrimaryOperator],[SecondaryOperator],[HighPriority],[Status],[Sublocation],
		[Location],[Exclusive],[Synopsis],[LastModifiedDate],[Reference],[ModifiedBy],[ArchiveDate],
		[EndTime],[ClosedTime],[IsGlobal],[SourceModuleID],[SourceID],[SourceGUID],[LockedBySource]
	)
	VALUES
	(
		@BlotterGUID,@Number,@Occured,@BlotterAction,@Subject,@Property,@Created,@Archive,@PrimaryOperator,
		@SecondaryOperator,@HighPriority,@Status,@Sublocation,@Location,@Exclusive,@Synopsis,@LastModifiedDate,
		@Reference,@ModifiedBy,@ArchiveDate,@EndTime,@ClosedTime,@IsGlobal,@SourceModuleID,@SourceID,
		@SourceGUID,@LockedBySource
	)
END 
ELSE
BEGIN
	UPDATE [Blotter]
	SET [Number] = @Number,
		[Occured] = @Occured,
		[BlotterAction] = @BlotterAction,
		[Subject] = @Subject,
		[Property] = @Property,
		[Created] = @Created,
		[Archive] = @Archive,
		[PrimaryOperator] = @PrimaryOperator,
		[SecondaryOperator] = @SecondaryOperator,
		[HighPriority] = @HighPriority,
		[Status] = @Status,
		[Sublocation] = @Sublocation,
		[Location] = @Location,
		[Exclusive] = @Exclusive,
		[Synopsis] = @Synopsis,
		[LastModifiedDate] = @LastModifiedDate,
		[Reference] = @Reference,
		[ModifiedBy] = @ModifiedBy,
		[ArchiveDate] = @ArchiveDate,
		[EndTime] = @EndTime,
		[ClosedTime] = @ClosedTime,
		[IsGlobal] = @IsGlobal,
		[SourceModuleID] = @SourceModuleID,
		[SourceID] = @SourceID,
		[SourceGUID] = @SourceGUID,
		[LockedBySource] = @LockedBySource
	WHERE [BlotterGUID] = @BlotterGUID
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

