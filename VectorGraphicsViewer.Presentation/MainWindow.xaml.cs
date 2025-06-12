using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VectorGraphicsViewer.Presentation.ViewModels;

namespace VectorGraphicsViewer.Presentation
{
    /// <summary>
    /// The main window of the application.
    /// The ViewModel is assigned, and user interactions (mouse, keyboard) are captured here.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // ViewModel is bound (simple DI)
            DataContext = new ShapeViewerViewModel();

            // Event handler is attached to listen to keyboard events
            PreviewKeyDown += MainWindow_PreviewKeyDown;

            Focusable = true;
            Focus(); // Ensures the window gets keyboard focus
        }

        /// <summary>
        /// Zoom in or out using the mouse wheel.
        /// </summary>
        private void ShapeHost_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (DataContext is ShapeViewerViewModel vm)
            {
                // Zoom in if delta is positive, zoom out if negative
                double zoomFactor = e.Delta > 0 ? 1.1 : 0.9;
                vm.Scale *= zoomFactor;
            }
        }

        /// <summary>
        /// When any shape is clicked with the mouse,
        /// ShapeViewerViewModel.SelectedShape is updated.
        /// </summary>
        private void ShapeHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ShapeViewerViewModel vm)
            {
                // Get the coordinates of the clicked point
                Point clickPoint = e.GetPosition((UIElement)sender);

                // Select the shape that contains the clicked point
                foreach (var shape in vm.Shapes)
                {
                    if (shape.Contains(clickPoint, vm.Scale, vm.OffsetX, vm.OffsetY))
                    {
                        vm.SelectedShape = shape;
                        return;
                    }
                }

                // If no shape is clicked, deselect the shape
                vm.SelectedShape = null;
            }
        }

        /// <summary>
        /// Moves the selected shape using the arrow keys on the keyboard.
        /// </summary>
        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var vm = DataContext as ShapeViewerViewModel;
            if (vm == null || vm.SelectedShape == null)
                return;

            const double delta = 5.0; // How much the shape moves per step (virtual units)

            switch (e.Key)
            {
                case Key.Left:
                    vm.MoveSelectedShape(-delta, 0); // Move left
                    break;
                case Key.Right:
                    vm.MoveSelectedShape(delta, 0); // Move right
                    break;
                case Key.Up:
                    vm.MoveSelectedShape(0, delta); // Move up (positive Y)
                    break;
                case Key.Down:
                    vm.MoveSelectedShape(0, -delta); // Move down (negative Y)
                    break;
            }
        }
    }

}
