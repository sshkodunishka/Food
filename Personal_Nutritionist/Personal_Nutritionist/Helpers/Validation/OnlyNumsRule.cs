using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Personal_Nutritionist.Helpers.Validation
{
    public class OnlyNumsRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string charString = value as string;
                if (!Regex.Match(charString, "^([1-9][0-9][0-9]?)$").Success)
                {
                    return new ValidationResult(false, $"Field can only contain positive number between 1 and 999");
                }

                return ValidationResult.ValidResult;
            }
            catch
            {
                MessageBox.Show("Can't check entered value");
                return null;
            }
        }
    }
}
