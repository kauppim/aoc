namespace WordSearch;

public class WordSearcher
{
    private List<string> searchItems;
    private char[,] sourceArray;

    public WordSearcher(List<string> searchItems, string sourcePath)
    {
        this.searchItems = searchItems;
        // Parse source into array
        sourceArray = ParseInput(sourcePath);
    }

    public void PerformSearch() {
        Console.WriteLine("Search init...");
        foreach (var item in searchItems)
        {
            Console.WriteLine($"Search for: {item}");
            var result = Search(item);
            Console.WriteLine($"Found {result.Count} appearances of search item.");
        }
        Console.WriteLine("Search Finished!");
    }
    
    // return list of coordinates where search item is
    private List<List<Tuple<int, int>>> Search(string searchWord) {
        var result = new List<List<Tuple<int, int>>>();
        var length = searchWord.Length;
        // Get all coordinates where first char is
        var firstLetterCoordinates = ScanFirstLetter(searchWord[0]);
        // Console.WriteLine(firstLetterCoordinates.Count);
        // Console.WriteLine("Coordinates to search:");

        // Construct list of possible coordinates to validate
        var listOfCoordinates = ConstructCoordinates(firstLetterCoordinates, length);
        // listOfCoordinates.ForEach(
        //     y => y.ForEach(
        //         x => Console.WriteLine($"({x.Item1}, {x.Item2}), ")
        //     )
        // );
        // Console.WriteLine("=======");


        // Validate if n-th coordinate pair equals n-th element of string
        foreach (var coordinates in listOfCoordinates)
        {
            // coordinates.ForEach(x => Console.WriteLine($"{x.Item1}, {x.Item2}"));
            var isMatch = true;
            for (int i = 0; i < searchWord.Length; i++)
            {
                var coordinate = coordinates[i];
                if (sourceArray[coordinate.Item1, coordinate.Item2] != searchWord[i]) {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch == true) {
                result.Add(coordinates);
            }
        }
        
        return result;
    }

    private static char[,] ParseInput(string sourcePath) {
        var input = File.ReadAllLines(sourcePath);
        var n = input.Length;
        var m = input[0].Length;
        var result = new char[n,m];
        for (int i = 0; i < n; i++)
        {
            var line = input[i].ToCharArray();
            for (int j = 0; j < m; j++)
            {
                result[i,j] = line[j];
            }
        }
        return result;
    }

    private List<List<Tuple<int, int>>> ConstructCoordinates(List<Tuple<int, int>> coordinates, int length) {
        var result = new List<List<Tuple<int, int>>>();
        var height = sourceArray.GetLength(0);
        var width = sourceArray.GetLength(1);


        foreach (var coordinate in coordinates)
        {
            var left = new List<Tuple<int, int>>();
            var right = new List<Tuple<int, int>>();
            var up = new List<Tuple<int, int>>();
            var down = new List<Tuple<int, int>>();
            
            // Left
            if (coordinate.Item2 - (length - 1) >= 0)
            {
                left = GenerateCoordinateSet(coordinate.Item1, -1, coordinate.Item2, coordinate.Item2 - length);
                
                result.Add(left);
            }
            // Right
            if (coordinate.Item2 + (length - 1) < width)
            {
                right = GenerateCoordinateSet(coordinate.Item1, -1, coordinate.Item2, coordinate.Item2 + length);
                result.Add(right);
            }
            // Up
            if (coordinate.Item1 - (length - 1) >= 0)
            {
                up = GenerateCoordinateSet(-1, coordinate.Item2, coordinate.Item1, coordinate.Item1 - length);
                result.Add(up);
            }
            // Down
            if (coordinate.Item1 + (length - 1 )< height)
            {
                down = GenerateCoordinateSet(-1, coordinate.Item2, coordinate.Item1, coordinate.Item1 + length);
                result.Add(down);
            }
            // All horizontal ways with zipping the previous? 
            // https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-9.0
            // L-U
            if (left.Count > 0 && up.Count > 0) {
                result.Add(
                    left.Zip(up, (l, u) => Tuple.Create(u.Item1, l.Item2)).ToList()
                );
            }

            // L-D
            if (left.Count > 0 && down.Count > 0) {
                result.Add(
                    left.Zip(down, (l, d) => Tuple.Create(d.Item1, l.Item2)).ToList()
                );
            }

            // R-U
            if (right.Count > 0 && up.Count > 0) {
                result.Add(
                    right.Zip(up, (r, u) => Tuple.Create(u.Item1, r.Item2)).ToList()
                );
            }

            // R-D
            if (right.Count > 0 && down.Count > 0) {
                result.Add(
                    right.Zip(down, (r, d) => Tuple.Create(d.Item1, r.Item2)).ToList()
                );
            }
        }
        return result;
    }

    private static List<Tuple<int, int>> GenerateCoordinateSet(int row, int col, int start, int end) {
        var temp = new List<Tuple<int, int>>();
        var range = CreateRange(start, end);
        // Console.WriteLine("Range created:");
        // range.ForEach(Console.WriteLine);

        if (col == -1)
        {
            range.ForEach(x => temp.Add(Tuple.Create(row, x)));
        }
        else
        {
            range.ForEach(x => temp.Add(Tuple.Create(x, col)));
        }
        return temp;
    }

    private static List<int> CreateRange(int start, int end) {
        var result = new List<int>();
        // Console.WriteLine($"Start: {start}, end: {end}");
        
        if (start > end) {
            // Console.WriteLine("Create reverse range");
            for (int i = start; i > end; i--)
            {
                result.Add(i);
            }
        }
        else
        {
            for (int i = start; i < end; i++)
            {
                result.Add(i);
            }
        }
        return result;
    }

    private List<Tuple<int, int>> ScanFirstLetter(char letter) {
        var result = new List<Tuple<int, int>>();
        for (int i = 0; i < sourceArray.GetLength(0); i++)
        {
            for (int j = 0; j < sourceArray.GetLength(1); j++)
            {
                if (sourceArray[i,j] == letter)
                {
                    result.Add( Tuple.Create(i, j) );
                }
            }
        }
        return result;
    }
}
