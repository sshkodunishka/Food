using Personal_Nutritionist.Command;
using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.Services;
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
    public class LoginViewModel : ViewModelBase
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
                _password = User.getHash(value);
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public ICommand RegistrationCommand { get; }

        public ICommand LoginCommand { get; }

        public LoginViewModel(NavigationStore navigationStore, PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                RegistrationCommand = new NavigateCommand<RegistrationViewModel>(
                    new NavigationService<RegistrationViewModel>(navigationStore,
                    () => new RegistrationViewModel(navigationStore, personalNavigationStore)));

                LoginCommand = new LoginCommand(this, personalNavigationStore, navigationStore);
            }
            catch
            {
                MessageBox.Show("Can't login");
            }
        }
    }
}
