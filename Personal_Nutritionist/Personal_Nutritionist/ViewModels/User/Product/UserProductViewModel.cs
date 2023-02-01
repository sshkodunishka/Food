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
    public class UserProductViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _product;
        public ObservableCollection<Product> Products
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ICommand NavigateUserAddProduct { get; }
        public UserProductViewModel(PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                ProductRepository repositoryProduct = new ProductRepository();

                User currentUser = Account.getInstance(null).CurrentUser;

                List<Product> products = repositoryProduct.GetByUserId(currentUser.UserId);
                Products = new ObservableCollection<Product>(products);

                NavigateUserAddProduct = new PersonalNavigateCommand<UserAddProductViewModel>(
                   new PersonalNavigationService<UserAddProductViewModel>(personalNavigationStore,
                   () =>
                   {
                       return new UserAddProductViewModel(personalNavigationStore);
                   }));
            }
            catch
            {
                MessageBox.Show("Can't see all products");
            }

        }
    }
}
