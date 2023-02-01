CREATE LOGIN FoodUser WITH PASSWORD = 'userpassword'
CREATE USER FoodUser FROM LOGIN FoodUser

use food

CREATE USER FoodUser FROM LOGIN FoodUser

create role FoodUserRole

alter role FoodUserRole add member FoodUser

use food 
select * from AdminCountingCalories


GRANT EXECUTE ON GetUsers to FoodUserRole
GRANT EXECUTE ON GetUserByLogin to FoodUserRole
GRANT EXECUTE ON GetUserById to FoodUserRole
GRANT EXECUTE ON GetRoleById to FoodUserRole
GRANT EXECUTE ON GetRoleByName to FoodUserRole
GRANT EXECUTE ON GetMealHistory to FoodUserRole
GRANT EXECUTE ON GetMealHistoryByType to FoodUserRole
GRANT EXECUTE ON GetMealFoodByMealHistoryId to FoodUserRole
GRANT EXECUTE ON GetProductById to FoodUserRole
GRANT EXECUTE ON GetProducts to FoodUserRole
GRANT EXECUTE ON GetRecipeById to FoodUserRole
GRANT EXECUTE ON GetRecipes to FoodUserRole
GRANT EXECUTE ON GetFavorites to FoodUserRole
GRANT EXECUTE ON GetRecommendation to FoodUserRole
GRANT EXECUTE ON GetAdminCountingCalories to FoodUserRole
GRANT EXECUTE ON GetFavoritesByRecipeAndUser to FoodUserRole

GRANT EXECUTE ON UpdateUser to FoodUserRole

GRANT EXECUTE ON AddNewRole to FoodUserRole
GRANT EXECUTE ON AddNewUser to FoodUserRole
GRANT EXECUTE ON AddNewProduct to FoodUserRole
GRANT EXECUTE ON AddNewRecipe to FoodUserRole
GRANT EXECUTE ON AddNewFavorite to FoodUserRole
GRANT EXECUTE ON AddNewMealHistory to FoodUserRole
GRANT EXECUTE ON AddNewMealFoodProduct to FoodUserRole
GRANT EXECUTE ON AddNewMealFoodRecipe to FoodUserRole

GRANT EXECUTE ON DeleteProduct to FoodUserRole
GRANT EXECUTE ON DeleteRecipe to FoodUserRole
GRANT EXECUTE ON DeleteFavorite to FoodUserRole
GRANT EXECUTE ON DeleteByProductId to FoodUserRole
GRANT EXECUTE ON DeleteByRecipeId to FoodUserRole
GRANT EXECUTE ON DeleteMealFood to FoodUserRole

exec GetUsers

exec GetUserByLogin @Login='qqq'