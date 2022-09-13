/* 
This program draws an image based on user input.
It takes numeric values from the input and draws associated images 
over the background for each value.
The ImageSharp package is used for all image operations here.
*/

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
                Random random = new Random();
                float opacityIndex = random.NextSingle();
                float rotationIndex = random.NextSingle() * 360;
                Point location = new Point(random.Next(-256, 256), random.Next(-256, 256)); 

                Image imageToBlend = Image.Load(Path.Combine(Directory.GetCurrentDirectory(), "images", $"{number}.png"));
                imageToBlend.Mutate(x => x.Rotate(rotationIndex));
                Image outputImage = img.Clone(x => x.DrawImage(imageToBlend, location, opacityIndex));
                img = outputImage;
            }

        img.Save(Path.Combine(Directory.GetCurrentDirectory(), "images", "output.png"));
    }
}