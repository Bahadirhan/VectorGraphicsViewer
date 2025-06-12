using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VectorGraphicsViewer.Domain.Shapes
{
    /// <summary>
    /// Base interface for all shape types in the application.
    /// Every shape should be drawable, selectable, and able to provide position information.
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// Draws the shape on the screen.
        /// </summary>
        /// <param name="context">WPF's DrawingContext object – the shape is drawn here.</param>
        /// <param name="scale">Applied zoom factor.</param>
        /// <param name="offsetX">Screen offset on the X-axis.</param>
        /// <param name="offsetY">Screen offset on the Y-axis.</param>
        void Draw(DrawingContext context, double scale, double offsetX, double offsetY);

        /// <summary>
        /// Determines whether a given point on the screen is inside this shape.
        /// Used in selection operations.
        /// </summary>
        /// <param name="point">The point to check, in screen coordinates.</param>
        /// <param name="scale">Zoom factor.</param>
        /// <param name="offsetX">X offset.</param>
        /// <param name="offsetY">Y offset.</param>
        /// <returns>Indicates whether the shape contains the given point.</returns>
        bool Contains(Point point, double scale, double offsetX, double offsetY);

        /// <summary>
        /// Color of the shape (supports ARGB).
        /// </summary>
        Color Color { get; }

        /// <summary>
        /// Indicates whether the shape is filled or only the outline will be drawn.
        /// </summary>
        bool Filled { get; }

        /// <summary>
        /// Returns the bounding box of the shape.
        /// Note: These bounds are relative to the shape's own coordinate system; screen scaling (scale) has not been applied.
        /// </summary>
        /// <returns>The rectangular bounds in the virtual coordinate space.</returns>
        Rect GetBounds(); // Sanal koordinat düzleminde (scale uygulanmamış)
    }
}
