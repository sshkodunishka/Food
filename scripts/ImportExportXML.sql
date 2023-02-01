--Export xml
go
Create or ALTER Procedure ExProductstoXml
AS
Begin
	select * from Products 
			for xml PATH('PRODUCT'), root('PRODUCTS');

	--to use xp_cmdshell
	EXEC master.dbo.sp_configure 'show advanced options', 1
		RECONFIGURE WITH OVERRIDE
	EXEC master.dbo.sp_configure 'xp_cmdshell', 1
		RECONFIGURE WITH OVERRIDE;
	-- Save XML records to a file
	DECLARE @fileName nVARCHAR(100)
	DECLARE @sqlStr VARCHAR(1000)
	DECLARE @sqlCmd VARCHAR(1000)

	SET @fileName = 'C:\univer\DB\products.xml'
	SET @sqlStr = 'USE food; select * from Products for xml path(''PRODUCT''), root(''PRODUCTS'') '
	SET @sqlCmd = 'bcp "' + @sqlStr + '" queryout ' + @fileName + ' -w -T'
	EXEC xp_cmdshell @sqlCmd;
End;

exec ExProductstoXml

delete Products
drop procedure ExProductstoXml
--import from xml to server 
go
Create or alter Procedure ImProductsfromXml
AS
Begin
	INSERT INTO Products(Name, Calories, UserId)
	SELECT
	   MY_XML.PRODUCT.query('Name').value('.', 'nvarchar(300)'),
	   MY_XML.PRODUCT.query('Calories').value('.', 'int'),
	   MY_XML.PRODUCT.query('UserId').value('.', 'int')
	FROM (SELECT CAST(MY_XML AS xml)
		  FROM OPENROWSET(BULK 'C:\univer\DB\products.xml', SINGLE_BLOB) AS T(MY_XML)) AS T(MY_XML)
		  CROSS APPLY MY_XML.nodes('PRODUCTS/PRODUCT') AS MY_XML (PRODUCT);
End;

exec ImProductsfromXml
drop procedure ImProductsfromXml

select * from Products