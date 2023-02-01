using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class AdminRecommendationRepository
    {
        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public AdminRecommendationRepository()
        {
            User user = Account.getInstance(null).CurrentUser;
            if (user != null && user.RoleId == 2)
            {
                connectionString = @"Server=localhost;Database=food;Trusted_Connection=True;";
            }
        }

        public List<AdminRecommendation> GetByUserId(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetRecommendation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = userId;
                var result = command.ExecuteReader();
                List<AdminRecommendation> recipes = new List<AdminRecommendation>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        recipes.Add(CreateAdminRecommendationeFromDB(result));
                    }
                }
                return recipes;
            }
        }

        public List<AdminRecommendation> GetByRecipe(int recipeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetRecommendationByRecipe", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("RecipeId", sqlDbType: SqlDbType.Int).Value = recipeId;
                var result = command.ExecuteReader();
                List<AdminRecommendation> recipes = new List<AdminRecommendation>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        recipes.Add(CreateAdminRecommendationeFromDB(result));
                    }
                }
                return recipes;
            }
        }

        public void Create(AdminRecommendation item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddNewAdminRecommendation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = item.UserId;
                command.Parameters.Add("RecipeId", sqlDbType: SqlDbType.Int).Value = item.RecipeId;
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteAdminRecommendation", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Id", sqlDbType: SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }


        public static AdminRecommendation CreateAdminRecommendationeFromDB(SqlDataReader result)
        {
            AdminRecommendation adminRecommendation = new AdminRecommendation();
            adminRecommendation.AdminRecommendationId = (int)result["AdminRecommendationId"];
            adminRecommendation.UserId = (int)result["UserId"];
            adminRecommendation.User = UserRepository.Get(adminRecommendation.UserId);
            adminRecommendation.RecipeId = (int)result["RecipeId"];
            adminRecommendation.Recipe = RecipeRepository.Get(adminRecommendation.RecipeId);
            return adminRecommendation;
        }
    }
}
