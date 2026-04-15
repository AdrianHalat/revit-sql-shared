REATE DATABASE Revit_AH_Test;
GO

USE Revit_AH_Test;
GO

CREATE TABLE AH_Doors
(
	    UniqueId        NVARCHAR(255) NOT NULL PRIMARY KEY,
	    FamilyType      NVARCHAR(255),
	    AH_CompanyCode  NVARCHAR(255),
	    LastSync        DATETIME DEFAULT GETDATE()
);
