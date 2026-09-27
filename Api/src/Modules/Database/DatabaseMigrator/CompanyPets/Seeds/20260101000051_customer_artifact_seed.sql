DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

INSERT INTO [tenants].[customer_artifact] ( [id]
                                          , [created_at]
                                          , [updated_at]
                                          , [inactived_at]
                                          , [customer_id]
                                          , [artifact_id])
VALUES ( '6a16915a-90d5-9e4a-ad6b-ef36f28b6d94'
       , @CurrentDate
       , @CurrentDate
       , NULL
       , '9d673361-ea55-292c-89f9-b4f94b676aca'
       , '4f6ac6cf-e672-8ed7-c00c-2715e75869d2');
GO
