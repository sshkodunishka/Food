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
    public class OnlyLettersRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string charString = value as string;
                if (!Regex.Match(charString, "^[a-zA-ZА-Яа-я][a-zA-ZА-Яа-я]*$").Success)
                {
                    return new ValidationResult(false, $"Field can only contain letters");
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
