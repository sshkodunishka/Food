using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class AdminCountingCaloriesRepository
    {
       

        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public AdminCountingCaloriesRepository()
        {
            User user = Account.getInstance(null).CurrentUser;
            if (user != null && user.RoleId == 2)
            {
                connectionString = @"Server=localhost;Database=food;Trusted_Connection=True;";
            }
        }
       

        public List<AdminCountingCalories> Get(int userId, DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetAdminCountingCalories", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = userId;
                command.Parameters.Add("Date", sqlDbType: SqlDbType.Date).Value = date;
                var result = command.ExecuteReader();
                List<AdminCountingCalories> recipes = new List<AdminCountingCalories>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        recipes.Add(CreateAdminCaloriesFromDB(result));
                    }
                }
                return recipes;
            }
        }

        public void Create(AdminCountingCalories item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddAdminCountingCalorie", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.NVarChar).Value = item.UserId;
                command.Parameters.Add("Date", sqlDbType: SqlDbType.Date).Value = item.Date;
                command.Parameters.Add("Calories", sqlDbType: SqlDbType.Int).Value = item.Calories;
                command.ExecuteNonQuery();
            }
        }

        public void Update(AdminCountingCalories item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateAdminCountingCalorie", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Id", sqlDbType: SqlDbType.NVarChar).Value = item.AdminCountingCaloriesId;
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.NVarChar).Value = item.UserId;
                command.Parameters.Add("Date", sqlDbType: SqlDbType.NVarChar).Value = item.Date;
                command.Parameters.Add("Calories", sqlDbType: SqlDbType.NVarChar).Value = item.Calories;
                command.ExecuteNonQuery();
            }
        }

        public static AdminCountingCalories CreateAdminCaloriesFromDB(SqlDataReader result)
        {
            AdminCountingCalories adminCountingCalories = new AdminCountingCalories();
            adminCountingCalories.AdminCountingCaloriesId = (int)result["AdminCountingCaloriesId"];
            adminCountingCalories.UserId = (int)result["UserId"];
            adminCountingCalories.Calories = (int)result["Calories"];
            adminCountingCalories.Date = (DateTime)result["Date"];
            return adminCountingCalories;
        }

       

    }
}
