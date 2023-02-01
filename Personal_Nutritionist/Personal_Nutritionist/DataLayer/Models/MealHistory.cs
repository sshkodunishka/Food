using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Nutritionist.DataLayer
{
    public class MealHistory
    {
        public int MealHistoryId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public MealType MealType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<MealFood> MealFood { get; set; }

        public MealHistory() { }

        public MealHistory(DateTime date, MealType type, int userId)
        {
            Date = date;
            MealType = type;
            UserId = userId;
        }
    }
}
