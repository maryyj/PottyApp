using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PottyAppNew.Models
{
    public class DadJoke
    {
        [JsonPropertyName("joke")]
        public string Joke { get; set; }
    }
}
