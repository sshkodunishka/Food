using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class RoleRepository
    {
        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public Role GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetRoleById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("RoleId", sqlDbType: SqlDbType.Int).Value = id;
                var result = command.ExecuteReader();
                Role role = new Role();
                if (result.HasRows)
                {
                    result.Read();
                    role = CreateRoleFromDB(result);
                }
                return role;
            }
        }

        public Role GetByName(RoleType name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetRoleByName", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Name", sqlDbType: SqlDbType.NVarChar).Value = name.ToString();
                var result = command.ExecuteReader();
                Role role = new Role();
                if (result.HasRows)
                {
                    result.Read();
                    role = CreateRoleFromDB(result);
                }
                return role;
            }
        }

        public static Role CreateRoleFromDB(SqlDataReader result)
        {
            Role role = new Role();
            role.RoleId = (int)result["RoleId"];
            role.RoleName = result["Name"].ToString() == "User" ? RoleType.User : RoleType.Admin;
            return role;
        }
    }
}
