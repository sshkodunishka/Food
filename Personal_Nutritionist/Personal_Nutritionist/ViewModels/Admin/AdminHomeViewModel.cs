using Personal_Nutritionist.Command;
using Personal_Nutritionist.Services;
using Personal_Nutritionist.Stores;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Personal_Nutritionist.ViewModels
{
    internal class AdminHomeViewModel : ViewModelBase
    {
        private readonly PersonalNavigationStore _personalNavigationStore;
        public ViewModelBase CurrentPersonalViewModel => _personalNavigationStore.CurrentPersonalViewModel;

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentPersonalViewModel));
        }

        public ICommand ProfileCommand { get; }
        public ICommand RecipeCommand { get; }
        public ICommand ProductCommand { get; }
        public ICommand LogOutCommand { get; }

        public AdminHomeViewModel(PersonalNavigationStore personalNavigationStore, NavigationStore navigationStore)
        {
            try
            {
                _personalNavigationStore = personalNavigationStore;
                _personalNavigationStore.CurrentPersonalViewModelChanged += OnCurrentViewModelChanged;

                ProfileCommand = new PersonalNavigateCommand<AdminUsersViewModel>(
                    new PersonalNavigationService<AdminUsersViewModel>(personalNavigationStore,
                    () => new AdminUsersViewModel(personalNavigationStore)));

                RecipeCommand = new PersonalNavigateCommand<AdminRecipeViewModel>(
                    new PersonalNavigationService<AdminRecipeViewModel>(personalNavigationStore,
                    () => new AdminRecipeViewModel(personalNavigationStore)));

                ProductCommand = new PersonalNavigateCommand<AdminProductViewModel>(
                    new PersonalNavigationService<AdminProductViewModel>(personalNavigationStore,
                    () => new AdminProductViewModel(personalNavigationStore)));

                LogOutCommand = new LogOutCommand( personalNavigationStore, navigationStore);
            }
            catch
            {
                MessageBox.Show("Can't load admin home page");
            }
        }
    }
}
