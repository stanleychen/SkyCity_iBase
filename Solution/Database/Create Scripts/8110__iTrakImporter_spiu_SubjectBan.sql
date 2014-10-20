 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_SubjectBan]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_SubjectBan]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_SubjectBan]
(
	@SubjectGUID uniqueidentifier,
	@DetailedReportGUID uniqueidentifier,
	@Commencement datetime,
	@IsPermanent bit,
	@EndDate datetime,
	@SubjectCharged bit = 0,
	@LetterSent bit = 0,
	@CompulsiveGambler bit = 0,
	@RecordType int = 1,
	@TypeOfBan nvarchar(100) = null,
	@IdentificationUsed nvarchar(100) = null,
	@ReasonForBan nvarchar(100) = null,
	@SelfExclusiveReport image = null,
	@RemovedBanIncidentGuid uniqueidentifier = null,
	@OriginalBanIncidentGuid uniqueidentifier = null,
	@Removed bit = 0,
	@RemovalDate datetime = NULL,
	@IsNew bit 
)

AS

SET NOCOUNT ON
IF @IsNew = 1
BEGIN
	INSERT INTO [SubjectBan]
	(
		[SubjectGUID],[DetailedReportGUID],[Commencement],[IsPermanent],[EndDate],[SubjectCharged],
		[LetterSent],[CompulsiveGambler],[RecordType],[TypeOfBan],[IdentificationUsed],[ReasonForBan],
		[SelfExclusiveReport],[RemovedBanIncidentGuid],[OriginalBanIncidentGuid],[Removed],
		[RemovalDate]
	)
	VALUES
	(
		@SubjectGUID,@DetailedReportGUID,@Commencement,@IsPermanent,@EndDate,@SubjectCharged,@LetterSent,
		@CompulsiveGambler,@RecordType,@TypeOfBan,@IdentificationUsed,@ReasonForBan,@SelfExclusiveReport,
		@RemovedBanIncidentGuid,@OriginalBanIncidentGuid,@Removed,@RemovalDate
	)
END 
ELSE
BEGIN
	UPDATE [SubjectBan]
	SET [Commencement] = @Commencement,
		[IsPermanent] = @IsPermanent,
		[EndDate] = @EndDate,
		[SubjectCharged] = @SubjectCharged,
		[LetterSent] = @LetterSent,
		[CompulsiveGambler] = @CompulsiveGambler,
		[RecordType] = @RecordType,
		[TypeOfBan] = @TypeOfBan,
		[IdentificationUsed] = @IdentificationUsed,
		[ReasonForBan] = @ReasonForBan,
		[SelfExclusiveReport] = @SelfExclusiveReport,
		[RemovedBanIncidentGuid] = @RemovedBanIncidentGuid,
		[OriginalBanIncidentGuid] = @OriginalBanIncidentGuid,
		[Removed] = @Removed,
		[RemovalDate] = @RemovalDate
	WHERE [SubjectGUID] = @SubjectGUID	AND [DetailedReportGUID] = @DetailedReportGUID
END
GO
