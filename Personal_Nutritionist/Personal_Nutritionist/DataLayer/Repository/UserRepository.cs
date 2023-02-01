using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class UserRepository
    {
        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public UserRepository(User user)
        {
            if (user != null && user.RoleId == 2)
            {
                connectionString = @"Server=localhost;Database=food;Trusted_Connection=True;";
            }
        }

        public static User Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUserById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = id;
                var result = command.ExecuteReader();
                User user = new User();
                if (result.HasRows)
                {
                    result.Read();
                    user = CreateUserFromDB(result);
                }
                return user;
            }
        }

        public User? Get(string login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUserByLogin", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Login", sqlDbType: SqlDbType.NVarChar).Value = login;
                var result = command.ExecuteReader();
                User user = new User();
                if (result.HasRows)
                {
                    result.Read();
                    user = CreateUserFromDB(result);
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<User> Get()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUsers", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var result = command.ExecuteReader();
                List<User> users = new List<User>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        users.Add(CreateUserFromDB(result));
                    }
                }
                return users;
            }
        }

        public List<User> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetWithUserRole", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var result = command.ExecuteReader();
                List<User> users = new List<User>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        users.Add(CreateUserFromDB(result));
                    }
                }
                return users;
            }
        }

        

        public void Create(User item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddNewUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Login", sqlDbType: SqlDbType.NVarChar).Value = item.Login;
                command.Parameters.Add("Password", sqlDbType: SqlDbType.NVarChar).Value = item.Password;
                command.Parameters.Add("Name", sqlDbType: SqlDbType.NVarChar).Value = item.Name;
                command.Parameters.Add("Surname", sqlDbType: SqlDbType.NVarChar).Value = item.Surname;
                command.Parameters.Add("Weight", sqlDbType: SqlDbType.Real).Value = item.Weight;
                command.Parameters.Add("Age", sqlDbType: SqlDbType.Int).Value = item.Age;
                command.Parameters.Add("Height", sqlDbType: SqlDbType.Int).Value = item.Height;
                command.Parameters.Add("Sex", sqlDbType: SqlDbType.Int).Value = item.Sex;
                command.Parameters.Add("RoleId", sqlDbType: SqlDbType.Int).Value = item.RoleId;
                command.ExecuteNonQuery();
            }
        }

        public void Update(User item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Login", sqlDbType: SqlDbType.NVarChar).Value = item.Login;
                command.Parameters.Add("Name", sqlDbType: SqlDbType.NVarChar).Value = item.Name;
                command.Parameters.Add("Surname", sqlDbType: SqlDbType.NVarChar).Value = item.Surname;
                command.Parameters.Add("Weight", sqlDbType: SqlDbType.Real).Value = item.Weight;
                command.Parameters.Add("Age", sqlDbType: SqlDbType.Int).Value = item.Age;
                command.Parameters.Add("Height", sqlDbType: SqlDbType.Int).Value = item.Height;
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = item.UserId;
                command.ExecuteNonQuery();
            }
        }

        public static User CreateUserFromDB(SqlDataReader result)
        {
            User user = new User();
            user.UserId = (int)result["UserId"];
            user.Login = result["Login"].ToString();
            user.Password = result["Password"].ToString();
            user.Name = result["Name"].ToString();
            user.Surname = result["Surname"].ToString();
            user.Weight = (float)result["Weight"];
            user.Age = (int)result["Age"];
            user.Height = (int)result["Height"];
            user.Sex = (int)result["Sex"] == 0 ? SexType.Female : SexType.Male;
            user.RoleId = (int)result["RoleId"];
            return user;
        }
    }
}
