namespace GuardGallivant;

public class LabMap
{
    private char[,] labMap;
    private (int, int) guardCoordinates;
    private List<(int, int, char)> guardSteps;
    private char guardDirection;
    private int height;
    private int width;
    public LabMap(string sourcePath) {
        guardSteps = [];
        labMap = ParseInput(sourcePath);
    }
    private char[,] ParseInput(string sourcePath) {
        var input = File.ReadAllLines(sourcePath);
        height = input.Length;
        width = input[0].Length;
        var result = new char[height,width];
        for (int i = 0; i < height; i++)
        {
            var line = input[i].ToCharArray();
            for (int j = 0; j < width; j++)
            {
                if (line[j] == '^')
                {
                    guardCoordinates = (i,j);
                    guardDirection = line[j];
                    guardSteps.Add((guardCoordinates.Item1, guardCoordinates.Item2, guardDirection));
                    result[i,j] = '.';
                }
                else
                {
                    result[i,j] = line[j];
                }
            }
        }
        return result;
    }
    public void PrintMap() {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(labMap[i,j]);
            }
            Console.WriteLine();
        }
    }

    public bool MoveGuard() {
        labMap[guardCoordinates.Item1, guardCoordinates.Item2] = 'X';
        switch (guardDirection) // Up
        {
            case '^': // Up
                {
                    var newCoordinates = (guardCoordinates.Item1 - 1, guardCoordinates.Item2);
                    if (newCoordinates.Item1 < 0)
                    { // Out of bounds, end execution
                        return false;
                    }
                    else if (labMap[newCoordinates.Item1, newCoordinates.Item2] == '#')
                    { // Guard turns right when facing obstacle
                        guardDirection = '>';
                    }
                    else // Move ahead
                    {
                        guardCoordinates = newCoordinates;
                        guardSteps.Add((guardCoordinates.Item1, guardCoordinates.Item2, guardDirection));
                    }
                    break;
                }
            case 'v': // Down
                {
                    var newCoordinates = (guardCoordinates.Item1 + 1, guardCoordinates.Item2);
                    if (newCoordinates.Item1 >= height)
                    { // Out of bounds, end execution
                        return false;
                    }
                    else if (labMap[newCoordinates.Item1, newCoordinates.Item2] == '#')
                    { // Guard turns right when facing obstacle
                        guardDirection = '<';
                    }
                    else // Move ahead
                    {
                        guardCoordinates = newCoordinates;
                        guardSteps.Add((guardCoordinates.Item1, guardCoordinates.Item2, guardDirection));
                    }
                    break;
                }
            case '<': // Left
                {
                    var newCoordinates = (guardCoordinates.Item1, guardCoordinates.Item2 - 1);
                    if (newCoordinates.Item2 < 0)
                    { // Out of bounds, end execution
                        return false;
                    }
                    else if (labMap[newCoordinates.Item1, newCoordinates.Item2] == '#')
                    { // Guard turns right when facing obstacle
                        guardDirection = '^';
                    }
                    else // Move ahead
                    {
                        guardCoordinates = newCoordinates;
                        guardSteps.Add((guardCoordinates.Item1, guardCoordinates.Item2, guardDirection));
                    }

                    break;
                }
            case '>': // Right
                {
                    var newCoordinates = (guardCoordinates.Item1, guardCoordinates.Item2 + 1);
                    if (newCoordinates.Item2 >= width)
                    { // Out of bounds, end execution
                        return false;
                    }
                    else if (labMap[newCoordinates.Item1, newCoordinates.Item2] == '#')
                    { // Guard turns right when facing obstacle
                        guardDirection = 'v';
                    }
                    else // Move ahead
                    {
                        guardCoordinates = newCoordinates;
                        guardSteps.Add((guardCoordinates.Item1, guardCoordinates.Item2, guardDirection));
                    }
                    break;
                }
        }
        return true;
    }
    public int GetStepsTaken() {
        return guardSteps.Count;
    }

    public int GetDistinctPositions() {
        var count = 0;

        foreach (var position in labMap)
        {
            if (position == 'X')
            {
                count++;
            }
        }

        return count;
    }
}