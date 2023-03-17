using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using PottyAppNew.Helpers;
using PottyAppNew.Models;
using PottyAppNew.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PottyAppNew.ViewModels
{
    public partial class AddChildPageViewModel : ObservableObject
    {
        private static IMongoCollection<Child> ChildCollection;
        private readonly Delegates.MyDelegateBool _regexDelegate;
        private readonly Delegates.MyDelegateCollection<Child> _saveDelegate;
        public ObservableCollection<Child> ChildList { get; set; }

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
            _regexDelegate = Delegates.RegexValidator;
            ChildList = new ObservableCollection<Child>();
            ChildCollection = DataAccessLayer.GetDbCollection<Child>("ChildCollection").Result;
        }

        [RelayCommand]
        public async void DeleteChild(object c)
        {
            //tar bort en produkt i databasen
            var child = (Child)c;
            await ChildCollection.DeleteOneAsync(x => x.Id == child.Id);
            ChildList.Remove(child);
            if (ChildList.Count == 0)
            {
                App.Child = null;
            }
        }
        public async Task<bool> AddChildToDatabase()
        {
            Child newChild = ValidateAndCreateChild();

            if (newChild != null)
            {
                await _saveDelegate(ChildCollection, newChild);
                App.Child = newChild;
                ChildList.Add(newChild);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Child ValidateAndCreateChild()
        {
            if (Name != null)
            {
                bool nameIsValid = _regexDelegate(@"\b[A-ZÅÄÖ][a-zåäö]+", Name);
                bool ageIsValid = _regexDelegate(@"(^[0-5]{1}$)", Age.ToString());

                //Om valideringen är falsk, returnera null annars en child objekt
                if (!nameIsValid || !ageIsValid)
                {
                    return null;
                }
                else
                {
                    return new Child
                    {
                        Id = ObjectId.GenerateNewId(),
                        Name = Name,
                        Age = Age,
                        Points = 0,
                        ParentId = App.LoggedInParent.Id
                    };
                }
            }
            else
            { 
                return null;
            }
        }
        public async void GetChildren()
        {
            var children = await ChildCollection.Find(c => c.ParentId == App.LoggedInParent.Id).ToListAsync();
            foreach (var child in children)
            {
                ChildList.Add(child);
            }
        }
    }
}
