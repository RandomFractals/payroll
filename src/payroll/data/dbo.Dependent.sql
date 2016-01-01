CREATE TABLE [dbo].[Dependent] (
    [DependentID]        INT            IDENTITY (1, 1) NOT NULL,
    [EmployeeEmployeeID] INT            NOT NULL,
    [FirstName]          NVARCHAR (MAX) NOT NULL,
    [LastName]           NVARCHAR (MAX) NOT NULL,
    [MiddleName]         NVARCHAR (MAX) NULL,
    [Relationship]       INT            NOT NULL,
    CONSTRAINT [PK_Dependent] PRIMARY KEY CLUSTERED ([DependentID] ASC),
    CONSTRAINT [FK_Dependent_Employee_EmployeeEmployeeID] FOREIGN KEY ([EmployeeEmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID])
);

