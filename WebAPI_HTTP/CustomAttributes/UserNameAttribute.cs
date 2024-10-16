using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebAPI_HTTP.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class UserNameAttribute : ValidationAttribute
    {
        private const string RegexValidator = @"^.{3,13}$";

        public string ErrorMessage { get; }
        
        public UserNameAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isUserValid = IsUserValid(value.ToString());

            if (!isUserValid)
            {
                return new ValidationResult(ErrorMessage);
            }

            return null;
        }

        private static bool IsUserValid(string userName)
        {
            return Regex.IsMatch(userName, RegexValidator, RegexOptions.IgnoreCase);
        }


    }
}
