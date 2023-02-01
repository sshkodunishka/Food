drop index #User_index on Users
drop index #Products_index on Products
drop index #Recipes_index on Recipes

create index #User_index on Users(Login)
create index #Products_index on Products(Name)
create index #Recipes_index on Recipes(Name)