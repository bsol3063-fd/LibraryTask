-- Создаём БД HL (HomeLibrary)

USE MASTER
GO
CREATE DATABASE [HL]
GO


-----------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[Books] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Title] [nvarchar](200) NOT NULL,
    [Author] [nvarchar](200) NOT NULL,
    [PublicationYear] [int] NULL,
    [Contents] [xml] NULL,
    [AddedDate] [datetime] NOT NULL,
    [Genre] [nvarchar](100) NULL
)

GO

CREATE PROCEDURE [sp_GetBooks]
    @SearchTerm NVARCHAR(200) = NULL
AS
BEGIN
    SELECT Id, Title, Author, PublicationYear, Contents, AddedDate, Genre
FROM Books
WHERE @SearchTerm IS NULL 
   OR Title LIKE '%' + @SearchTerm + '%'
   OR Author LIKE '%' + @SearchTerm + '%'
   OR CAST(Contents AS NVARCHAR(MAX)) LIKE '%' + @SearchTerm + '%'
ORDER BY Title
end

GO

CREATE PROCEDURE sp_InsertBook
    @Title NVARCHAR(200),
    @Author NVARCHAR(200),
    @PublicationYear INT,
    @Contents XML,
    @Genre NVARCHAR(100)
AS
BEGIN
    INSERT INTO Books (Title, Author, PublicationYear, Contents, Genre)
    VALUES (@Title, @Author, @PublicationYear, @Contents, @Genre)
    
    SELECT SCOPE_IDENTITY() AS NewId
END

GO

CREATE PROCEDURE [dbo].[sp_UpdateBook]
    @Id INT,
    @Title NVARCHAR(200),
    @Author NVARCHAR(200),
    @PublicationYear INT,
    @Contents XML,
    @Genre NVARCHAR(100)
AS
BEGIN
    UPDATE Books
    SET Title = @Title,
        Author = @Author,
        PublicationYear = @PublicationYear,
        Contents = @Contents,
        Genre = @Genre
    WHERE Id = @Id
END

GO

CREATE PROCEDURE [dbo].[sp_DeleteBook]
    @Id INT
AS
BEGIN
    DELETE FROM Books WHERE Id = @Id
END

GO

CREATE PROCEDURE [dbo].[sp_GetBookById]
    @Id INT
AS
BEGIN
    SELECT Id, Title, Author, PublicationYear, Contents, AddedDate, Genre
    FROM Books
    WHERE Id = @Id
END