using gestadh45.model;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace gestadh45.wpf.Tools
{
	public class StatutInscriptionToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (targetType != typeof(Brush)) {
				throw new InvalidOperationException("The target must be a Brush");
			}

			var result = Brushes.Black;

			if (value is StatutInscription) {
				switch ((StatutInscription)value) {
					case StatutInscription.ASuivre:
						result = Brushes.Orange;
						break;

					case StatutInscription.Validee:
						result = Brushes.Green;
						break;

					case StatutInscription.Annulee:
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
