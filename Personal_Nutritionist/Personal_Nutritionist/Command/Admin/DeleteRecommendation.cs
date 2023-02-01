using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
using Personal_Nutritionist.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.Command
{
    public class DeleteRecommendation : CommandBase
    {
        private readonly AdminRecommendationViewModel _viewModel;

        public DeleteRecommendation(AdminRecommendationViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {
                AdminRecommendationRepository adminRecommRepository = new AdminRecommendationRepository();
                User user = Account.getInstance(null).CurrentUser;


                var id = _viewModel.SelectedAdminRecommendation.AdminRecommendationId;
                _viewModel.AdminRecommendations = new ObservableCollection<AdminRecommendation>(
                    _viewModel.AdminRecommendations.Where(f => f.AdminRecommendationId != _viewModel.SelectedAdminRecommendation.AdminRecommendationId));
                adminRecommRepository.Remove(id);
            }

            catch
            {
                MessageBox.Show("Can't delete selected recommendation");
            }
        }
    }
}
