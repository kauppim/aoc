namespace GuardGallivant;

public static class MapSearcher
{
    public static string TraverseMap(string sourcePath) {
        var mapObject = new LabMap(sourcePath);
        // mapObject.PrintMap();
        Console.WriteLine();
        bool takeNextStep;
        do
        {
            takeNextStep = mapObject.MoveGuard();
        } while (takeNextStep == true);

        mapObject.PrintMap();
        Console.WriteLine(mapObject.GetStepsTaken());
        return $"Guard visited {mapObject.GetDistinctPositions()} distinct positions.";
    }

}
