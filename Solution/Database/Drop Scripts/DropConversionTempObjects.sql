if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_PropertyMappings]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_PropertyMappings]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporterView_PropertyMappings]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[__iTrakImporterView_PropertyMappings]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_DetailedReport]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_DetailedReport]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_ParticipantAssignment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_ParticipantAssignment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_ParticipantAssignment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_ParticipantAssignment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_SubjectProfile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_SubjectProfile]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_Vehicle]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_Vehicle]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_Vehicles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_Vehicles]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_GameAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_GameAudit]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_GameAudit_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_GameAudit_UnMatchedTableData]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_GameAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_GameAudit]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_MiniAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_MiniAudit]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_MiniAudit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_MiniAudit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_MiniAuditViolation]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_MiniAuditViolation]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_MiniAuditViolation]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_MiniAuditViolation]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_RecordAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_RecordAttachment]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_RecordAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_RecordAttachment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_DetailedReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_DetailedReport]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_Blotter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_Blotter]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_DetailedReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_DetailedReport]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_iTrakImporter_DetailedReport_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_iTrakImporter_DetailedReport_UnMatchedTableData]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_SubjectProfile]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_SubjectProfile]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_SubjectProfile]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_SubjectProfile]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_GameAudit_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_GameAudit_UnMatchedTableData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spi_RecordAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spi_RecordAttachment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_Vehicles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_Vehicles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_GameAudit_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_GameAudit_UnMatchedTableData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_MiniAudit_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_MiniAudit_UnMatchedTableData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_DetailedReport_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_DetailedReport_UnMatchedTableData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_GameAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_GameAudit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_MiniAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_MiniAudit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_MiniAudit_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_MiniAudit_UnMatchedTableData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_MiniAuditViolation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_MiniAuditViolation]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_IncidentAttachment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_IncidentAttachment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_SubjectBan]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_SubjectBan]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_IncidentAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_IncidentAttachment]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_IncidentAttachment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_spiu_IncidentAttachment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_SubjectBan]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_SubjectBan]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_SubjectBant]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_SubjectBan]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_BanWatchStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_BanWatchStatus]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_LostFoundLostReport]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_LostFoundLostReport]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_LostFoundFoundReport]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_LostFoundFoundReport]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_LostFoundFoundReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_LostFoundFoundReport]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_LostFoundLostReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_LostFoundLostReport]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_LostFoundFoundReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_LostFoundFoundReport]
GO

 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_LostFoundLostReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_LostFoundLostReport]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_LostFoundReturnVerification]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_LostFoundReturnVerification]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_LostFoundDisposalReport]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_LostFoundDisposalReport]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_spiu_SubjectBan]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[__iTrakImporter_spiu_SubjectBan]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_ParticipantAssignment]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_ParticipantAssignment]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_SubjectBan]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_SubjectBan]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_Vehicle_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[__iTrakImporter_Vehicle_UnMatchedTableData]
GO
 if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[__iTrakImporter_sps_Vehicle_UnMatchedTableData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[__iTrakImporter_sps_Vehicle_UnMatchedTableData]
GO



