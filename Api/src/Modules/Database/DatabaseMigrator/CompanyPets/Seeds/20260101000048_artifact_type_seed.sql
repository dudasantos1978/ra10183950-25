DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

DECLARE @OrganizationId UNIQUEIDENTIFIER = '45143ad7-dc93-4664-982e-b93c7feb5f75';

INSERT INTO [tenants].[artifact_type] ( [id]
                                      , [created_at]
                                      , [updated_at]
                                      , [organization_id]
                                      , [inactived_at]
                                      , [name])
VALUES ( '4905f835-06f9-a62d-9feb-e6343939739e'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , N'Artifact Type 1');
GO
