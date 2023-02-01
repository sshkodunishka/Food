use food
------------------------------Admin Calories----------------------------------
--Update  AdminCountingCalories 
go
Create Procedure UpdateAdminCountingCalorie
	@Id int,
	@UserId int,
	@Date date,
	@Calories int 
AS
Begin
	update AdminCountingCalories set 
	Date = @Date,
	Calories = @Calories
	where UserId = @UserId and AdminCountingCalories.AdminCountingCaloriesId = @Id
	End;
go

-------------------Users---------------------
--Update Users 
go
Create Procedure UpdateUser
    @UserId int,
	@Login nvarchar(100),
	@Name nvarchar(max),
	@Surname nvarchar(max),
	@Weight real,
	@Age int,
	@Height int
AS
Begin
	Update Users set 
	Login = @Login,
	Name = @Name,
	Surname =@Surname, 
	Weight = @Weight, 
	Age = @Age, 
	Height = @Height
	where UserId = @UserId
End;
go