BEGIN TRANSACTION;
CREATE TABLE "Options" (
	`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	`OptionTitle`	TEXT,
	`OptionType`	TEXT,
	`ClassView`	TEXT
);
CREATE TABLE "OptionPermissions" (
	`OptionId`	INTEGER NOT NULL,
	`PermissionId`	INTEGER NOT NULL,
	PRIMARY KEY(OptionId,PermissionId),
	FOREIGN KEY(`OptionId`) REFERENCES Options ( Id ),
	FOREIGN KEY(`PermissionId`) REFERENCES Permissions ( Id )
);
COMMIT;
