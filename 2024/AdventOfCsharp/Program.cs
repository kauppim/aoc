using HistorianHysteria;
using RedNosedReports;

internal class Program
{
    private static int Main(string[] args)
    {
        Console.WriteLine("============AdventOfCsharp============");
        Console.WriteLine("The following subroutines may be run:");
        Console.WriteLine(" 1 - Historian Hysteria");
        Console.WriteLine(" 2a - Red Nosed Reports");
        Console.WriteLine(" 2b - Red Nosed Reports: Problem dampener");
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
            default:
                Console.WriteLine("Good bye!");
            break;
        }

        return 0;
    }
}