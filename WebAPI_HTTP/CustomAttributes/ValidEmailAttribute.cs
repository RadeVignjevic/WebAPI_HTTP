using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace WebAPI_HTTP.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class ValidEmailAttribute : ValidationAttribute
    {
        private const string RegexPattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*" +
                                            @"@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        private const string RegexPatternForSpesificDomain = @"^[A-Za-z0-9._%+-]+@gmail\.com$";

        public ValidEmailAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isEmailValid = IsValidEmail(value.ToString());

            bool spesificDomainvalidation = Regex.IsMatch(value.ToString(), RegexPatternForSpesificDomain, RegexOptions.IgnoreCase);

            if (!isEmailValid || !spesificDomainvalidation)
            {
                string validationMessage = isEmailValid ? "" : "Email e od nevalidna struktura";
                ErrorMessage = spesificDomainvalidation ? validationMessage : "Domainot ne e ni e tocen";

                return new ValidationResult(ErrorMessage);
            }

            return null;
        }

        private static bool IsValidEmail(string emailAddress)
        {
            return Regex.IsMatch(emailAddress, RegexPattern, RegexOptions.IgnoreCase);
        }
    }
}
