using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class FavoritesRepository
    {
        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public FavoritesRepository()
        {
            User user = Account.getInstance(null).CurrentUser;
            if (user != null && user.RoleId == 2)
            {
                connectionString = @"Server=localhost;Database=food;Trusted_Connection=True;";
            }
        }

        public List<Favorites> GetByUserId(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetFavorites", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = userId;
                var result = command.ExecuteReader();
                List<Favorites> products = new List<Favorites>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        products.Add(CreateFavoritesFromDB(result));
                    }
                }
                return products;
            }
        }

        public List<Favorites> GetByRecipeId(int recipeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetFavoritesByRecipe", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("RecipeId", sqlDbType: SqlDbType.Int).Value = recipeId;
                var result = command.ExecuteReader();
                List<Favorites> products = new List<Favorites>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        products.Add(CreateFavoritesFromDB(result));
                    }
                }
                return products;
            }
        }

        public Favorites GetByRecipeId(int recipeId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetFavoritesByRecipeAndUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("RecipeId", sqlDbType: SqlDbType.Int).Value = recipeId;
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = userId;
                var result = command.ExecuteReader();
                Favorites fav = new Favorites();
                if (result.HasRows)
                {
                    result.Read();
                    fav = CreateFavoritesFromDB(result);
                }
                return fav;
            }
        }

        public void Create(Favorites item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddNewFavorite", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.NVarChar).Value = item.UserId;
                command.Parameters.Add("RecipeId", sqlDbType: SqlDbType.NVarChar).Value = item.RecipeId;
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteFavorite", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Id", sqlDbType: SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public static Favorites CreateFavoritesFromDB(SqlDataReader result)
        {
            Favorites favorites = new Favorites();
            favorites.FavoritesId = (int)result["FavoritesId"];
            favorites.UserId = (int)result["UserId"];
            favorites.RecipeId = (int)result["RecipeId"];
            favorites.User = UserRepository.Get(favorites.UserId);
            favorites.Recipe = RecipeRepository.Get(favorites.RecipeId);
            return favorites;
        }
    }
}
