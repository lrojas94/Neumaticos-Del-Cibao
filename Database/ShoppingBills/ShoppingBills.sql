BEGIN TRANSACTION;
CREATE TABLE "ShoppingBillsArticles" (
	`ShoppingBillId`	INTEGER NOT NULL,
	`ArticleId`	INTEGER NOT NULL,
	`ArticlePrice`	REAL NOT NULL DEFAULT 0,
	`ArticleCount`	REAL NOT NULL DEFAULT 0,
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	FOREIGN KEY(`ShoppingBillId`) REFERENCES ShoppingBills ( Id ),
	FOREIGN KEY(`ArticleId`) REFERENCES Articles ( Id )
);
INSERT INTO `ShoppingBillsArticles` VALUES (1,1,100.0,200.0,1);
INSERT INTO `ShoppingBillsArticles` VALUES (3,1,123.0,123.0,3);
INSERT INTO `ShoppingBillsArticles` VALUES (3,1,33.0,3.0,4);
INSERT INTO `ShoppingBillsArticles` VALUES (4,1,199.0,100.0,5);
CREATE TABLE "ShoppingBills" (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`Company`	INTEGER,
	`Date`	TEXT,
	`Credit`	TEXT DEFAULT 'f' CHECK(Credit = 'f' or Credit = 't'),
	`ITBIS`	TEXT DEFAULT 'f' CHECK(ITBIS = 'f' or ITBIS = 't'),
	`ITBISPercent`	REAL,
	FOREIGN KEY(`Company`) REFERENCES Clients ( Id )
);
INSERT INTO `ShoppingBills` VALUES (1,1,NULL,'f',NULL,NULL);
INSERT INTO `ShoppingBills` VALUES (3,1,NULL,'f',NULL,NULL);
INSERT INTO `ShoppingBills` VALUES (4,1,'11/18/2015 12:00:00 AM','f',NULL,NULL);
COMMIT;
