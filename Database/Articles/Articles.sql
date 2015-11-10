BEGIN TRANSACTION;
CREATE TABLE "Articles" (
	`Id`	TEXT NOT NULL UNIQUE,
	`Name`	TEXT NOT NULL,
	`MeasureUnit`	TEXT,
	`Measure`	REAL,
	`Description`	TEXT,
	PRIMARY KEY(Id)
);
COMMIT;
