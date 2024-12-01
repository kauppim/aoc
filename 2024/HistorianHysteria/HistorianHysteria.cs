namespace HistorianHysteria;

public static class Hysteria
{
    public static string Calculate() {
        uint totalDistance = 0;
        uint similarityScore = 0;
        var lines = File.ReadAllLines("/Users/mikael/dev/repos/aoc/2024/HistorianHysteria/testInput");
        var lefties = new List<int>();
        var righties = new List<int>();
        
        foreach (var line in lines)
        {
            var pair = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            lefties.Add(Convert.ToInt32( pair[0] ));
            righties.Add(Convert.ToInt32( pair[1] ));
        }
        lefties.Sort();
        righties.Sort();

        foreach (var (x, y) in lefties.Zip(righties))
        {
            totalDistance += (uint) Math.Abs(x - y);
        }

        

        foreach (var x in lefties)
        {
            var count = righties.Where(el => el == x).Count();
            similarityScore += (uint)(x * count);
            Console.WriteLine(similarityScore);
        }
        
        return $"Total distance: {totalDistance}, Similarity score: {similarityScore}";
    }

}
