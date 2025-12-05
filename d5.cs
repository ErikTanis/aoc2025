using System.Text.RegularExpressions;

namespace aoc2025;

public partial class d5
{

    private static void Solve1()
    {
        string lines = File.ReadAllText("input/d5.txt");
        string[] freshRanges = EmptyLineRegex().Split(lines)[0].Split("\n");
        long[] productIds = [.. EmptyLineRegex().Split(lines)[1].Split("\n").Select(long.Parse)];
        
        int freshCount = 0;

        foreach(long productId in productIds)
        {
            foreach(string range in freshRanges)
            {
                long lower = long.Parse(range.Split("-")[0]);
                long upper = long.Parse(range.Split("-")[1]);
                if(productId >= lower && productId <= upper)
                {
                    freshCount += 1;
                    break;
                } 
            }
        }
        Console.WriteLine(freshCount);
    }

    private static void Solve2()
    {
        string lines = File.ReadAllText("input/d5.txt");
        string[] rangesStrings = EmptyLineRegex().Split(lines)[0].Split("\n");
        (long, long)[] freshRanges = [.. rangesStrings.Select(r => (long.Parse(r.Split("-")[0]), long.Parse(r.Split("-")[1])))];

        (long, long)[] ranges = RemoveDuplicateRanges(freshRanges);
        int changed = 1;

        while(changed != 0)
        {
            int oldCount = ranges.Length;
            ranges = RemoveDuplicateRanges(ranges);
            changed = oldCount - ranges.Length;
        }
        
        long total = ranges.Select(r => r.Item2 - r.Item1 + 1).Sum();
        Console.WriteLine(total);
    }


    private static (long, long)[] RemoveDuplicateRanges((long, long)[] freshRanges)
    {
        (long, long)[] ranges = [freshRanges[0]];

        for(int i = 1; i < freshRanges.Length; i++)
        {
            long lower = freshRanges[i].Item1;
            long upper = freshRanges[i].Item2;

            bool modified = false;

            for(int j = 0; j < ranges.Length; j++)
            {
                if(lower <= ranges[j].Item1 && lower <= ranges[j].Item2)
                {
                    if(upper >= ranges[j].Item1)
                    {
                        ranges[j].Item1 = lower;
                        ranges[j].Item2 = Math.Max(ranges[j].Item2, upper);
                        modified = true;
                        break;
                    }
                }
                else if(lower <= ranges[j].Item2)
                {
                    ranges[j].Item2 = Math.Max(ranges[j].Item2, upper);
                    modified = true;
                    break;
                }
            }

            if(!modified) ranges = [.. ranges.Append(freshRanges[i])];
        }

        return ranges;
    }


    public static void Solve()
    {
        Solve1();
        Solve2();
    }

    [GeneratedRegex(@"\r?\n\r?\n")]
    private static partial Regex EmptyLineRegex();
}