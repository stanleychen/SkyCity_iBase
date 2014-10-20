 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_IncidentAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_IncidentAttachment]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_IncidentAttachment]
(
	@IncidentAttachment uniqueidentifier = null output,
	@DetailedReportGUID uniqueidentifier,
	@SourceID varchar(50),
	@Attached datetime,
	@AttachedmentData image,
	@OriginalFilename nvarchar(256),
	@AttachedBy nvarchar(16),
	@Thumbnail image = null,
	@AttachedType nvarchar(10),
	@AttachmentSize bigint,
	@Linked bit = 0,
	@DigitalSignature image = null,
	@DataProviderType int = 0,
	@Deleted bit = 0,
	@LastModifiedDate datetime = null,
	@ServerCreateDate datetime = null,
	@HostType varchar(50),
	@IsBestAsset bit = 0,
	@MediaTitle varchar(20) = null
)

AS

SET NOCOUNT ON
DECLARE @BestGuid uniqueidentifier

if @IncidentAttachment is null
BEGIN
	set @IncidentAttachment = NewID()
	INSERT INTO [dbo].[IncidentAttachment]
	(
		[IncidentAttachment],[DetailedReportGUID],[Attached],[AttachedmentData],[OriginalFilename],
		[AttachedBy],[Thumbnail],[AttachedType],[AttachmentSize],[Linked],[DigitalSignature],
		[DataProviderType],[Deleted],[LastModifiedDate],[ServerCreateDate]
	)
	VALUES
	(
		@IncidentAttachment,@DetailedReportGUID,@Attached,@AttachedmentData,@OriginalFilename,
		@AttachedBy,@Thumbnail,@AttachedType,@AttachmentSize,@Linked,@DigitalSignature,
		@DataProviderType,@Deleted,@LastModifiedDate,@ServerCreateDate
	)
	
	INSERT INTO [_ConversionTmp]
	(
		[ConvertGuid],[TableName],[ConvertId1],[ConvertId2],[CreateDate]
	)
	VALUES
	(
		@IncidentAttachment,'Media',@SourceID,null,GetDate()
	)
	IF @MediaTitle IS NOT NULL 
		INSERT INTO [IncidentAttachmentDescription]
		([AttachmentGUID],[MediaTitle],[CreatedBy],[DateCreated],[ModifiedBy],[DateModified]
		)VALUES
		(@IncidentAttachment,@MediaTitle,@AttachedBy,@Attached,@AttachedBy,@Attached
		)
END 
ELSE
BEGIN
	UPDATE [IncidentAttachment]
	SET [DetailedReportGUID] = @DetailedReportGUID,
		[Attached] = @Attached,
		[AttachedmentData] = @AttachedmentData,
		[OriginalFilename] = @OriginalFilename,
		[AttachedBy] = @AttachedBy,
		[Thumbnail] = @Thumbnail,
		[AttachedType] = @AttachedType,
		[AttachmentSize] = @AttachmentSize,
		[Linked] = @Linked,
		[DigitalSignature] = @DigitalSignature,
		[DataProviderType] = @DataProviderType,
		[Deleted] = @Deleted,
		[LastModifiedDate] = @LastModifiedDate,
		[ServerCreateDate] = @ServerCreateDate
	WHERE [IncidentAttachment] = @IncidentAttachment

	IF @MediaTitle IS NOT NULL 
	BEGIN
		IF EXISTS(SELECT * FROM [dbo].[IncidentAttachmentDescription]WHERE [AttachmentGUID] = @IncidentAttachment)
		BEGIN
			UPDATE [IncidentAttachmentDescription]
			SET [MediaTitle] = @MediaTitle,
				[CreatedBy] = @AttachedBy,
				[DateCreated] = @Attached,
				[ModifiedBy] = @AttachedBy,
				[DateModified] = @Attached
			WHERE [AttachmentGUID] = @IncidentAttachment
		END
		ELSE
		BEGIN
			INSERT INTO [IncidentAttachmentDescription]
			([AttachmentGUID],[MediaTitle],[CreatedBy],[DateCreated],[ModifiedBy],[DateModified]
			)VALUES
			(@IncidentAttachment,@MediaTitle,@AttachedBy,@Attached,@AttachedBy,@Attached
			)
		END
	END 
END

IF @IsBestAsset = 1
	IF @HostType = 'Subject'
		UPDATE SubjectProfile SET BestAssetGUID = @IncidentAttachment WHERE SubjectGuid = @DetailedReportGUID


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

