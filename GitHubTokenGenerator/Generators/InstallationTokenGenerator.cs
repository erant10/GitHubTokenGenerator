namespace GitHubTokenGenerator.Generators
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using GitHubTokenGenerator.CommandLineOptions;
    using GitHubTokenGenerator.Utils;

    public class InstallationTokenGenerator
    {
        private readonly HttpClient _httpClient;

        public InstallationTokenGenerator()
        {
            this._httpClient = new HttpClient();
            this._httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubTokenGenerator/1.0");
        }
        
        public async Task<string> GenerateToken(InstallationTokenOptions options)
        {
            AppTokenOptions appTokenOptions = new AppTokenOptions
                {PrivateKeyPath = options.PrivateKeyPath, AppId = options.AppId};
            string appToken = AppTokenGenerator.GenerateToken(appTokenOptions);
            string installationTokenUri = $"https://api.github.com/app/installations/{options.InstallationId}/access_tokens";
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, installationTokenUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", appToken);
  
            using HttpResponseMessage response = await _httpClient.SendAsJsonAsync(request, new StringContent(string.Empty));
            if (response.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                throw new Exception($"GitHub App not installed. installationId: {options.InstallationId} not found.");
            }
            GitHubInstallationTokenResponse tokenResult = await response.ProcessResponseAsync<GitHubInstallationTokenResponse>();

            return tokenResult.token;
        }
        
        
    }
}