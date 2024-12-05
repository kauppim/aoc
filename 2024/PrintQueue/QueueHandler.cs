namespace PrintQueue;

public class QueueHandler
{
    private Dictionary<int, List<int>> forwardRules, backwardRules;
    private List<List<int>> instructions, invalidInstructions;
    public QueueHandler(string inputPath) {
        // Read input.
        var lines = File.ReadAllLines(inputPath);
        var isInstructions = false;
        forwardRules = [];
        backwardRules = [];
        instructions = [];
        invalidInstructions = [];
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isInstructions = true;
                continue;
            }
            else if (isInstructions == false)
            {
                // Parse rules.
                var ruleSet = line.Split('|').ToList();
                var left = Convert.ToInt32(ruleSet[0]);
                var right = Convert.ToInt32(ruleSet[1]);
                var rulesOut = new List<int>();
                if (forwardRules.TryGetValue(left, out rulesOut))
                {
                    rulesOut.Add(right);
                }
                else
                {
                    var temp = new List<int> { right };
                    forwardRules[left] = temp;
                }
                if (backwardRules.TryGetValue(right, out rulesOut))
                {
                    rulesOut.Add(left);
                }
                else
                {
                    var temp = new List<int> { left };
                    backwardRules[right] = temp;
                }
            }
            else
            {
                // Parse instructions.
                var instructionSet = line.Split(',').ToList();
                var temp = new List<int>();
                instructionSet.ForEach(x => temp.Add(Convert.ToInt32(x)));
                instructions.Add(temp);
            }
        }
    }
    public string Execute()
    {
        // WriteLines for debugging.
        // PrintInstructions(instructions);
        // Console.WriteLine("Forward rules:");
        // PrintRules(forwardRules);
        // Console.WriteLine("Backward rules:");
        // PrintRules(backwardRules);

        // Remember to collect the middle page numbers of approved print instructions.
        var middlePages = new List<int>();
        // Validate instructions.
        foreach (var instructionSet in instructions)
        {
            var temp = Validate(instructionSet);
            if (temp.Item2 == instructionSet.Count) {
                middlePages.Add(temp.Item3);
            }
            else
            {
                invalidInstructions.Add(instructionSet);
            }
        }
        // End.
        var total = middlePages.Sum();
        Console.WriteLine("Invalid instruction sets:");
        PrintInstructions(invalidInstructions);
        return $"Total value of middle page numbers is {total}.";
    }

    public string ExecuteReorder() {
        var middlePages = new List<int>();
        Console.WriteLine("Attempting to reorder invalid instruction sets...");
        foreach (var instructionSet in invalidInstructions)
        {
            var isValid = false;
            do
            {
                var result = Validate(instructionSet);
                if (result.Item2 != instructionSet.Count) {
                    if (result.Item1 == true)
                    {
                        var left = result.Item2 - 1;
                        var right = result.Item2;
                        (instructionSet[right], instructionSet[left]) = (instructionSet[left], instructionSet[right]);
                    }
                    else
                    {
                        var left = result.Item2;
                        var right = result.Item2 + 1;
                        (instructionSet[right], instructionSet[left]) = (instructionSet[left], instructionSet[right]);
                    }
                    
                    Console.Write("Trying new instruction set: ");
                    Console.WriteLine(InstructionSetToString(instructionSet));
                }
                else
                {
                    isValid = true;
                    middlePages.Add(result.Item3);
                }
            } while (isValid == false);
        }
        var total = middlePages.Sum();
        return $"Total value of reordered middle page numbers is {total}.";
    }
    private (bool, int, int) Validate(List<int> instructionSet)
    {
        var count = instructionSet.Count;
        var middle = instructionSet[count / 2];
        var isValid = true;
        var breakIndex = count;
        var breakDirForward = true;
        for (int i = 0; isValid == true && i < count; i++)
        {
            // Check forward
            if (isValid == true && forwardRules.TryGetValue(instructionSet[i], out var forwardRule))
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (!forwardRule.Contains(instructionSet[j]))
                    {
                        isValid = false;
                        breakIndex = j;
                        break;
                    }
                }
            }
            // Check backward
            if (isValid == true && backwardRules.TryGetValue(instructionSet[i], out var backwardRule))
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    if (!backwardRule.Contains(instructionSet[k]))
                    {
                        isValid = false;
                        breakIndex = k;
                        breakDirForward = false;
                        break;
                    }
                }
            }
        }

        if (isValid)
        {
            Console.Write("Instruction set: ");
            Console.Write(InstructionSetToString(instructionSet));
            Console.WriteLine(" is valid!");
        }
        
        return (breakDirForward, breakIndex, middle);
    }

    private static void PrintInstructions(List<List<int>> instructions) {
        Console.WriteLine("Instruction sets:");
        foreach (var set in instructions)
        {
            Console.WriteLine(InstructionSetToString(set));
        }
        Console.WriteLine("=================");
    }
    
    private static string InstructionSetToString(List<int> instructionSet) {
        var temp = "";
        instructionSet.ForEach(x => temp += x + ", ");
        return "(" + temp.Trim().TrimEnd(',') + ")";
    }

    private static void PrintRules(Dictionary<int,List<int>> rules) {
        foreach (KeyValuePair<int, List<int>> pair in rules)
        {
            var temp = "Page: " + pair.Key + ", rules: ";
            pair.Value.ForEach(x => temp += x + ", ");
            Console.WriteLine(temp.Trim().TrimEnd(','));
        }
        Console.WriteLine("=================");
    }
}
