using ImageProcessor;

internal class Program
{
    private static void Main(string[] args)
    {
        string input = GetInput();


    }

    static string GetInput()
    {
        string? _input = null;

        while (_input == null || _input == "")
        {
            Console.Write("Enter any number: ");
            _input = Console.ReadLine();
        }

        return _input;
    }

    
}