DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

DECLARE @OrganizationId UNIQUEIDENTIFIER = '45143ad7-dc93-4664-982e-b93c7feb5f75';

INSERT INTO [tickets].[status_reason] ( [id]
                                      , [created_at]
                                      , [updated_at]
                                      , [organization_id]
                                      , [inactived_at]
                                      , [type]
                                      , [name]
                                      , [is_opening_default])
VALUES ( '9b636342-19a7-680c-cc2a-20520ccaec1b'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , 1
       , N'Aberto'
       , 1)

     , ( 'de1c99a0-d9ab-2d71-2002-51a6634882f1'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , 1
       , N'Em execução'
       , 0)

     , ( 'c56341ed-7f75-41ed-4541-3f0c4307c4f6'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , 1
       , N'Fechado'
       , 0);
GO
