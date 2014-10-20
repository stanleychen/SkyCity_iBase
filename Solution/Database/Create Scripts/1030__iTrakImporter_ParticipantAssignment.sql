if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_ParticipantAssignment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_ParticipantAssignment]
GO

CREATE TABLE [dbo].[__iTrakImporter_ParticipantAssignment] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[IncidentRowID] [int] NOT NULL ,
	[SubjectRowID] [int] NOT NULL ,
	[Assigned] [datetime] NULL ,
	[AssignedBy] [varchar] (16) NULL ,
	[ParticipantType] [varchar] (16) NULL ,
	[ParticipantRole] [varchar] (50) NULL ,
	[ParticipantNotes] [varchar] (6000) NULL ,
	[SecondaryRole] [varchar] (50) NULL ,
	[PoliceContacted] [bit] NULL ,
	[TakenFromScene] [bit] NULL ,
	[PoliceContactedResult] [nvarchar] (50) NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_ParticipantAssignment] ADD 
	CONSTRAINT [PK___iTrakImporter_ParticipantAssignment] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

