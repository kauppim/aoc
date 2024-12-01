using HistorianHysteria;

internal class Program
{
    private static int Main(string[] args)
    {
        Console.WriteLine("============AdventOfCsharp============");
        Console.WriteLine("The following subroutines may be run:");
        Console.WriteLine(" 1 - Historian Hysteria");
        Console.WriteLine("=====================================");
        Console.WriteLine("Please select a subroutine:");

        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine(Hysteria.Calculate());
            break;
            default:
                Console.WriteLine("Good bye!");
            break;
        }

        return 0;
    }
}