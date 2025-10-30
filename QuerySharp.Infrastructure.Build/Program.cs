// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using ADotNet.Clients.Builders;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;

namespace QuerySharp.Infrastructure.Build
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
            string directoryPath = Path.GetDirectoryName(buildScriptPath);

            if (Directory.Exists(directoryPath) is false)
            {
                Directory.CreateDirectory(directoryPath);
            }

            GitHubPipelineBuilder.CreateNewPipeline()
                .SetName("Build & Test QuerySharp")
                .OnPush("main")
                .OnPullRequest("main")

                .AddJob("build", job => job
                    .WithName("Build")
                    .RunsOn(BuildMachines.WindowsLatest)
                    .AddCheckoutStep("Check Out")

                    .AddSetupDotNetStep(
                        version: "9.0.101",
                        includePrerelease: true)

                    .AddRestoreStep()
                    .AddBuildStep())

                .SaveToFile(buildScriptPath);
        }
    }
}