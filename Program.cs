using System.IO;
using CliWrap;
using CliWrap.Buffered;

namespace GitCloneAutomator;

internal static class Program
{
    public static async Task Main()
    {
        await Start();
    }

    private static async Task Start()
    {
        Console.WriteLine("WELCOME TO GITCLONER");

        /// Get directory from user
        String? directory = null;
        {
            InputValidation(ref directory, "Please input the directory you want the repository to be cloned into: ");
        }

        // Get repository link from user
        String? repo = null;
        {
            InputValidation(ref repo, "Please provide the repository link: ");
        }

        // get current directory and change it to user directory

        if (directory != null) Directory.SetCurrentDirectory(directory);

        // run command to clone repository given by into request directory
        var gitResult = await Cli.Wrap("git").WithArguments(new[] { "clone", $"{repo}" }).ExecuteBufferedAsync();

        // print output of git execution

        // standard/success output
        Console.WriteLine(@$"
                    REPONSITORY CLONED SUCCESSFULLY
                    Time Span: {gitResult.RunTime}
                    Output:
                    {gitResult.StandardOutput}
                ");

        Console.ReadLine();
    }

    private static void InputValidation(ref String? str, String message)
    {
        // checks if input string by user is not empty
        // if so ask user to provide the requested info again
        while (str == null || str.Trim().Length == 0)
        {
            Console.Write(message);
            str = Console.ReadLine();
            str = str?.Trim();
            Thread.Sleep(500);
        }
    }
}
