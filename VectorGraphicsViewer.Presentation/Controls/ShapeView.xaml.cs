using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VectorGraphicsViewer.Domain.Shapes;
using VectorGraphicsViewer.Presentation.ViewModels;

namespace VectorGraphicsViewer.Presentation.Controls
{
    /// <summary>
    /// A custom UserControl used to draw a shape (IShape) on the WPF UI.
    /// Draws the contained Shape object according to scale and offset.
    /// Also visually indicates the selection state.
    /// </summary>
    public partial class ShapeView : UserControl
    {
        public ShapeView()
        {
            InitializeComponent();
        }

        // --- IsSelected: Dependency property indicating whether the shape is selected ---

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(ShapeView),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender)); // triggers render

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        // --- Shape: The shape to be visualized (drawing logic is defined inside IShape) ---

        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.Register(
                nameof(Shape),
                typeof(IShape),
                typeof(ShapeView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public IShape Shape
        {
            get => (IShape)GetValue(ShapeProperty);
            set => SetValue(ShapeProperty, value);
        }

        // --- Scale: Zoom factor ---

        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register(
                nameof(Scale),
                typeof(double),
                typeof(ShapeView),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        // --- OffsetX: Screen offset on the X axis ---

        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.Register(
                nameof(OffsetX),
                typeof(double),
                typeof(ShapeView),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double OffsetX
        {
            get => (double)GetValue(OffsetXProperty);
            set => SetValue(OffsetXProperty, value);
        }

        // --- OffsetY: Screen offset on the Y axis (calculation is critical due to inverted screen Y axis) ---

        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.Register(
                nameof(OffsetY),
                typeof(double),
                typeof(ShapeView),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double OffsetY
        {
            get => (double)GetValue(OffsetYProperty);
            set => SetValue(OffsetYProperty, value);
        }

        /// <summary>
        /// Called by WPF on every redraw.
        /// Draws the Shape object on the screen with the appropriate scale and position.
        /// If selected, also draws a red rectangle around it.
        /// </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // Draw the shape
            if (Shape != null)
            {
                Shape.Draw(drawingContext, Scale, OffsetX, OffsetY);
            }

            // If the shape is selected, draw a red frame around it
            if (IsSelected)
            {
                var bounds = Shape.GetBounds();

                // Convert virtual coordinates to screen coordinates (Y axis inverted)
                var rect = new Rect(
                    bounds.X * Scale + OffsetX,
                    -bounds.Y * Scale + OffsetY - bounds.Height * Scale,
                    bounds.Width * Scale,
                    bounds.Height * Scale);

                drawingContext.DrawRectangle(null, new Pen(Brushes.Red, 2), rect);
            }
        }
    }
}
