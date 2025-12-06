using System.Text.RegularExpressions;

namespace aoc2025;

public partial class d6
{

    private static void Solve1()
    {
        string[] lines = File.ReadAllLines("input/d6.txt");
        lines = [.. lines.Select(l => l.Trim())];
        string[][] grid = [.. lines.Select(l => WhitespaceRegex().Split(l))];

        long total = 0;
        
        string[][] calcs = Transpose(grid);

        foreach(var calculation in calcs)
        {
            long[] numbers = [.. calculation.Take(calculation.Length - 1).Select(long.Parse)];
            string op = calculation.TakeLast(1).First();
            if(op == "+") total += numbers.Sum();
            if(op == "*") total += numbers.Aggregate((a, b) => a * b);
        }
        Console.WriteLine(total);
    }
    

    private static void Solve2()
    {
        string[] lines = File.ReadAllLines("input/d6.txt");
        string[] operators = [.. OperatorRegex().Matches(lines[lines.Length - 1]).Select(m => m.Value)];
        operators[operators.Length-1] += " ";
        lines = [.. lines.Take(lines.Length - 1)];

        // Split every row to correct column length
        string[][] splitLines = new string[lines.Length][];
        for(int i = 0; i < lines.Length; i++)
        {
            splitLines[i] = new string[operators.Length];
            int index = 0;
            for(int j = 0; j < operators.Length; j++)
            {
                splitLines[i][j] = lines[i].Substring(index, operators[j].Length - 1);
                index += operators[j].Length;
            }
        }
        
        string[][] calcs = Transpose(splitLines);
        long total = 0;

        for(int i = 0; i < calcs.Length; i++)
        {
            int numLength = calcs[i].Max(x => x.Length);
            string[] numStrings = new string[numLength];
            for(int j = 0; j < calcs[i].Length; j++)
            {
                for(int k = 0; k < numLength; k++)
                {
                    if(calcs[i][j][k] != ' ')
                        numStrings[k] += calcs[i][j][k];
                }
            }

            long[] nums = [.. numStrings.Select(long.Parse)];
            if(operators[i][0] == '+') total += nums.Sum();
            if(operators[i][0] == '*') total += nums.Aggregate((a, b) => a * b);
            
        }

        Console.WriteLine(total);
    }

    private static T[][] Transpose<T>(T[][] matrix)
    {
        int rowCount = matrix.Length;
        int colCount = matrix.Max(r => r.Length);

        T[][] transposedMatrix = new T[colCount][]; 
        for(int Y = 0; Y < colCount; Y++)
        {
            transposedMatrix[Y] = new T[rowCount];
            for(int X = 0; X < rowCount; X++)
            {
                transposedMatrix[Y][X] = matrix[X][Y];
            }
        }
        return transposedMatrix;
    }

    public static void Solve()
    {
        Solve1();
        Solve2();
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
    
    [GeneratedRegex(@"[+*]\s*")]
    private static partial Regex OperatorRegex();
}