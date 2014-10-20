 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_MiniAuditViolation]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_MiniAuditViolation]
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_MiniAuditViolation]
(
	@MiniAuditEmployeeViolationGUID uniqueidentifier = null output,
	@MiniAuditGUID uniqueidentifier,
	@EmployeeGUID uniqueidentifier = null,
	@SupervisorGUID uniqueidentifier = null,
	@ViolationDateTime datetime,
	@ErrorType nvarchar(50),
	@Violation nvarchar(100),
	@ViolationDescription ntext,
	@Remarks ntext
)
AS
SET NOCOUNT ON
IF @MiniAuditEmployeeViolationGUID IS NULL
BEGIN
	set @MiniAuditEmployeeViolationGUID = NewID()
	INSERT INTO [MiniAuditViolation]
		([MiniAuditEmployeeViolationGUID],[MiniAuditGUID],[EmployeeGUID],[SupervisorGUID],[ViolationDateTime],
		[ErrorType],[Violation],[ViolationDescription],[Remarks])
	VALUES
	(@MiniAuditEmployeeViolationGUID,@MiniAuditGUID,@EmployeeGUID,@SupervisorGUID,
		@ViolationDateTime,	@ErrorType,@Violation,@ViolationDescription,@Remarks
	)
END
ELSE
	UPDATE [MiniAuditViolation] SET [MiniAuditGUID] = @MiniAuditGUID,
		[EmployeeGUID] = @EmployeeGUID,
		[SupervisorGUID] = @SupervisorGUID,
		[ViolationDateTime] = @ViolationDateTime,
		[ErrorType] = @ErrorType,
		[Violation] = @Violation,
		[ViolationDescription] = @ViolationDescription,
		[Remarks] = @Remarks
	WHERE [MiniAuditEmployeeViolationGUID] = @MiniAuditEmployeeViolationGUID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

