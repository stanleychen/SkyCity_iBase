if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_PropertyMappings]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_PropertyMappings]
GO

CREATE TABLE [dbo].[__iTrakImporter_PropertyMappings] (
	[SourcePropertyCode] [varchar] (255) NOT NULL ,
	[iTrakPropertyName] [varchar] (255) NOT NULL ,
	[Description] [varchar] (255) NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[__iTrakImporter_PropertyMappings] ADD 
	CONSTRAINT [PK___iTrakImporter_PropertyMappings] PRIMARY KEY  CLUSTERED 
	(
		[SourcePropertyCode]
	)  ON [PRIMARY] 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporterView_PropertyMappings]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[__iTrakImporterView_PropertyMappings]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW dbo.__iTrakImporterView_PropertyMappings
AS
SELECT     dbo.__iTrakImporter_PropertyMappings.SourcePropertyCode, dbo.SystemProperty.PropertyGUID
FROM         dbo.__iTrakImporter_PropertyMappings INNER JOIN
                      dbo.SystemProperty ON dbo.__iTrakImporter_PropertyMappings.iTrakPropertyName = dbo.SystemProperty.PropertyName

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


INSERT INTO [__iTrakImporter_PropertyMappings](SourcePropertyCode,iTrakPropertyName,Description)
VALUES('AKL','Auckland Security','Auckland')
GO
INSERT INTO __iTrakImporter_PropertyMappings(SourcePropertyCode,iTrakPropertyName,Description)
VALUES('QTN','Queenstown Security','Queenstown')
GO
INSERT INTO __iTrakImporter_PropertyMappings(SourcePropertyCode,iTrakPropertyName,Description)
VALUES('HAM','Hamilton Security','Hamilton')
GO
