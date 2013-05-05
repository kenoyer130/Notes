CREATE TABLE [dbo].[Note]
(
	[NoteID] INT NOT NULL, 
	[CategoryID] INT NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [BucketID] INT NOT NULL, 
    CONSTRAINT [PK_Note] PRIMARY KEY ([NoteID]) 
)
