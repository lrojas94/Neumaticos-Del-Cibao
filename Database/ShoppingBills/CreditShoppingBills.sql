BEGIN TRANSACTION;
CREATE TABLE "CreditShoppingBillsRegisters" (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`ShoppingBillId`	INTEGER NOT NULL,
	`Payed`	REAL,
	`Owed`	REAL,
	`Date`	TEXT,
	FOREIGN KEY(`ShoppingBillId`) REFERENCES CreditShoppingBills ( ShoppingBillId )
);
CREATE TABLE "CreditShoppingBills" (
	`ShoppingBillId`	INTEGER NOT NULL UNIQUE,
	`Payed`	REAL,
	`Owed`	REAL,
	`DonePaying`	TEXT CHECK(DonePaying = 'f' or DonePaying = 't'),
	PRIMARY KEY(ShoppingBillId),
	FOREIGN KEY(`ShoppingBillId`) REFERENCES ShoppingBills ( Id )
);
COMMIT;
