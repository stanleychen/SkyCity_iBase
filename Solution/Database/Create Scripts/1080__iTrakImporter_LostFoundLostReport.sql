 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_LostFoundLostReport]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_LostFoundLostReport]
GO

CREATE TABLE [dbo].[__iTrakImporter_LostFoundLostReport] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[LostUID] [varchar] (50) NOT NULL ,
	[ItemCategory] [nvarchar] (50) NULL ,
	[Colour] [nvarchar] (50) NULL ,
	[SerialNumber] [nvarchar] (50) NULL ,
	[ItemValue] [money] NULL ,
	[Material] [nvarchar] (50) NULL ,
	[Manufacturer] [nvarchar] (50) NULL ,
	[AgeYrs] [int] NULL ,
	[AgeMonths] [int] NULL ,
	[KeyWords] [ntext] NULL ,
	[LostPropertyGUID] [uniqueidentifier] NULL ,
	[LostRoomNumber] [nvarchar] (50) NULL ,
	[whenLost] [datetime] NULL ,
	[ReportedAsStolen] [bit] NULL ,
	[Contents] [ntext] NULL ,
	[Description] [nvarchar] (1000) NULL ,
	[ReportByContactGUID] [uniqueidentifier] NULL ,
	[Owner] [bit] NULL ,
	[AlternateContactGUID] [uniqueidentifier] NULL ,
	[HotelGuest] [bit] NULL ,
	[GuestPropertyGUID] [uniqueidentifier] NULL ,
	[GuestRoom] [nvarchar] (10) NULL ,
	[PoliceReportFiled] [bit] NULL ,
	[PoliceReportNumber] [nvarchar] (50) NULL ,
	[PoliceReportOfficer] [nvarchar] (50) NULL ,
	[PoliceReportLocation] [nvarchar] (50) NULL ,
	[InsuredByCompanyGUID] [uniqueidentifier] NULL ,
	[FollowUp] [bit] NULL ,
	[FollowUpDate] [datetime] NULL ,
	[Notes] [ntext] NULL ,
	[Operator] [nvarchar] (50) NULL ,
	[DateCreated] [datetime] NULL ,
	[LostLocation] [nvarchar] (100) NULL ,
	[Sublocation] [nvarchar] (50) NULL ,
	[ModifiedBy] [nvarchar] (16) NULL ,
	[uString] [varchar] (1000) NULL,
	[DateModified] [datetime] NULL,
	[IsReturned] [bit] NULL,
	[uText1Caption] [varchar] (50) NULL,
	[uText1Value] [ntext]  NULL
	

	
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_LostFoundLostReport] ADD 
	CONSTRAINT [PK___iTrakImporter_LostFoundLostReport] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

