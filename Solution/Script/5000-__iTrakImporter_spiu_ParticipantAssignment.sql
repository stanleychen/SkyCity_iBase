if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_ParticipantAssignment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_ParticipantAssignment]
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spiu_ParticipantAssignment]
(
	@IncidentNumber varchar(50),
	@SubjectSourceID varchar(50),
	@Assigned datetime,
	@AssignedBy varchar(16),
	@ParticipantType varchar(16),
	@ParticipantRole nvarchar(50),
	@ParticipantNotes varchar(6000),
	@SecondaryRole varchar(50),
	@PoliceContacted bit,
	@TakenFromScene bit,
	@PoliceContactedResult nvarchar(50)
)


