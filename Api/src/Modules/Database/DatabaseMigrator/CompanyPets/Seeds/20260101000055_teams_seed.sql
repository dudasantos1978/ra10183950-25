DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

DECLARE @OrganizationId UNIQUEIDENTIFIER = '45143ad7-dc93-4664-982e-b93c7feb5f75';

INSERT INTO [tenants].[team] ( [id]
                             , [created_at]
                             , [updated_at]
                             , [organization_id]
                             , [inactived_at]
                             , [name]
                             , [code]
                             , [role_id])
VALUES ( '592b24f8-cd88-641d-8994-f669e5ac59e9'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , N'Default Team'
       , N'DEFAULT_TEAM'
       , 'e597e22c-e22e-4849-928c-e919717d2b8c')

     , ( 'dc8900d5-5740-2a1c-66a4-2f6346cd5900'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , N'Team 2'
       , N'TEAM001'
       , 'dc8c024b-69e2-4800-af50-f0fbd690279d');
GO
