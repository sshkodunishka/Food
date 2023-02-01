using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.Stores;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.Command
{
    public class SaveChanges : CommandBase
    {
        private readonly UserSettingsViewModel _viewModel;

        public SaveChanges(UserSettingsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            try
            {
                if (_viewModel.Login != null && _viewModel.Name != null && _viewModel.Surname != null && _viewModel.Age != 0 && _viewModel.Weight != 0 && _viewModel.Height != 0)
                    return true;
                return false;
            }
            catch
            {
                MessageBox.Show("Data cannot be updated");
                return false;
            }
        }

        public override void Execute(object parameter)
        {
            try
            {

                User currentUser = Account.getInstance(null).CurrentUser;
                UserRepository repository = new UserRepository(currentUser);
                currentUser.Login = _viewModel.Login;
                currentUser.Name = _viewModel.Name;
                currentUser.Surname = _viewModel.Surname;
                currentUser.Age = _viewModel.Age;
                currentUser.Weight = (int)_viewModel.Weight;
                currentUser.Height = _viewModel.Height;
                _viewModel.IsOpen = true;

                repository.Update(currentUser);
            }
            catch
            {
                MessageBox.Show("Data cannot be saved");
            }
        }
    }
}
