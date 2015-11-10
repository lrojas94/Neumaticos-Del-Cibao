BEGIN TRANSACTION;
CREATE TABLE "Article" (
	`Id`	TEXT NOT NULL UNIQUE,
	`Name`	TEXT NOT NULL,
	`Price`	INTEGER,
	`MeasureUnit`	TEXT,
	`Measure`	REAL,
	`Description`	TEXT,
	PRIMARY KEY(Id)
);
COMMIT;
