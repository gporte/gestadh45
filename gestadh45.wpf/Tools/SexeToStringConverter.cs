﻿using gestadh45.model;
using System;
using System.Windows.Data;

namespace gestadh45.wpf.Tools
{
	public class SexeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (targetType != typeof(string)) {
				throw new InvalidOperationException("The target must be a string");
			}

			return Adherent.GetLibelleSexe((Sexe)value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
