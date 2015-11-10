BEGIN TRANSACTION;
CREATE TABLE "Clients" (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Name`	TEXT,
	`RNC`	INTEGER,
	`NCF`	INTEGER,
	`Credit`	TEXT,
	`CreditLimit`	INTEGER,
	`Adress`	TEXT,
	`Email`	TEXT,
	`Phone`	TEXT,
	`ContactPhone`	TEXT,
	`ContactName`	TEXT,
	`ContactEmail`	TEXT,
	`ContactExt`	TEXT,
	`ContactCharge`	TEXT
);
COMMIT;
