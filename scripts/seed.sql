exec AddNewRole @Name = 'User'
exec AddNewRole @Name = 'Admin'

declare @roleId int; 
select @roleId = RoleId from Roles where Name = 'Admin'


select * from Products
exec AddNewUser @Login = 'admin',
	@Password = '4AzyWtQmg7PfZ4xh9Cxr2g==',
	@Name = 'Kristina',
	@Surname = 'Shkoda',
	@Weight = 58,
	@Age = 19,
	@Height = 167,
	@Sex = 1,
	@RoleId = @roleId

declare @userId int; 
select @userId = UserId from Users where RoleId = 2

exec AddNewProduct @Name = 'Banana',
	@Calories = 89,
	@UserId = @userId

exec AddNewProduct @Name = 'Tomato',
	@Calories = 18,
	@UserId = @userId

exec AddNewProduct @Name = 'Сucumber',
	@Calories = 16,
	@UserId = @userId

exec AddNewProduct @Name = 'Milk',
	@Calories = 42,
	@UserId = @userId

exec AddNewProduct @Name = 'Boiled egg',
	@Calories = 155,
	@UserId = @userId

exec AddNewProduct @Name = 'Sour cream',
	@Calories = 193,
	@UserId = @userId

exec AddNewRecipe @Name = 'Omelet',
	@Calories = 154,
	@Description = 'eggs - 2 pcs., milk - 120 ml, vegetable oil - 1 tsp., butter - 5g',
	@UserId = @userId

exec AddNewRecipe @Name = 'Pancakes',
	@Calories = 206,
	@Description = 'Kefir - 200 ml., Egg - 2 pcs., Sunflower oil - 1 tbsp., Wheat flour - 200 g., Water - 20 g., Salt - 3 g., Soda - 1 tsp., Sugar - 2 tsp.',
	@UserId = @userId
	
exec AddNewRecipe @Name = 'Draniki',
	@Calories = 157,
	@Description = 'Potatoes-5 pieces, Onions-½ piece, Chicken egg-1 item, Wheat flour-2 tablespoons, Ground black pepper, Salt, Vegetable oil',
	@UserId = @userId

exec AddNewRecipe @Name = 'Cutlet',
	@Calories = 154,
	@Description = '400 gr. minced pork, 400 gr. ground beef, 1 small onion, 2 eggs or 1 tbsp. mayonnaise, 100-150 gr. white bread, vegetable oil, ground black pepper, salt to taste, flour or breadcrumbs',
	@UserId = @userId