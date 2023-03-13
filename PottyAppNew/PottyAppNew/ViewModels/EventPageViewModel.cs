using PottyAppNew.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using PottyAppNew.Helpers;
using System.Collections.ObjectModel;

namespace PottyAppNew.ViewModels
{
    public partial class EventPageViewModel : ObservableObject
    {
        private static IMongoCollection<Event> EventCollection;
        private static IMongoCollection<Child> ChildCollection;
        private Delegates.MyDelegate _myAlertDelegate;
        private Delegates.MyDelegateCollection<Event> _delCollection;

        [ObservableProperty]
        DateTime date;
        [ObservableProperty]
        string eventDescription;
        [ObservableProperty]
        ObjectId childId;
        [ObservableProperty]
        Guid parentId;
        public EventPageViewModel()
        {
            _myAlertDelegate = Delegates.DisplayAlerts;
            _delCollection = Delegates.SaveToDatabase;
            EventCollection = DataAccessLayer.GetDbCollection<Event>("EventCollection").Result;
            ChildCollection = DataAccessLayer.GetDbCollection<Child>("ChildCollection").Result;
        }
        public async Task<bool> AddEventToDatabase(DateTime date, string EventDescription, Child child)
        {

            //EventCollection = await Helpers.DataAccessLayer.GetDbCollection<Event>("EventCollection");
            //ChildCollection = await Helpers.DataAccessLayer.GetDbCollection<Child>("ChildCollection");

            if (child != null)
            {

                Event newEvent = new Event
                {
                    Date = date,
                    EventDescription = EventDescription,
                    ChildId = child.Id,
                    ParentId = App.LoggedInParent.Id
                };

                int pointsToAdd = 0;

                if (EventDescription == "Bajsat i pottan" || EventDescription == "Kissat i pottan")
                {
                    pointsToAdd = 1;
                    var filter = Builders<Child>.Filter.Eq(c => c.Id, child.Id); //Kollar att barnets Id i DB är samma som Appens childId
                    var update = Builders<Child>.Update.Inc(c => c.Points, pointsToAdd); //Skapar en uppdatering barnets poäng med +1 poäng
                    await ChildCollection.UpdateOneAsync(filter, update); //uppdaterar databasen.
                    App.Child.Points = pointsToAdd;
                }
                await _delCollection(EventCollection, newEvent);
                return true;
            }
            else
            {
                _myAlertDelegate("Error", "Barnet existerar inte i databasen.");
                return false;
            }
        }
    }
}
