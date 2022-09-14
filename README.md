# -.NET--Numeric_Generative_Art-
**Version 0.1**   
**Dependencies:**   
- [ImageSharp](https://www.nuget.org/packages/SixLabors.ImageSharp/)

![Example image](/example_image.png)

## Description
This .NET 6.0 console app generates abstract art based on numeric values in user input.

Each value in the 0-9 range has an image associated with it. Each image's opacity, position, rotation and size are randomized, then the image is drawn on top of the previous one. The end result has a chance to go through several effects with randomized parameters. 

Non-numeric values in the input are ignored in this version. The more digits the input string has, the more complex the end result will be. 

The size of the resulting image is currently defined by the size of bg.png.
