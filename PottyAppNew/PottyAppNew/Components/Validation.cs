using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PottyAppNew.Components
{
    internal class Validation : IValidation
    {
        public bool validateFirstName(string firstName)
        {
            return Regex.IsMatch(firstName, @"(\b[A-ZÅÄÖ][a-zåäö]+)");
        }

        public bool validateLastName(string lastName)
        {
            return Regex.IsMatch(lastName, @"(\b[A-ZÅÄÖ][a-zåäö]+)");
        }

        public bool validatePhone(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\+46[0-9]{9}$");
        }

        public bool validateEmail(string emailAddress)
        {
            return Regex.IsMatch(emailAddress, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|)(\]?)$");
        }

        public bool validatePassword(string password1, string password2)
        {
            if (!Regex.IsMatch(password1, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s)(?=.*?[#?!@$%^&*-]).{8,64}$"))
            {
                return false;
            }

            if (password1 != password2)
            {
                return false;
            }

            return true;
        }
    }
}
