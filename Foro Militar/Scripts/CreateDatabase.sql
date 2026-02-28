-- ============================================================
--  PASO 1: Ejecutar esto primero en Nueva Consulta
--  Crea la BD directamente en App_Data del proyecto
-- ============================================================

USE master;
GO

-- Si ya existe, eliminarla
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Foro')
BEGIN
    ALTER DATABASE Foro SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Foro;
END
GO

-- Crear la BD en App_Data
CREATE DATABASE Foro
ON PRIMARY (
    NAME = 'Foro',
    FILENAME = 'C:\Users\joel.medina\Source\Repos\Foro-Militar\Foro Militar\App_Data\Foro.mdf'
)
LOG ON (
    NAME = 'Foro_log',
    FILENAME = 'C:\Users\joel.medina\Source\Repos\Foro-Militar\Foro Militar\App_Data\Foro_log.ldf'
);
GO

USE Foro;
GO

-- =========================
-- 1. USERS
-- =========================
CREATE TABLE Users (
    Id           INT            IDENTITY(1,1) PRIMARY KEY,
    Username     NVARCHAR(50)   NOT NULL UNIQUE,
    Email        NVARCHAR(100)  NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255)  NOT NULL,
    Role         NVARCHAR(20)   NOT NULL DEFAULT 'User',
    CreatedAt    DATETIME2      NOT NULL DEFAULT GETDATE(),
    IsActive     BIT            NOT NULL DEFAULT 1
);
GO

-- =========================
-- 2. COMMUNITIES
-- =========================
CREATE TABLE Communities (
    Id              INT            IDENTITY(1,1) PRIMARY KEY,
    Name            NVARCHAR(100)  NOT NULL UNIQUE,
    Description     NVARCHAR(500),
    CreatedByUserId INT            NOT NULL,
    CreatedAt       DATETIME2      NOT NULL DEFAULT GETDATE(),
    IsActive        BIT            NOT NULL DEFAULT 1,
    CONSTRAINT FK_Communities_Users
        FOREIGN KEY (CreatedByUserId) REFERENCES Users(Id)
);
GO

-- =========================
-- 3. CATEGORIES
-- =========================
CREATE TABLE Categories (
    Id          INT            IDENTITY(1,1) PRIMARY KEY,
    Name        NVARCHAR(100)  NOT NULL UNIQUE,
    ColorHex    NVARCHAR(7)    NOT NULL,
    Description NVARCHAR(300),
    CommunityId INT            NULL,
    CreatedAt   DATETIME2      NOT NULL DEFAULT GETDATE(),
    IsActive    BIT            NOT NULL DEFAULT 1,
    CONSTRAINT FK_Categories_Communities
        FOREIGN KEY (CommunityId) REFERENCES Communities(Id)
);
GO

-- =========================
-- 4. POSTS
-- =========================
CREATE TABLE Posts (
    Id             INT            IDENTITY(1,1) PRIMARY KEY,
    Title          NVARCHAR(200)  NOT NULL,
    Content        NVARCHAR(MAX)  NOT NULL,
    Image          NVARCHAR(500)  NULL,
    Country        NVARCHAR(200)  NULL,
    UserId         INT            NOT NULL,
    CommunityId    INT            NOT NULL,
    MainCategoryId INT            NOT NULL,
    CreatedAt      DATETIME2      NOT NULL DEFAULT GETDATE(),
    UpdatedAt      DATETIME2      NULL,
    IsDeleted      BIT            NOT NULL DEFAULT 0,
    CONSTRAINT FK_Posts_Users
        FOREIGN KEY (UserId)         REFERENCES Users(Id),
    CONSTRAINT FK_Posts_Communities
        FOREIGN KEY (CommunityId)    REFERENCES Communities(Id),
    CONSTRAINT FK_Posts_MainCategory
        FOREIGN KEY (MainCategoryId) REFERENCES Categories(Id)
);
GO

-- =========================
-- 5. POSTCATEGORIES
-- =========================
CREATE TABLE PostCategories (
    PostId     INT NOT NULL,
    CategoryId INT NOT NULL,
    PRIMARY KEY (PostId, CategoryId),
    CONSTRAINT FK_PostCategories_Posts
        FOREIGN KEY (PostId)     REFERENCES Posts(Id)      ON DELETE CASCADE,
    CONSTRAINT FK_PostCategories_Categories
        FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);
GO

-- =========================
-- 6. COMMENTS
-- =========================
CREATE TABLE Comments (
    Id              INT            IDENTITY(1,1) PRIMARY KEY,
    Content         NVARCHAR(MAX)  NOT NULL,
    UserId          INT            NOT NULL,
    PostId          INT            NOT NULL,
    ParentCommentId INT            NULL,
    CreatedAt       DATETIME2      NOT NULL DEFAULT GETDATE(),
    IsDeleted       BIT            NOT NULL DEFAULT 0,
    CONSTRAINT FK_Comments_Users
        FOREIGN KEY (UserId)          REFERENCES Users(Id),
    CONSTRAINT FK_Comments_Posts
        FOREIGN KEY (PostId)          REFERENCES Posts(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Comments_Parent
        FOREIGN KEY (ParentCommentId) REFERENCES Comments(Id)
);
GO

-- =========================
-- 7. VOTES
-- =========================
CREATE TABLE Votes (
    Id        INT       IDENTITY(1,1) PRIMARY KEY,
    UserId    INT       NOT NULL,
    PostId    INT       NULL,
    CommentId INT       NULL,
    VoteType  INT       NOT NULL CHECK (VoteType IN (1, -1)),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Votes_Users
        FOREIGN KEY (UserId)    REFERENCES Users(Id),
    CONSTRAINT FK_Votes_Posts
        FOREIGN KEY (PostId)    REFERENCES Posts(Id)    ON DELETE CASCADE,
    CONSTRAINT FK_Votes_Comments
        FOREIGN KEY (CommentId) REFERENCES Comments(Id)
);
GO

CREATE UNIQUE INDEX UX_Votes_User_Post
    ON Votes(UserId, PostId)    WHERE PostId    IS NOT NULL;
CREATE UNIQUE INDEX UX_Votes_User_Comment
    ON Votes(UserId, CommentId) WHERE CommentId IS NOT NULL;
GO

-- =========================
-- 8. USERCOMMUNITIES
-- =========================
CREATE TABLE UserCommunities (
    UserId      INT       NOT NULL,
    CommunityId INT       NOT NULL,
    JoinedAt    DATETIME2 NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (UserId, CommunityId),
    CONSTRAINT FK_UserCommunities_Users
        FOREIGN KEY (UserId)      REFERENCES Users(Id)       ON DELETE CASCADE,
    CONSTRAINT FK_UserCommunities_Communities
        FOREIGN KEY (CommunityId) REFERENCES Communities(Id) ON DELETE CASCADE
);
GO

SELECT 'BD y tablas creadas correctamente' AS Resultado;
