using System.Diagnostics;
using FluentAssertions;
using NUnit.Framework;
using SorterApp;

namespace SorterTests;

public class GeneratorTests
{
    [OneTimeTearDown]
    [OneTimeSetUp]
    public void RemoveFiles()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string fileMask = "Test*.txt";

        try
        {
            string[] files = Directory.GetFiles(currentDirectory, fileMask);

            foreach (string file in files)
            {
                File.Delete(file);
                Console.WriteLine($"Deleted: {file}");
            }

            Console.WriteLine("All matching files have been deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    [Test]
    //[TestCase(1_000_000)]
    [TestCase(1_000_000_000)]
    public void DefaultGenerateFileTest(long size)
    {
        var generator = new DefaultGenerator();
        var sw = Stopwatch.StartNew();
        generator.Generate($"Test_{size}.txt", size);
        sw.Stop();
        Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
        sw.ElapsedMilliseconds.Should().BeLessOrEqualTo(30_000);
    }
    
    [Test]
    [TestCase(1_000_000)]
    [TestCase(1_000_000_000)]
    public void StaticGenerateFileTest(long size)
    {
        var generator = new StaticGenerator();
        var sw = Stopwatch.StartNew();
        generator.Generate($"Test_{size}.txt", size);
        sw.Stop();
        Console.WriteLine($"Time taken: {sw.ElapsedMilliseconds}ms");
        sw.ElapsedMilliseconds.Should().BeLessOrEqualTo(30_000);
    }
}