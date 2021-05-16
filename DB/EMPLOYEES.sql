CREATE TABLE [dbo].[EMPLOYEES]
(
	[employeeLogin] VARCHAR(10) NOT NULL, 
    [employeeName] VARCHAR(70) NOT NULL, 
    [employeeSurname] VARCHAR(70) NOT NULL, 
    [employeeNo] VARCHAR(15) NOT NULL, 
    [employeePosition] NUMERIC(3) NULL,
    CONSTRAINT PK_employeeLogin PRIMARY KEY ([employeeLogin]),
    CONSTRAINT FK_employeePosition_positionId FOREIGN KEY ([employeePosition])
        REFERENCES POSITIONS ([positionId]), 
    CONSTRAINT FK_login_login FOREIGN KEY ([employeeLogin])
        REFERENCES USERS ([userLogin])
)
