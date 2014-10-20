if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_Vehicle]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_Vehicle]
GO

CREATE TABLE [dbo].[__iTrakImporter_Vehicle] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[SourceKey] [varchar] (50) NOT NULL ,
	[License] [varchar] (20) NULL ,
	[Country] [nvarchar] (100) NULL ,
	[State] [nvarchar] (50) NULL ,
	[Condition] [ntext] NULL ,
	[Color] [varchar] (50) NULL ,
	[VIN] [varchar] (50) NULL ,
	[Odometer] [nvarchar] (50) NULL ,
	[Make] [varchar] (50) NULL ,
	[Model] [nvarchar] (50) NULL ,
	[VehicleType] [varchar] (50) NULL ,
	[Year] [nvarchar] (4) NULL ,
	[SubjectSourceID] [varchar] (50) NULL ,
	[Note] [ntext] NULL ,
	[DateCreated] [datetime] NULL ,
	[DateModified] [datetime] NULL ,
	[CreatedBy] [nvarchar] (16) NULL ,
	[ModifiedBy] [varchar] (16) NULL ,
	[IncidentNumber] [nvarchar] (50) NULL ,
	[uString] [varchar] (6000) NULL ,
	[uText1Caption] [varchar] (50) NULL ,
	[uText1Value] [text] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_Vehicle] ADD 
	CONSTRAINT [PK___iTrakImporter_Vehicle] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] ,
	CONSTRAINT [IX___iTrakImporter_Vehicle_SourceKey] UNIQUE  NONCLUSTERED 
	(
		[SourceKey]
	)  ON [PRIMARY] 
GO

