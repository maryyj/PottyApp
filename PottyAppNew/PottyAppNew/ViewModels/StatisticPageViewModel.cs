using MongoDB.Driver;
using PottyAppNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Collections;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using PottyAppNew.Helpers;

namespace PottyAppNew.ViewModels
{
    internal partial class StatisticPageViewModel : ObservableObject
    {
        //TODO: Visa barnets poäng

        private static IMongoCollection<Event> EventCollection;

        [ObservableProperty]
        ObservableCollection<Event> events;

        [ObservableProperty]
        DateTime date;

        [ObservableProperty]
        string eventDescription;

        public StatisticPageViewModel()
        {
            Events = new ObservableCollection<Event>();
            EventCollection = DataAccessLayer.GetDbCollection<Event>("EventCollection").Result;
        }

        public async static Task<ObservableCollection<Event>> GetStatistic(Child child, string description)
        {

            var filter = Builders<Event>.Filter.And(
                         Builders<Event>.Filter.Eq(e => e.ChildId, child.Id),
                         Builders<Event>.Filter.Eq(e => e.EventDescription, description));

            var sort = Builders<Event>.Sort.Ascending(e => e.Date);

            var eventList = await EventCollection.Find(filter).Sort(sort).ToListAsync();
            var viewModel = new StatisticPageViewModel();
            viewModel.Events = new ObservableCollection<Event>(eventList);

            return viewModel.Events;
        }
        public async static Task<ObservableCollection<Event>> GetStatisticTwoEvent(Child child, string description1, string description2)
        {

            var filter = Builders<Event>.Filter.And(
                         Builders<Event>.Filter.Eq(e => e.ChildId, child.Id),
                         Builders<Event>.Filter.In(e => e.EventDescription, new[] {description1, description2})
                         );
            var sort = Builders<Event>.Sort.Ascending(e => e.Date);

            var eventList = await EventCollection.Find(filter).Sort(sort).ToListAsync();
            var viewModel = new StatisticPageViewModel();
            viewModel.Events = new ObservableCollection<Event>(eventList);

            return viewModel.Events;
        }
    }
}
