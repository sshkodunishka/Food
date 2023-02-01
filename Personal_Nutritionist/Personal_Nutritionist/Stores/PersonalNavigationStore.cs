using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.Stores
{
    public class PersonalNavigationStore
    {
        public event Action CurrentPersonalViewModelChanged;

        private ViewModelBase _currentPersonalViewModel;
        public ViewModelBase CurrentPersonalViewModel
        {
            get => _currentPersonalViewModel;
            set
            {
                _currentPersonalViewModel?.Dispose();
                _currentPersonalViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            try
            {
                CurrentPersonalViewModelChanged?.Invoke();
            }
            catch
            {
                MessageBox.Show("Can't change page");
            }
        }
    }
}
