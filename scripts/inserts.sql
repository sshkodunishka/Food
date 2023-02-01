use food

--drop procedure AddNewRole
--drop procedure AddNewUser
--drop procedure AddNewProduct
--drop procedure AddNewRecipe
--drop procedure AddNewFavorite
--drop procedure AddNewMealHistory
--drop procedure AddNewMealFood
--drop procedure AddNewAdminRecommendation
--drop procedure AdminCountingCalorie


--Insert inside Roles 
go
Create Procedure AddNewRole
		@Name nvarchar(max)
AS
Begin
	Insert into Roles(Name)
	values(@Name);
End;
go

--Insert inside Users 
go
Create Procedure AddNewUser
	@Login nvarchar(100),
	@Password nvarchar(max),
	@Name nvarchar(max),
	@Surname nvarchar(max),
	@Weight real,
	@Age int,
	@Height int,
	@Sex int,
	@RoleId int
AS
Begin
	Insert into Users(Login, Password, Name, Surname, Weight, Age, Height, Sex, RoleId)
	values(@Login, @Password, @Name, @Surname, @Weight, @Age, @Height, @Sex, @RoleId);
End;
go

--Insert inside Products 
go
Create Procedure AddNewProduct
	@Name nvarchar(300),
	@Calories int,
	@UserId int
AS
Begin
	Insert into Products(Name, Calories, UserId)
	values(@Name, @Calories, @UserId);
End;
go

--Insert inside Recipes 
go
Create Procedure AddNewRecipe
	@Name nvarchar(300),
	@Calories int,
	@Description nvarchar(max),
	@UserId int
AS
Begin
	Insert into Recipes(Name, Calories, Description, UserId)
	values(@Name, @Calories,@Description, @UserId);
End;
go

--Insert inside Favorites 
go
Create Procedure AddNewFavorite
	@UserId int,
	@RecipeId int
AS
Begin
	Insert into Favorites(UserId, RecipeId)
	values(@UserId, @RecipeId);
End;
go

--Insert inside MealHistory 
go
Create Procedure AddNewMealHistory
	@Date date,
	@MealType int,
	@UserId int
AS
Begin
	Insert into MealHistory(Date, MealType, UserId)
	values(@Date, @MealType, @UserId);
End;
go


--Insert inside MealFood product
go
Create Procedure AddNewMealFoodProduct
	@ProductId int ,
	@MealHistoryId int 
AS
Begin
	Insert into MealFood(ProductId, RecipeId, MealHistoryId)
	values(@ProductId, NULL, @MealHistoryId);
End;
go

--Insert inside MealFood recipe
go
Create Procedure AddNewMealFoodRecipe
	@RecipeId int ,
	@MealHistoryId int 
AS
Begin
	Insert into MealFood(ProductId, RecipeId, MealHistoryId)
	values(NULL, @RecipeId, @MealHistoryId);
End;
go

--Insert inside AdminRecommendations 
go
Create Procedure AddNewAdminRecommendation
	@UserId int,
	@RecipeId int  
AS
Begin
	Insert into AdminRecommendations(UserId, RecipeId)
	values(@UserId, @RecipeId);
End;
go

--Insert inside AdminCountingCalories 
go
Create Procedure AddAdminCountingCalorie
	@UserId int,
	@Date date,
	@Calories int 
AS
Begin
	Insert into AdminCountingCalories(UserId, Date, Calories)
	values(@UserId, @Date, @Calories);
End;
go
