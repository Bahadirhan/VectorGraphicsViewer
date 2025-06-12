using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorGraphicsViewer.Domain.Shapes;

namespace VectorGraphicsViewer.Infrastructure.Parsing
{
    /// <summary>
    /// Interface used for parsing shape definitions from text input.
    /// Can be extended to support different data formats (e.g., JSON, XML, CSV).
    /// </summary>
    public interface IShapeParser
    {
        /// <summary>
        /// Parses shape objects from the input string.
        /// </summary>
        /// <param name="input">Raw text containing shape data (e.g., JSON content).</param>
        /// <returns>Collection of parsed IShape objects.</returns>
        IEnumerable<IShape> Parse(string input);
    }
}
