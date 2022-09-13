using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

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
        Image img = Image.Load(Path.Combine(Directory.GetCurrentDirectory(), "images", "bg.png"));
            
            foreach (int number in input)
            {
                Image imageToBlend = Image.Load(Path.Combine(Directory.GetCurrentDirectory(), "images", $"{number}.png"));
                var random = new Random();
                float opacityIndex = random.NextSingle();
                Image outputImage = img.Clone(x => x.DrawImage(imageToBlend, opacityIndex));
                img = outputImage;
            }

        img.Save(Path.Combine(Directory.GetCurrentDirectory(), "images", "output.png"));
    }
}