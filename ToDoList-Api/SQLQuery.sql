CREATE DATABASE ToDoDB;
GO

USE ToDoDB;


GO

CREATE TABLE Users (
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
	Email  VARCHAR(50) NOT NULL ,
	Password  VARCHAR(50) NOT NULL ,
);
CREATE TABLE Tasks(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Title VARCHAR(50) NOT NULL,
	Description VARCHAR(50) ,
	UserId INT NOT NULL ,
	FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE ,
);
INSERT INTO Tasks(Title, Description, UserId)
VALUES ('Task P', 'Prepare monthly report', 8);

SET IDENTITY_INSERT Tasks ON;

INSERT INTO Tasks(Id, Title, Description, UserId)
SELECT Id, Title, Description, UserId FROM TASK;


SET IDENTITY_INSERT Users ON;
INSERT INTO Users (Id,Username, Email, Password)
SELECT * FROM  [User] ;

SET IDENTITY_INSERT USers OFF;

INSERT INTO Users(Username, Email, Password)
VALUES ('aya', 'aya@i.com', 123);


CREATE TABLE UserPermission  (
   UserId INT NOT NULL REFERENCES Users(Id) ON DELETE CASCADE,
   PermissionId VARCHAR(50) NOT NULL ,
   Primary Key (UserId, PermissionId),
);