if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_GameAudit_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_GameAudit_UnMatchedTableData]
GO

CREATE TABLE [dbo].[__iTrakImporter_GameAudit_UnMatchedTableData] (
	[RowID] [int] IDENTITY (1, 1) NOT NULL ,
	[GameAuditRowID] INT NOT NULL ,
	[TableName] [varchar] (50) NULL ,
	[uTableString] [varchar] (6000) NULL ,
	[uTableText1Caption] [varchar] (50) NULL ,
	[uTableText1Value] [text] NULL ,
	[uTableText2Caption] [varchar] (50) NULL ,
	[uTableText2Value] [text] NULL ,
	[uTableText3Caption] [varchar] (50) NULL ,
	[uTableText3Value] [text] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_GameAudit_UnMatchedTableData] ADD 
	CONSTRAINT [PK___iTrakImporter_GameAudit_UnMatchedTableData] PRIMARY KEY  CLUSTERED 
	(
		[RowID]
	)  ON [PRIMARY] 
GO

 