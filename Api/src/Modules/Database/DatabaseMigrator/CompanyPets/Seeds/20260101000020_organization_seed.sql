DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

INSERT INTO [tenants].[organization] ( [id]
                                     , [created_at]
                                     , [updated_at]
                                     , [name]
                                     , [document]
                                     , [slug])
VALUES ( '45143ad7-dc93-4664-982e-b93c7feb5f75'
       , @CurrentDate
       , @CurrentDate
       , N'Organization 1'
       , N'01.141.900/0001-01'
       , N'ACME');

INSERT INTO [tenants].[organization_plan] ( [id]
                                          , [created_at]
                                          , [updated_at]
                                          , [organization_id]
                                          , [inactived_at]
                                          , [plan_id]
                                          , [description]
                                          , [max_users]
                                          , [max_clients]
                                          , [max_tickets])
VALUES ( 'f85ea274-cac4-2c47-3051-1951d2f0c364'
       , @CurrentDate
       , @CurrentDate
       , '45143ad7-dc93-4664-982e-b93c7feb5f75'
       , NULL
       , '4dda0cb7-c327-4669-b657-e12ffcaf7205'
       , N'Organization 1 plan'
       , 100
       , 100
       , 1000);
GO
