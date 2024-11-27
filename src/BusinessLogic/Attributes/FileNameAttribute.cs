namespace Businesslogic.Attributes
{
    /// <summary>
    /// Attribute to specify a file name for a field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class FileNameAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileNameAttribute"/> class with the specified file name.
        /// </summary>
        /// <param name="fileName">The file name to associate with the field.</param>
        public FileNameAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}