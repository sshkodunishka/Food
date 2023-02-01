using Personal_Nutritionist.Command;
using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Personal_Nutritionist.ViewModels
{
    public class UserSettingsViewModel : ViewModelBase
    {
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(_age));
            }
        }



        private int _weight;
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private int _height;
        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen == value)
                    return;
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }

        public ICommand SaveChanges { get; }

        public UserSettingsViewModel(PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                User currentUser = Account.getInstance(null).CurrentUser;
                Login = currentUser.Login;
                Password = currentUser.Password;
                Name = currentUser.Name;
                Surname = currentUser.Surname;
                Age = currentUser.Age;
                Weight = (int)currentUser.Weight;
                Height = currentUser.Height;

                SaveChanges = new SaveChanges(this);

            }
            catch
            {
                MessageBox.Show("Can't see all datas");
            }
        
        }
    }
}
