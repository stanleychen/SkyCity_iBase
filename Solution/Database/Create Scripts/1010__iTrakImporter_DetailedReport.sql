if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_DetailedReport]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_DetailedReport]
GO

CREATE TABLE [dbo].[__iTrakImporter_DetailedReport] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[BlotterNumber] [varchar] (50) NOT NULL,
	[IncidentNumber] [varchar] (50) NOT NULL,
	[GameDisputeNumber] [varchar] (50)  NULL,
	[PropertyGUID] [uniqueidentifier] NOT NULL ,
	[Occured] [datetime] NOT NULL ,
	[EndTime] [datetime] NULL ,
	[Status] [varchar] (50) NULL ,
	[ClosedBy] [varchar] (16) NULL ,
	[Closed] [datetime] NULL ,
	[Created] [datetime] NULL ,
	[CreatedBy] [varchar] (16) NULL ,
	[Type] [varchar] (100) NULL ,
	[Specific] [varchar] (100) NULL ,
	[Category] [varchar] (100) NULL ,
	[SecondaryOperator] [varchar] (50) NULL ,
	[Synopsis] [text] NULL ,
	[Details] [ntext] NULL ,
	[ClosingRemarks] [text] NULL ,
	[LastModifiedDate] [datetime] NULL ,
	[ExecutiveBrief] [text] NULL ,
	[ModifiedBy] [varchar] (16) NULL ,
	[ArchiveDate] [datetime] NULL ,	
	[Location] [varchar] (50) NULL ,
	[SubLocation] [varchar] (50) NULL,
	[UnmatchedData] [varchar](6000) NULL,
	[uText1Caption] [varchar] (50) NULL,
	[uText1Value] [text]  NULL,
	[uText2Caption] [varchar] (50) NULL,
	[uText2Value] [text]  NULL,
	[uText3Caption] [varchar] (50) NULL,
	[uText3Value] [text]  NULL,
	[uText4Caption] [varchar] (50) NULL,
	[uText4Value] [text]  NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_DetailedReport] ADD 
	CONSTRAINT [PK___iTrakImporter_DetailedReport] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

