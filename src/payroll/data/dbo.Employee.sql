CREATE TABLE [dbo].[Employee] (
    [EmployeeID] INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (MAX)  NOT NULL,
    [LastName]   NVARCHAR (MAX)  NOT NULL,
    [MiddleName] NVARCHAR (MAX)  NULL,
    [Salary]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
);

