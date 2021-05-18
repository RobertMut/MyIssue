CREATE TABLE [dbo].[TASKS]
(
	[taskId] NUMERIC(12) NOT NULL IDENTITY(1,1), 
    [taskType] NUMERIC(3) NOT NULL, 
    [taskTitle] VARCHAR(100) NOT NULL, 
    [taskDesc] VARCHAR(MAX) NULL, 
    [taskOwner] VARCHAR(10) NULL, 
    [taskAssignment] VARCHAR(10) NULL, 
    [taskClient] NUMERIC(5) NULL, 
    [taskStart] DATETIME NULL, 
    [taskEnd] DATETIME NULL, 
    [taskCreation] DATETIME NOT NULL,
    CONSTRAINT PK_taskId PRIMARY KEY ([taskId]),
    CONSTRAINT FK_taskType_typeId FOREIGN KEY ([taskType])
        REFERENCES TASKTYPES ([typeId])
    ON UPDATE CASCADE,
    CONSTRAINT FK_taskOwner_login FOREIGN KEY ([taskOwner])
        REFERENCES EMPLOYEES ([employeeLogin]),
    CONSTRAINT FK_taskAssignment_login FOREIGN KEY ([taskAssignment])
        REFERENCES EMPLOYEES ([employeeLogin]),
    CONSTRAINT FK_taskClient_clientId FOREIGN KEY ([taskClient])
        REFERENCES CLIENTS ([clientId])
    ON UPDATE CASCADE

    
)
