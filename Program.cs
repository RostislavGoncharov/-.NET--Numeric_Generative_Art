using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        while (numbers == null || !numbers.Any())
        {
            string input = GetInput();
            numbers = AnalyzeInput(input);
        }
        
        ProcessInput(numbers);
    }

    static string GetInput()
    {
        string? input = null;

        while (input == null || input == "")
        {
            Console.Write("Enter any number: ");
            input = Console.ReadLine();
        }

        return input;
    }

    static List<int> AnalyzeInput(string input)
    {
        List<int> numbers = new List<int>();

        foreach (char c in input)
        {
            try {
                int number = Int32.Parse(c.ToString());
                numbers.Add(number);
            }
            catch (FormatException) {
                continue;
            }
        }

        return numbers;
    }

    static void ProcessInput(List<int> input)
    {
        
    }
}