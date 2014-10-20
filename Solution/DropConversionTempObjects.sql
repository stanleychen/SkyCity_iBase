if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_ParticipantAssignment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_ParticipantAssignment]
GO

