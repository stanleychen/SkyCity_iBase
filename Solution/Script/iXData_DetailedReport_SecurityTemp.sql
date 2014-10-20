if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_DetailedReport_SecurityTemp]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_DetailedReport_SecurityTemp]
GO

CREATE TABLE [dbo].[__iTrakImporter_DetailedReport_SecurityTemp] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[Number] [varchar] (50) NOT NULL ,
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
	[Details] [text] NULL ,
	[ClosingRemarks] [text] NULL ,
	[LastModifiedDate] [datetime] NULL ,
	[ExecutiveBrief] [text] NULL ,
	[ModifiedBy] [varchar] (16) NULL ,
	[ArchiveDate] [datetime] NULL ,
	[Location] [varchar] (50) NULL ,
	[SubLocation] [varchar] (50) NULL ,
	[PrimaryOperator] [varchar] (16) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

