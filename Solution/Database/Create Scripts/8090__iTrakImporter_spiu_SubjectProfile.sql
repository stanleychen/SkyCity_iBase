 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_SubjectProfile]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_SubjectProfile]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_SubjectProfile]
(
	@SubjectGUID uniqueidentifier = null output,
	@SubjectSourceID nvarchar(50),
	@FirstName nvarchar(100),
	@MiddleName nvarchar(100),
	@LastName nvarchar(100),
	@Gender nvarchar(16),
	@DateOfBirth datetime,
	@AgeRangeLower int,
	@AgeRangeUpper int,
	@Height int,
	@Weight int,
	@HairColour nvarchar(128),
	@EyeColour nvarchar(128),
	@Race nvarchar(128),
	@DateCreated datetime,
	@DateModified datetime,
	@CreatoruserID nvarchar(16),
	@PropertyGUID uniqueidentifier = null,
	@Comment ntext,
	@LastIncidentDate datetime,
	@FRSAcSysUserID int = 0,
	@Exclusive ntext = null,
	@BestAssetGUID uniqueidentifier = null,
	@Category nvarchar(50),
	@Activities ntext,
	@Specifics ntext,
	@Groups ntext,
	@Aliases ntext,
	@Traits ntext,
	@Address nvarchar(256),
	@City nvarchar(50),
	@State nvarchar(20),
	@PostalCode nvarchar(20),
	@Country nvarchar(50),
	@HomePhone nvarchar(30),
	@WorkPhone nvarchar(30),
	@Email nvarchar(128),
	@ClientID nvarchar(50),
	@SINNumber nvarchar(50),
	@Occupation nvarchar(50),
	@DriversLicenseNum nvarchar(50),
	@DigitalSignature image = null,
	@DataProviderType int = 0,
	@ModifiedBy nvarchar(16),
	@Custom1 nvarchar(50),
	@Custom2 nvarchar(50),
	@CompanyName nvarchar(50),
	@PassportNumber nvarchar(100),
	@BusinessFaxNumber nvarchar(50),
	@WebAddress nvarchar(250)
)

AS

SET NOCOUNT ON
 
IF @SubjectGUID IS NULL 
BEGIN
	SET @SubjectGUID = NewID()
	INSERT INTO [SubjectProfile]
	(
		[SubjectGUID],[FirstName],[MiddleName],[LastName],[Gender],[DateOfBirth],[AgeRangeLower],[AgeRangeUpper],
		[Height],[Weight],[HairColour],[EyeColour],[Race],[DateCreated],[DateModified],[CreatoruserID],[PropertyGUID],
		[Comment],[LastIncidentDate],[FRSAcSysUserID],[Exclusive],[BestAssetGUID],[Category],[Activities],
		[Specifics],[Groups],[Aliases],[Traits],[Address],[City],[State],[PostalCode],[Country],[HomePhone],[WorkPhone],
		[Email],[ClientID],[SINNumber],[Occupation],[DriversLicenseNum],[DigitalSignature],[DataProviderType],
		[ModifiedBy],[Custom1],[Custom2],[CompanyName],[PassportNumber],[BusinessFaxNumber],[WebAddress]
	)
	VALUES
	(
		@SubjectGUID,@FirstName,@MiddleName,@LastName,@Gender,@DateOfBirth,@AgeRangeLower,@AgeRangeUpper,
		@Height,@Weight,@HairColour,@EyeColour,@Race,@DateCreated,@DateModified,@CreatoruserID,
		@PropertyGUID,@Comment,@LastIncidentDate,@FRSAcSysUserID,@Exclusive,@BestAssetGUID,@Category,
		@Activities,@Specifics,@Groups,@Aliases,@Traits,@Address,@City,@State,@PostalCode,@Country,
		@HomePhone,@WorkPhone,@Email,@ClientID,@SINNumber,@Occupation,@DriversLicenseNum,@DigitalSignature,
		@DataProviderType,@ModifiedBy,@Custom1,@Custom2,@CompanyName,@PassportNumber,@BusinessFaxNumber,
		@WebAddress
	)

	INSERT INTO [_ConversionTmp]
	(
		[ConvertGuid],[TableName],[ConvertId1],[ConvertId2],[CreateDate]
	)
	VALUES
	(
		@SubjectGUID,'SubjectProfile',@SubjectSourceID,null,GetDate()
	)

END
ELSE
BEGIN

	UPDATE [SubjectProfile]
	SET [FirstName] = @FirstName,
		[MiddleName] = @MiddleName,
		[LastName] = @LastName,
		[Gender] = @Gender,
		[DateOfBirth] = @DateOfBirth,
		[AgeRangeLower] = @AgeRangeLower,
		[AgeRangeUpper] = @AgeRangeUpper,
		[Height] = @Height,
		[Weight] = @Weight,
		[HairColour] = @HairColour,
		[EyeColour] = @EyeColour,
		[Race] = @Race,
		[DateCreated] = @DateCreated,
		[DateModified] = @DateModified,
		[CreatoruserID] = @CreatoruserID,
		[PropertyGUID] = @PropertyGUID,
		[Comment] = @Comment,
		[LastIncidentDate] = @LastIncidentDate,
		[FRSAcSysUserID] = @FRSAcSysUserID,
		[Exclusive] = @Exclusive,
		[BestAssetGUID] = @BestAssetGUID,
		[Category] = @Category,
		[Activities] = @Activities,
		[Specifics] = @Specifics,
		[Groups] = @Groups,
		[Aliases] = @Aliases,
		[Traits] = @Traits,
		[Address] = @Address,
		[City] = @City,
		[State] = @State,
		[PostalCode] = @PostalCode,
		[Country] = @Country,
		[HomePhone] = @HomePhone,
		[WorkPhone] = @WorkPhone,
		[Email] = @Email,
		[ClientID] = @ClientID,
		[SINNumber] = @SINNumber,
		[Occupation] = @Occupation,
		[DriversLicenseNum] = @DriversLicenseNum,
		[DigitalSignature] = @DigitalSignature,
		[DataProviderType] = @DataProviderType,
		[ModifiedBy] = @ModifiedBy,
		[Custom1] = @Custom1,
		[Custom2] = @Custom2,
		[CompanyName] = @CompanyName,
		[PassportNumber] = @PassportNumber,
		[BusinessFaxNumber] = @BusinessFaxNumber,
		[WebAddress] = @WebAddress
	WHERE [SubjectGUID] = @SubjectGUID
END 

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

