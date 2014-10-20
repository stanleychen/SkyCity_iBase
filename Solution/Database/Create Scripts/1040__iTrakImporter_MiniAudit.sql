if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_MiniAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_MiniAudit]
GO

CREATE TABLE [dbo].[__iTrakImporter_MiniAudit] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[PropertyGUID] [uniqueidentifier] NOT NULL ,
	[MiniAuditStartDateTime] [datetime] NOT NULL ,
	[MiniAuditEndDateTime] [datetime] NOT NULL ,
	[ReviewMethod] [varchar] (50) NOT NULL ,
	[VCRConsoleNumber] [varchar] (50) NULL ,
	[CreatedBy] [varchar] (50) NULL ,
	[DateCreated] [datetime] NULL ,
	[Closed] [bit] NULL ,
	[SecondAuditor] [varchar] (50) NULL ,
	[Area] [varchar] (100) NULL ,
	[Section] [varchar] (100) NULL ,
	[Remarks] [text] NULL ,
	[ModifiedBy] [varchar] (50) NULL ,
	[LastModifiedDate] [datetime] NULL ,
	[Number] [varchar] (50) NULL ,
	[RequestedByGUID] [uniqueidentifier] NULL ,
	[Status] [varchar] (50) NULL ,
	[SubjectGUID] [uniqueidentifier] NULL ,
	[Violation] [varchar] (100) NULL ,
	[ViolationDescription] [text] NULL,
	[uString] [varchar] (6000) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_MiniAudit] ADD 
	CONSTRAINT [PK___iTrakImporter_MiniAudit] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO


