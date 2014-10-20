 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_LostFoundReturnVerification]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_LostFoundReturnVerification]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_LostFoundReturnVerification]
(
	@FoundReportGUID uniqueidentifier,
	@LostReportGUID uniqueidentifier = null,
	@ReturnDate datetime,
	@EmployeeGUID uniqueidentifier = null,
	@ID1 nvarchar(50),
	@ID1Number nvarchar(50),
	@ID2 nvarchar(50),
	@ID2Number nvarchar(50),
	@ItemReturned tinyint,
	@ItemToBeMailed bit,
	@DeliveryCost bit,
	@DeliveryInvoiceID nvarchar(50),
	@MailInfoGUID uniqueidentifier = null,
	@RewardOffered bit,
	@RewardAmount money,
	@RewardPaidToGUID uniqueidentifier = null,
	@PhotoGUID uniqueidentifier = null,
	@Operator nvarchar(50),
	@DateCreated datetime,
	@SignString ntext = null,
	@SignEncryptBytes image = null,
	@SignEncryptIV binary(20) = null,
	@ReturnDueDate datetime = null,
	@IsNew bit = 0
)

AS

SET NOCOUNT ON
IF @IsNew  = 1
BEGIN
	INSERT INTO [LostFoundReturnVerification]
	(
		[FoundReportGUID],[LostReportGUID],[ReturnDate],[EmployeeGUID],[ID1],[ID1Number],[ID2],[ID2Number],[ItemReturned],
		[ItemToBeMailed],[DeliveryCost],[DeliveryInvoiceID],[MailInfoGUID],[RewardOffered],[RewardAmount],[RewardPaidToGUID],
		[PhotoGUID],[Operator],[DateCreated],[SignString],[SignEncryptBytes],[SignEncryptIV],[ReturnDueDate]
	)
	VALUES
	(
		@FoundReportGUID,@LostReportGUID,@ReturnDate,@EmployeeGUID,@ID1,@ID1Number,@ID2,@ID2Number,@ItemReturned,
		@ItemToBeMailed,@DeliveryCost,@DeliveryInvoiceID,@MailInfoGUID,@RewardOffered,@RewardAmount,@RewardPaidToGUID,
		@PhotoGUID,@Operator,@DateCreated,@SignString,@SignEncryptBytes,@SignEncryptIV,@ReturnDueDate
	)
END
ELSE
BEGIN
	UPDATE [LostFoundReturnVerification]
	SET [LostReportGUID] = @LostReportGUID,
		[ReturnDate] = @ReturnDate,
		[EmployeeGUID] = @EmployeeGUID,
		[ID1] = @ID1,
		[ID1Number] = @ID1Number,
		[ID2] = @ID2,
		[ID2Number] = @ID2Number,
		[ItemReturned] = @ItemReturned,
		[ItemToBeMailed] = @ItemToBeMailed,
		[DeliveryCost] = @DeliveryCost,
		[DeliveryInvoiceID] = @DeliveryInvoiceID,
		[MailInfoGUID] = @MailInfoGUID,
		[RewardOffered] = @RewardOffered,
		[RewardAmount] = @RewardAmount,
		[RewardPaidToGUID] = @RewardPaidToGUID,
		[PhotoGUID] = @PhotoGUID,
		[Operator] = @Operator,
		[DateCreated] = @DateCreated,
		[SignString] = @SignString,
		[SignEncryptBytes] = @SignEncryptBytes,
		[SignEncryptIV] = @SignEncryptIV,
		[ReturnDueDate] = @ReturnDueDate
	WHERE [FoundReportGUID] = @FoundReportGUID
END