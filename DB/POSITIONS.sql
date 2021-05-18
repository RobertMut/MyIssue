CREATE TABLE [dbo].[POSITIONS]
(
	[positionId] NUMERIC(3) NOT NULL IDENTITY(1,1), 
    [positionName] VARCHAR(50) NOT NULL,
	CONSTRAINT PK_positionId PRIMARY KEY ([positionId])
)
