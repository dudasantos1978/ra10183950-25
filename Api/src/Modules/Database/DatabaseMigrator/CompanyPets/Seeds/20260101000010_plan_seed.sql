DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

INSERT INTO [tenants].[plan] ( [id]
                             , [created_at]
                             , [updated_at]
                             , [inactived_at]
                             , [name]
                             , [description]
                             , [max_users]
                             , [max_clients]
                             , [max_tickets])
VALUES ( '4dda0cb7-c327-4669-b657-e12ffcaf7205'
       , @CurrentDate
       , @CurrentDate
       , NULL
       , N'Plan 1'
       , N'Seed plan 1'
       , 100
       , 100
       , 1000);
GO
