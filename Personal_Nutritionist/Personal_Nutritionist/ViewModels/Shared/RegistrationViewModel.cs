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
    public class RegistrationViewModel : ViewModelBase
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

        SexType sexType = SexType.Female;

        public SexType SexType
        {
            get { return sexType; }
            set
            {
                if (sexType == value)
                    return;

                sexType = value;
                OnPropertyChanged("SexType");
                OnPropertyChanged("IsFemale");
                OnPropertyChanged("IsMale");
            }
        }

        public bool IsFemale
        {
            get { return SexType == SexType.Female; }
            set { SexType = value ? SexType.Female : SexType; }
        }

        public bool IsMale
        {
            get { return SexType == SexType.Male; }
            set { SexType = value ? SexType.Male : SexType; }
        }

        
        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                if (_error == value)
                    return;
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        private bool _isSended;
        public bool IsSended
        {
            get { return _isSended; }
            set
            {
                if (_isSended == value)
                    return;
                _isSended = value;
                OnPropertyChanged(nameof(IsSended));
            }
        }

        public ICommand ToProfile { get; }
        public ICommand LoginCommand { get; }
        public ICommand Ok { get; }

        public RegistrationViewModel(NavigationStore navigationStore, PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                ToProfile = new RegistrationCommand(this, navigationStore, personalNavigationStore);

                LoginCommand = new NavigateCommand<LoginViewModel>(
                    new NavigationService<LoginViewModel>(navigationStore,
                    () => new LoginViewModel(navigationStore, personalNavigationStore)));
                
            }
            catch
            {
                MessageBox.Show("Can't register user");
            }
        }

    }
}
