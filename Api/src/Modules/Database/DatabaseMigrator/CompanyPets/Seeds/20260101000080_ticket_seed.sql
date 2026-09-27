DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

DECLARE @OrganizationId UNIQUEIDENTIFIER = '45143ad7-dc93-4664-982e-b93c7feb5f75';
DECLARE @CreatedByUserId UNIQUEIDENTIFIER = '6a508a12-6e9b-4ef6-bdb9-8372347cb8a8';

INSERT INTO [tickets].[ticket] ( [id]
                               , [created_at]
                               , [updated_at]
                               , [organization_id]
                               , [deleted_at]
                               , [customer_id]
                               , [artifact_id]
                               , [status_id]
                               , [classification_id]
                               , [allocation_center]
                               , [created_by_user_id]
                               , [description]
                               , [resolution])
VALUES ( '1290594f-25a7-c5ef-a4a8-5125b6ab5d7e'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , '9d673361-ea55-292c-89f9-b4f94b676aca'
       , '4f6ac6cf-e672-8ed7-c00c-2715e75869d2'
       , '9b636342-19a7-680c-cc2a-20520ccaec1b'
       , 'abd9df9e-17d2-5283-9ba5-fdc79f1ec90e'
       , 1
       , @CreatedByUserId
       , N'Ticket seed 1'
       , NULL)

     , ( 'b7b054fb-8ba2-e7f0-0cf2-739f294cf422'
       , @CurrentDate
       , @CurrentDate
       , @OrganizationId
       , NULL
       , '9d673361-ea55-292c-89f9-b4f94b676aca'
       , '4f6ac6cf-e672-8ed7-c00c-2715e75869d2'
       , 'c56341ed-7f75-41ed-4541-3f0c4307c4f6'
       , 'abd9df9e-17d2-5283-9ba5-fdc79f1ec90e'
       , 1
       , @CreatedByUserId
       , N'Ticket seed 2'
       , N'Resolved in seed 2');
GO
