
use food
-----------------------------Users------------------------
--Get Users
GO
CREATE PROCEDURE GetUsers
AS
Begin
    SELECT *
	FROM Users 
End	
GO

select * from Users
exec GetUsers
--Get Users with User Role
GO
CREATE PROCEDURE GetWithUserRole
AS
Begin
    SELECT *
	FROM Users 
	left join Roles on Roles.RoleId = Users.RoleId
	where Roles.Name = 'User'
End	
GO
select * from 

--Get User By login
GO
CREATE PROCEDURE GetUserByLogin
	@Login nvarchar(100)
AS
Begin
    SELECT *
	FROM Users 
	where Login = @Login
End	
GO

--Get User By id
GO
CREATE PROCEDURE GetUserById
	@UserId int
AS
Begin
    SELECT *
	FROM Users 
	where UserId = @UserId
End	
GO

-----------------------------Roles------------------------
--Get Role by Id
GO
CREATE PROCEDURE GetRoleById
	@RoleId int
AS
Begin
    SELECT *
	FROM Roles
	where RoleId = @RoleId
End	
GO

--Get Role by Name
GO
CREATE PROCEDURE GetRoleByName
	@Name nvarchar(max)
AS
Begin
    SELECT *
	FROM Roles
	where Name = @Name
End	
GO

----------------------------Meal history-------------------------
--Get meal history
GO
CREATE PROCEDURE GetMealHistory
	@UserId int,
	@Date date
AS
Begin
    SELECT *
	FROM MealHistory
	where UserId = @UserId and MealHistory.Date = @Date
End	
GO

--Get meal history by type
GO
CREATE PROCEDURE GetMealHistoryByType
	@UserId int,
	@Date date,
	@Type int
AS
Begin
    SELECT *
	FROM MealHistory
	where UserId = @UserId and MealHistory.Date = @Date and MealHistory.MealType = @Type
End	
GO

------------------------------Meal food----------------------------------
--Get meal food by id 
GO
CREATE PROCEDURE GetMealFoodByMealHistoryId
	@MealHistoryId int
AS
Begin
    SELECT *
	FROM MealFood
	where MealHistoryId = @MealHistoryId
End	
GO

GO
CREATE PROCEDURE GetMealFoodByProductId
	@ProductId int
AS
Begin
    SELECT *
	FROM MealFood
	where ProductId = @ProductId
End	
GO

GO
CREATE PROCEDURE GetMealFoodByRecipeId
	@RecipeId int
AS
Begin
    SELECT *
	FROM MealFood
	where RecipeId = @RecipeId
End	
GO



------------------------------Product----------------------------------
--Get product by id
GO
CREATE PROCEDURE GetProductById
	@ProductId int
AS
Begin
    SELECT *
	FROM Products
	where ProductId = @ProductId
End	
GO

--Get product by user
GO
CREATE OR ALTER PROCEDURE GetProducts
	@UserId int
AS
Begin
    SELECT *
	FROM Products
	left join Users on Users.UserId = Products.UserId 
	left join Roles on Roles.RoleId = Users.RoleId
	where Products.UserId = @UserId or Roles.Name = 'Admin'  
End	
GO

--Get all products
GO
CREATE PROCEDURE GetAllProducts
AS
Begin
    SELECT *
	FROM Products
End	
GO

------------------------------Recipes----------------------------------
--Get recipe by id
GO
CREATE PROCEDURE GetRecipeById
	@RecipeId int
AS
Begin
    SELECT *
	FROM Recipes
	where RecipeId = @RecipeId
End	
GO 

--Get all recipes
GO
CREATE PROCEDURE GetAllRecipes
AS
Begin
    SELECT *
	FROM Recipes
End	
GO

--Get product by user
GO
CREATE OR ALTER PROCEDURE GetRecipes
	@UserId int
AS
Begin
    SELECT *
	FROM Recipes
	left join Users on Users.UserId = Recipes.UserId 
	left join Roles on Roles.RoleId = Users.RoleId
	where Recipes.UserId = @UserId or Roles.Name = 'Admin'  
End	
GO

--Get recipes for users exclude already recommended to user
GO
CREATE OR ALTER PROCEDURE GetRecipesForRecommendations
	@UserId int
AS
Begin
    SELECT *
	FROM Recipes
	left join Users on Users.UserId = Recipes.UserId 
	left join Roles on Roles.RoleId = Users.RoleId
	where (Recipes.UserId = @UserId or Roles.Name = 'Admin') 
	and RecipeId not in (select RecipeId from AdminRecommendations where UserId = @UserId)
End	
GO

------------------------------Favorites----------------------------------
GO
CREATE OR ALTER PROCEDURE GetFavorites
	@UserId int
AS
Begin
    SELECT *
	FROM Favorites
	where Favorites.UserId = @UserId
End	
GO

GO
CREATE OR ALTER PROCEDURE GetFavoritesByRecipe
	@RecipeId int
AS
Begin
    SELECT *
	FROM Favorites
	where Favorites.RecipeId = @RecipeId
End	
GO

GO
CREATE OR ALTER PROCEDURE GetFavoritesByRecipeAndUser
	@RecipeId int,
	@UserId int
AS
Begin
    SELECT *
	FROM Favorites
	where Favorites.RecipeId = @RecipeId and UserId = @UserId
End	
GO

------------------------------Recommendation----------------------------------
GO
CREATE OR ALTER PROCEDURE GetRecommendation
	@UserId int
AS
Begin
    SELECT *
	FROM AdminRecommendations
	left join Users on Users.UserId = AdminRecommendations.UserId 
	left join Recipes on Recipes.UserId = Users.UserId
	where AdminRecommendations.UserId = @UserId 
End	
GO

GO
CREATE OR ALTER PROCEDURE GetRecommendationByRecipe
	@RecipeId int
AS
Begin
    SELECT *
	FROM AdminRecommendations
	where AdminRecommendations.RecipeId = @RecipeId 
End	
GO

------------------------------Admin Calories----------------------------------
GO
CREATE OR ALTER PROCEDURE GetAdminCountingCalories
	@UserId int,
	@Date date
AS
Begin
    SELECT *
	FROM AdminCountingCalories
	where UserId = @UserId and AdminCountingCalories.Date = @Date
End	
GO

select * from AdminCountingCalories


select * from Users

exec GetMealFoodByMealHistoryId @MealHistoryId = 1
exec GetMealHistory @UserId = 5, @Date='2022-12-04'


exec GetFavorites @UserId = 4 
select * from Favorites