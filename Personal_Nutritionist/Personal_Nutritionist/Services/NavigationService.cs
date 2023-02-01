using Personal_Nutritionist.Stores;
using Personal_Nutritionist.ViewModels;
using System;
using System.Windows;

namespace Personal_Nutritionist.Services
{
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            try
            {
                _navigationStore.CurrentViewModel = _createViewModel();
            }
            catch
            {
                MessageBox.Show("Can't switch pages");
            }
        }
    }
}
