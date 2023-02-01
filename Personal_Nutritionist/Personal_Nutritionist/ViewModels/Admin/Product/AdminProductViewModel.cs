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
    public class AdminProductViewModel : ViewModelBase
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

        public Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public ICommand NavigateAdminAddProduct { get; }
        public ICommand AdminDeleteProduct { get; }

        public AdminProductViewModel(PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                ProductRepository repositoryProduct = new ProductRepository();

                User currentUser = Account.getInstance(null).CurrentUser;

                List<Product> products = repositoryProduct.GetAll();
                Products = new ObservableCollection<Product>(products);

                NavigateAdminAddProduct = new PersonalNavigateCommand<AdminAddProductViewModel>(
                   new PersonalNavigationService<AdminAddProductViewModel>(personalNavigationStore,
                   () =>
                   {
                       return new AdminAddProductViewModel(personalNavigationStore);
                   }));

                AdminDeleteProduct = new AdminDeleteProduct(this);

            }
            catch
            {
                MessageBox.Show("Can't see all products");
            }
        }
    }
}
