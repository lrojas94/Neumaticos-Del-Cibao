BEGIN TRANSACTION;
CREATE TABLE "ShoppingOptionPermissions" (
	`ShoppingOptionId`	INTEGER NOT NULL,
	`PermissionId`	INTEGER NOT NULL,
	PRIMARY KEY(ShoppingOptionId,PermissionId),
	FOREIGN KEY(`ShoppingOptionId`) REFERENCES ShoppingOptions ( Id ),
	FOREIGN KEY(`PermissionId`) REFERENCES Permissions (Id)
);
COMMIT;
