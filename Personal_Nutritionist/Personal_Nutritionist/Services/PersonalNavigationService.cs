using Personal_Nutritionist.Stores;
using Personal_Nutritionist.ViewModels;
using System;
using System.Windows;

namespace Personal_Nutritionist.Services
{
    public class PersonalNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly PersonalNavigationStore _personalNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public PersonalNavigationService(PersonalNavigationStore personalNavigationStore, Func<TViewModel> createViewModel)
        {
            _personalNavigationStore = personalNavigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            try
            {
                var CurrentPersonalViewModel = _createViewModel();
                if (CurrentPersonalViewModel == null)
                    return;
                _personalNavigationStore.CurrentPersonalViewModel = CurrentPersonalViewModel;
            }
            catch
            {
                MessageBox.Show("Can't switch pages");
            }
        }
    }
}
