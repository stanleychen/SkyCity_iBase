 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_MiniAuditViolation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_MiniAuditViolation]
GO

CREATE TABLE [dbo].[__iTrakImporter_MiniAuditViolation] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[MiniAuditRowID] [int] NOT NULL ,
	[EmployeeGUID] [uniqueidentifier] NULL ,
	[SupervisorGUID] [uniqueidentifier] NULL ,
	[ViolationDateTime] [datetime] NOT NULL ,
	[ErrorType] [nvarchar] (50)  NULL ,
	[Violation] [nvarchar] (100) NULL ,
	[ViolationDescription] [ntext] NULL ,
	[Remarks] [ntext] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_MiniAuditViolation] ADD 
	CONSTRAINT [PK___iTrakImporter_MiniAuditViolation] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

