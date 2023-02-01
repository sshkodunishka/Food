using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class MealHistoryRepository
    {
        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public MealHistoryRepository()
        {
            User user = Account.getInstance(null).CurrentUser;
            if (user != null && user.RoleId == 2)
            {
                connectionString = @"Server=localhost;Database=food;Trusted_Connection=True;";
            }
        }

        public List<MealHistory> GetMealHistory(User user, DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                    SqlCommand command = new SqlCommand("GetMealHistory", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = user.UserId;
                command.Parameters.Add("Date", sqlDbType: SqlDbType.Date).Value = date;
                var result = command.ExecuteReader();

                List<MealHistory> histories = new List<MealHistory>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        histories.Add(CreateMealHistoryFromDB(result));
                    }
                }
                return histories;
            }
        }

        public MealHistory GetMealHistory(User user, DateTime date, MealType SelectedType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetMealHistoryByType", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = user.UserId;
                command.Parameters.Add("Date", sqlDbType: SqlDbType.Date).Value = date;
                command.Parameters.Add("Type", sqlDbType: SqlDbType.Int).Value = SelectedType;
                var result = command.ExecuteReader();

                List<MealHistory> histories = new List<MealHistory>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        histories.Add(CreateMealHistoryFromDB(result));
                    }
                }
                else
                {
                    MealHistory mealHistory = new MealHistory(date, SelectedType, user.UserId);
                    this.Create(mealHistory);
                    histories.Add(mealHistory);
                }
                return histories.First();
            }
        }

        public void Create(MealHistory item)
        {
            using (SqlConnection createConnection = new SqlConnection(connectionString))
            {
                createConnection.Open();
                SqlCommand createCommand = new SqlCommand("AddNewMealHistory", createConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                createCommand.Parameters.Add("Date", sqlDbType: SqlDbType.Date).Value = item.Date;
                createCommand.Parameters.Add("MealType", sqlDbType: SqlDbType.Int).Value = item.MealType;
                createCommand.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = item.UserId;
                var createCommandres = createCommand.ExecuteReader();
              
            }
        }

        public static MealHistory CreateMealHistoryFromDB(SqlDataReader result)
        {
            MealHistory mealHistory = new MealHistory();
            mealHistory.MealHistoryId = (int)result["MealHistoryId"];
            mealHistory.Date = (DateTime)result["Date"];
            mealHistory.MealType = (MealType)result["MealType"];
            mealHistory.UserId = (int)result["UserId"];
            mealHistory.User = UserRepository.Get(mealHistory.UserId);
            mealHistory.MealFood = MealFoodRepository.GetByMealHistoryId(mealHistory.MealHistoryId);
            return mealHistory;
        }

    }
}
