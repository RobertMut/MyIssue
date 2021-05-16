CREATE TABLE [dbo].[USERS]
(
	[userLogin] VARCHAR(10) NOT NULL, 
    [password] VARCHAR(128) NOT NULL, 
    [type] NUMERIC(1) NOT NULL,
    CONSTRAINT PK_userLogin PRIMARY KEY ([userLogin])
)
