DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

DECLARE @OrganizationId UNIQUEIDENTIFIER = '45143ad7-dc93-4664-982e-b93c7feb5f75';

INSERT INTO [tenants].[customer] ( [id]
                                 , [created_at]
                                 , [updated_at]
                                 , [organization_id]
                                 , [inactived_at]
                                 , [name]
                                 , [document])
VALUES ( '9d673361-ea55-292c-89f9-b4f94b676aca'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , N'Customer 1'
       , N'01.591.503/0001-01');
GO
