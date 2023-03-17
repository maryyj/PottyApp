using CommunityToolkit.Mvvm.ComponentModel;
using MongoDB.Driver;
using PottyAppNew.ApplicationFacade;
using PottyAppNew.Helpers;
using PottyAppNew.Models;
using PottyAppNew.Contracts;

namespace PottyAppNew.ViewModels
{
    internal partial class SignUpPageViewModel : ObservableObject
    {
        readonly ISignUpFacade _signUpFacade = new SignUpFacade();
        private static IMongoCollection<Parent> ParentCollection;
        private readonly Delegates.MyDelegate _alertDelegate;
        private readonly Delegates.MyDelegateCollection<Parent> _saveDelegate;

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
            ParentCollection = DataAccessLayer.GetDbCollection<Parent>("ParentCollection").Result;
        }
        public async Task<bool> AddParentToDatabase()
        {
            //Kollar om den inmatade emailadressen redan finns i databasen.
            var emailfilter = Builders<Parent>.Filter.Eq(p => p.Email, Email);
            var existingParent = await ParentCollection.Find(emailfilter).FirstOrDefaultAsync();

            if (existingParent != null)
            {
                _alertDelegate("Felmeddelande", "Emailadressen finns redan.");
                return false;
            }

            if (FirstName != null || LastName != null || PhoneNumber != null || Email != null || Password1 != null || Password2 != null)
            {
                Parent parentObject = _signUpFacade.ValidateAndCreateParent(FirstName, LastName, PhoneNumber, Email, Password1, Password2);
                if (parentObject != null)
                {
                    await _saveDelegate(ParentCollection, parentObject);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}