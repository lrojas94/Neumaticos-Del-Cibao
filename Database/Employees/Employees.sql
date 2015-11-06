
CREATE TABLE "Persons" (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT NOT NULL,
	`LastName`	TEXT NOT NULL,
	`Sex`	TEXT NOT NULL,
	`BirthDate`	TEXT,
	`Phone`	TEXT,
	`Email`	TEXT,
	`ContactRelativeName`	TEXT,
	`ContactRelativePhone`	TEXT,
	`IdentityDocument`	TEXT,
	`Address`	TEXT
);
CREATE TABLE "Employees_Permissions" (
	`EmployeeId`	INTEGER NOT NULL,
	`PermissionId`	INTEGER NOT NULL,
	PRIMARY KEY(EmployeeId,PermissionId),
	FOREIGN KEY(`EmployeeId`) REFERENCES Employees ( Id ),
	FOREIGN KEY(`PermissionId`) REFERENCES Permissions ( Id )
);
CREATE TABLE "Employees" (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Username`	TEXT NOT NULL,
	`Password`	TEXT NOT NULL,
	`StartDate`	TEXT,
	`PersonId`	INTEGER NOT NULL UNIQUE,
	FOREIGN KEY(`PersonId`) REFERENCES Persons ( Id )
);
