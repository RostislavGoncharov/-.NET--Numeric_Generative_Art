using ImageProcessor;

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
        Console.WriteLine("Success");
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
        List<int> numbersToProcess = new List<int>();

        foreach (char c in input)
        {
            try {
                int number = Int32.Parse(c.ToString());
                numbersToProcess.Add(number);
            }
            catch (FormatException) {
                continue;
            }
        }

        return numbersToProcess;
    }
}