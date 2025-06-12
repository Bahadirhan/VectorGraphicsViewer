using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorGraphicsViewer.Domain.Shapes;
using VectorGraphicsViewer.Infrastructure;

namespace VectorGraphicsViewer.Application.Services
{
    /// <summary>
    /// Application service responsible for loading shape data.
    /// Implements the IShapeService interface.
    /// </summary>
    public class ShapeService : IShapeService
    {
        private readonly ShapeFileReader _fileReader;

        /// <summary>
        /// ShapeFileReader dependency is injected from outside (improves testability via Dependency Injection).
        /// </summary>
        /// <param name="fileReader">Helper class that reads and parses shape files</param>
        public ShapeService(ShapeFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        /// <summary>
        /// Loads shapes asynchronously from the specified file path.
        /// </summary>
        /// <param name="filePath">Path to the JSON shape file</param>
        /// <returns>Collection of IShape objects</returns>
        public async Task<IEnumerable<IShape>> LoadShapesAsync(string filePath)
        {
            return await _fileReader.LoadShapesAsync(filePath);
        }
    }
}
