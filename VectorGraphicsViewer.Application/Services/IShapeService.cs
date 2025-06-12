using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorGraphicsViewer.Domain.Shapes;

namespace VectorGraphicsViewer.Application.Services
{
    /// <summary>
    /// Service interface for shape loading operations in the application layer.
    /// The UI layer uses this interface to load shapes without knowing infrastructure details.
    /// </summary>
    public interface IShapeService
    {
        /// <summary>
        /// Loads and parses shape data asynchronously from the given file path.
        /// </summary>
        /// <param name="filePath">Full path to the shape file</param>
        /// <returns>List of parsed shapes</returns>
        Task<IEnumerable<IShape>> LoadShapesAsync(string filePath);
    }
}
