using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer
{
    public class Product
    { 
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; }
        public ICollection<MealFood> MealFoods { get; set; }


        public Product() { }

        public Product(string name, int calories, int userId)
        {
            Name = name;
            Calories = calories;
            UserId = userId;
        }
    }
}
