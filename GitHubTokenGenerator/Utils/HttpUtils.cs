namespace GitHubTokenGenerator.Utils
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading.Tasks;

    public static class HttpUtils
    {
        public static async Task<string> ReadContentAsync(this HttpResponseMessage response)
        {
            if (response.Content != null)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
        
        public static async Task<HttpResponseMessage> SendAsJsonAsync(this HttpClient client, HttpRequestMessage request, StringContent content)
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;    

            return await client.SendAsync(request);
        }
        
        public static async Task<T> ProcessResponseAsync<T>(this HttpResponseMessage response)
        {
            string content = await response.ReadContentAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Request failed with status code {response.StatusCode.ToString()} and content: {content}");    
            }
            return Deserialize<T>(content);
        }
        
        public static T Deserialize<T>(string content)
        {
            if (String.IsNullOrEmpty(content))
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(content);
        }
    }
}