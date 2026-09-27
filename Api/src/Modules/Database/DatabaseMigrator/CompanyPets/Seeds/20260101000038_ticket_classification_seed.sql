DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

DECLARE @OrganizationId UNIQUEIDENTIFIER = '45143ad7-dc93-4664-982e-b93c7feb5f75';

INSERT INTO [tickets].[ticket_classification] ( [id]
                                              , [created_at]
                                              , [updated_at]
                                              , [organization_id]
                                              , [inactived_at]
                                              , [name]
                                              , [code])
VALUES ( 'abd9df9e-17d2-5283-9ba5-fdc79f1ec90e'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , N'Classification 1'
       , N'CLS001');
GO
