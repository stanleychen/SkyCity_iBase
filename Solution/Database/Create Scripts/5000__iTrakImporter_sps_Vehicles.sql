 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_Vehicles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_Vehicles]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_Vehicles] AS

SELECT     iv.*, v.VehicleGUID AS iTrakVehicleGuid, s.SubjectGUID AS iTrakSubjectGuid
FROM __iTrakImporter_Vehicle iv (nolock) left outer join (SELECT * FROM _ConversionTmp (nolock) WHERE TableName='Vehicle') vt 
ON iv.SourceKey = vt.ConvertID1 left outer join Vehicle  v (nolock) 
ON vt.ConvertGuid = v.VehicleGuid left outer join (SELECT * FROM _ConversionTmp (nolock) WHERE TableName='SubjectProfile') st
ON iv.SubjectSourceID = st.ConvertID1 left outer join SubjectProfile  s (nolock)
ON st.ConvertGuid = s.SubjectGuid

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

