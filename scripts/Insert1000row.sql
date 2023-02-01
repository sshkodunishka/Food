-- create random word
Create or Alter Procedure CreateRandomWord
	@size integer, 
	@Name char(20) OUTPUT
AS 
Begin
	SET @Name = (
	SELECT
		c1 AS [text()]
	FROM
		(
		SELECT TOP (@size) c1
		FROM
		  (
		VALUES
		  ('A'), ('B'), ('C'), ('D'), ('E'), ('F'), ('G'), ('H'), ('I'), ('J'),
		  ('K'), ('L'), ('M'), ('N'), ('O'), ('P'), ('Q'), ('R'), ('S'), ('T'),
		  ('U'), ('V'), ('W'), ('X'), ('Y'), ('Z')
		  ) AS T1(c1)
		ORDER BY ABS(CHECKSUM(NEWID()))
		) AS T2
	FOR XML PATH('')
	);
End;



go
Create or Alter Procedure InsertProducts AS
Begin
DECLARE @Name nvarchar(300),
		@Calories datetime,
		@UserId int,
		@number int
SET @number = 1;
select top (1) @UserId = Users.UserId from Users
While @number <= 10000
	BEGIN
	exec CreateRandomWord 20, @Name OUTPUT;
	set @Calories = RAND()*(300-50)+50
	Insert into Products(Name,Calories,UserId)
		      values(@Name,@UserId,@UserId);
	SET @number = @number + 1;
	END;
End;
delete Products

EXEC InsertProducts;

delete Products

select * from Products where Name = 'apple'