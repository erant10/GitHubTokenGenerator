namespace GitHubTokenGenerator.CommandLineOptions
{
    using CommandLine;

    [Verb("get-installation-token", HelpText = "Generate Installation Token")]
    public class InstallationTokenOptions
    {
        [Option(Required = true)]
        public string PrivateKeyPath { get; set; }
        
        [Option(Required = true)]
        public string AppId { get; set; }
        
        [Option(Required = true)]
        public string InstallationId { get; set; }
    }

}