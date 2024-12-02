namespace RedNosedReports;

public class Reports
{
    public static string CalculateSafeReports() {
        var safeReports = 0;
        var lines = File.ReadAllLines("/Users/mikael/dev/repos/aoc/2024/RedNosedReports/input");
        
        foreach (var line in lines)
        {
            var input = line.Split(' ');
            if (IsReportSafe(input))
            {
                safeReports += 1;
            }
        }

        return $"Safe reports: {safeReports}";
    }

    public static string CalculateSafeReportsDampener() {
        var safeReports = 0;
        var lines = File.ReadAllLines("/Users/mikael/dev/repos/aoc/2024/RedNosedReports/input");
        
        foreach (var line in lines)
        {
            var input = line.Split(' ');
            if (IsReportSafe(input))
            {
                safeReports += 1;
            }
            else
            {
                for (int j = 0; j < input.Length; j++)
                {
                    var temp = new List<string>(input.ToList());
                    temp.RemoveAt(j);
                    var input2 = temp.ToArray();
                    if (IsReportSafe(input2))
                    {
                        safeReports += 1;
                        break;
                    }
                }
            }
        }
        
        return $"Safe reports: {safeReports}";
    }

    private static bool IsReportSafe(string[] input)
    {
        var increasing = false;
        var decreasing = false;
        var safeMargin = true;
        for (int i = 0; i < input.Length - 1; i++)
        {
            var left = Convert.ToInt32(input[i]);
            var right = Convert.ToInt32(input[i + 1]);
            var difference = right - left;
            if (difference < 0)
            {
                decreasing = true;
            }
            else
            {
                increasing = true;
            }
            
            var absDifference = Math.Abs(difference);
            if (!(absDifference > 0 && absDifference < 4))
            {
                safeMargin = false;
            }
        }

        if (safeMargin == true && (increasing != decreasing))
        {
            return true;
        }

        return false;
    }

}
