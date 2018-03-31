using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateIn.Console.UI
{
    public class PushCommand
    {

        public bool  PushOK(Terminal paramTerminal )
        {
            bool result = false;
            //if using paramterminal remark below syntac
            var tn = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
            tn.Connect(); // physcial connection
            do
            {
                tn.SendResponse("<ACTION>OK!</ACTION>", true); // send dir command
                result = true;
            }
            while (false);

            //if using paramterminal remark below syntac
            tn.Close(); // physically close on TcpClient
            return result;
        }
        public bool PushER(Terminal paramTerminal)
        {
            bool result = false;
            //if using paramterminal remark below syntac
            var tn = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
            tn.Connect(); // physcial connection
            do
            {
                tn.SendResponse("<ACTION>ER!</ACTION>", true); // send dir command
                result = true;
            }
            while (false);

            //if using paramterminal remark below syntac
            tn.Close(); // physically close on TcpClient
            return result;
        }

    }
}
