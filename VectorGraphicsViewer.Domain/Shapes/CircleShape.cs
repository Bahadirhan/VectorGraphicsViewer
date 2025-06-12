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
    /// Represents a circle defined by its center and radius.
    /// Implements the IShape interface.
    /// </summary>
    public class CircleShape : IShape
    {
        /// <summary>
        /// The center of the circle (in virtual coordinate space).
        /// </summary>
        public Point Center { get; }

        /// <summary>
        /// The radius of the circle (in virtual units, before applying scale).
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// The color of the circle in ARGB format.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Indicates whether the circle is filled.
        /// </summary>
        public bool Filled { get; }

        public CircleShape(Point center, double radius, Color color, bool filled)
        {
            Center = center;
            Radius = radius;
            Color = color;
            Filled = filled;
        }

        /// <summary>
        /// Draws the circle onto the provided DrawingContext.
        /// </summary>
        public void Draw(DrawingContext context, double scale, double offsetX, double offsetY)
        {
            // Convert virtual coordinates to screen coordinates
            var center = new Point(Center.X * scale + offsetX, -Center.Y * scale + offsetY);
            var radius = Radius * scale;

            // Create a brush for the stroke (pen)
            var penBrush = new SolidColorBrush(Color);
            penBrush.Freeze(); // Optimize performance by freezing

            var pen = new Pen(penBrush, 1.0); // Fixed thickness in screen units

            // Create a fill brush if the circle is filled
            SolidColorBrush fillBrush = null;
            if (Filled)
            {
                fillBrush = new SolidColorBrush(Color);
                fillBrush.Freeze(); // Optimize the fill brush too
            }

            context.DrawEllipse(fillBrush, pen, center, radius, radius);
        }

        /// <summary>
        /// Determines whether the given screen coordinate point is inside the circle.
        /// </summary>
        public bool Contains(Point point, double scale, double offsetX, double offsetY)
        {
            var center = new Point(Center.X * scale + offsetX, -Center.Y * scale + offsetY);
            var radius = Radius * scale;

            // If the point is equal to or closer than the radius, it is inside the circle
            return (center - point).Length <= radius;
        }

        /// <summary>
        /// Returns the bounding rectangle of the shape (in virtual coordinates).
        /// </summary>
        public Rect GetBounds()
        {
            return new Rect(
                new Point(Center.X - Radius, Center.Y - Radius),
                new Size(Radius * 2, Radius * 2)
            );
        }
    }
}
