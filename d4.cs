namespace aoc2025;

public class d4
{

    private static void Solve1()
    {
        string[] lines = File.ReadAllLines("input/d4.txt");
        char[][] grid = [.. lines.Select(l => l.ToCharArray())];
        Console.WriteLine(RemoveRolls(grid).Item1);
    }

    private static void Solve2()
    {
        string[] lines = File.ReadAllLines("input/d4.txt");
        char[][] grid = [.. lines.Select(l => l.ToCharArray())];
        
        int totalRemoved = 0;
        bool done = false;
        
        do
        {
            (int, char[][]) removed = RemoveRolls(grid);
            totalRemoved += removed.Item1;
            grid = removed.Item2;
            if(removed.Item1 == 0) done = true;
        } while(!done);

        Console.WriteLine(totalRemoved);
    }

    private static (int, char[][]) RemoveRolls(char[][] grid)
    {
        HashSet<(int, int)> rolls = new();
        int amountRemoved = 0;

        for(int Y = 0; Y < grid.Length; Y++)
        {
            for(int X = 0; X < grid[Y].Length; X++)
            {
                if(grid[Y][X] == '.') continue;
                (int, int)[] surroundingRolls = [];

                for(int dY = -1; dY <= 1; dY++)
                {
                    for(int dX = -1; dX <= 1; dX++)
                    {
                        try
                        {
                            if(grid[Y + dY][X + dX] == '@')
                            {
                                if(dY == 0 && dX == 0) continue;
                                surroundingRolls = [.. surroundingRolls.Append((X + dX, Y + dY))];
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {}
                    }
                }

                if(surroundingRolls.Length < 4)
                {
                    rolls.Add((X, Y));
                }
            }
        }

        foreach(var roll in rolls)
        {
            grid[roll.Item2][roll.Item1] = '.';
            amountRemoved++;
        }

        return (amountRemoved, grid);
    }

    public static void Solve()
    {
        Solve1();
        Solve2();
    }

}