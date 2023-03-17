using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using CommunityToolkit.Maui.Views;

namespace PottyAppNew.ViewModels
{
    internal class VideoPageViewModel
    {
        //Skickar med MediaElement och ett videoId 
        public static async Task GetVideoAsync(MediaElement mediaElement, string videoId)
        {
            var youtube = new YoutubeClient();
            try //try/catch om videon skulle vara borttagen från youtube
            {

                var video = await youtube.Videos.GetAsync("https://youtube.com/watch?v=" + videoId);

                // Hämtar videon med högsta kvalité
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);

                //Sätter MediaElement Source till videons stream URL
                mediaElement.Source = streamInfo.Url;
            }
            catch (Exception)
            {
                await Console.Out.WriteLineAsync("Videon är inte tillgänglig");
            }
        }
    }
}
