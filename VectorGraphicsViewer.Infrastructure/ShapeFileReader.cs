using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorGraphicsViewer.Domain.Shapes;
using VectorGraphicsViewer.Infrastructure.Parsing;

namespace VectorGraphicsViewer.Infrastructure
{

    /// <summary>
    /// Class that reads shape data from the file system and parses it.
    /// Uses an external IShapeParser to perform the parsing (e.g., JsonShapeParser).
    /// </summary>
    public class ShapeFileReader
    {
        private readonly IShapeParser _parser;

        /// <summary>
        /// Parser dependency that determines how the data read from the file will be parsed.
        /// </summary>
        /// <param name="parser">Parser object that converts data into shapes.</param>
        public ShapeFileReader(IShapeParser parser)
        {
            _parser = parser;
        }

        /// <summary>
        /// Asynchronously reads content from the specified file path, parses the shapes, and returns them.
        /// </summary>
        /// <param name="filePath">Full path of the file containing shape data.</param>
        /// <returns>List of parsed shapes.</returns>
        public async Task<IEnumerable<IShape>> LoadShapesAsync(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                // Read entire content of the file
                string input = await reader.ReadToEndAsync();

                // Convert string content into shapes using the parser
                return _parser.Parse(input);
            }
        }

        /// <summary>
        /// Synchronous version of the same operation – may be useful for small files or testing purposes.
        /// </summary>
        public IEnumerable<IShape> LoadShapes(string filePath)
        {
            string input = File.ReadAllText(filePath);
            return _parser.Parse(input);
        }
    }
}
