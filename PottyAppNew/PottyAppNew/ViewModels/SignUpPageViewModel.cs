using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PottyAppNew.Helpers;
using PottyAppNew.Models;

namespace PottyAppNew.ViewModels
{
    //TODO Refaktorera.
    internal partial class SignUpPageViewModel : ObservableObject
    {
        private static IMongoCollection<Parent> ParentCollection;
        private Delegates.MyDelegate _alertDelegate;
        private Delegates.MyDelegateBool _regexDelegate;
        private Delegates.MyDelegateCollection<Parent> _saveDelegate;

        [ObservableProperty]
        Guid id;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password1;
        [ObservableProperty]
        string password2;
        [ObservableProperty]
        string firstName;
        [ObservableProperty]
        string lastName;
        [ObservableProperty]
        string phoneNumber;
        public SignUpPageViewModel()
        {
            _alertDelegate = Delegates.DisplayAlerts;
            _saveDelegate = Delegates.SaveToDatabase;
            _regexDelegate = Delegates.RegexValidator;
        }
        public async Task<bool> AddParentToDatabase()
        {
            ParentCollection = await DataAccessLayer.GetDbCollection<Parent>("ParentCollection");

            bool firstNameIsValid = _regexDelegate(@"(\b[A-ZÅÄÖ][a-zåäö]+)", FirstName);
            bool lastNameIsValid = _regexDelegate(@"(\b[A-ZÅÄÖ][a-zåäö]+)", LastName);
            bool phoneIsValid = _regexDelegate(@"^\+46[0-9]{9}$", PhoneNumber);
            bool emailIsValid = _regexDelegate(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|)(\]?)$", Email);
            bool passwordIsValid = _regexDelegate(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s)(?=.*?[#?!@$%^&*-]).{8,64}$", Password1);

            var emailfilter = Builders<Parent>.Filter.Eq(p => p.Email, Email);
            var existingParentEmail = await ParentCollection.Find(emailfilter).FirstOrDefaultAsync();

            if (existingParentEmail != null)
            {
                _alertDelegate("Error", "Emailadressen finns redan.");
                return false;
            }

            if (Password1 != Password2)
            {
                _alertDelegate("Error", "Lösenordet matchar inte");
                return false;
            }
            if (firstNameIsValid && lastNameIsValid && phoneIsValid && emailIsValid && passwordIsValid)
            {
                Parent newParent = new Parent()
                {
                    Id = Guid.NewGuid(),
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password1
                };
                await _saveDelegate(ParentCollection, newParent);
                return true;
            }
            else
            {
                _alertDelegate("Error", "Fel inmatning");
                return false;
            }
        }
    }
}