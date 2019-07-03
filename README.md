# AiKismet's SearchableStream
A System.IO.Stream wrapper that adds some useful search and read abilities.



# Example using SearchableStringStream
Default encoding for SearchableStringStream is ASCII. You can set a different encoding type in the constructor ``new SearchableStringStream(Stream stream, Encoding encoding)```. As an example, if you have a pdf (strings are usually ASCII encoded) you might want to first find the "trailer" of the file that's indicated by that as a string in the Byte data. The code below finds the last position of "trailer" and then renders three lines of that trailer. 

```csharp
using (var fs = File.Open(@"C:\temp\test.pdf", FileMode.Open, FileAccess.Read))
using (var samplePdfStringStream = new SearchableStringStream(fs))
{
    var positionOfTrailer = samplePdfStringStream.LastIndexOf("trailer");
    samplePdfStringStream.Position--; // Set position to one before to include "t" of trailer.
    var threeLinesOfTrailer = samplePdfStringStream.ReadLines(3);
    Console.WriteLine($"Position of trailer in the pdf is: {positionOfTrailer}");
    Console.WriteLine("\n\nThe first 3 lines of the trailer are:\n");
    Console.WriteLine(threeLinesOfTrailer);
}
```

Possible output output
```
trailer
<</Size 11677/Root 1 0 R/Info 1943 0 R/ID[<F2D672E0489DBF46AEF8400DCC1DB98E><F2D672E0489DBF46AEF8400DCC1DB98E>] /Prev 12539514/XRefStm 12512836>>
startxref
```
