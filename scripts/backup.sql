--Restore DB
go
Create or alter Procedure ImportDB
AS
Begin
	ALTER DATABASE food 
	SET SINGLE_USER 
	WITH ROLLBACK IMMEDIATE;

	RESTORE DATABASE food
		FROM disk = N'C:\univer\DB\food.bak' 
			WITH replace,
			stats = 10;

	ALTER DATABASE food 
	SET MULTI_USER; 
End;

drop procedure ImportDB

use master
exec ImportDB

go
use food
select * from Products
delete Products

--Export DB
go
Create or alter Procedure ExportDB
AS
Begin
	BACKUP DATABASE food  
	TO DISK = N'C:\univer\DB\food.bak' 
	with 
		stats = 10
End;

drop procedure ExportDB

exec ExportDB