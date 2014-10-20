 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_GameAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_GameAudit]
GO

CREATE TABLE [dbo].[__iTrakImporter_GameAudit] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[PropertyGUID] [uniqueidentifier] NOT NULL ,
	[ReviewMethod] [varchar] (50) NOT NULL ,
	[GameAuditStartDateTime] [datetime] NOT NULL ,
	[GameAuditEndDateTime] [datetime] NOT NULL ,
	[VCRConsoleNumber] [varchar] (50) NULL ,
	[CreatorUserID] [varchar] (50) NULL ,
	[LastModifiedDate] [datetime] NULL ,
	[Closed] [bit] NULL ,
	[Archived] [bit] NULL ,
	[AreaAudited] [varchar] (100) NULL ,
	[Section] [varchar] (100) NULL ,
	[Remarks] [text] NULL ,
	[ModifiedBy] [varchar] (16) NULL ,
	[DateCreated] [datetime] NULL ,
	[SecondAuditor] [varchar] (50) NULL ,
	[Number] [varchar] (50) NULL ,
	[RequestedByGUID] [uniqueidentifier] NULL ,
	[Status] [varchar] (50) NULL,
	[uString] [varchar] (6000) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_GameAudit] ADD 
	CONSTRAINT [PK___iTrakImporter_GameAudit] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

