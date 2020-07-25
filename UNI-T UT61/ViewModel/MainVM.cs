/****************************** Module Header ******************************\
Module Name:  
Project:      -
Author:       Pavel Andreev

\***************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_T_UT61.Model;
using UNI_T_UT61.View;
using UNI_T_UT61.ViewModel.Commands;

namespace UNI_T_UT61.ViewModel
{
    #region Delegates
    #endregion

    class MainVM : INotifyPropertyChanged
    {
        #region Events
        #endregion

        #region Fields
        #endregion

        #region Constructor
        public MainVM()
        {
            ComList = new ObservableCollection<string>(SerialPort.GetPortNames());
            OpenConnectionSettingsWindow = new RelayCommand(o => { OpenConnectionSettingAction(); }, o => true);
            SelectPortCommand = new RelayCommand((str) => { SelectPort(str); }, o => true);
            DisconnectCommand = new RelayCommand(o => { Disconnect(); }, o => true);

            uT61 = new UT61();
            uT61.ParsedReadingRecivedEvent += UT61Callback;

        }

        private void UT61Callback(object o, ReadingArgs e)
        {
            Reading = e.Value;
            Unit7SymbolVisibilityByte = (byte)e.Units7Symbol;
            Unit8SymbolVisibilityByte = (byte)e.Units8Symbol;
            UnitExponenetSymbolVisibilityByte = (byte)e.UnitExponent;
            UnitSymbolVisibilityByte = (byte)e.Units;
            //switch (e.UnitExponent)
            //{
            //    //[9]	    Symbole		    µ	    m	    k	    M	    Beeps	Diode	%	    0
            //    case UnitExponent.Diode:
            //        UnitExponenetSymbolVisibilityByte = 0b0000_0100;
            //        break;
            //    case UnitExponent.Kilo:
            //        UnitExponenetSymbolVisibilityByte = 0b0010_0000;
            //        break;
            //    case UnitExponent.Mega:
            //        UnitExponenetSymbolVisibilityByte = 0b0001_0000;
            //        break;
            //    case UnitExponent.Micro:
            //    case UnitExponent._Micro:
            //        UnitExponenetSymbolVisibilityByte = 0b1000_0000;
            //        break;
            //    case UnitExponent.Mili:
            //        UnitExponenetSymbolVisibilityByte = 0b0100_0000;
            //        break;
            //    case UnitExponent.Beeps:
            //        UnitExponenetSymbolVisibilityByte = 0b0000_1000;
            //        break;
            //    case UnitExponent.Precent:
            //        UnitExponenetSymbolVisibilityByte = 0b0000_0010;
            //        break;
            //    case UnitExponent.Blank:
            //    case UnitExponent.Unknown:
            //    default:
            //        UnitExponenetSymbolVisibilityByte = 0b0000_0000;
            //        break;
            //}

            //switch (e.Units)
            //{
            //    //[10]	Symbole		    V	    A	    Ohm	    0	    Hz	    F	    °C	    °F
            //    case Units.Amperes:
            //        UnitSymbolVisibilityByte = 0b0100_0000;
            //        break;
            //    case Units.Celcius:
            //        UnitSymbolVisibilityByte = 0b0000_0010;
            //        break;
            //    case Units.Faerengheit:
            //        UnitSymbolVisibilityByte = 0b0000_0001;
            //        break;
            //    case Units.Farad:
            //        UnitSymbolVisibilityByte = 0b0000_0100;
            //        break;
            //    case Units.Hz:
            //        UnitSymbolVisibilityByte = 0b0000_1000;
            //        break;
            //    case Units.Ohms:
            //        UnitSymbolVisibilityByte = 0b0010_0000;
            //        break;
            //    case Units.Volts:
            //    case Units._Volts:
            //        UnitSymbolVisibilityByte = 0b1000_0000;
            //        break;
            //    case Units.Zero:
            //    default:
            //        UnitSymbolVisibilityByte = 0b0001_0000;
            //        break;
            //}

            
        }
        #endregion

        #region Properties
        public RelayCommand OpenConnectionSettingsWindow { get; set; }
        public RelayCommand SelectPortCommand { get; set; }
        public RelayCommand DisconnectCommand { get; set; }

        UT61 uT61;

        private string reading;

        public string Reading
        {
            get { return reading; }
            set { reading = value; OnPropertyChange("Reading"); }
        }

        private byte unit7SymbolVisibilityByte;

        public byte Unit7SymbolVisibilityByte
        {
            get { return unit7SymbolVisibilityByte; }
            set { unit7SymbolVisibilityByte = value; OnPropertyChange("Unit7SymbolVisibilityByte"); }
        }

        private byte unit8SymbolVisibilityByte;

        public byte Unit8SymbolVisibilityByte
        {
            get { return unit8SymbolVisibilityByte; }
            set { unit8SymbolVisibilityByte = value; OnPropertyChange("Unit8SymbolVisibilityByte"); }
        }

        private byte unitSymbolVisibilityByte;

        public byte UnitSymbolVisibilityByte
        {
            get { return unitSymbolVisibilityByte; }
            set { unitSymbolVisibilityByte = value; OnPropertyChange("UnitSymbolVisibilityByte"); }
        }

        private byte unitExponenetSymbolVisibilityByte;

        public byte UnitExponenetSymbolVisibilityByte
        {
            get { return unitExponenetSymbolVisibilityByte; }
            set { unitExponenetSymbolVisibilityByte = value; OnPropertyChange("UnitExponenetSymbolVisibilityByte"); }
        }

        private ObservableCollection<String> comList;

        public ObservableCollection<String> ComList
        {
            get { return comList; }
            set { comList = value; OnPropertyChange("ComList"); }
        }

        private string portName;

        public string PortName
        {
            get { return portName; }
            set { portName = value; OnPropertyChange("PortName"); }
        }

        ConnectionWindow connectionWindow;

        #endregion

        #region Methods
        void OpenConnectionSettingAction()
        {
            connectionWindow = new ConnectionWindow();
            connectionWindow.Show();
        }

        void SelectPort(object o)
        {
            PortName = o as string;
            uT61.Connect(PortName);
            connectionWindow?.Close();
        }

        void Disconnect()
        {
            uT61.Disconnect();
            connectionWindow?.Close();
            Reading = "";
            Unit7SymbolVisibilityByte = 0x00;
            Unit8SymbolVisibilityByte = 0x00;
            UnitExponenetSymbolVisibilityByte = 0x00;
            UnitSymbolVisibilityByte = 0x00;
        }
        #endregion  

        #region PropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChange(string iName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(iName));
        }
        #endregion










    }
}
