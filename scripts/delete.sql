use food
------------------------------ Products ----------------------------------
go
Create Procedure DeleteProduct
	@Id int
AS
Begin
	delete Products where ProductId = @Id
end;
go

------------------------------ Recipe ----------------------------------
go
Create Procedure DeleteRecipe
	@Id int
AS
Begin
	delete Recipes where RecipeId = @Id
end;
go

------------------------------ Favorite ----------------------------------
go
Create Procedure DeleteFavorite
	@Id int
AS
Begin
	delete Favorites where FavoritesId = @Id
end;
go

------------------------------ Recommendations ----------------------------------
go
Create Procedure DeleteAdminRecommendation
	@Id int
AS
Begin
	delete AdminRecommendations where AdminRecommendationId = @Id
end;
go


----------------------------Meal Food------------------------------
go
Create Procedure DeleteByProductId
	@ProductId int
AS
Begin
	delete MealFood where ProductId = @ProductId
end;
go

go
Create Procedure DeleteByRecipeId
	@RecipeId int
AS
Begin
	delete MealFood where RecipeId = @RecipeId
end;
go

go
Create Procedure DeleteMealFood
	@Id int
AS
Begin
	delete MealFood where MealFoodId = @Id
end;
go