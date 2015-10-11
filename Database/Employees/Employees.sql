BEGIN TRANSACTION;
CREATE TABLE `Persons` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT NOT NULL,
	`Last Name`	TEXT NOT NULL,
	`Sex`	TEXT NOT NULL,
	`BirthDate`	TEXT,
	`Phone`	TEXT,
	`Email`	TEXT,
	`ContactRelativeName`	TEXT,
	`ContactRelativePhone`	TEXT
);
CREATE TABLE `Employees` (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Username`	TEXT NOT NULL,
	`Password`	TEXT NOT NULL,
	`StartDate`	TEXT,
	`Person`	INTEGER,
	FOREIGN KEY(`Person`) REFERENCES Persons(Id)
);
COMMIT;
