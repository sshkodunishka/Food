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
    public class AdminAddProduct : CommandBase
    {
        private readonly AdminAddProductViewModel _viewModel;
        private readonly PersonalNavigationStore _personalNavigationStore;

        public AdminAddProduct(AdminAddProductViewModel viewModel, PersonalNavigationStore personalNavigationStore)
        {
            _viewModel = viewModel;
            _personalNavigationStore = personalNavigationStore;
        }

        public override bool CanExecute(object parameter)
        {
            try
            {
                if (_viewModel.Name != null && _viewModel.Calories != 0 )
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public override void Execute(object parameter)
        {
            try
            {
                ProductRepository productRepository = new ProductRepository();

                User user = Account.getInstance(null).CurrentUser;

                Product product = new Product(_viewModel.Name,
                    _viewModel.Calories, user.UserId);
                productRepository.Create(product);

                _personalNavigationStore.CurrentPersonalViewModel = new AdminProductViewModel(_personalNavigationStore);
            }
            catch
            {
                MessageBox.Show("Can't add product");
            }
        }
    }
}
