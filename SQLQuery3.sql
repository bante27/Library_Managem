CREATE TABLE [dbo].[Books] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [BookTitle]     NVARCHAR (MAX) NULL,
    [Author]        NVARCHAR (MAX) NULL,
    [PublishedDate] DATE          NULL,
    [Status]        NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);