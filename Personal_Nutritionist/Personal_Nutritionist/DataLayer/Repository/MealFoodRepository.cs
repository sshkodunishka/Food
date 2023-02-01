using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class MealFoodRepository
    {
        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public MealFoodRepository()
        {
            User user = Account.getInstance(null).CurrentUser;
            if (user != null && user.RoleId == 2)
            {
                connectionString = @"Server=localhost;Database=food;Trusted_Connection=True;";
            }
        }

        public List<MealFood> GetProductId(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetMealFoodByProductId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("ProductId", sqlDbType: SqlDbType.Int).Value = productId;
                var result = command.ExecuteReader();
                List<MealFood> foods = new List<MealFood>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        foods.Add(CreateMealFoodFromDB(result));
                    }
                }
                return foods;
            }
        }

        public List<MealFood> GetRecipeId(int recipeid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetMealFoodByRecipeId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("RecipeId", sqlDbType: SqlDbType.Int).Value = recipeid;
                var result = command.ExecuteReader();
                List<MealFood> foods = new List<MealFood>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        foods.Add(CreateMealFoodFromDB(result));
                    }
                }
                return foods;
            }
        }


        public static List<MealFood> GetByMealHistoryId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetMealFoodByMealHistoryId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("MealHistoryId", sqlDbType: SqlDbType.Int).Value = id;
                var result = command.ExecuteReader();
                List<MealFood> foods = new List<MealFood>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        foods.Add(CreateMealFoodFromDB(result));
                    }
                }
                return foods;
            }
        }

        public void Create(MealFood item)
        {
            if (item.ProductId != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("AddNewMealFoodProduct", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add("ProductId", sqlDbType: SqlDbType.Int).Value = item.ProductId;
                    command.Parameters.Add("MealHistoryId", sqlDbType: SqlDbType.NVarChar).Value = item.MealHistoryId;
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("AddNewMealFoodRecipe", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add("RecipeId", sqlDbType: SqlDbType.Int).Value = item.RecipeId;
                    command.Parameters.Add("MealHistoryId", sqlDbType: SqlDbType.NVarChar).Value = item.MealHistoryId;
                    command.ExecuteNonQuery();
                }
            }
           
        }

        public void RemoveByProductId(int id)
        {
            using (SqlConnection createConnection = new SqlConnection(connectionString))
            {
                createConnection.Open();
                SqlCommand createCommand = new SqlCommand("DeleteByProductId", createConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                createCommand.Parameters.Add("ProductId", sqlDbType: SqlDbType.Int).Value = id;
                var createCommandres = createCommand.ExecuteReader();

            }
        }

        public void RemoveByRecipeId(int id)
        {
            using (SqlConnection createConnection = new SqlConnection(connectionString))
            {
                createConnection.Open();
                SqlCommand createCommand = new SqlCommand("DeleteByRecipeId", createConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                createCommand.Parameters.Add("RecipeId", sqlDbType: SqlDbType.Int).Value = id;
                var createCommandres = createCommand.ExecuteReader();

            }
        }

        public void Remove(int id)
        {
            using (SqlConnection createConnection = new SqlConnection(connectionString))
            {
                createConnection.Open();
                SqlCommand createCommand = new SqlCommand("DeleteMealFood", createConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                createCommand.Parameters.Add("Id", sqlDbType: SqlDbType.Int).Value = id;
                var createCommandres = createCommand.ExecuteReader();
            }
        }


        public static MealFood CreateMealFoodFromDB(SqlDataReader result)
        {
            MealFood mealFood = new MealFood();
            mealFood.MealFoodId = (int)result["MealFoodId"];
            if(result["ProductId"] is not DBNull)
            {
                mealFood.ProductId = (int)result["ProductId"];
                mealFood.Product = ProductRepository.Get((int)mealFood.ProductId);
            }
            if (result["RecipeId"] is not DBNull)
            {
                mealFood.RecipeId = (int)result["RecipeId"];
                mealFood.Recipe = RecipeRepository.Get((int)mealFood.RecipeId);
            }
            mealFood.MealHistoryId = (int)result["MealHistoryId"];
            return mealFood;
        }
    }
}
