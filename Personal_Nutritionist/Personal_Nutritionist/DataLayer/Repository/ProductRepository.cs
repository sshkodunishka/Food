using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer.Repository
{
    internal class ProductRepository
    {
        static string connectionString = @"Server=localhost;Database=food;Trusted_Connection=False;User ID=FoodUser;Password=userpassword";

        public ProductRepository()
        {
            User user = Account.getInstance(null).CurrentUser;
            if (user != null && user.RoleId == 2)
            {
                connectionString = @"Server=localhost;Database=food;Trusted_Connection=True;";
            }
        }

        public static Product Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetProductById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("ProductId", sqlDbType: SqlDbType.Int).Value = id;
                var result = command.ExecuteReader();
                Product product = new Product();
                if (result.HasRows)
                {
                    result.Read();
                    product = CreateProductFromDB(result);
                }
                return product;
            }
        }

        public List<Product> GetByUserId(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetProducts", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.Int).Value = userId;
                var result = command.ExecuteReader();
                List<Product> products = new List<Product>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        products.Add(CreateProductFromDB(result));
                    }
                }
                return products;
            }
        }

        public List<Product> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetAllProducts", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var result = command.ExecuteReader();
                List<Product> products = new List<Product>();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        products.Add(CreateProductFromDB(result));
                    }
                }
                return products;
            }
        }

        public void Create(Product item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("AddNewProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Name", sqlDbType: SqlDbType.NVarChar).Value = item.Name;
                command.Parameters.Add("Calories", sqlDbType: SqlDbType.NVarChar).Value = item.Calories;
                command.Parameters.Add("UserId", sqlDbType: SqlDbType.NVarChar).Value = item.UserId;
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DeleteProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("Id", sqlDbType: SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public static Product CreateProductFromDB(SqlDataReader result)
        {
            Product product = new Product();
            product.ProductId = (int)result["ProductId"];
            product.Name = result["Name"].ToString();
            product.Calories = (int)result["Calories"];
            product.UserId = (int)result["UserId"];
            product.User = UserRepository.Get(product.UserId);
            return product;
        }
    }
}
