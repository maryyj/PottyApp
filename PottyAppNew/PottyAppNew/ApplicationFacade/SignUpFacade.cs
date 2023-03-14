using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PottyAppNew.Components;
using PottyAppNew.Contracts;
using PottyAppNew.Models;

namespace PottyAppNew.ApplicationFacade
{
    class SignUpFacade : ISignUpFacade
    {
        private static readonly IValidation _validation = new Validation();

        public Parent ValidateAndCreateParent(string firstName, string lastName, string phoneNumber, string email, string password1, string password2)
        {
            //Om valideringen är falsk, returnera null annars en parent objekt
            if (!_validation.validateFirstName(firstName) || !_validation.validateLastName(lastName) || !_validation.validatePhone(phoneNumber) || !_validation.validateEmail(email) || !_validation.validatePassword(password1, password2))
            {
                return null;
            }
            else
            {
                return new Parent
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Password = password1
                };
            }
        }
    }
}
