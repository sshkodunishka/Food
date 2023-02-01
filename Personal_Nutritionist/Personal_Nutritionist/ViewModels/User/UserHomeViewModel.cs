
using System.Windows;
using System.Windows.Input;
using Personal_Nutritionist.Command;
using Personal_Nutritionist.Services;
using Personal_Nutritionist.Stores;
using Personal_Nutritionist.ViewModels;

namespace Personal_Nutritionist.ViewModels
{
    public class UserHomeViewModel : ViewModelBase
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
        public ICommand FavoriteCommand { get; }
        public ICommand RecommendationCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand SettingsCommand { get; }

        public UserHomeViewModel(PersonalNavigationStore personalNavigationStore, NavigationStore navigationStore)
        {
            try
            {
                _personalNavigationStore = personalNavigationStore;
                _personalNavigationStore.CurrentPersonalViewModelChanged += OnCurrentViewModelChanged;

                ProfileCommand = new PersonalNavigateCommand<UserProfileViewModel>(
                    new PersonalNavigationService<UserProfileViewModel>(personalNavigationStore,
                    () => new UserProfileViewModel(personalNavigationStore)));

                RecipeCommand = new PersonalNavigateCommand<UserRecipeViewModel>(
                    new PersonalNavigationService<UserRecipeViewModel>(personalNavigationStore,
                    () => new UserRecipeViewModel(personalNavigationStore)));

                ProductCommand = new PersonalNavigateCommand<UserProductViewModel>(
                    new PersonalNavigationService<UserProductViewModel>(personalNavigationStore,
                    () => new UserProductViewModel(personalNavigationStore)));

                FavoriteCommand = new PersonalNavigateCommand<UserFavoriteViewModel>(
                    new PersonalNavigationService<UserFavoriteViewModel>(personalNavigationStore,
                    () => new UserFavoriteViewModel(personalNavigationStore)));

                RecommendationCommand = new PersonalNavigateCommand<AdminRecommendationsViewModel>(
                    new PersonalNavigationService<AdminRecommendationsViewModel>(personalNavigationStore,
                    () => new AdminRecommendationsViewModel(personalNavigationStore)));

                SettingsCommand = new PersonalNavigateCommand<UserSettingsViewModel>(
                   new PersonalNavigationService<UserSettingsViewModel>(personalNavigationStore,
                   () => new UserSettingsViewModel(personalNavigationStore)));


                LogOutCommand = new LogOutCommand( personalNavigationStore, navigationStore);
            }
            catch
            {
                MessageBox.Show("Can't load user home page");
            }
        }
    }
}

