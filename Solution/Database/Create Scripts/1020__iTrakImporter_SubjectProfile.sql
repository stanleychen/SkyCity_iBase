if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_SubjectProfile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_SubjectProfile]
GO

CREATE TABLE [dbo].[__iTrakImporter_SubjectProfile] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[FirstName] [varchar] (100) NULL ,
	[MiddleName] [varchar] (100) NULL ,
	[LastName] [varchar] (100) NULL ,
	[Gender] [varchar] (16) NULL ,
	[DateOfBirth] [datetime] NULL ,
	[AgeRangeLower] [int] NULL ,
	[AgeRangeUpper] [int] NULL ,
	[Height] [int] NULL ,
	[Weight] [int] NULL ,
	[HairColour] [varchar] (128) NULL ,
	[EyeColour] [varchar] (128) NULL ,
	[Race] [varchar] (128) NULL ,
	[DateCreated] [datetime] NULL ,
	[DateModified] [datetime] NULL ,
	[CreatoruserID] [varchar] (16) NULL ,
	[Password] [varchar] (50) NULL ,
	[PropertyGUID] [uniqueidentifier] NULL ,
	[Comment] [ntext] NULL ,	
	[BestAssetGUID] [uniqueidentifier] NULL ,
	[Category] [varchar] (50) NULL ,
	[Activities] [ntext] NULL ,
	[Specifics] [ntext] NULL ,
	[Aliases] [varchar] (500) NULL ,
	[Traits] [varchar] (500) NULL ,
	[Address] [varchar] (256) NULL ,
	[City] [varchar] (50) NULL ,
	[State] [varchar] (20) NULL ,
	[PostalCode] [varchar] (20) NULL ,
	[Country] [varchar] (50) NULL ,
	[HomePhone] [varchar] (30) NULL ,
	[WorkPhone] [varchar] (30) NULL ,
	[Email] [varchar] (128) NULL ,
	[ClientID] [varchar] (50) NULL ,
	[SINNumber] [varchar] (50) NULL ,
	[Occupation] [varchar] (50) NULL ,
	[DriversLicenseNum] [varchar] (50) NULL ,
	[SubjectId] [int] NULL ,
	[ModifiedBy] [varchar] (16) NULL ,
	[Custom1] [varchar] (50) NULL ,
	[Custom2] [varchar] (50) NULL ,
	[CompanyName] [varchar] (50) NULL ,
	[PassportNumber] [varchar] (100) NULL ,
	[BusinessFaxNumber] [varchar] (50) NULL ,
	[WebAddress] [varchar] (50) NULL ,
	[MediaFileName] [varchar] (150) NULL ,
	[IncidentNumber] [varchar] (150) NULL,
	[DetailedReportGuid] [uniqueidentifier] NULL,
	[LastIncidentDate] [datetime] NULL ,
	[IsBanned] [bit] NULL ,
	[BanStartDate] [datetime] NULL ,
	[BanEndDate] [datetime] NULL ,
	[BanIdentificationUsed] [varchar] (150) NULL ,
	[TypeOfBan] [varchar] (250) NULL ,
	[ReasonOfBan] [varchar] (250) NULL ,
	[IsPermanentBanned] [bit] NULL ,
	[SubjectCharged] [bit] NULL ,
	[LetterSent] [bit] NULL ,
	[RecordType] [int] NULL ,
	[Status] [int] NULL,
	[Groups] [text] NULL,
	[CompulsiveGambler] [bit] null,
	[SourceID] [varchar] (150) NULL ,
	[uString] [varchar] (4000) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_SubjectProfile] ADD 
	CONSTRAINT [PK___iTrakImporter_SubjectProfile] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

