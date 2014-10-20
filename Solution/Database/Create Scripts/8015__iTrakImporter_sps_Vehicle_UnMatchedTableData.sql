 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_Vehicle_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_Vehicle_UnMatchedTableData]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE [dbo].[__iTrakImporter_sps_Vehicle_UnMatchedTableData]
	@RowID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  dbo.__iTrakImporter_Vehicle_UnMatchedTableData.*
	FROM    dbo.__iTrakImporter_Vehicle (nolock) INNER JOIN
            dbo.__iTrakImporter_Vehicle_UnMatchedTableData (nolock) ON 
            dbo.__iTrakImporter_Vehicle.RowID = dbo.__iTrakImporter_Vehicle_UnMatchedTableData.VehicleRowID
	WHERE __iTrakImporter_Vehicle_UnMatchedTableData.VehicleRowID = @RowID
	ORDER BY __iTrakImporter_Vehicle_UnMatchedTableData.TableName

END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

