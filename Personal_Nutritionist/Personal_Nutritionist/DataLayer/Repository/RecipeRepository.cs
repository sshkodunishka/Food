using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class RecipeRepository
    {
        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public RecipeRepository()
        {
            User user = Account.getInstance(null).CurrentUser;
            if (user != null && user.RoleId == 2)
            {
                connectionString = @"Server=localhost;Database=food;Trusted_Connection=True;";
            }
        }

        public static Recipe Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetRecipeById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("RecipeId", sqlDbType: SqlDbType.Int).Value = id;
                var result = command.ExecuteReader();
                Recipe recipe = new Recipe();
                if (result.HasRows)
                {
                    result.Read();
                    recipe = CreateRecipeFromDB(result);
                }
                return recipe;
            }
        }

        public List<Recipe> GetByUserId(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetRecipes", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = userId;
                var result = command.ExecuteReader();
                List<Recipe> recipes = new List<Recipe>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        recipes.Add(CreateRecipeFromDB(result));
                    }
                }
                return recipes;
            }
        }

        public List<Recipe> GetRecipeForRecomendations(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetRecipesForRecommendations", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = userId;
                var result = command.ExecuteReader();
                List<Recipe> recipes = new List<Recipe>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        recipes.Add(CreateRecipeFromDB(result));
                    }
                }
                return recipes;
            }
        }

        public List<Recipe> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetAllRecipes", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var result = command.ExecuteReader();
                List<Recipe> recipes = new List<Recipe>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        recipes.Add(CreateRecipeFromDB(result));
                    }
                }
                return recipes;
            }
        }
        

        public void Create(Recipe item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddNewRecipe", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Name", sqlDbType: SqlDbType.NVarChar).Value = item.Name;
                command.Parameters.Add("Calories", sqlDbType: SqlDbType.NVarChar).Value = item.Calories;
                command.Parameters.Add("Description", sqlDbType: SqlDbType.NVarChar).Value = item.Description;
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.NVarChar).Value = item.UserId;
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteRecipe", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Id", sqlDbType: SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public static Recipe CreateRecipeFromDB(SqlDataReader result)
        {
            Recipe recipe = new Recipe();
            recipe.RecipeId = (int)result["RecipeId"];
            recipe.Name = result["Name"].ToString();
            recipe.Calories = (int)result["Calories"];
            recipe.UserId = (int)result["UserId"];
            recipe.User = UserRepository.Get(recipe.UserId);
            return recipe;
        }
    }
}
