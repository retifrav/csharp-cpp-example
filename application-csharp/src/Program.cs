using System;
using System.CommandLine;
using System.Text;

namespace applctn;

class Program
{
    static int Main(string[] args)
    {
        Option<string> someOption = new("--something", "-s")
        {
            Description = "Some thing to pass to the application"
        };

        RootCommand rootCommand = new("applctn")
        {
            Description = "Just some application"
        };
        rootCommand.Options.Add(someOption);

        var parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count == 0)
        {
            var smOpt = parseResult.GetValue(someOption);
            if (!string.IsNullOrEmpty(smOpt))
            {
                Console.WriteLine($"Something that was passed: {smOpt}");
            }

            return 0;
        }
        else
        {
            foreach (var parseError in parseResult.Errors)
            {
                Console.Error.WriteLine(parseError.Message);
            }

            return 1;
        }
    }
}
