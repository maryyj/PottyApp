using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PottyAppNew.Components
{
    interface IValidationService
    {
        bool validateFirstName(string firstNeme);
        bool validateLastName(string lastName);
        bool validateEmail(string emailAddress);
        bool validatePhone(string phonenumber);
        bool validatePassword(string password1, string password2);
    }
}
