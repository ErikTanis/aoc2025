namespace aoc2025;

public class d1
{
    private static void Solve1()
    {
        int dial = 50;
        int pointsAtZero = 0;
        string[] lines = File.ReadAllLines("input/d1.txt");
        foreach(string line in lines)
        {
            int direction = line[0] == 'R' ? 1: -1; // R = 1, L = -1
            int change = int.Parse(line.Substring(1));
            dial = ((dial + direction * change) % 100 + 100) % 100;
            
            if(dial == 0) pointsAtZero += 1;
        }
        Console.WriteLine(pointsAtZero);
    }

    private static void Solve2()
    {
        int dial = 50;
        int passedZero = 0;
        string[] lines = File.ReadAllLines("input/d1.txt");
        foreach(string line in lines)
        {
            int oldDial = dial;
            int direction = line[0] == 'R' ? 1: -1; // R = 1, L = -1
            int change = int.Parse(line.Substring(1));
            dial = ((dial + direction * change) % 100 + 100) % 100;
            
            if(dial == 0) passedZero += 1;

            int rounds = change / 100;
            passedZero += rounds;

            if(dial == 0 || oldDial == 0) continue;
            if((direction == -1 && dial > oldDial) || (direction == 1 && dial < oldDial)) passedZero += 1; 
        }
        Console.WriteLine(passedZero);
    }

    public static void Solve()
    {
        Solve1();
        Solve2();
    }

}