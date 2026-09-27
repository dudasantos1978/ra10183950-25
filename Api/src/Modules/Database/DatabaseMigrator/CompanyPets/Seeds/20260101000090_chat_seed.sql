DECLARE @CurrentDate DATETIME2 = GETUTCDATE();

INSERT INTO [tickets].[chat] ( [id]
                             , [created_at]
                             , [updated_at]
                             , [deleted_at]
                             , [ticket_id]
                             , [user_id]
                             , [message])
VALUES ( 'ae7dd396-e316-1cbb-ddd4-5ff269bc1749'
       , @CurrentDate
       , @CurrentDate
       , NULL
       , '1290594f-25a7-c5ef-a4a8-5125b6ab5d7e'
       , '6a508a12-6e9b-4ef6-bdb9-8372347cb8a8'
       , N'Chat message for ticket 1')

     , ( '0f00e384-4dce-1e67-c7f9-455e9e21f5db'
       , @CurrentDate
       , @CurrentDate
       , NULL
       , 'b7b054fb-8ba2-e7f0-0cf2-739f294cf422'
       , '820a63c4-5e10-0664-6a32-a1325293b324'
       , N'Chat message for ticket 2');
GO
