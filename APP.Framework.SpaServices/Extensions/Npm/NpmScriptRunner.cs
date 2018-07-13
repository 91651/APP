using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using APP.Framework.SpaServices.NodeServices.Util;
using Microsoft.Extensions.Logging;

namespace APP.Framework.SpaServices.NodeServices.Npm
{
    internal class NpmScriptRunner
    {
        public EventedStreamReader StdOut { get; }
        public EventedStreamReader StdErr { get; }

        private static Regex AnsiColorRegex = new Regex("\x001b\\[[0-9;]*m", RegexOptions.None, TimeSpan.FromSeconds(1));

        public NpmScriptRunner(string workingDirectory, string scriptName, string arguments, IDictionary<string, string> envVars)
        {
            if (string.IsNullOrEmpty(workingDirectory))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(workingDirectory));
            }

            if (string.IsNullOrEmpty(scriptName))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(scriptName));
            }

            var npmExe = "npm";
            var completeArguments = $"run {scriptName} -- {arguments ?? string.Empty}";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                npmExe = "cmd";
                completeArguments = $"/c npm {completeArguments}";
            }

            var processStartInfo = new ProcessStartInfo(npmExe)
            {
                Arguments = completeArguments,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = workingDirectory
            };

            if (envVars != null)
            {
                foreach (var keyValuePair in envVars)
                {
                    processStartInfo.Environment[keyValuePair.Key] = keyValuePair.Value;
                }
            }

            var process = LaunchNodeProcess(processStartInfo);
            StdOut = new EventedStreamReader(process.StandardOutput);
            StdErr = new EventedStreamReader(process.StandardError);
        }

        public void AttachToLogger(ILogger logger)
        {
            StdOut.OnReceivedLine += line =>
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    logger.LogInformation(StripAnsiColors(line));
                }
            };

            StdErr.OnReceivedLine += line =>
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    logger.LogError(StripAnsiColors(line));
                }
            };

            StdErr.OnReceivedChunk += chunk =>
            {
                var containsNewline = Array.IndexOf(
                    chunk.Array, '\n', chunk.Offset, chunk.Count) >= 0;
                if (!containsNewline)
                {
                    Console.Write(chunk.Array, chunk.Offset, chunk.Count);
                }
            };
        }

        private static string StripAnsiColors(string line)
            => AnsiColorRegex.Replace(line, string.Empty);

        private static Process LaunchNodeProcess(ProcessStartInfo startInfo)
        {
            try
            {
                var process = Process.Start(startInfo);
                process.EnableRaisingEvents = true;

                return process;
            }
            catch (Exception ex)
            {
                var message = $"Failed to start 'npm'. To resolve this:.\n\n"
                            + "[1] Ensure that 'npm' is installed and can be found in one of the PATH directories.\n"
                            + $"    Current PATH enviroment variable is: { Environment.GetEnvironmentVariable("PATH") }\n"
                            + "    Make sure the executable is in one of those directories, or update your PATH.\n\n"
                            + "[2] See the InnerException for further details of the cause.";
                throw new InvalidOperationException(message, ex);
            }
        }
    }
}
