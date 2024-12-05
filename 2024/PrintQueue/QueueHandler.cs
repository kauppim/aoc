namespace PrintQueue;

public static class QueueHandler
{
    public static string Validate() {
        // Read input.
        var lines = File.ReadAllLines("/Users/mikael/dev/repos/aoc/2024/PrintQueue/input");
        var isInstructions = false;
        var forwardRules = new Dictionary<int,List<int>>();
        var backwardRules = new Dictionary<int,List<int>>();
        var instructions = new List<List<int>>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) {
                isInstructions = true;
                continue;
            }
            else if (isInstructions == false) {
                // Parse rules.
                var ruleSet = line.Split('|').ToList();
                var left = Convert.ToInt32(ruleSet[0]);
                var right = Convert.ToInt32(ruleSet[1]);
                var rulesOut = new List<int>();
                if (forwardRules.TryGetValue(left, out rulesOut)) {
                    rulesOut.Add(right);
                }
                else
                {
                    var temp = new List<int> { right };
                    forwardRules[left] = temp;
                }
                if (backwardRules.TryGetValue(right, out rulesOut)) {
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
            var count = instructionSet.Count;
            var isValid = true;
            for (int i = 0; i < count; i++)
            {
                // Check forward
                var forwardRule = new List<int>();
                if (forwardRules.TryGetValue(instructionSet[i], out forwardRule))
                {
                    for (int j = i + 1; j < count; j++)
                    {
                        if(!forwardRule.Contains( instructionSet[j] )) {
                            isValid = false;
                            break;
                        }
                    }
                }
                // Check backward
                var backwardRule = new List<int>();
                if (backwardRules.TryGetValue(instructionSet[i], out backwardRule))
                {
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if(!backwardRule.Contains( instructionSet[k] )) {
                            isValid = false;
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
                middlePages.Add(instructionSet[count/2]);
            }
        }
        
        // End.
        var total = middlePages.Sum();
        return $"Total value of middle page numbers is {total}.";
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
