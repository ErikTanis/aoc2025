namespace aoc2025;

public class d2
{

    private static void Solve1()
    {
        string line = File.ReadAllLines("input/d2.txt")[0];
        string[] ranges = line.Split(',');

        Int64 counter = 0;

        foreach(string range in ranges)
        {
            Int64 lower = Int64.Parse(range.Split('-')[0]);
            Int64 higher = Int64.Parse(range.Split('-')[1]);
            for(Int64 i = lower; i <= higher; i++)
            {
                string num = i.ToString();
                int mid = num.Length / 2;
                string firstHalf = num.Substring(0, mid);
                string secondHalf = num.Substring(mid);
                if(firstHalf == secondHalf)
                {
                    counter += i;
                }
            }
        }

        Console.WriteLine(counter);
    }

    private static void Solve2()
    {
        string line = File.ReadAllLines("input/d2.txt")[0];
        string[] ranges = line.Split(',');

        Int64 counter = 0;

        foreach(string range in ranges)
        {
            Int64 lower = Int64.Parse(range.Split('-')[0]);
            Int64 higher = Int64.Parse(range.Split('-')[1]);
            for(Int64 i = lower; i <= higher; i++)
            {
                if(i < 10) continue;
                string num = i.ToString();
                int mid = num.Length / 2;
                
                for(int p = 1; p <= mid; p++)
                {
                    string block = num.Substring(0, p);
                    if(block.Length == 0) continue;
                    if(num.Length % block.Length != 0) continue;
                    int repeats = num.Length / block.Length;
                    string repeated = string.Concat(Enumerable.Repeat(block, repeats));

                    if(repeated == num)
                    {
                        counter += i;
                        break;
                    }
                }

            }
        }

        Console.WriteLine(counter);
    }

    public static void Solve()
    {
        Solve1();
        Solve2();
    }

}