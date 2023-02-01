using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.DataLayer
{
    public class Role
    {
        public int RoleId { get; set; }
        [NotMapped]
        public RoleType RoleName { get; set; }

        public string Name
        {
            get { return RoleName.ToString(); }
            private set
            {
                RoleName = (RoleType)Enum.Parse(typeof(RoleType), value, true);
            }
        }

        ICollection<User> Users { get; set; }

        public Role(string name)
        {
            try
            {
                Name = name;
            }
            catch
            {
                MessageBox.Show("Can't create role");
            }
        }
        public Role() { }
    }
}
