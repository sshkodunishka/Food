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
    public class AdminDeleteProduct : CommandBase
    {
        private readonly AdminProductViewModel _viewModel;

        public AdminDeleteProduct(AdminProductViewModel viewModel)
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
                Context context = new Context();

                ProductRepository productRepository = new ProductRepository();
                MealFoodRepository mealFoodRepository = new MealFoodRepository();
                User user = Account.getInstance(null).CurrentUser;


                var id = _viewModel.SelectedProduct.ProductId;
                _viewModel.Products = new ObservableCollection<Product>(
                    _viewModel.Products.Where(f => f.ProductId != _viewModel.SelectedProduct.ProductId));
                var food = ProductRepository.Get(id);
                var mealFoods = mealFoodRepository.GetProductId(food.ProductId);
                mealFoods.ForEach(meal =>
                {
                    mealFoodRepository.RemoveByProductId(meal.MealFoodId);
                });
                productRepository.Remove(food.ProductId);

            }
            catch
            {
                MessageBox.Show("Can't delete selected product");
            }
        }
    }
}
