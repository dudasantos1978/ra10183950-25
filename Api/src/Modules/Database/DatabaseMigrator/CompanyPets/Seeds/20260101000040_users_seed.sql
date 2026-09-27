DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

INSERT INTO [tenants].[user] ( [id]
                             , [created_at]
                             , [updated_at]
                             , [inactived_at]
                             , [name]
                             , [email]
                             , [role_id])
VALUES ( '3f3816db-5580-4039-91c3-ddca5d51970c'
       , @CurrentDate
       , @CurrentDate
       , NULL
       , N'Super Admin'
       , N'superadmin@example.com'
       , '8f42805c-d971-4fdb-b099-d69c6dc6b2a4')

     , ( '6a508a12-6e9b-4ef6-bdb9-8372347cb8a8'
       , @CurrentDate
       , @CurrentDate
       , NULL
       , N'User 1'
       , N'user01@example.com'
       , NULL)

     , ( '820a63c4-5e10-0664-6a32-a1325293b324'
       , @CurrentDate
       , @CurrentDate
       , NULL
       , N'User 2'
       , N'user02@example.com'
       , NULL);
GO
