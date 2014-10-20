 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_LostFoundFoundReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_LostFoundFoundReport]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_LostFoundFoundReport]
(
	@FoundItemGUID uniqueidentifier = null output,
	@PropertyGUID uniqueidentifier,
	@FoundUID nvarchar(50),
	@ItemCategory nvarchar(50),
	@ItemValue money = 0,
	@colour nvarchar(20),
	@SerialNumber nvarchar(50),
	@Material nvarchar(50),
	@Manufacturer nvarchar(50),
	@AgeYrs int = 0,
	@AgeMonths int = 0,
	@Contents ntext = null,
	@Description ntext = null,
	@KeyWords ntext = null,
	@FoundDateTime datetime = null,
	@FoundStatus nvarchar(50),
	@SpecificLocation nvarchar(50),
	@FoundByContactUID uniqueidentifier = null,
	@ReportByContactUID uniqueidentifier = null,
	@ReceivedByEmployeeUID uniqueidentifier = null,
	@StoreLocation nvarchar(50),
	@AdditionalInfo ntext = null,
	@HoldUntil datetime = null,
	@BestImageGUID uniqueidentifier = null,
	@Operator nvarchar(50),
	@Barcode nvarchar(50),
	@DateCreated datetime,
	@Department nvarchar(50),
	@Location nvarchar(100),
	@Sublocation nvarchar(50),
	@ModifiedBy nvarchar(16),
	@DateModified datetime,
	@IsNew bit = 0
)

AS

SET NOCOUNT ON
IF @FoundItemGUID IS NULL 
BEGIN
	SET @FoundItemGUID = NEWID()
	SET @IsNew = 1
END
IF @IsNew = 1
BEGIN
	INSERT INTO [LostFoundFoundReport]
	(
		[FoundItemGUID],[PropertyGUID],[FoundUID],[ItemCategory],[ItemValue],[colour],[SerialNumber],[Material],
		[Manufacturer],[AgeYrs],[AgeMonths],[Contents],[Description],[KeyWords],[FoundDateTime],[FoundStatus],
		[SpecificLocation],[FoundByContactUID],[ReportByContactUID],[ReceivedByEmployeeUID],[StoreLocation],
		[AdditionalInfo],[HoldUntil],[BestImageGUID],[Operator],[Barcode],[DateCreated],[Department],[Location],
		[Sublocation],[ModifiedBy],[DateModified]
	)
	VALUES
	(
		@FoundItemGUID,@PropertyGUID,@FoundUID,@ItemCategory,@ItemValue,@colour,@SerialNumber,@Material,@Manufacturer,
		@AgeYrs,@AgeMonths,@Contents,@Description,@KeyWords,@FoundDateTime,@FoundStatus,@SpecificLocation,@FoundByContactUID,
		@ReportByContactUID,@ReceivedByEmployeeUID,@StoreLocation,@AdditionalInfo,@HoldUntil,
		@BestImageGUID,@Operator,@Barcode,@DateCreated,@Department,@Location,@Sublocation,@ModifiedBy,@DateModified
	)
END
ELSE
BEGIN
	UPDATE [LostFoundFoundReport]
	SET [PropertyGUID] = @PropertyGUID,
		[FoundUID] = @FoundUID,
		[ItemCategory] = @ItemCategory,
		[ItemValue] = @ItemValue,
		[colour] = @colour,
		[SerialNumber] = @SerialNumber,
		[Material] = @Material,
		[Manufacturer] = @Manufacturer,
		[AgeYrs] = @AgeYrs,
		[AgeMonths] = @AgeMonths,
		[Contents] = @Contents,
		[Description] = @Description,
		[KeyWords] = @KeyWords,
		[FoundDateTime] = @FoundDateTime,
		[FoundStatus] = @FoundStatus,
		[SpecificLocation] = @SpecificLocation,
		[FoundByContactUID] = @FoundByContactUID,
		[ReportByContactUID] = @ReportByContactUID,
		[ReceivedByEmployeeUID] = @ReceivedByEmployeeUID,
		[StoreLocation] = @StoreLocation,
		[AdditionalInfo] = @AdditionalInfo,
		[HoldUntil] = @HoldUntil,
		[BestImageGUID] = @BestImageGUID,
		[Operator] = @Operator,
		[Barcode] = @Barcode,
		[DateCreated] = @DateCreated,
		[Department] = @Department,
		[Location] = @Location,
		[Sublocation] = @Sublocation,
		[ModifiedBy] = @ModifiedBy,
		[DateModified] = @DateModified
	WHERE [FoundItemGUID] = @FoundItemGUID
END
GO
