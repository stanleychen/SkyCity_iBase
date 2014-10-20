 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_LostFoundFoundReport]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_LostFoundFoundReport]
GO

CREATE TABLE [dbo].[__iTrakImporter_LostFoundFoundReport] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[PropertyGUID] [uniqueidentifier] NOT NULL ,
	[FoundUID] [varchar] (50) NOT NULL ,
	[ItemCategory] [nvarchar] (50) NULL ,
	[ItemValue] [money] NULL ,
	[colour] [nvarchar] (20) NULL ,
	[SerialNumber] [nvarchar] (50) NULL ,
	[Material] [nvarchar] (50) NULL ,
	[Manufacturer] [nvarchar] (50) NULL ,
	[AgeYrs] [int] NULL ,
	[AgeMonths] [int] NULL ,
	[Contents] [ntext] NULL ,
	[Description] [ntext] NULL ,
	[KeyWords] [ntext] NULL ,
	[FoundDateTime] [datetime] NULL ,
	[FoundStatus] [nvarchar] (50) NULL ,
	[SpecificLocation] [nvarchar] (50) NULL ,
	[FoundByContactUID] [uniqueidentifier] NULL ,
	[ReportByContactUID] [uniqueidentifier] NULL ,
	[ReceivedByEmployeeUID] [uniqueidentifier] NULL ,
	[StoreLocation] [nvarchar] (50) NULL ,
	[AdditionalInfo] [ntext] NULL ,
	[HoldUntil] [datetime] NULL ,
	[BestImageGUID] [uniqueidentifier] NULL ,
	[Operator] [nvarchar] (50) NULL ,
	[Barcode] [nvarchar] (50) NULL ,
	[DateCreated] [datetime] NULL ,
	[Department] [nvarchar] (50) NULL ,
	[Location] [nvarchar] (100) NULL ,
	[Sublocation] [nvarchar] (50) NULL ,
	[ModifiedBy] [nvarchar] (16) NULL ,
	[DateModified] [datetime] NULL,
	[ReturnDate] [DateTime] NULL,
	[IsDisposed] [bit] NULL,
	[DispositionInfo] [nvarchar] (200) NULL,
	[DispositionDescription] [nvarchar] (200) null,
	[IsReturned] [bit] NULL,
	[uString] [varchar] (5000) NULL
	
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_LostFoundFoundReport] ADD 
	CONSTRAINT [PK___iTrakImporter_LostFoundFoundReport] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

