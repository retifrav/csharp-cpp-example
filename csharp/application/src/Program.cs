using System;
using System.CommandLine;
using System.Text;
using System.IO;
using Lbr;

namespace Applctn;

class Program
{
    static int Main(string[] args)
    {
        Option<string> someOption = new("--something", "-s")
        {
            Description = "Some thing to pass to the application"
        };

        Option<FileInfo> fileOption = new("--json", "-j")
        {
            Description = "JSON file with the grils",
            Required = true
        };

        RootCommand rootCommand = new("applctn")
        {
            Description = "Just some application"
        };
        rootCommand.Options.Add(someOption);
        rootCommand.Options.Add(fileOption);

        var parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count != 0)
        {
            foreach (var parseError in parseResult.Errors)
            {
                Console.Error.WriteLine(parseError.Message);
            }

            return 1;
        }

        var smOpt = parseResult.GetValue(someOption);
        if (!string.IsNullOrEmpty(smOpt))
        {
            Console.WriteLine($"Something that was passed as CLI argument: {smOpt}");
        }

        //if (parseResult.GetValue(fileOption) is FileInfo grilsJSON)
        var grilsJSON = parseResult.GetValue(fileOption);
        if (!grilsJSON.Exists)
        {
            Console.WriteLine(
                $"[ERROR] Provided JSON file with grils [{grilsJSON.FullName}] does not exist"
            );
            return 2;
        }
        Console.WriteLine($"JSON file with grils: {grilsJSON}");

        Console.WriteLine();

        Console.WriteLine($"Something from C# library | {Some.getSome()}");

        string jsonFileContents = string.Empty;
        using (var streamReader = new StreamReader(grilsJSON.FullName, Encoding.UTF8))
        {
            jsonFileContents = streamReader.ReadToEnd();

        }

        // --- P/Invoke with C API wrapper

        Console.WriteLine();

        Console.WriteLine($"Something from C++ library through C# library via P/Invoke | {Some.DoThingyC()}");
        Console.WriteLine(
            $"Best gril, objectively:{Environment.NewLine}{Some.WhoHasTheBestBoobsC(jsonFileContents)}"
        );

        // --- CLI/C++ CLR wrapper

        Console.WriteLine();

        Console.WriteLine($"Something from C++ library through C# library via CLI/C++ CLR | {Some.DoThingyCLR()}");
        Console.WriteLine(
            $"Best gril, objectively:{Environment.NewLine}{Some.WhoHasTheBestBoobsCLR(jsonFileContents)}"
        );

        return 0;
    }
}
