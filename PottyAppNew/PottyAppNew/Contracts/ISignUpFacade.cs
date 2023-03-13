using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PottyAppNew.Models;

namespace PottyAppNew.Contracts
{
    interface ISignUpFacade
    {
        Parent ValidateAndCreateParent(string firstName, string lastName, string phoneNumber, string email, string password1, string password2);
    }
}
