using HistorianHysteria;
using RedNosedReports;
using MullItOver;

internal class Program
{
    private static int Main(string[] args)
    {
        Console.WriteLine("============AdventOfCsharp============");
        Console.WriteLine("The following subroutines may be run:");
        Console.WriteLine(" 1 - Historian Hysteria");
        Console.WriteLine(" 2a - Red Nosed Reports");
        Console.WriteLine(" 2b - Red Nosed Reports: Problem dampener");
        Console.WriteLine(" 3a - Mull It Over");
        Console.WriteLine(" 3b - Mull It Over: Do or Don't");
        Console.WriteLine("======================================");
        Console.WriteLine("Please select a subroutine:");

        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine(Hysteria.Calculate());
            break;
            case "2a":
                Console.WriteLine(Reports.CalculateSafeReports());
            break;
            case "2b":
                Console.WriteLine(Reports.CalculateSafeReportsDampener());
            break;
            case "3a":
                Console.WriteLine(ParseMemory.Parse());
            break;
            case "3b":
                Console.WriteLine(ParseMemory.ParseV2());
            break;
            default:
                Console.WriteLine("Good bye!");
            break;
        }

        return 0;
    }
}