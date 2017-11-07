using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Soroban.ViewModels;

namespace Soroban
{
    public class OperatorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Operator op)
            {
                switch (op)
                {
                    case Operator.None: return string.Empty;
                    case Operator.Plus: return "+";
                    case Operator.Minus: return "-";
                    case Operator.Multiply: return "×";
                    case Operator.Divide: return "÷";
                }
            }

            return "?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
