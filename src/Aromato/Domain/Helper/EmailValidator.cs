using System;
using System.Text.RegularExpressions;

namespace Aromato.Domain.Helper
{
    public class EmailValidator
    {
        private const string ValidEmailPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                         @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public static bool IsValidEmail(string email)
        {
            try
            {
                var regex = new Regex(ValidEmailPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException ex)
            {
                return false;
            }
        }
    }
}