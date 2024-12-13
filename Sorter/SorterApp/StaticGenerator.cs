﻿using System.Security.Cryptography;
using System.Text;

namespace SorterApp;

public class StaticGenerator : IFileGenerator
{
    // Lists of sample words
    List<string> words = new List<string> { "Apple", "Beaver", "Cat", "Dog", "Elephant", "Frog", "Giraffe", "Horse", "Iguana", "Jaguar" };

    Random random = new Random();

    const int batchSize = 10_000;
    
    double predefinedPercentage = 0.1; // 10% from predefined list
    
    public void Generate(string filePath, long targetFileSize)
    {
        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            long currentFileSize = 0;

            while (currentFileSize < targetFileSize)
            {
                var batch = GenerateStringsBatch();

                foreach (var entry in batch)
                {
                    writer.Write(entry);
                    currentFileSize += Encoding.UTF8.GetByteCount(entry);

                    if (currentFileSize >= targetFileSize)
                    {
                        break;
                    }
                }
            }
        }
    }
    
    IEnumerable<string> GenerateStringsBatch()
    {
        for (int i = 0; i < batchSize; i++)
        {
            int number = random.Next(100);
            int number2 = random.Next(100);
            yield return $"{number}.{number2}string\n";
        }
    }
    
    static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var result = new char[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            var buffer = new byte[length];
            rng.GetBytes(buffer);

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[buffer[i] % chars.Length];
            }
        }
        return new string(result);
    }
}