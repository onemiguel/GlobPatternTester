using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using CommandLine;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;


namespace GlobPatternTester
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitGlobString(this string str, string separators = ";")
        {
            var separator = separators.ToCharArray();
            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
    class Program
    {
        
        public class Options
        {
            [Option('i', "include", Required = true, HelpText = "Include Glob patterns.  Use ; for multiple.")]
            public string IncludePattern { get; set; }

            [Option('e', "exclude", Required = true, HelpText = "Exclude Glob patterns.  Use ; for multiple.")]
            public string ExcludePattern { get; set; }

            [Option('d', "targetDir", Required = true, HelpText = "Target Directory")]
            public string TargetDirectory { get; set; }
        }

        static bool ValidateArguments(string[] args, out Matcher matcherResult, out DirectoryInfoWrapper targetDirectory)
        {
            var matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
            string targetDir = Directory.GetCurrentDirectory();
            bool validationResult = false;
            var parserResult = Parser.Default.ParseArguments<Options>(args)
                .WithParsed((o) =>
                {
                    if (false == string.IsNullOrWhiteSpace(o.IncludePattern))
                    {
                        matcher.AddIncludePatterns(o.IncludePattern.SplitGlobString());
                    }

                    if (false == string.IsNullOrWhiteSpace(o.ExcludePattern))
                    {
                        matcher.AddExcludePatterns(o.ExcludePattern.SplitGlobString());
                    }

                    targetDir = o.TargetDirectory;
                    validationResult = true;
                });
            matcherResult = matcher;
            targetDirectory = new DirectoryInfoWrapper(new DirectoryInfo(targetDir));
            return validationResult;
        }
        static void Main(string[] args)
        {
            if (false == ValidateArguments(args, out Matcher matcher, out DirectoryInfoWrapper targetDir))
            {
                return;
            }
            
            try
            {
                var results = matcher.Execute(targetDir);
                if (results.HasMatches)
                {
                    foreach (var result in results.Files)
                    {
                        Console.WriteLine(result.Path);
                    }
                }
                else
                {
                    Console.WriteLine("No matches.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return;
            }

        }
    }
}
