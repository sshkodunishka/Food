using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Personal_Nutritionist.Helpers.Validation
{
    class NotContainSpaceRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                return value.ToString().Contains(" ") ? new ValidationResult(false, "Spaces are not allowed in this field")
                : ValidationResult.ValidResult;
            }
            catch
            {
                MessageBox.Show("Can't check entered value");
                return null;
            }
        }
    }
}
