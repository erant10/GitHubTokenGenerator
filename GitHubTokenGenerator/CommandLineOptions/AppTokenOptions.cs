namespace GitHubTokenGenerator.CommandLineOptions
{
    using CommandLine;

    [Verb("get-app-token", HelpText = "Generate App Token")]
    public class AppTokenOptions
    {
        [Option(Required = true)]
        public string PrivateKeyPath { get; set; }
        
        [Option(Required = true)]
        public string AppId { get; set; }
    }
}