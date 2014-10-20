 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_DetailedReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_DetailedReport]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_DetailedReport]
(
	@DetailedReportGUID uniqueidentifier = null output,
	@Number nvarchar(50),
	@BlotterGUID uniqueidentifier,
	@Occured datetime,
	@Status nvarchar(50),
	@AmbulanceOffered bit = 0,
	@AmbulanceDeclined bit = 0,
	@FirstAidOffered bit = 0,
	@FirstAidDeclined bit = 0,
	@TaxiFareOffered bit = 0,
	@TaxiFareDeclined bit = 0,
	@ClosedBy nvarchar(16) = null,
	@Closed datetime = null,
	@Archive bit = 0,
	@Created datetime = null,
	@CreatedBy nvarchar(16),
	@Type nvarchar(100),
	@Specific nvarchar(100),
	@Category nvarchar(100),
	@Exclusive ntext = null,
	@SecondaryOperator nvarchar(50),
	@Details ntext,
	@ClosingRemarks ntext,
	@LastModifiedDate datetime = null,
	@ExecutiveBrief ntext = null,
	@ModifiedBy nvarchar(16),
	@ArchiveDate datetime = null,
	@IsGlobal bit = 0
)

AS

SET NOCOUNT ON

IF @DetailedReportGUID IS NULL
BEGIN
	SET @DetailedReportGUID = NEWID()
	INSERT INTO [DetailedReport]
	(
		[DetailedReportGUID],[Number],[BlotterGUID],[Occured],[Status],[AmbulanceOffered],[AmbulanceDeclined],[FirstAidOffered],
		[FirstAidDeclined],[TaxiFareOffered],[TaxiFareDeclined],[ClosedBy],[Closed],[Archive],[Created],[CreatedBy],
		[Type],[Specific],[Category],[Exclusive],[SecondaryOperator],[Details],[ClosingRemarks],[LastModifiedDate],
		[ExecutiveBrief],[ModifiedBy],[ArchiveDate],[IsGlobal])
	VALUES
	(
		@DetailedReportGUID,@Number,@BlotterGUID,@Occured,@Status,@AmbulanceOffered,@AmbulanceDeclined,@FirstAidOffered,
		@FirstAidDeclined,@TaxiFareOffered,@TaxiFareDeclined,@ClosedBy,@Closed,@Archive,@Created,@CreatedBy,
		@Type,@Specific,@Category,@Exclusive,@SecondaryOperator,@Details,@ClosingRemarks,@LastModifiedDate,
		@ExecutiveBrief,@ModifiedBy,@ArchiveDate,@IsGlobal
	)
END
ELSE
BEGIN
	UPDATE [DetailedReport]
	SET [Number] = @Number,	[BlotterGUID] = @BlotterGUID,[Occured] = @Occured,[Status] = @Status,
		[AmbulanceOffered] = @AmbulanceOffered,	[AmbulanceDeclined] = @AmbulanceDeclined,
		[FirstAidOffered] = @FirstAidOffered,[FirstAidDeclined] = @FirstAidDeclined,
		[TaxiFareOffered] = @TaxiFareOffered,[TaxiFareDeclined] = @TaxiFareDeclined,
		[ClosedBy] = @ClosedBy,[Closed] = @Closed,[Archive] = @Archive,	[Created] = @Created,
		[CreatedBy] = @CreatedBy,[Type] = @Type,[Specific] = @Specific,
		[Category] = @Category,[Exclusive] = @Exclusive,[SecondaryOperator] = @SecondaryOperator,
		[Details] = @Details,[ClosingRemarks] = @ClosingRemarks,[LastModifiedDate] = @LastModifiedDate,
		[ExecutiveBrief] = @ExecutiveBrief,[ModifiedBy] = @ModifiedBy,[ArchiveDate] = @ArchiveDate,
		[IsGlobal] = @IsGlobal
	WHERE [DetailedReportGUID] = @DetailedReportGUID
END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

