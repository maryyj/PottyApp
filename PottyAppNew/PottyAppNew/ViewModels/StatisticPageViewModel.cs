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
        //TODO: Visa barnets poäng,
        //Statistik för att gå på potta, torr hela natten, olycka
        //Torr hela natten >3 ggr i rad, tips om att prova utan blöja.


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
        }

        public async static Task<ObservableCollection<Event>> GetStatistic(Child child)
        {
            EventCollection = await DataAccessLayer.GetDbCollection<Event>("EventCollection");

            var filter = Builders<Event>.Filter.And(
                         Builders<Event>.Filter.Eq(e => e.ChildId, child.Id),
                         Builders<Event>.Filter.In(e => e.EventDescription, new[] { "Bajsat i pottan", "Kissat i pottan" })
                         );
            var sort = Builders<Event>.Sort.Ascending(e => e.Date);

            var eventList = await EventCollection.Find(filter).Sort(sort).ToListAsync();
            var viewModel = new StatisticPageViewModel();
            viewModel.Events = new ObservableCollection<Event>(eventList);

            return viewModel.Events;
        }
    }
}
