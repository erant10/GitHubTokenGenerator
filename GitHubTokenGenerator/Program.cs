using System;

namespace GitHubTokenGenerator
{
    using CommandLine;
    using GitHubTokenGenerator.CommandLineOptions;
    using GitHubTokenGenerator.Generators;

    class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AppTokenOptions, InstallationTokenOptions>(args)
                .MapResult(
                    (AppTokenOptions options) => GenerateGitHubAppToken(options),
                    (InstallationTokenOptions options) => GenerateGitHubInstallationToken(options),
                    errors => 1
                );
        }

        private static int GenerateGitHubAppToken(AppTokenOptions options)
        {
            string appToken = AppTokenGenerator.GenerateToken(options);
            return ExitWithResult(appToken);
        }
        
        private static int GenerateGitHubInstallationToken(InstallationTokenOptions options)
        {
            string installationToken = new InstallationTokenGenerator().GenerateToken(options).GetAwaiter().GetResult();
            return ExitWithResult(installationToken);
        }
        
        private static int ExitWithResult(string result)
        {
            Console.WriteLine(result);
            return 1;
        }
    }
}
