BEGIN TRANSACTION;
CREATE TABLE "ShoppingBillsArticles" (
	`ShoppingBillId`	INTEGER NOT NULL,
	`ArticleId`	INTEGER NOT NULL,
	`ArticlePrice`	REAL,
	`ArticleCount`	REAL,
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	FOREIGN KEY(`ShoppingBillId`) REFERENCES ShoppingBills ( Id ),
	FOREIGN KEY(`ArticleId`) REFERENCES Articles ( Id )
);
CREATE TABLE "ShoppingBills" (
	`Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`Company`	INTEGER,
	`Date`	TEXT,
	FOREIGN KEY(`Company`) REFERENCES Client(Id)
);
COMMIT;
