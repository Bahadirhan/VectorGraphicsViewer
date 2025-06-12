using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VectorGraphicsViewer.Application.Services;
using VectorGraphicsViewer.Domain.Shapes;
using VectorGraphicsViewer.Infrastructure;
using VectorGraphicsViewer.Infrastructure.Parsing;

namespace VectorGraphicsViewer.Presentation.ViewModels
{
    /// <summary>
    /// The main ViewModel of the application.
    /// Acts as a bridge between the user interface and the model layer.
    /// </summary>
    public class ShapeViewerViewModel : ObservableObject
    {
        // List of shapes to be displayed on the screen
        private ObservableCollection<IShape> _shapes = new ObservableCollection<IShape>();
        public ObservableCollection<IShape> Shapes
        {
            get => _shapes;
            set => SetProperty(ref _shapes, value);
        }

        // Shape selected by the user (for selection operations)
        private IShape _selectedShape;
        public IShape SelectedShape
        {
            get => _selectedShape;
            set => SetProperty(ref _selectedShape, value);
        }

        // Full path of the loaded file (can be displayed in the UI)
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        // Application service used for loading shapes
        private readonly IShapeService _shapeService;

        // Command to be bound in the View
        public ICommand LoadFileCommand { get; }

        public ShapeViewerViewModel()
        {
            // Manual dependency binding (if no simple DI container)
            var parser = new JsonShapeParser();
            var fileReader = new ShapeFileReader(parser);
            _shapeService = new ShapeService(fileReader);

            // Bind the command with RelayCommand
            LoadFileCommand = new Helpers.RelayCommand(async () => await LoadFileAsync());
        }

        /// <summary>
        /// Opens a file selection dialog and loads shapes.
        /// </summary>
        public async Task LoadFileAsync()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "JSON Files|*.json|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;

                // Load shapes
                var loadedShapes = await _shapeService.LoadShapesAsync(FilePath);

                // Update the list
                Shapes.Clear();
                foreach (var shape in loadedShapes)
                    Shapes.Add(shape);

                // Automatically apply fit-to-screen
                (double s, double ox, double oy) = CalculateFit(Shapes, 900, 700);
                Scale = s;
                OffsetX = ox;
                OffsetY = oy;
            }
        }

        /// <summary>
        /// Calculates the appropriate scale and offset so all shapes fit within the screen size.
        /// </summary>
        public (double scale, double offsetX, double offsetY) CalculateFit(IEnumerable<IShape> shapes, double canvasWidth, double canvasHeight)
        {
            if (!shapes.Any())
                return (1.0, canvasWidth / 2, canvasHeight / 2);

            // The bounding rectangle that includes all shapes
            var totalBounds = shapes
                .Select(s => s.GetBounds())
                .Aggregate((r1, r2) => Rect.Union(r1, r2));

            double padding = 20;

            // Appropriate scale value
            double scaleX = (canvasWidth - padding * 2) / totalBounds.Width;
            double scaleY = (canvasHeight - padding * 2) / totalBounds.Height;
            double scale = Math.Min(scaleX, scaleY);

            // Calculate offset for centering (Y is inverted because drawing is done on an inverted Y-axis)
            double offsetX = -totalBounds.X * scale + (canvasWidth - totalBounds.Width * scale) / 2;
            double offsetY = totalBounds.Y * scale + (canvasHeight + totalBounds.Height * scale) / 2;

            return (scale, offsetX, offsetY);
        }

        // Zoom factor used for drawing in the View
        private double _scale = 1.0;
        public double Scale
        {
            get => _scale;
            set => SetProperty(ref _scale, value);
        }

        // Screen offset in the X-axis for drawing
        private double _offsetX;
        public double OffsetX
        {
            get => _offsetX;
            set => SetProperty(ref _offsetX, value);
        }

        // Screen offset in the Y-axis for drawing
        private double _offsetY;
        public double OffsetY
        {
            get => _offsetY;
            set => SetProperty(ref _offsetY, value);
        }

        /// <summary>
        /// Moves the selected shape by a specified amount.
        /// This is typically called with arrow keys.
        /// </summary>
        public void MoveSelectedShape(double dx, double dy)
        {
            if (SelectedShape == null)
                return;

            // Calculate new coordinates based on shape type and create a new shape instance
            if (SelectedShape is LineShape line)
            {
                var newShape = new LineShape(
                    new Point(line.Start.X + dx, line.Start.Y + dy),
                    new Point(line.End.X + dx, line.End.Y + dy),
                    line.Color);

                ReplaceSelectedShape(newShape);
            }
            else if (SelectedShape is CircleShape circle)
            {
                var newShape = new CircleShape(
                    new Point(circle.Center.X + dx, circle.Center.Y + dy),
                    circle.Radius,
                    circle.Color,
                    circle.Filled);

                ReplaceSelectedShape(newShape);
            }
            else if (SelectedShape is TriangleShape triangle)
            {
                var newShape = new TriangleShape(
                    new Point(triangle.A.X + dx, triangle.A.Y + dy),
                    new Point(triangle.B.X + dx, triangle.B.Y + dy),
                    new Point(triangle.C.X + dx, triangle.C.Y + dy),
                    triangle.Color,
                    triangle.Filled);

                ReplaceSelectedShape(newShape);
            }
        }

        /// <summary>
        /// Updates the shape in the collection and preserves the selection.
        /// </summary>
        private void ReplaceSelectedShape(IShape newShape)
        {
            int index = Shapes.IndexOf(SelectedShape);
            if (index >= 0)
            {
                Shapes[index] = newShape;
                SelectedShape = newShape; // Update the selected shape
            }
        }
    }
}
