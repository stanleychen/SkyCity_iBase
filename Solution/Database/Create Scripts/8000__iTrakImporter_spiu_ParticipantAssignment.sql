 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_ParticipantAssignment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_ParticipantAssignment]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_ParticipantAssignment]
(
	@DetailedReportGuid uniqueidentifier,
	@ParticipantGuid uniqueidentifier,
	@Assigned datetime,
	@AssignedBy varchar(16),
	@ParticipantType varchar(16),
	@ParticipantRole nvarchar(50),
	@ParticipantNotes varchar(6000),
	@SecondaryRole varchar(50),
	@PoliceContacted bit,
	@TakenFromScene bit,
	@PoliceContactedResult nvarchar(50),
	@IsNew bit
)
AS

DECLARE @ParticipantRoleGuid uniqueidentifier

SELECT @ParticipantRoleGuid = SelectionGUID FROM DROPDOWNSELECTION (nolock)
	WHERE SelectionType='ParticipantRole' AND SelectionText = @ParticipantRole
	
IF 	@ParticipantRoleGuid IS NULL
BEGIN
	SET	@ParticipantRoleGuid = NEWID()
	INSERT INTO DropdownSelection (SelectionGUID, SelectionType, SelectionText, Hidden,ParentGUID,lock)
	 VALUES (@ParticipantRoleGuid, 'ParticipantRole', @ParticipantRole, 0, NULL,0)
END
   
IF @IsNew = 0
BEGIN -- UPDATE Existing Record

	UPDATE [ParticipantAssignment]
	SET [Assigned] = @Assigned,
		[AssignedBy] = @AssignedBy,
		[ParticipantType] = @ParticipantType,
		[ParticipantRole] = @ParticipantRoleGuid,
		[ParticipantNotes] = @ParticipantNotes,
		[SecondaryRole] = @SecondaryRole,
		[PoliceContacted] = @PoliceContacted,
		[TakenFromScene] = @TakenFromScene,
		[PoliceContactedResult] = @PoliceContactedResult
	WHERE DetailedReportGUID = @DetailedReportGuid	AND ParticipantGUID = @ParticipantGuid

END
ELSE -- INSERT
BEGIN

	INSERT INTO [ParticipantAssignment]
	(
		[DetailedReportGUID],
		[ParticipantGUID],
		[Assigned],
		[AssignedBy],
		[ParticipantType],
		[ParticipantRole],
		[ParticipantNotes],
		[SecondaryRole],
		[PoliceContacted],
		[TakenFromScene],
		[PoliceContactedResult]
	)
	VALUES
	(
		@DetailedReportGuid,
		@ParticipantGuid,
		@Assigned,
		@AssignedBy,
		@ParticipantType,
		@ParticipantRoleGuid,
		@ParticipantNotes,
		@SecondaryRole,
		@PoliceContacted,
		@TakenFromScene,
		@PoliceContactedResult
	)

END

RETURN 0   

