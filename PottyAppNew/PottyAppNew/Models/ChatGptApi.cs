using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PottyAppNew.Models
{
    public class ChatGptApi
    {
        internal class CompletionRequest
        {
            [JsonPropertyName("model")]
            public string? Model { get; set; }

            [JsonPropertyName("prompt")]
            public string? Prompt { get; set; }

            [JsonPropertyName("max_tokens")]
            public int? MaxTokens { get; set; }
        }
        internal class CompletionResponse
        {
            [JsonPropertyName("choices")]
            public List<ChatGPTChoice>? Choices { get; set; }

            [JsonPropertyName("usage")]
            public ChatGPTUsage? Usage { get; set; }
        }
        public class ChatGPTUsage
        {
            [JsonPropertyName("prompt_tokens")]
            public int PromptTokens { get; set; }

            [JsonPropertyName("completion_token")]
            public int CompletionTokens { get; set; }

            [JsonPropertyName("total_tokens")]
            public int TotalTokens { get; set; }
        }
        [DebuggerDisplay("Text = {Text}")]
        public class ChatGPTChoice
        {
            [JsonPropertyName("text")]
            public string? Text { get; set; }
        }
    }
}
