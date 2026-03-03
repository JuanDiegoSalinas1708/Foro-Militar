ALTER TABLE Communities
ADD 
    Slug NVARCHAR(150) NOT NULL DEFAULT '',
    ImageUrl NVARCHAR(500) NULL,
    BannerUrl NVARCHAR(500) NULL,
    MainCategoryId INT NULL,
    Rules NVARCHAR(1000) NULL,
    Visibility INT NOT NULL DEFAULT 0;
GO

ALTER TABLE Communities
ADD CONSTRAINT FK_Communities_MainCategory
FOREIGN KEY (MainCategoryId)
REFERENCES Categories(Id);
GO