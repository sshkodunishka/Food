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
    public class AddNewRecommendation : CommandBase
    {
        private readonly AdminAddNewRecommendation _viewModel;
        private readonly PersonalNavigationStore _personalNavigationStore;

        public AddNewRecommendation(AdminAddNewRecommendation viewModel, PersonalNavigationStore personalNavigationStore)
        {
            _viewModel = viewModel;
            _personalNavigationStore = personalNavigationStore;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }


        public override void Execute(object parameter)
        {
            try
            {
                Context context = new Context();
                AdminRecommendationRepository adminRecommendationRepository = new AdminRecommendationRepository();
                var fav = new AdminRecommendation(_viewModel.SelectedUser.UserId, _viewModel.SelectedRecipe.RecipeId);
                adminRecommendationRepository.Create(fav);
                _personalNavigationStore.CurrentPersonalViewModel = new AdminRecommendationViewModel(_personalNavigationStore, _viewModel.SelectedUser);
            }

            catch
            {
                MessageBox.Show("Can't add recommendation");
            }
        }
    }
}
