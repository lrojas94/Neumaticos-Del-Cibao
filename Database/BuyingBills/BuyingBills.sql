BEGIN TRANSACTION;
CREATE TABLE "BuyingBills" (
	`Id`	INTEGER,
	`BillCode`	TEXT,
	`Provider`	INTEGER,
	`Date`	TEXT,
	`ITBIS`	INTEGER,
	`TotalArticles`	INTEGER,
	PRIMARY KEY(Id),
	FOREIGN KEY(`Provider`) REFERENCES Clients
);
CREATE TABLE "BuyingBillArticles" (
	`Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`ArticleId`	TEXT,
	`Quantity`	INTEGER NOT NULL,
	`BuyingBillId`	INTEGER,
	FOREIGN KEY(`ArticleId`) REFERENCES Articles,
	FOREIGN KEY(`BuyingBillId`) REFERENCES BuyingBills
);
COMMIT;
