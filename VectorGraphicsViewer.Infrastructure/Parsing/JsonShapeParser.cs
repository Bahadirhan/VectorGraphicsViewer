using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using VectorGraphicsViewer.Domain.Shapes;
using System.Windows;
using System.Windows.Media;

namespace VectorGraphicsViewer.Infrastructure.Parsing
{
    /// <summary>
    /// Parses JSON formatted input data into IShape objects.
    /// Creates the appropriate shape based on the "type" field.
    /// </summary>
    public class JsonShapeParser : IShapeParser
    {
        /// <summary>
        /// Parses the JSON text and returns a list of IShape objects.
        /// </summary>
        public IEnumerable<IShape> Parse(string input)
        {
            var shapes = new List<IShape>();

            // Read the JSON array
            var array = JArray.Parse(input);

            // Iterate through each JSON object
            foreach (var obj in array)
            {
                string type = obj.Value<string>("type")?.ToLower();

                switch (type)
                {
                    case "line":
                        {
                            // Parse start and end points
                            var a = ParsePoint(obj.Value<string>("a"));
                            var b = ParsePoint(obj.Value<string>("b"));
                            var color = ParseColor(obj.Value<string>("color"));

                            shapes.Add(new LineShape(a, b, color));
                            break;
                        }
                    case "circle":
                        {
                            var center = ParsePoint(obj.Value<string>("center"));
                            var radius = obj.Value<double>("radius");
                            var color = ParseColor(obj.Value<string>("color"));
                            bool filled = obj.Value<bool>("filled");

                            shapes.Add(new CircleShape(center, radius, color, filled));
                            break;
                        }
                    case "triangle":
                        {
                            var a = ParsePoint(obj.Value<string>("a"));
                            var b = ParsePoint(obj.Value<string>("b"));
                            var c = ParsePoint(obj.Value<string>("c"));
                            var color = ParseColor(obj.Value<string>("color"));
                            bool filled = obj.Value<bool>("filled");

                            shapes.Add(new TriangleShape(a, b, c, color, filled));
                            break;
                        }
                }
            }

            return shapes;
        }

        /// <summary>
        /// Converts a string in the format "x; y" to a System.Windows.Point.
        /// Supports decimals with comma or dot, culture-independent.
        /// </summary>
        private Point ParsePoint(string text)
        {
            var parts = text.Split(';');

            // Culture-independent number parsing (for comma/dot compatibility)
            double x = double.Parse(parts[0].Replace(',', '.'), CultureInfo.InvariantCulture);
            double y = double.Parse(parts[1].Replace(',', '.'), CultureInfo.InvariantCulture);

            return new Point(x, y);
        }

        /// <summary>
        /// Converts a string containing color (ARGB) information to a System.Windows.Media.Color object.
        /// Format: "alpha; red; green; blue"
        /// </summary>
        private Color ParseColor(string text)
        {
            var parts = text.Split(';');

            byte a = byte.Parse(parts[0].Trim());
            byte r = byte.Parse(parts[1].Trim());
            byte g = byte.Parse(parts[2].Trim());
            byte b = byte.Parse(parts[3].Trim());

            return Color.FromArgb(a, r, g, b);
        }
    }
}
