using Personal_Nutritionist.Services;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.Command
{
    public class PersonalNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly PersonalNavigationService<TViewModel> _navigationService;

        public PersonalNavigateCommand(PersonalNavigationService<TViewModel> navigationService)
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
