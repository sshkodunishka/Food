using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer
{
    public class AdminCountingCalories
    {
        public int AdminCountingCaloriesId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int Calories { get; set; }

        public AdminCountingCalories(int userId, DateTime date, int calories)
        {
            UserId = userId;
            Date = date;
            Calories = calories;
        }
        public AdminCountingCalories()
        {
        }
    }
}
