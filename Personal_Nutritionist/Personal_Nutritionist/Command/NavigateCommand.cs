using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Personal_Nutritionist.Services;
using Personal_Nutritionist.ViewModels;


namespace Personal_Nutritionist.Command
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            try
            {
                _navigationService.Navigate();
            }
            catch
            {
                MessageBox.Show("Can't switch pages");
            }
        }
    }
}
