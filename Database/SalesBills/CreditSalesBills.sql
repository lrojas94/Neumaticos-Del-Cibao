BEGIN TRANSACTION;
CREATE TABLE "CreditSalesBillsRegister" (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`SalesBillsId`	INTEGER NOT NULL,
	`Payed`	REAL,
	`Owed`	REAL,
	`Date`	TEXT,
	FOREIGN KEY(`SalesBillsId`) REFERENCES CreditSalesBills ( SalesBillId )
);
CREATE TABLE "CreditSalesBills" (
	`SalesBillId`	INTEGER NOT NULL UNIQUE,
	`Payed`	REAL,
	`Owed`	REAL,
	`DonePaying`	TEXT CHECK(DonePaying = 'f' or DonePaying = 't'),
	PRIMARY KEY(SalesBillId),
	FOREIGN KEY(`SalesBillId`) REFERENCES SalesBills ( Id )
);
COMMIT;
