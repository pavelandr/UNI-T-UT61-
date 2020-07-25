using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_T_UT61.Model.Interfaces
{
    public interface IUniT
    {
        string Port { get; set; }
        bool Connected { get; set; }

        void Connect(string port);
        void Disconnect();

    }
}
