 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_LostFoundLostReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_LostFoundLostReport]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_LostFoundLostReport]
(
	@LostGUID uniqueidentifier = null output,
	@LostUID nvarchar(50),
	@ItemCategory nvarchar(50),
	@Colour nvarchar(50),
	@SerialNumber nvarchar(50),
	@ItemValue money,
	@Material nvarchar(50),
	@Manufacturer nvarchar(50),
	@AgeYrs int = 0,
	@AgeMonths int = 0,
	@KeyWords ntext,
	@LostPropertyGUID uniqueidentifier = null,
	@LostRoomNumber nvarchar(50),
	@whenLost datetime = null,
	@ReportedAsStolen bit = 0,
	@Contents ntext,
	@Description ntext,
	@ReportByContactGUID uniqueidentifier = null,
	@Owner bit,
	@AlternateContactGUID uniqueidentifier = null,
	@HotelGuest bit,
	@GuestPropertyGUID uniqueidentifier = null,
	@GuestRoom nvarchar(10),
	@PoliceReportFiled bit,
	@PoliceReportNumber nvarchar(50),
	@PoliceReportOfficer nvarchar(50),
	@PoliceReportLocation nvarchar(50),
	@InsuredByCompanyGUID uniqueidentifier = null,
	@FollowUp bit = 0,
	@FollowUpDate datetime = null,
	@Notes ntext,
	@Operator nvarchar(50),
	@DateCreated datetime,
	@LostLocation nvarchar(100),
	@Sublocation nvarchar(50),
	@ModifiedBy nvarchar(16),
	@DateModified datetime
	
)

AS

SET NOCOUNT ON
IF @LostGUID IS NULL
BEGIN
	SET @LostGUID = NEWID()
	INSERT INTO [LostFoundLostReport]
	(
		[LostGUID],[LostUID],[ItemCategory],[Colour],[SerialNumber],[ItemValue],[Material],[Manufacturer],[AgeYrs],
		[AgeMonths],[KeyWords],[LostPropertyGUID],[LostRoomNumber],[whenLost],[ReportedAsStolen],[Contents],[Description],
		[ReportByContactGUID],[Owner],[AlternateContactGUID],[HotelGuest],[GuestPropertyGUID],[GuestRoom],[PoliceReportFiled],
		[PoliceReportNumber],[PoliceReportOfficer],[PoliceReportLocation],[InsuredByCompanyGUID],[FollowUp],[FollowUpDate],
		[Notes],[Operator],[DateCreated],[LostLocation],[Sublocation],[ModifiedBy],[DateModified]
		
	)
	VALUES
	(
		@LostGUID,@LostUID,@ItemCategory,@Colour,@SerialNumber,@ItemValue,@Material,@Manufacturer,@AgeYrs,@AgeMonths,
		@KeyWords,@LostPropertyGUID,@LostRoomNumber,@whenLost,@ReportedAsStolen,@Contents,@Description,@ReportByContactGUID,
		@Owner,@AlternateContactGUID,@HotelGuest,@GuestPropertyGUID,@GuestRoom,@PoliceReportFiled,@PoliceReportNumber,
		@PoliceReportOfficer,@PoliceReportLocation,@InsuredByCompanyGUID,@FollowUp,@FollowUpDate,@Notes,
		@Operator,@DateCreated,@LostLocation,@Sublocation,@ModifiedBy,@DateModified
	)
END 
ELSE
BEGIN
	UPDATE [LostFoundLostReport]
	SET [LostUID] = @LostUID,
		[ItemCategory] = @ItemCategory,
		[Colour] = @Colour,
		[SerialNumber] = @SerialNumber,
		[ItemValue] = @ItemValue,
		[Material] = @Material,
		[Manufacturer] = @Manufacturer,
		[AgeYrs] = @AgeYrs,
		[AgeMonths] = @AgeMonths,
		[KeyWords] = @KeyWords,
		[LostPropertyGUID] = @LostPropertyGUID,
		[LostRoomNumber] = @LostRoomNumber,
		[whenLost] = @whenLost,
		[ReportedAsStolen] = @ReportedAsStolen,
		[Contents] = @Contents,
		[Description] = @Description,
		[ReportByContactGUID] = @ReportByContactGUID,
		[Owner] = @Owner,
		[AlternateContactGUID] = @AlternateContactGUID,
		[HotelGuest] = @HotelGuest,
		[GuestPropertyGUID] = @GuestPropertyGUID,
		[GuestRoom] = @GuestRoom,
		[PoliceReportFiled] = @PoliceReportFiled,
		[PoliceReportNumber] = @PoliceReportNumber,
		[PoliceReportOfficer] = @PoliceReportOfficer,
		[PoliceReportLocation] = @PoliceReportLocation,
		[InsuredByCompanyGUID] = @InsuredByCompanyGUID,
		[FollowUp] = @FollowUp,
		[FollowUpDate] = @FollowUpDate,
		[Notes] = @Notes,
		[Operator] = @Operator,
		[DateCreated] = @DateCreated,
		[LostLocation] = @LostLocation,
		[Sublocation] = @Sublocation,
		[ModifiedBy] = @ModifiedBy,
		[DateModified] = @DateModified
	WHERE [LostGUID] = @LostGUID
END
GO
