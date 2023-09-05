CREATE TABLE [dbo].[EmailConnection]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [PersonId] INT NOT NULL, 
    [EmailAddressId] INT NOT NULL
)
