using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.Stores;
using Personal_Nutritionist.ViewModels;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Personal_Nutritionist.Command
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly PersonalNavigationStore _personalNavigationStore;
        private readonly NavigationStore _navigationStore;

        public LoginCommand(LoginViewModel viewModel, PersonalNavigationStore personalNavigationStore, NavigationStore navigationStore)
        {
            _viewModel = viewModel;
            _personalNavigationStore = personalNavigationStore;
            _navigationStore = navigationStore;
        }

        public override bool CanExecute(object parameter)
        {
            if (_viewModel.Login != null && _viewModel.Password != null)
                return true;
            return false;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Account.Dispose();

                UserRepository repository = new UserRepository(null);
                RoleRepository repositoryRole = new RoleRepository();

                foreach (User user in repository.Get())
                {
                    if (user.Login == _viewModel.Login)
                    {
                        if (user.Password == _viewModel.Password)
                        {
                            Account account = Account.getInstance(user);

                            Role role = repositoryRole.GetById(user.RoleId);
                            if (role.Name == RoleType.User.ToString())
                            {
                                _navigationStore.CurrentViewModel = new UserHomeViewModel(_personalNavigationStore, _navigationStore);
                                _personalNavigationStore.CurrentPersonalViewModel = new UserProfileViewModel(_personalNavigationStore);
                            }
                            else
                            {
                                _navigationStore.CurrentViewModel = new AdminHomeViewModel(_personalNavigationStore, _navigationStore);
                                _personalNavigationStore.CurrentPersonalViewModel = new AdminUsersViewModel(_personalNavigationStore);
                            }
                        }
                        else
                        {
                            _viewModel.Error = "Invalid password";
                            break;
                        }
                    }
                    else
                    {
                        _viewModel.Error = "No user with such login";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error in login");
            }
        }
    }
}
