using System.Text.RegularExpressions;

namespace MullItOver;

public class ParseMemory
{
    public static int Parse() {
        var input = File.ReadAllText("/Users/mikael/dev/repos/aoc/2024/MullItOver/input");
        return Calculate(input);
    }

    public static int ParseV2()
    {
        var input = File.ReadAllText("/Users/mikael/dev/repos/aoc/2024/MullItOver/input");
        input = Regex.Replace(input, "\\s+", "");
        var re = new Regex("(don't\\(\\)).*?(do\\(\\))");
        input = re.Replace(input, string.Empty);
        return Calculate(input);
    }

    private static int Calculate(string input)
    {
        var pattern = "mul\\(\\d+\\,\\d+\\)";
        var result = 0;
        var matches = Regex.Matches(input, pattern);
        foreach (Match match in matches)
        {
            var digits = Regex.Matches(match.Value, "\\d+");
            var left = Convert.ToInt32(digits[0].Value);
            var right = Convert.ToInt32(digits[1].Value);
            result += left * right;
        }
        return result;
    }
}
