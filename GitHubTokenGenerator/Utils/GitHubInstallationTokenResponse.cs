namespace GitHubTokenGenerator.Utils
{
    using System.Collections.Generic;

    public class GitHubInstallationTokenResponse
    {
        public string token { get; set; }
        public string expires_at { get; set; }
        public Dictionary<string, string> permissions { get; set; }
        public string repository_selection { get; set; }
    }
}