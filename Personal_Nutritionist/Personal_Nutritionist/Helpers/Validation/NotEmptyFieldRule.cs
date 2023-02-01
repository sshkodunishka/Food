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
    public class NotEmptyFieldRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                return string.IsNullOrWhiteSpace((value ?? "").ToString()) ? new ValidationResult(false, "Field is required and must be not empty")
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
