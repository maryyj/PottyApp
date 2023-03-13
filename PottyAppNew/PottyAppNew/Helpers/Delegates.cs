using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PottyAppNew.Helpers
{
    internal class Delegates
    {
        public delegate void MyDelegate(string alertText, string prompt);
        public delegate bool MyDelegateBool(string pattern, string text);

        public delegate Task MyDelegateCollection<T>(IMongoCollection<T> collection, T newObject);

        public async static void DisplayAlerts(string alertText, string prompt)
        {
            await Application.Current.MainPage.DisplayAlert(alertText, prompt, "OK");
        }
        public static async Task SaveToDatabase<T>(IMongoCollection<T> collection, T newObject)
        {
            await collection.InsertOneAsync(newObject);
        }
        public static bool RegexValidator(string pattern, string text)
        {
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(text);

            if (matches.Any())
            {
                foreach (Match match in matches)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
