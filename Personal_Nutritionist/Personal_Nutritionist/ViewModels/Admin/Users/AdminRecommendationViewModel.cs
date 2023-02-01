using Personal_Nutritionist.Command;
using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.Services;
using Personal_Nutritionist.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Personal_Nutritionist.ViewModels
{
    public class AdminRecommendationViewModel : ViewModelBase
    {
        private ObservableCollection<AdminRecommendation> _adminRecommendations;
        public ObservableCollection<AdminRecommendation> AdminRecommendations
        {
            get => _adminRecommendations;
            set
            {
                _adminRecommendations = value;
                OnPropertyChanged(nameof(AdminRecommendations));
            }
        }

        public AdminRecommendation _selectedAdminRecommendation;
        public AdminRecommendation SelectedAdminRecommendation
        {
            get => _selectedAdminRecommendation;
            set
            {
                _selectedAdminRecommendation = value;
                OnPropertyChanged(nameof(SelectedAdminRecommendation));
            }
        }

        public User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ICommand DeleteRecommendationCommand { get; }
        public ICommand NavigateAddRecommendationCommand { get; }



        public AdminRecommendationViewModel(PersonalNavigationStore personalNavigationStore, User selectedUser)
        {

            try
            {
                AdminRecommendationRepository repositoryFavorite = new AdminRecommendationRepository();
                SelectedUser = selectedUser;
                List<AdminRecommendation> adminRecommendations = repositoryFavorite.GetByUserId(SelectedUser.UserId);
                AdminRecommendations = new ObservableCollection<AdminRecommendation>(adminRecommendations);


                DeleteRecommendationCommand = new DeleteRecommendation(this);

                NavigateAddRecommendationCommand = new PersonalNavigateCommand<AdminAddNewRecommendation>(
                 new PersonalNavigationService<AdminAddNewRecommendation>(personalNavigationStore,
                 () =>
                 {
                     return new AdminAddNewRecommendation(personalNavigationStore, selectedUser);
                 }));


            }
            catch
            {
                MessageBox.Show("Can't see all records");
            }


        }
    }
}
