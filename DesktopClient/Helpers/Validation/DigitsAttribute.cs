using System;
using System.ComponentModel.DataAnnotations;

namespace DesktopClient.Helpers.Validation
{
    public class DigitsAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string s = value as string ?? throw new ArgumentException("Value is not string.");
            foreach (char c in s)
                if (!char.IsDigit(c))
                    return false;
            return true;
        }
    }
}
