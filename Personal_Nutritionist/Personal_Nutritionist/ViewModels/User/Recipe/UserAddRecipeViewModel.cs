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
    public class UserAddRecipeViewModel : ViewModelBase
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

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
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

        public ICommand UserAddRecipe { get; }

        public UserAddRecipeViewModel(PersonalNavigationStore personalNavigationStore)
        {
            try
            {
                UserAddRecipe = new UserAddRecipe(this, personalNavigationStore);
            }
            catch
            {
                MessageBox.Show("Can't add recipe");
            }
        }

    }
}
