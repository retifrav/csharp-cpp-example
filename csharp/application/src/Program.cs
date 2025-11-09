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
        // if you are going to print UTF-8 string to console
        //Console.OutputEncoding = Encoding.UTF8;

        Option<string> someOption = new("--something", "-s")
        {
            Description = "Some thing to pass to the application"
        };

        Option<FileInfo> fileOption = new("--json", "-j")
        {
            Description = "JSON file with the grils",
            Required = true
        };

        Option<int> yearOption = new("--year", "-y")
        {
            Description = "Year of birth for the grils"
        };

        RootCommand rootCommand = new("applctn")
        {
            Description = "Just some application"
        };
        rootCommand.Options.Add(someOption);
        rootCommand.Options.Add(fileOption);
        rootCommand.Options.Add(yearOption);

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

        int grilsYear = parseResult.GetValue(yearOption);

        Console.WriteLine();

        Console.WriteLine($"Something from C# library | {Some.getSome()}");

        string jsonFileContents = string.Empty;
        using (var streamReader = new StreamReader(grilsJSON.FullName, Encoding.UTF8))
        {
            jsonFileContents = streamReader.ReadToEnd();

        }

        // --- P/Invoke with C API wrapper

        Console.WriteLine();

        string resultFromPInvoke1 = Some.DoThingyC();
        string resultFromPInvoke2 = Some.WhoHasTheBestBoobsC(jsonFileContents, grilsYear);

        // if you want to make sure that your UTF-8 strings are okay and it's just console/terminal output who is fucked
        /*
        using(StreamWriter sw = new StreamWriter("tmp-results-pinvoke.txt"))
        {
            sw.WriteLine(resultFromPInvoke1);
            sw.WriteLine(resultFromPInvoke2);
        }
        */

        Console.WriteLine($"Something from C++ library through C# library via P/Invoke | {resultFromPInvoke1}");
        Console.WriteLine($"Best gril(s), objectively: {resultFromPInvoke2}");

        // --- CLI/C++ CLR wrapper

#if OS_WINDOWS
        Console.WriteLine();

        string resultFromCppCli1 = Some.DoThingyCLR();
        string resultFromCppCli2 = Some.WhoHasTheBestBoobsCLR(jsonFileContents, grilsYear);

        // if you want to make sure that your UTF-8 strings are okay and it's just console/terminal output who is fucked
        /*
        using(StreamWriter sw = new StreamWriter("tmp-results-cppcli.txt"))
        {
            sw.WriteLine(resultFromCppCli1);
            sw.WriteLine(resultFromCppCli2);
        }
        */

        Console.WriteLine($"Something from C++ library through C# library via CLI/C++ CLR | {resultFromCppCli1}");
        Console.WriteLine($"Best gril(s), objectively: {resultFromCppCli2}");
#endif

        return 0;
    }
}
