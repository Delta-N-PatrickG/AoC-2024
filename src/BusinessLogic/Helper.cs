using System.Drawing;
using System.Text;
using Businesslogic.Attributes;
using Businesslogic.Enums;
using Businesslogic.Extensions;
using Pastel;

namespace BusinessLogic
{

    /// <summary>
    /// Provides helper methods for file operations and result writing.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets the contents of a file based on the specified file type.
        /// </summary>
        /// <param name="fileType">The type of the file.</param>
        /// <returns>A list of strings representing the file contents.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the file name attribute is not defined or is null/empty.</exception>
        public static List<string> GetFileContents(FileType fileType)
        {
            var fileNameAttribute = fileType.GetAttributeOfType<FileNameAttribute>();
            if (fileNameAttribute == null || string.IsNullOrEmpty(fileNameAttribute.FileName))
            {
                throw new InvalidOperationException($"FileNameAttribute is not defined for {fileType} or FileName is null or empty.");
            }
            var filename = fileNameAttribute.FileName;
            var text = System.IO.File.ReadAllText(filename);
            return text.Split(Environment.NewLine).ToList();
        }

        /// <summary>
        /// Writes the result of a function that processes a list of strings and returns an integer.
        /// </summary>
        /// <param name="func">The function to process the file contents.</param>
        /// <param name="fileType">The type of the file.</param>
        /// <param name="result">The expected result.</param>
        public static void WriteResult(Func<List<string>, int> func, FileType fileType, int result = 0)
        {
            WriteResult((x) => func(x).ToString(), fileType, result.ToString());
        }

        /// <summary>
        /// Writes the result of a function that processes a list of strings and returns a string.
        /// </summary>
        /// <param name="func">The function to process the file contents.</param>
        /// <param name="fileType">The type of the file.</param>
        /// <param name="result">The expected result.</param>
        public static void WriteResult(Func<List<string>, string> func, FileType fileType, string result)
        {
            var result1Test = func(GetFileContents(fileType));
            Console.WriteLine($"Result of {fileType} is: {result1Test.Pastel(Color.Red)}");
            if (result == "0")
            {
                return;
            }

            var resultString = new StringBuilder();
            resultString.Append(result).Append(" == ").Append(result1Test);
            resultString.Append(result == result1Test ? " CORRECT".Pastel(Color.Green) : " INCORRECT".Pastel(Color.Red));

            Console.WriteLine($"Result of {fileType} is: {resultString}");
        }

        /// <summary>
        /// Writes the result of a function that processes a list of strings and returns a long.
        /// </summary>
        /// <param name="func">The function to process the file contents.</param>
        /// <param name="fileType">The type of the file.</param>
        /// <param name="result">The expected result.</param>
        public static void WriteResult(Func<List<string>, long> func, FileType fileType, int result = 0)
        {
            WriteResult((x) => func(x).ToString(), fileType, result.ToString());
        }

        public static void GetFileData(string filePath, out List<string> Data, out string RawData)
        {
            Data = File.ReadLines(filePath).ToList();
            RawData = File.ReadAllText($"{filePath}");
        }
        
    }
}