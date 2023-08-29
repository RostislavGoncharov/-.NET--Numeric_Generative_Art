/* 
This program draws an image based on user input.
It takes numeric values from the input and draws associated images 
over the background for each value.
The ImageSharp package is used for all image operations here.
*/

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
    /*
    Gets user input, derives a list of numbers from it, then uses 
    the numbers for image generation.
    If no numbers can be parsed from input, the user is prompted to enter 
    a number again.
    */
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
    // Prompts the user to provide input until input is not null.
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
    /*
    Attempts to derive a list of integers from user input. 
    The user is allowed to enter any other symbols, 
    but everything except numbers is ignored (may change in the future).
    */
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
    /*
    Draws a background image, then overlays another image over it
    for each number in the list. Each overlay's size, position, rotation and opacity
    are randomized.
    Saves the image to ./images/output.png afterwards.
    */
        Image img = Image.Load(Path.Combine(Directory.GetCurrentDirectory(), "images", "bg.png"));
            
            foreach (int number in input)
            {
                Random random = new Random();
                float opacityIndex = random.NextSingle();
                float rotationIndex = random.NextSingle() * 360;
                int offset = img.Width / 4;
                Point location = new Point(random.Next(-offset, offset), random.Next(-offset, offset)); 
                int width = (int)(img.Width * random.NextSingle() * 2) + offset; // adding offset seems to help with the "images don't overlap" error
                Size size = new Size(width);

                Image imageToBlend = Image.Load(Path.Combine(Directory.GetCurrentDirectory(), "images", $"{number}.png"));
                imageToBlend.Mutate(x => x.Resize(size).Rotate(rotationIndex));
                Image outputImage = img.Clone(x => x.DrawImage(imageToBlend, location, opacityIndex));
                img = outputImage;
            }
        img = ApplyEffects(img, 0.5f);
        
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "output.png");
        img.Save(outputPath);
        DisplayImage(outputPath);
    }

    static Image ApplyEffects(Image input, float probability)
    {
    // Applies randomized filtering with a given probability.
        if (probability > 1)
        {
            probability = 1;
        }

        Random random = new Random();
        
        if (probability >= random.NextSingle()) 
        {
            input.Mutate(x => x.GaussianBlur(random.NextSingle()));
            input.Mutate(x => x.Pixelate(random.Next(1, 5)));
            input.Mutate(x => x.Glow(random.NextSingle() * 500));
            input.Mutate(x => x.Saturate(random.NextSingle() * 2));
        }

        return input;
    }

    static void DisplayImage(string path)
    {
        Process imageViewer = new Process();
        imageViewer.StartInfo.FileName = path;
        imageViewer.StartInfo.UseShellExecute = true;
        imageViewer.Start();
    }
}