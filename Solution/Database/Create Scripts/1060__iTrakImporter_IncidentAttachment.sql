if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_IncidentAttachment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_IncidentAttachment]
GO

CREATE TABLE [dbo].[__iTrakImporter_IncidentAttachment] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[SourceID] [varchar] (50) NOT NULL,
	[HostRowID] [int] NOT NULL ,
	[HostType] [varchar] (50) NOT NULL ,
	[Attached] [datetime] NULL ,
	[AttachedmentData] [image] NULL ,
	[OriginalFilename] [nvarchar] (256) NULL ,
	[AttachedBy] [varchar] (16) NULL ,
	[Thumbnail] [image] NULL ,
	[AttachedType] [nvarchar] (50) NULL ,
	[AttachmentSize] [bigint] NULL ,
	[Linked] [bit] NULL ,
	[DigitalSignature] [image] NULL ,
	[DataProviderType] [int] NULL ,
	[LastModifiedDate] [datetime] NULL ,
	[ServerCreateDate] [datetime] NULL,
	[Title] [varchar] (500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_IncidentAttachment] ADD 
	CONSTRAINT [PK___iTrakImporter_IncidentAttachment] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

