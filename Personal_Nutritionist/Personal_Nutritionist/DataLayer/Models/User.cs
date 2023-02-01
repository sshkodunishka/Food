using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.DataLayer
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public SexType Sex { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        ICollection<Product> Products { get; set; }
        ICollection<Recipe> Resipes { get; set; }
        ICollection<AdminCountingCalories> AdminCountingCalories { get; set; }

        public ICollection<Favorites> Favorites { get; set; }
        public ICollection<AdminRecommendation> AdminRecommendations { get; set; }
        public ICollection<MealHistory> MealHistories { get; set; }

        public User() { }
        public User(string login, string password, string name, string surname, int age, int weight, int height)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Age = age;
            Weight = weight;
            Height = height;
        }


        public User(string login, string password, string name, string surname, int age, int roleId)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Age = age;
            RoleId = roleId;
        }

        public User(string login, string password, string name, string surname, int weight, int age, int roleId, int height, SexType sex)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Weight = weight;
            Age = age;
            RoleId = roleId;
            Height = height;
            Sex = sex;
        }

        public static string getHash(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return "Error";
            }
            else
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
