 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_SubjectProfile]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_SubjectProfile]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE [dbo].[__iTrakImporter_sps_SubjectProfile]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ss.*,s.SubjectGuid as SubjectGUID FROM __iTrakImporter_SubjectProfile ss  (nolock) 
            left outer join _ConversionTmp  ct (nolock) on ss.SourceID = ct.ConvertId1 
            left outer join SubjectProfile s (nolock)  on ct.ConvertGUID = s.SubjectGUID 
            WHERE ct.TableName='SubjectProfile' or ct.ConvertGuid is null

END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

