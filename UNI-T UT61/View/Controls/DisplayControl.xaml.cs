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

namespace UNI_T_UT61.View.Controls
{
    /// <summary>
    /// Interaction logic for DisplayControl.xaml
    /// </summary>
    public partial class DisplayControl : UserControl
    {
        public DisplayControl()
        {
            InitializeComponent();
        }
        /*
         * 
         *         DC = 0b0001_0000,
        AC = 0b0000_1000,
        REL = 0b000_0100,
        HOLD = 0b0000_0010,
        */
        public bool DC
        {
            get { return (bool)GetValue(DCProperty); }
            set { SetValue(DCProperty, value); }
        }

        public bool AC
        {
            get { return (bool)GetValue(ACProperty); }
            set { SetValue(ACProperty, value); }
        }

        public bool REL
        {
            get { return (bool)GetValue(RELProperty); }
            set { SetValue(RELProperty, value); }
        }

        public bool HOLD
        {
            get { return (bool)GetValue(HOLDProperty); }
            set { SetValue(HOLDProperty, value); }
        }

        public string Value
        {
            get { return ValueTextBlock.Text; }
            set { ValueTextBlock.Text = value.ToString();}
        }

        public bool MAX
        {
            get { return (bool)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public bool MIN
        {
            get { return (bool)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public bool Beeps
        {
            get { return (bool)GetValue(BeepsProperty); }
            set { SetValue(BeepsProperty, value); }
        }

        public bool Diode
        {
            get { return (bool)GetValue(DiodeProperty); }
            set { SetValue(DiodeProperty, value); }
        }

        public bool Nano
        {
            get { return (bool)GetValue(NanoProperty); }
            set { SetValue(NanoProperty, value); }
        }
        public bool Mili
        {
            get { return (bool)GetValue(MiliProperty); }
            set { SetValue(MiliProperty, value); }
        }
        public bool Micro
        {
            get { return (bool)GetValue(MicroProperty); }
            set { SetValue(MicroProperty, value); }
        }


        public bool Volt
        {
            get { return (bool)GetValue(VoltProperty); }
            set { SetValue(VoltProperty, value); }
        }
        public bool Amper
        {
            get { return (bool)GetValue(AmperProperty); }
            set { SetValue(AmperProperty, value); }
        }
        public bool Farad
        {
            get { return (bool)GetValue(FaradProperty); }
            set { SetValue(FaradProperty, value); }
        }

        public bool Fahrenheit
        {
            get { return (bool)GetValue(FahrenheitProperty); }
            set { SetValue(FahrenheitProperty, value); }
        }
        public bool Celsius
        {
            get { return (bool)GetValue(CelsiusProperty); }
            set { SetValue(CelsiusProperty, value); }
        }
        public bool Precent
        {
            get { return (bool)GetValue(PrecentProperty); }
            set { SetValue(PrecentProperty, value); }
        }


        public bool Hz
        {
            get { return (bool)GetValue(HzProperty); }
            set { SetValue(HzProperty, value); }
        }
        public bool Ohm
        {
            get { return (bool)GetValue(OhmProperty); }
            set { SetValue(OhmProperty, value); }
        }

        public bool Kilo
        {
            get { return (bool)GetValue(KiloProperty); }
            set { SetValue(KiloProperty, value); }
        }

        public bool Mega
        {
            get { return (bool)GetValue(MegaProperty); }
            set { SetValue(MegaProperty, value); }
        }


        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(string), typeof(DisplayControl));

        public static readonly DependencyProperty ACProperty =
           DependencyProperty.Register(nameof(AC), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty DCProperty =
            DependencyProperty.Register(nameof(DC), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty RELProperty =
            DependencyProperty.Register(nameof(REL), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty HOLDProperty =
            DependencyProperty.Register(nameof(HOLD), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register(nameof(MAX), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register(nameof(MIN), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty BeepsProperty =
            DependencyProperty.Register(nameof(Beeps), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty DiodeProperty =
            DependencyProperty.Register(nameof(Diode), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty NanoProperty =
            DependencyProperty.Register(nameof(Nano), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty MiliProperty =
            DependencyProperty.Register(nameof(Mili), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty MicroProperty =
            DependencyProperty.Register(nameof(Micro), typeof(bool), typeof(DisplayControl));


        public static readonly DependencyProperty VoltProperty =
            DependencyProperty.Register(nameof(Volt), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty AmperProperty =
            DependencyProperty.Register(nameof(Amper), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty PrecentProperty =
            DependencyProperty.Register(nameof(Precent), typeof(bool), typeof(DisplayControl));

        public static readonly DependencyProperty FahrenheitProperty =
            DependencyProperty.Register(nameof(Fahrenheit), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty CelsiusProperty =
            DependencyProperty.Register(nameof(Celsius), typeof(bool), typeof(DisplayControl));
        public static readonly DependencyProperty FaradProperty =
            DependencyProperty.Register(nameof(Farad), typeof(bool), typeof(DisplayControl));


        public static readonly DependencyProperty HzProperty =
            DependencyProperty.Register(nameof(Hz), typeof(bool), typeof(DisplayControl));

        public static readonly DependencyProperty OhmProperty =
            DependencyProperty.Register(nameof(Ohm), typeof(bool), typeof(DisplayControl));

        public static readonly DependencyProperty KiloProperty =
            DependencyProperty.Register(nameof(Kilo), typeof(bool), typeof(DisplayControl));

        public static readonly DependencyProperty MegaProperty =
            DependencyProperty.Register(nameof(Mega), typeof(bool), typeof(DisplayControl));

    }
}
