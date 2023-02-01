using Personal_Nutritionist.Command;
using Personal_Nutritionist.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Personal_Nutritionist.ViewModels
{
    public class AdminAddProductViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _calories;
        public int Calories
        {
            get => _calories;
            set
            {
                _calories = value;
                OnPropertyChanged(nameof(Calories));
            }
        }



        public ICommand AddProduct { get; }

        public AdminAddProductViewModel(PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                AddProduct = new AdminAddProduct(this, personalNavigationStore);
            }
            catch
            {
                MessageBox.Show("Can't add product");
            }
        }
    }
}
