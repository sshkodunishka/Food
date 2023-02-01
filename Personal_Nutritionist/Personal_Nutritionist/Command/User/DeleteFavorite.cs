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
    public class DeleteFavorite : CommandBase
    {
        private readonly UserFavoriteViewModel _viewModel;

        public DeleteFavorite(UserFavoriteViewModel viewModel)
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
                FavoritesRepository favoriteRepository = new FavoritesRepository();
                User user = Account.getInstance(null).CurrentUser;


                var id = _viewModel.SelectedFavorite.FavoritesId;
                _viewModel.Favorites = new ObservableCollection<Favorites>(
                    _viewModel.Favorites.Where(f => f.FavoritesId != _viewModel.SelectedFavorite.FavoritesId));
                favoriteRepository.Remove(id);

            }

            catch
            {
                MessageBox.Show("Can't delete selected meal");
            }
        }
    }
}
