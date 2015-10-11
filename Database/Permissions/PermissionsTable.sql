BEGIN TRANSACTION;
CREATE TABLE `Permissions` (
	`Id`	INTEGER,
	`PermissionName`	TEXT,
	`Description`	TEXT,
	PRIMARY KEY(Id)
);
COMMIT;
