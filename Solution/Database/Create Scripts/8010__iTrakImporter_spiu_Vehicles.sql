 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_Vehicles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_Vehicles]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_Vehicles]
(
	@VehicleGUID uniqueidentifier = null output,
	@SourceKey varchar(50),
	@BestImageGUID uniqueidentifier = null,
	@License nvarchar(20),
	@Country nvarchar(100),
	@State nvarchar(50),
	@Condition ntext,
	@Color nvarchar(50),
	@VIN nvarchar(50),
	@Odometer nvarchar(50),
	@Make nvarchar(50),
	@Model nvarchar(50),
	@VehicleType nvarchar(50),
	@Year nvarchar(4),
	@Owner uniqueidentifier = null,
	@Note ntext,
	@DateCreated datetime,
	@DateModified datetime,
	@CreatedBy nvarchar(16),
	@ModifiedBy nvarchar(16)
)

AS

SET NOCOUNT ON

IF @VehicleGUID is null --new 
BEGIN
	SELECT @VehicleGUID = ConvertGuid FROM _ConversionTmp WHERE TableName='Vehicle' and ConvertId1=@SourceKey
	
	IF @VehicleGUID IS NULL
	BEGIN
		SET @VehicleGUID = NewID()
		INSERT INTO _ConversionTmp(ConvertGuid,TableName,ConvertId1,CreateDate) 
		VALUES(@VehicleGUID,'Vehicle',@SourceKey,GetDate())
	END 

	INSERT INTO [Vehicle]([VehicleGUID],[BestImageGUID],[License],[Country],[State],[Condition],[Color],[VIN],[Odometer],
		[Make],[Model],[VehicleType],[Year],[Owner],[Note],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy],[LockForAPI]
	) VALUES (@VehicleGUID,@BestImageGUID,@License,@Country,@State,@Condition,@Color,@VIN,@Odometer,@Make,@Model,@VehicleType,
		@Year,@Owner,@Note,@DateCreated,@DateModified,@CreatedBy,@ModifiedBy,0)
		
END	
ELSE
	UPDATE [Vehicle] SET [BestImageGUID] = @BestImageGUID,[License] = @License,	[Country] = @Country,
	[State] = @State,[Condition] = @Condition,[Color] = @Color,	[VIN] = @VIN,[Odometer] = @Odometer,
	[Make] = @Make,	[Model] = @Model,[VehicleType] = @VehicleType,[Year] = @Year,[Owner] = @Owner,
	[Note] = @Note,	[DateCreated] = @DateCreated,[DateModified] = @DateModified,[CreatedBy] = @CreatedBy,
	[ModifiedBy] = @ModifiedBy
WHERE [VehicleGUID] = @VehicleGUID
GO

