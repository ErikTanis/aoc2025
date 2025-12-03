namespace aoc2025;

public class d3
{

    private static void Solve1() => GetLargest(2);

    private static void Solve2() => GetLargest(12);

    private static long[] GetLargest(int bankSize)
    {
        string[] lines = File.ReadAllLines("input/d3.txt");
        long total = 0;
        long[] test = [];

        foreach(string line in lines)
        {
            int[] reversedBank = [.. line.ToCharArray().Select((c) => int.Parse($"{c}")).Reverse()];
            int[] curr = [.. reversedBank.Take(bankSize).Reverse()];
            
            for(int i = bankSize; i < reversedBank.Length; i++)
            {
                curr = Check(curr, 0, reversedBank[i]);
            }
            total += long.Parse(string.Join("", curr));
            test = test.Append(long.Parse(string.Join("", curr))).ToArray();
        }
        Console.WriteLine(total);
        return test;
    }

    private static int[] Check(int[] curr, int pos, int num)
    {
        if(pos == curr.Length) return curr;
        if(num >= curr[pos])
        {
            int oldVal = curr[pos];
            
            curr[pos] = num;
            
            if(pos < curr.Length - 1)
            {
                return Check(curr, pos + 1, oldVal);
            }
        }
        return curr;
    }

    public static void Solve()
    {
        Solve1();
        Solve2();
        
    }

}