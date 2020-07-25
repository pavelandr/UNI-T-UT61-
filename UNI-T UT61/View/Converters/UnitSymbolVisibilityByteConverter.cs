using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UNI_T_UT61.View.Converters
{
    public class Unit7SymbolVisibilityByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string type = (string)parameter;
            if (System.Convert.ToBoolean((byte)value & 0b0010_0000) && type == "Auto")
                return true;
            if (System.Convert.ToBoolean((byte)value & 0b0001_0000) && type == "DC")
                return true;
            if (System.Convert.ToBoolean((byte)value & 0b0000_1000) && type == "AC")
                return true;
            if (System.Convert.ToBoolean((byte)value & 0b0000_0100) && type == "REL")
                return true;
            if (System.Convert.ToBoolean((byte)value & 0b0000_0010) && type == "HOLD")
                return true;
            if (System.Convert.ToBoolean((byte)value & 0b0000_0001) && type == "BG")
                return true;
            else return false;

            //[7]	    Symbole		    0	    0	    Auto    DC	AC	    REL	    HOLD	BG
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // If necessary, here you can convert back. Check if which brush it is (if its one),
            // get its Color-value and return it.

            throw new NotImplementedException();
        }
    }

    public class Unit8SymbolVisibilityByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string type = (string)parameter;
            if (System.Convert.ToBoolean((byte)value == 0b0000_0000))
                return false;
            if (System.Convert.ToBoolean((byte)value & 0b0010_0000) && type == "MAX")
                return true;
            if (System.Convert.ToBoolean((byte)value & 0b0001_0000) && type == "MIN")
                return true;
            if (System.Convert.ToBoolean((byte)value & 0b0000_0100) && type == "LowBat")
                return true;
            if (System.Convert.ToBoolean((byte)value & 0b0000_0010) && type == "n")
                return true;
            else return false;

            //[8]	    Symbole		    0	    0	    MAX	    MIN	    0	    LowBat	n	    0
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // If necessary, here you can convert back. Check if which brush it is (if its one),
            // get its Color-value and return it.

            throw new NotImplementedException();
        }
    }


    public class UnitSymbolVisibilityByteConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string type = (string)parameter;
            if (System.Convert.ToBoolean((byte)value == 0b1000_0000) && type == "Volt")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0011_1111) && type == "Volt")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0100_0000) && type == "Amper")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0010_0000) && type == "Ohm")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0000_1000) && type == "Hz")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0000_0100) && type == "Farad")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0000_0010) && type == "Celsius")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0000_0001) && type == "Fahrenheit")
                return true;
            else return false;

            //[10]	Symbole		    V	    A	    Ohm	    0	    Hz	    F	    °C	    °F
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // If necessary, here you can convert back. Check if which brush it is (if its one),
            // get its Color-value and return it.

            throw new NotImplementedException();
        }
    }

    public class UnitExponentSymbolVisibilityByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string type = (string)parameter;
            if (System.Convert.ToBoolean((byte)value == 0b0011_1111) && type == "Micro")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0100_0000) && type == "Mili")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0010_0000) && type == "Kilo")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0001_0000) && type == "Mega")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0000_1000) && type == "Beeps")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0000_0100) && type == "Diode")
                return true;
            if (System.Convert.ToBoolean((byte)value == 0b0000_0010) && type == "Precent")
                return true;
            else return false;

            //[9]	    Symbole		    µ	    m	    k	    M	    Beeps	Diode	%	    0
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // If necessary, here you can convert back. Check if which brush it is (if its one),
            // get its Color-value and return it.

            throw new NotImplementedException();
        }
    }
}
