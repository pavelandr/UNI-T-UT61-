/****************************** Module Header ******************************\
Module Name:  
Project:      -
Author:       Pavel Andreev
Date: 
Description:
Revision: 1.0
Open issues:
\***************************************************************************/


/****************************** Instructions ******************************\
UNI-T UT61B Digital Multi Meter (DVM), measure 2 times a second.
Every measure, a packet is sent.
BaudRate: 2400, Parity:None, DataBits: 8, StopBits:One, RTS:False, DTR:True
Packet is 14 bytes long. Every Packet ends with 0x0D, 0x0A
    (0x0D - Carrige return , 0x0A - New Line)

Byte 0 - Sign Byte  - 0x2B for + , 0x2D for -
Byte 1 - First digit from the left.
Byte 2 - Second digit.
Byte 3 - Third Digit.
Byte 4 - Fourth Digit.
        * If any of the digits will contain "?" mark (0x3f), that means that the DMM is on OVER LOAD. Value 9999 should be given as an output.
        
Byte 5 - Always will be 0x20, meaning "space".
Byte 6 - Divisor
        0x00 - 10e0  /1   |   0x01 - 10e-3  /1000  |   0x02 - 10e-2  /100    |   0x04 - 10e-1   /10


        https://www-user.tu-chemnitz.de/~heha/hs/UNI-T/UT61BCD.LOG
Byte		    Bit	    7       6	    5	    4	    3	    2	    1	    0
[0]	Vorzeichen	        0	    0	    1	    0	    1	    0	    POS	    1	    ASCII '+' oder '-'
[1]			            0	    0	    1	    1	===== Erste Ziffer  =====	        '?' für Leerstelle (bei 0.L)
[2]			            0   	0   	1   	1  	===== Zweite Ziffer =====
[3]			            0	    0	    1	    1	===== Dritte Ziffer =====	':' für "L"
[4]			            0	    0	    1	    1	===== Vierte Ziffer =====
[5]			            0	    0	    1	    0	    0	    0	    0	    0	stets ' ' Leerzeichen (evtl. 5. Ziffer?)
[6]	    Komma-Bitfeld, als Ziffer, '1'=0.000, '2'=00.00, '4'=000.0, '0'=0000
[7]	    Symbole		    0	    0	    AutoDC	AC	    REL	    HOLD	BG
[8]	    Symbole		    0	    0	    MAX	    MIN	    0	    LowBat	n	    0
[9]	    Symbole		    µ	    m	    k	    M	    Beeps	Diode	%	    0
[10]	Symbole		    V	    A	    Ohm	    0	    Hz	    F	    °C	    °F
[11]	Länge der Bargraf-Anzeige, 0..41, Bit 7 = Minuszeichen (der Bargraf-Anzeige)
[12]	0D	\r	0	0	0	0	1	1	0	1
[13]	0A	\n	0	0	0	0	1	0	1	0

\***************************************************************************/
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UNI_T_UT61.Model.Interfaces;


namespace UNI_T_UT61.Model
{
    #region Delegates
    public delegate void ParsedReadingRecivedEvent(object o, ReadingArgs e);
    #endregion



    public class ReadingArgs:EventArgs
    {
        public string Value { get; }
        public byte Units7Symbol { get; }
        public byte Units8Symbol { get; }
        public byte UnitExponent { get; }
        public byte Units { get; }


        public ReadingArgs(string value, Byte units7Symbol, Byte units8Symbol, byte unitExponent, byte units)
        {
            Value = value;
            Units7Symbol = units7Symbol;
            Units8Symbol = units8Symbol;
            UnitExponent = unitExponent;
            Units = units;
        }
    }

    public class UT61 : IUniT
    {
        public string Port { get; set; }

        public bool Connected { get; set; }

        private SerialPort SerialPort { get; set; }

        public event ParsedReadingRecivedEvent ParsedReadingRecivedEvent;

        public void Connect(string port)
        {
            Port = port;
            if (!Connected)
            {
                try
                {
                    SerialPort = new SerialPort(port, baudRate: 2400, Parity.None, dataBits: 8, StopBits.One);
                    SerialPort.RtsEnable = false;
                    SerialPort.DtrEnable = true;
                    SerialPort.DataReceived += SerialPortDataReceivedCallBack;
                    SerialPort.Open();
                    Connected = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public void Disconnect()
        {
            if (Connected)
            {
                SerialPort?.Close();
                SerialPort?.Dispose();
                Connected = false;

            }
        }

        private void SerialPortDataReceivedCallBack(object sender, SerialDataReceivedEventArgs e)
        {
            string data = SerialPort.ReadLine();
            PasreRecievedData(data);
        }

        /// <summary>
        /// Parse derial Data
        /// </summary>
        /// <param name="data"></param>
        private void PasreRecievedData(string data)
        {
            string value = string.Empty;
            byte _Units7Symbol;
            byte _Units8Symbol;
            byte _UnitExponent;
            byte _Units;
            if (data.Length == 13)
            {
                //Parse value sign 0x2B is '+' and 0x2D is '-'
                if (data[0] == 0x2D)
                    value += '-';

                //Check if Over Voltage
                if (data.Substring(1, 4).Equals("?0:?"))
                    value = "OL.";
                else // if not parse the value (byte 1 to 4) along with  Comma-Bitfeld (byte 6)
                {
                    float.TryParse(data.Substring(1, 4), out float result);
                    switch ((0x0F)&((byte)data[6]))
                    {
                        case 0x01:
                            value += (result / 1000).ToString("0.000");
                            break;
                        case 0x02:
                            value += (result / 100).ToString("00.00");
                            break;
                        case 0x04:
                            value += (result / 10).ToString("000.0");
                            break;
                        default:
                            value += result.ToString("0000");
                            break;

                    }
                }

                _Units7Symbol =(byte) data[7];

                _Units8Symbol = (byte)data[8];

                //Parse unit exponent  µ,m,k,M,Beeps,Diode,%,0
                _UnitExponent = (byte)data[9];

                //Parse units  V,A,Ohm,0,Hz,F,°C,°F
                _Units = (byte)data[10];
                ParsedReadingRecivedEvent?.Invoke(this, new ReadingArgs(value, _Units7Symbol, _Units8Symbol, _UnitExponent, _Units));

                //if (data[9] == 0x04)  // Diode
                //{
                //    if (data.Substring(1, 4).Equals("?0:?"))
                //    {
                //        iValue = 9999;
                //        sValue = "O.L";
                //    }
                //    else
                //    {
                //        int.TryParse(data.Substring(1, 4), out iValue);
                //        sValue = iValue.ToString();
                //    }
                   
                //}
            }
        }

    }
}
