 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_BanWatchStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_BanWatchStatus]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_BanWatchStatus]
(
	@SubjectGUID uniqueidentifier,
	@Status int,
	@Commencement datetime,
	@EndDate datetime,
	@IsPermanent bit,
	@TypeOfBan nvarchar(100),
	@ReasonForBan nvarchar(100),
	@DetailedReportGUID uniqueidentifier = null,
	@IsNew bit = 0
)

AS

SET NOCOUNT ON

IF @IsNew = 1
	INSERT INTO [BanWatchStatus]
	(
		[SubjectGUID],[Status],[Commencement],[EndDate],[IsPermanent],[TypeOfBan],
		[ReasonForBan],	[DetailedReportGUID]
	)
	VALUES
	(
		@SubjectGUID,@Status,@Commencement,@EndDate,@IsPermanent,@TypeOfBan,
		@ReasonForBan,@DetailedReportGUID
	)
ELSE
	UPDATE [BanWatchStatus]
	SET [Status] = @Status,
		[Commencement] = @Commencement,
		[EndDate] = @EndDate,
		[IsPermanent] = @IsPermanent,
		[TypeOfBan] = @TypeOfBan,
		[ReasonForBan] = @ReasonForBan,
		[DetailedReportGUID] = @DetailedReportGUID
	WHERE [SubjectGUID] = @SubjectGUID
GO
