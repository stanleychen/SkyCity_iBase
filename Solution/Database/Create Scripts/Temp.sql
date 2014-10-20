if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spi_DropdownSelection]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spi_DropdownSelection]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_spi_DropdownSelection]
(@SelectionGUID uniqueidentifier,@SelectionType nvarchar(50),@SelectionText nvarchar(50),@Hidden bit,@ParentGUID uniqueidentifier, @lock bit)
AS

if not exists(select * from DropdownSelection where SelectionGUID = @SelectionGUID)
	INSERT INTO DropdownSelection (SelectionGUID, SelectionType, SelectionText, Hidden,ParentGUID,lock) VALUES (@SelectionGUID, @SelectionType, @SelectionText, @Hidden, @ParentGUID,@lock)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

 