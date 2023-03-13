using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PottyAppNew.Models;
using System.Text.Json;
using MongoDB.Bson;
using Newtonsoft.Json;
using static PottyAppNew.Models.ChatGptApi;

namespace PottyAppNew.ViewModels
{
    internal class DadJokesViewModel
    {
        private static string baseAddress = "https://api.api-ninjas.com";
        private static readonly string apiKeyNinja = "API-KEY";
        private static readonly string apiKeyChatGPT = "API-KEY";


        public static async Task<List<DadJoke>> GetJokesAsync(string uri)
        {
            HttpClient client = new HttpClient();
            List<DadJoke> jokes = null;
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Add("X-Api-Key", apiKeyNinja);
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    jokes = JsonConvert.DeserializeObject<List<DadJoke>>(responseString);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            return jokes;
        }
        public static async Task<string> ChatTranslate(string question)
        {
            string answer = "Inget svar";

            CompletionRequest request = new CompletionRequest()  //skapat completionrequest objekt som skickas till chatGPT
            {
                Model = "text-davinci-003",
                Prompt = "Översätt texten till svenska: " + question,
                MaxTokens = 120
            };

            HttpClient client = new HttpClient();
            var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/completions");

            httpReq.Headers.Add("Authorization", "Bearer " + apiKeyChatGPT);


            string requestString = System.Text.Json.JsonSerializer.Serialize(request);
            httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.SendAsync(httpReq);
                var completionResponse = new CompletionResponse();

                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //Läser strängen från responsen.
                        string responseString = await response.Content.ReadAsStringAsync();
                        // Gör om det till ett C#-objekt(CompletionResponse)
                        completionResponse = System.Text.Json.JsonSerializer.Deserialize<CompletionResponse>(responseString);
                    }
                }
                if (completionResponse != null)
                {
                    answer = completionResponse.Choices[0].Text;
                }
                else
                {
                    await Console.Out.WriteLineAsync("Inget svar");
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Gick inte att översätta");
            }

            return answer;

        }
    }
}
