namespace PrintQueue;

public static class QueueHandler
{
    public static string Validate() {
        // Read input.
        var lines = File.ReadAllLines("/Users/mikael/dev/repos/aoc/2024/PrintQueue/test1");
        // Split into two parts?

        // Parse rules.

        // Parse instructions.

        // Remember to collect the middle page numbers of approved print instructions.
        var middlePages = new List<int>();
        
        // Validate instructions.
        
        // End.
        var total = middlePages.Sum();
        return $"Total value of middle page numbers is {total}.";
    }
    
}
