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
    internal class LogOutCommand : CommandBase
    {
       
 //       private readonly AdministratorHomePageViewModel _viewModelAdmin;
        private readonly PersonalNavigationStore _personalNavigationStore;
        private readonly NavigationStore _navigationStore;

        public LogOutCommand( PersonalNavigationStore personalNavigationStore, NavigationStore navigationStore)
        {
         
            _personalNavigationStore = personalNavigationStore;
            _navigationStore = navigationStore;
        }

        //public LogOutCommand(AdministratorHomePageViewModel viewModel, PersonalNavigationStore personalNavigationStore, NavigationStore navigationStore)
        //{
        //    _viewModelAdmin = viewModel;
        //    _personalNavigationStore = personalNavigationStore;
        //    _navigationStore = navigationStore;
        //}

        public override void Execute(object parameter)
        {
            try
            {
                _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _personalNavigationStore);
                //Account.Dispose();
            }
            catch
            {
                MessageBox.Show("Can't logout from account");
            }
        }
    }
}
