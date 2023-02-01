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
    public class PasswordRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string charString = value as string;
                if (!Regex.Match(charString, @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9@#!^&*.]{4,})$").Success)
                {
                    return new ValidationResult(false, $"Password must contains only english letter and at least one digit and one letter.(can contain @#!^&*.)");
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
