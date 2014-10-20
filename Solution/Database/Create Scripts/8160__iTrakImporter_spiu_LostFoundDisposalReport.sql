 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_LostFoundDisposalReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_LostFoundDisposalReport]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_LostFoundDisposalReport]
(
	@DisposalGUID uniqueidentifier,
	@FoundReportGUID uniqueidentifier,
	@DisposalDate datetime,
	@DisposerEmployeeGUID uniqueidentifier = NULL,
	@Durationheld nvarchar(50),
	@DispositionInfo nvarchar(50),
	@DispositionDescription ntext,
	@Signature image = NULL,
	@Operator nvarchar(50),
	@DateCreated datetime,
	@IsNew bit = 0
)

AS

SET NOCOUNT ON
IF @IsNew = 1
BEGIN
	INSERT INTO [LostFoundDisposalReport]
	(
		[DisposalGUID],[FoundReportGUID],[DisposalDate],[DisposerEmployeeGUID],[Durationheld],[DispositionInfo],
		[DispositionDescription],[Signature],[Operator],[DateCreated])
	VALUES
	(
		@DisposalGUID,@FoundReportGUID,@DisposalDate,@DisposerEmployeeGUID,
		@Durationheld,@DispositionInfo,@DispositionDescription,@Signature,
		@Operator,@DateCreated
	)
END 
ELSE
BEGIN
	UPDATE [LostFoundDisposalReport]
	SET [FoundReportGUID] = @FoundReportGUID,
		[DisposalDate] = @DisposalDate,
		[DisposerEmployeeGUID] = @DisposerEmployeeGUID,
		[Durationheld] = @Durationheld,
		[DispositionInfo] = @DispositionInfo,
		[DispositionDescription] = @DispositionDescription,
		[Signature] = @Signature,
		[Operator] = @Operator,
		[DateCreated] = @DateCreated
	WHERE [DisposalGUID] = @DisposalGUID
END
GO
