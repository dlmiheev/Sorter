namespace SorterApp;

public interface IFileGenerator
{ 
    void Generate(string filePath, long targetFileSize);
}