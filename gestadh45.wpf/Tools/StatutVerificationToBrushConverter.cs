using gestadh45.model;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace gestadh45.wpf.Tools
{
	public class StatutVerificationToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (targetType != typeof(Brush)) {
				throw new InvalidOperationException("The target must be a Brush");
			}

			var result = Brushes.Black;

			if (value is StatutVerification) {
				switch ((StatutVerification)value) {
					case StatutVerification.AVerifier:
						result = Brushes.Orange;
						break;

					case StatutVerification.Ok:
						result = Brushes.Green;
						break;

					case StatutVerification.Rebut:
						result = Brushes.Red;
						break;

					default:
						result = Brushes.Black;
						break;
				}
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
