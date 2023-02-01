using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.DataLayer
{
    public class Account
    {
        private static Account instance;

        public User CurrentUser { get; private set; }

        protected Account(User user)
        {
            this.CurrentUser = user;
        }

        public static Account getInstance(User user)
        {
            try
            {
                if (instance == null)
                    instance = new Account(user);
                return instance;
            }
            catch
            {
                MessageBox.Show("Can't get account");
                return null;
            }
        }


        public static void Dispose()
        {
            try
            {
                instance = null;
            }
            catch
            {
                MessageBox.Show("Can't logout from account");
            }
        }
    }
}
