using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VectorGraphicsViewer.Presentation.Converters
{
    /// <summary>
    /// A converter that compares multiple values.
    /// Used on the UI side to determine whether a shape is selected.
    /// </summary>
    public class IShapeSelectedMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Compares the Shape inside a ShapeView with the SelectedShape reference in the ViewModel.
        /// Returns true if they refer to the same object, meaning the shape is selected.
        /// </summary>
        /// <param name="values">0: Shape (in the control), 1: SelectedShape (in the ViewModel)</param>
        /// <returns>true if selected, false otherwise</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] != null && values[1] != null)
                return ReferenceEquals(values[0], values[1]); // selected if same object

            return false;
        }

        /// <summary>
        /// ConvertBack is not supported (one-way binding only).
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
