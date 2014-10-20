if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_SubjectBan]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_SubjectBan]
GO

CREATE TABLE [dbo].[__iTrakImporter_SubjectBan] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[SubjectRowID] [int] NOT NULL ,
	[IncidentRowID] [int] NOT NULL ,
	[Commencement] [datetime] NULL ,
	[IsPermanent] [bit] NULL ,
	[EndDate] [datetime] NULL ,
	[SubjectCharged] [bit] NULL ,
	[LetterSent] [bit] NULL ,
	[CompulsiveGambler] [bit] NULL ,
	[RecordType] [int] NULL ,
	[TypeOfBan] [varchar] (100) NULL ,
	[IdentificationUsed] [varchar] (100) NULL ,
	[ReasonForBan] [varchar] (100) NULL ,
	[SelfExclusiveReport] [image] NULL ,
	[RemovedBanIncidentGuid] [uniqueidentifier] NULL ,
	[OriginalBanIncidentGuid] [uniqueidentifier] NULL ,
	[Removed] [bit] NULL ,
	[RemovalDate] [datetime] NULL,
	[BanDescriptionText] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_SubjectBan] ADD 
	CONSTRAINT [PK___iTrakImporter_SubjectBan] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

 