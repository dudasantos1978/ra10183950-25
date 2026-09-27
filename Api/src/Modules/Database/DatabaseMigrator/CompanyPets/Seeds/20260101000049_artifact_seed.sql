DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

INSERT INTO [tenants].[artifact] ( [id]
                                 , [created_at]
                                 , [updated_at]
                                 , [inactived_at]
                                 , [artifact_type_id]
                                 , [name]
                                 , [code])
VALUES ( '4f6ac6cf-e672-8ed7-c00c-2715e75869d2'
       , @CurrentDate
       , @CurrentDate
       , NULL
       , '4905f835-06f9-a62d-9feb-e6343939739e'
       , N'Artifact 1'
       , N'ART001');
GO
