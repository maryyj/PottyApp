using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PottyAppNew.Models;
using PottyAppNew.Helpers;

namespace PottyAppNew.ViewModels
{
    internal partial class MainPageViewModel : ObservableObject
    {
        private static IMongoCollection<Parent> ParentCollection;
        private static IMongoCollection<Child> ChildCollection;
        private Delegates.MyDelegate _myAlertDelegate;
        public MainPageViewModel()
        {
            ParentCollection = DataAccessLayer.GetDbCollection<Parent>("ParentCollection").Result;
            ChildCollection = DataAccessLayer.GetDbCollection<Child>("ChildCollection").Result;
            _myAlertDelegate = Delegates.DisplayAlerts;
        }

        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;

        [RelayCommand]
        public async Task<bool> CheckUserExists()
        {
            //filtrerar och kollar om användarens email existerar
            var filter = Builders<Parent>.Filter.Eq(p => p.Email, Email);
            var parent = await ParentCollection.Find(filter).FirstOrDefaultAsync();
            //filtrerar och kollar att barnets parentid är den samma som dne inloggade användaren
            var childFilter = Builders<Child>.Filter.Eq(c => c.ParentId, parent.Id);
            var child = await ChildCollection.Find(childFilter).FirstOrDefaultAsync();

            if (parent != null && parent.Password == Password)
            {
                // Användaren finns och lösenordet är korrekt
                //Sätter parent i loggedInParent
                App.LoggedInParent = parent;
                //sätter child i App.Child
                App.Child = child;
                return true;
            }
            else
            {
                // Användaren finns inte eller lösenordet är inkorrekt
                _myAlertDelegate("Error", "Emailadressen existerar inte / lösenordet är inkorrekt");
                return false;
            }
        }
    }
}
