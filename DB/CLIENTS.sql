CREATE TABLE [dbo].[CLIENTS]
(
	[clientId] NUMERIC(5) NOT NULL, 
    [clientName] VARCHAR(250) NOT NULL, 
    [clientCountry] VARCHAR(3) NULL, 
    [clientNo] VARCHAR(20) NOT NULL, 
    [clientStreet] VARCHAR(70) NULL, 
    [clientStreetNo] VARCHAR(5) NULL, 
    [clientFlatNo] VARCHAR(4) NULL, 
    [clientDesc] VARCHAR(MAX) NULL,
    CONSTRAINT PK_clientId PRIMARY KEY ([clientId])
)
