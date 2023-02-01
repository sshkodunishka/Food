drop database food
Create database food

use food
GO
create table Roles 
(
	RoleId int IDENTITY(1,1) NOT NULL,
	Name nvarchar(max) NOT NULL,

	CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC )
)
GO 
create table Users
(
	UserId int IDENTITY(1,1) NOT NULL,
	Login nvarchar(100) NOT NULL,
	Password nvarchar(max) NOT NULL,
	Name nvarchar(max) NOT NULL,
	Surname nvarchar(max) NOT NULL,
	Weight real NOT NULL,
	Age int NOT NULL,
	Height int NOT NULL,
	Sex int NOT NULL,
	RoleId int NOT NULL,

	CONSTRAINT PK_Users PRIMARY KEY CLUSTERED ( UserId ASC ),
	CONSTRAINT FK_Users_Roles_RoleId FOREIGN KEY(RoleId) References Roles(RoleId) ON DELETE CASCADE
)
GO
create table Products
(
    ProductId int IDENTITY(1,1) NOT NULL,
	Name nvarchar(300) NOT NULL,
	Calories int NOT NULL,
	UserId int NOT NULL,

	CONSTRAINT PK_Products PRIMARY KEY CLUSTERED (	ProductId ASC ),
	CONSTRAINT FK_Products_Users_UserId FOREIGN KEY(UserId) References Users(UserId) ON DELETE CASCADE
)

create table Recipes
(
	RecipeId int IDENTITY(1,1) NOT NULL,
	Name nvarchar(300) NOT NULL,
	Calories int NOT NULL,
	Description nvarchar(max) NOT NULL,
	UserId int NOT NULL,
	CONSTRAINT PK_Recipes PRIMARY KEY CLUSTERED ( 	RecipeId ASC ),
	CONSTRAINT FK_Recipes_Users_UserId FOREIGN KEY(UserId) References Users(UserId) ON DELETE CASCADE
)

create table Favorites 
(
FavoritesId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	RecipeId int NOT NULL,
	CONSTRAINT PK_Favorites PRIMARY KEY CLUSTERED (FavoritesId ASC),
	CONSTRAINT FK_Favorites_Users_UserId FOREIGN KEY(UserId) References Users(UserId),
	CONSTRAINT FK_Favorites_Recipes_RecipeId FOREIGN KEY(RecipeId) References Recipes(RecipeId) ON DELETE CASCADE
)

create table MealHistory
(
	MealHistoryId int IDENTITY(1,1) NOT NULL,
	Date date NOT NULL,
	MealType int NOT NULL,
	UserId int NOT NULL,
	CONSTRAINT PK_MealHistory PRIMARY KEY CLUSTERED ( MealHistoryId ASC),
	CONSTRAINT FK_MealHistory_Users_UserId FOREIGN KEY(UserId) References Users(UserId)
)

create table MealFood
(
	MealFoodId int IDENTITY(1,1) NOT NULL,
	ProductId int NULL,
	RecipeId int NULL,
	MealHistoryId int NOT NULL,
	CONSTRAINT PK_MealFood PRIMARY KEY CLUSTERED (MealFoodId ASC),
	CONSTRAINT FK_MealFood_MealHistory_MealHistoryId FOREIGN KEY(MealHistoryId) References MealHistory(MealHistoryId),
	CONSTRAINT FK_MealFood_Products_ProductId FOREIGN KEY(ProductId) References Products(ProductId) ON DELETE CASCADE, 
	CONSTRAINT FK_MealFood_Recipes_RecipeId FOREIGN KEY(RecipeId) References Recipes(RecipeId),
)

create table AdminRecommendations
(
	AdminRecommendationId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	RecipeId int NOT NULL,
	CONSTRAINT PK_AdminRecommendations PRIMARY KEY CLUSTERED (AdminRecommendationId ASC),
	CONSTRAINT FK_AdminRecommendations_Recipes_RecipeId FOREIGN KEY(RecipeId) References Recipes(RecipeId) ON DELETE CASCADE,
	CONSTRAINT FK_AdminRecommendations_Users_UserId FOREIGN KEY(UserId) References Users(UserId),
)

create table AdminCountingCalories
(
	AdminCountingCaloriesId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	Date date NOT NULL,
	Calories int NOT NULL,
	CONSTRAINT PK_AdminCountingCalories PRIMARY KEY CLUSTERED (	AdminCountingCaloriesId ASC),
	CONSTRAINT FK_AdminCountingCalories_Users_UserId FOREIGN KEY(UserId) References Users(UserId),
)

