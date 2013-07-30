using gestadh45.business.Enums;
using System;
using System.Windows.Data;

namespace gestadh45.wpf.Tools
{
	public class CodeUCToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (targetType != typeof(string)) {
				throw new InvalidOperationException("The target must be a string");
			}

			return EnumHelper.GetLibelle((CodesUC)value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
