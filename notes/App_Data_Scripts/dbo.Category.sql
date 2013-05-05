CREATE TABLE [dbo].[Category] (
    [CategoryID]       INT          NOT NULL,
    [Title]            VARCHAR (50) NOT NULL,
    [ParentCategoryID] INT          NULL,
    PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);

