using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VersaoWPF
{
    [ValueConversion(typeof(object), typeof(String))]
    public class ConverterCPF : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                 
            if (value != null)
            {
                
                string valor = ((string)value).ToString().Trim();
                string retorno;

                retorno = valor.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                return retorno;


            }

            // Retorna o valor que veio
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value != null)
            {
                
                string OldValue = value.ToString().Replace("-", string.Empty).Replace("/", "").Replace(".", "");

                
                return OldValue;
            }
            
            return value;
        }

    }
}
