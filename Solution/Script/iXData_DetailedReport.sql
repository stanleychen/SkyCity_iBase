if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[iXData_DetailedReport]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[iXData_DetailedReport]
GO

CREATE TABLE [dbo].[iXData_DetailedReport] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[DetailedReportGUID]  uniqueidentifier NOT NULL ,
	[Number] [varchar] (50) NOT NULL ,
	[BlotterGUID] [uniqueidentifier] NOT NULL ,
	[PropertyGUID] [uniqueidentifier] NOT NULL ,
	[Occured] [datetime] NOT NULL ,
	[EndTime] [datetime] NULL ,
	[Status] [varchar] (50) NULL ,
	[AmbulanceOffered] [bit] NULL ,
	[AmbulanceDeclined] [bit] NULL ,
	[FirstAidOffered] [bit] NULL ,
	[FirstAidDeclined] [bit] NULL ,
	[TaxiFareOffered] [bit] NULL ,
	[TaxiFareDeclined] [bit] NULL ,
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
	[IsGlobal] [bit] NULL ,
	[Location] [varchar] (50) NULL ,
	[SubLocation] [varchar] (50) NULL ,
	[PrimaryOperator] [varchar] (16) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[iXData_DetailedReport] ADD 
	CONSTRAINT [DF_iXData_DetailedReport_DetailedReportGUID] DEFAULT (newid()) FOR [DetailedReportGUID],
	CONSTRAINT [DF_iXData_DetailedReport_BlotterGUID] DEFAULT (newid()) FOR [BlotterGUID],
	CONSTRAINT [PK_iXData_DetailedReport] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

