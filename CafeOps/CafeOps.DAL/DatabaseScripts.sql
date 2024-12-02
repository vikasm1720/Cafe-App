
-- Create the database
CREATE DATABASE CafeOps;
GO

-- Use the database
USE CafeOps;
GO

-- Create Employee Table
CREATE TABLE Employees (
    Id NVARCHAR(10) NOT NULL PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    EmailAddress NVARCHAR(100) NOT NULL,
    PhoneNumber CHAR(8) NOT NULL, 
    Gender NVARCHAR(10) NOT NULL CHECK (Gender IN ('Male', 'Female')),
    CafeId UNIQUEIDENTIFIER NULL, -- Foreign Key to Cafes
    StartDate DATE NULL,
    CONSTRAINT CHK_PhoneNumber CHECK (PhoneNumber LIKE '[89]%' AND LEN(PhoneNumber) = 8)
);

-- Create Cafe Table
CREATE TABLE Cafes (
    Id UNIQUEIDENTIFIER PRIMARY KEY, -- UUID
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(256) NOT NULL,
    Logo VARBINARY(MAX) NULL, -- Optional
    Location NVARCHAR(100) NOT NULL
);

-- Add Foreign Key Constraint to Link Employees and Cafes
ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_Cafes FOREIGN KEY (CafeId)
REFERENCES Cafes(Id) ON DELETE SET NULL;

-- Add seed data to Cafes
INSERT INTO Cafes (Id, Name, Description, Logo, Location)
VALUES	
    (NEWID(), 'Third Wave Coffee', 'we make coffee an experience every step of the way', NULL, 'Bangalore'),
	(NEWID(), 'Baristart Coffee', 'A Hokkaido-based concept cafe that specializes in using Hokkaido BIEI Jersey Milk for our coffee', NULL, 'Singapore');
    (NEWID(), 'Central Perk', 'A cozy coffee house in the heart of the city', NULL, 'Bangalore'),
    (NEWID(), 'Java Cafe', 'Best coffee blends in town', NULL, 'Singapore'),	
    
    (NEWID(), 'Bistro 101', 'A quiet spot for your daily grind', NULL, 'Boston');

-- Add seed data to Employees
INSERT INTO Employees (Id, Name, EmailAddress, PhoneNumber, Gender, CafeId, StartDate)
VALUES
    ('UI5G8ZK01', 'Vikas M', 'vikas.m@example.com', '91234567', 'Male',(SELECT Id FROM Cafes WHERE Name = 'Central Perk'), '2024-12-01'),
    ('UIA12C3X2', 'Jane Smith', 'jane.smith@example.com', '82345678', 'Female', (SELECT Id FROM Cafes WHERE Name = 'Bistro 101'), '2022-01-15'),
    ('UIX91B2T3', 'Peter Parker', 'peter.parker@example.com', '93456789', 'Male', (SELECT Id FROM Cafes WHERE Name = 'Java Cafe'), '2023-05-01'),
	('UIX91B2T4', 'John Peterson', 'john@example.com', '99874567', 'Male',(SELECT Id FROM Cafes WHERE Name = 'Central Perk'), '2022-10-05'),
	('UIA12C3X4', 'Amy Jackson', 'amy@example.com', '87234567', 'Female',(SELECT Id FROM Cafes WHERE Name = 'Bistro 101'), '2024-11-06'),
	('UIV34C2X2', 'Robert Redford', 'robert@example.com', '98734567', 'Male',(SELECT Id FROM Cafes WHERE Name = 'Bistro 101'), '2023-12-01'),
	('UICAEAD23', 'Abhay S', 'abhay_s@example.com', '88834567', 'Male',(SELECT Id FROM Cafes WHERE Name = 'Third Wave Coffee'), '2021-10-05'),
	('UIBLR0068', 'Alisa lee', 'alisa@example.com', '99934567', 'Female',(SELECT Id FROM Cafes WHERE Name = 'Baristart Coffee'), '2024-11-06'),
	('UICHN1VM1', 'Ryan Ali', 'ryan@example.com', '98984567', 'Male',(SELECT Id FROM Cafes WHERE Name = 'Java Cafe'), '2023-12-01');
