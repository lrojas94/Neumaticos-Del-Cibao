BEGIN TRANSACTION;
CREATE TABLE "SalesBills" (
	`Id`	INTEGER,
	`BillCode`	TEXT,
	`Client`	INTEGER,
	`Date`	TEXT,
	`ITBIS`	REAL,
	`TotalArticles`	INTEGER,
	PRIMARY KEY(Id),
	FOREIGN KEY(`Client`) REFERENCES Clients
);
CREATE TABLE "SalesBillArticles" (
	`Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`ArticleId`	TEXT,
	`Quantity`	INTEGER NOT NULL,
	`SalesBillId`	INTEGER,
	FOREIGN KEY(`ArticleId`) REFERENCES Articles,
	FOREIGN KEY(`SalesBillId`) REFERENCES SalesBills
);
COMMIT;
