using Personal_Nutritionist.Command;
using Personal_Nutritionist.DataLayer;
using Personal_Nutritionist.DataLayer.Repository;
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
    public class UserFavoriteViewModel : ViewModelBase
    {
        private ObservableCollection<Favorites> _favorites;
        public ObservableCollection<Favorites> Favorites
        {
            get => _favorites;
            set
            {
                _favorites = value;
                OnPropertyChanged(nameof(Favorites));
            }
        }

        public Favorites _selectedFavorite;
        public Favorites SelectedFavorite
        {
            get => _selectedFavorite;
            set
            {
                _selectedFavorite = value;
                OnPropertyChanged(nameof(SelectedFavorite));
            }
        }

        public ICommand DeleteFavoriteCommand { get; }



        public UserFavoriteViewModel(PersonalNavigationStore personalNavigationStore)
        {

            try
            {
                Context context = new Context();
                FavoritesRepository repositoryFavorite = new FavoritesRepository();

                User currentUser = Account.getInstance(null).CurrentUser;

                List<Favorites> favorites = repositoryFavorite.GetByUserId(currentUser.UserId);
                Favorites = new ObservableCollection<Favorites>(favorites);


                DeleteFavoriteCommand = new DeleteFavorite(this);
               

            }
            catch
            {
                MessageBox.Show("Can't see all fauvorites");
            }


        }
    }
}
