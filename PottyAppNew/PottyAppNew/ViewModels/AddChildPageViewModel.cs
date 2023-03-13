using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using PottyAppNew.Helpers;
using PottyAppNew.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PottyAppNew.ViewModels
{
    //TODO: Möjlighet att ha flera barn.
    internal partial class AddChildPageViewModel : ObservableObject
    {
        private static IMongoCollection<Child> ChildCollection;
        private Delegates.MyDelegate _alertDelegate;
        private Delegates.MyDelegateBool _regexDelegate;
        private Delegates.MyDelegateCollection<Child> _saveDelegate;

        [ObservableProperty]
        ObservableCollection<Child> children;
        [ObservableProperty]
        ObjectId id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        int age;
        [ObservableProperty]
        int points;
        [ObservableProperty]
        Guid parentId;

        public AddChildPageViewModel()
        {
            _saveDelegate = Delegates.SaveToDatabase;
            _alertDelegate = Delegates.DisplayAlerts;
            _regexDelegate = Delegates.RegexValidator;
            Children = new ObservableCollection<Child>();
            ChildCollection = DataAccessLayer.GetDbCollection<Child>("ChildCollection").Result;
        }

        [RelayCommand]
        public async void DeleteChild(object c)
        {
            //tar bort en produkt i databasen
            var child = (Child)c;
            await ChildCollection.DeleteOneAsync(x => x.Id == child.Id);
            Children.Remove(child);
        }
        public async Task<bool> AddChildToDatabase()
        {
            bool nameIsValid = _regexDelegate(@"\b[A-ZÅÄÖ][a-zåäö]+", Name);
            bool ageIsValid = _regexDelegate(@"(^[0-5]{1}$)", Age.ToString());


            if (nameIsValid && ageIsValid)
            {
                Child newChild = new Child()
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = Name,
                    Age = Age,
                    Points = 0,
                    ParentId = App.LoggedInParent.Id
                };

                await _saveDelegate(ChildCollection, newChild);
                App.Child = newChild;
                Children.Add(newChild);
                return true;
            }
            else
            {
                _alertDelegate("Error", "Fel inmatning. Ditt barn sparades inte, försök igen.");
                return false;
            }
        }
        internal async void GetChildren()
        {
            Children = new ObservableCollection<Child>(await ChildCollection
                       .Find(c => c.ParentId == App.LoggedInParent.Id)
                       .ToListAsync());
        }
    }
}
