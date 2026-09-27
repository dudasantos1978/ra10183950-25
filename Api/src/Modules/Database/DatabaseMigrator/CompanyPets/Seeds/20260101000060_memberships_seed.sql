DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

DECLARE @OrganizationId UNIQUEIDENTIFIER = '45143ad7-dc93-4664-982e-b93c7feb5f75';

INSERT INTO [tenants].[organization_user] ( [id]
                                          , [created_at]
                                          , [updated_at]
                                          , [organization_id]
                                          , [inactived_at]
                                          , [user_id]
                                          , [team_id])
VALUES ( 'c10feb2b-fd6c-61ca-ea9b-8df5d549df45'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , '6a508a12-6e9b-4ef6-bdb9-8372347cb8a8'
       , '592b24f8-cd88-641d-8994-f669e5ac59e9')

     , ( 'cc97f6c2-ab1b-2418-7161-9a0fd0f2ec11'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , '820a63c4-5e10-0664-6a32-a1325293b324'
       , 'dc8900d5-5740-2a1c-66a4-2f6346cd5900');

INSERT INTO [tenants].[customer_user] ( [id]
                                      , [created_at]
                                      , [updated_at]
                                      , [organization_id]
                                      , [inactived_at]
                                      , [customer_id]
                                      , [user_id]
                                      , [role_id])
VALUES ( 'ed636d59-4f4e-499a-6e82-adacd8ca98e7'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , '9d673361-ea55-292c-89f9-b4f94b676aca'
       , '6a508a12-6e9b-4ef6-bdb9-8372347cb8a8'
       , '7d99c5e6-e0ca-4189-b9aa-fb2c8c5ba0f5')

     , ( 'ddb05893-4433-6b95-6956-58304d41e1be'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , '9d673361-ea55-292c-89f9-b4f94b676aca'
       , '820a63c4-5e10-0664-6a32-a1325293b324'
       , '11fe3aaa-d36d-4cae-9bce-7c5084360425');
GO
