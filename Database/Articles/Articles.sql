BEGIN TRANSACTION;
CREATE TABLE "Articles" (
	`Id`	INTEGER NOT NULL UNIQUE,
	`Name`	TEXT NOT NULL,
	`MeasureUnit`	TEXT,
	`Measure`	REAL,
	`Description`	TEXT,
	`CodeIdentifier`	TEXT,
	PRIMARY KEY(Id)
);
COMMIT;
