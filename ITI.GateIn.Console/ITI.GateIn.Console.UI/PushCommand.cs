namespace ITI.GateIn.Console.UI
{
    public class PushCommand
    {
        public static bool PushOK(Terminal terminal)
        {
            bool result = false;
            //if using paramterminal remark below syntac
            //var tn = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
            terminal.Connect(); // physcial connection
            do
            {
                terminal.SendResponse("<ACTION>OK!</ACTION>", true); // send dir command
                result = true;
            }
            while (false);

            //if using paramterminal remark below syntac
            terminal.Close(); // physically close on TcpClient
            return result;
        }

        public bool PushOK()
        {
            bool result = false;
            //if using paramterminal remark below syntac
            var terminal = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
            terminal.Connect(); // physcial connection
            do
            {
                terminal.SendResponse("<ACTION>OK!</ACTION>", true); // send dir command
                result = true;
            }
            while (false);

            //if using paramterminal remark below syntac
            terminal.Close(); // physically close on TcpClient
            return result;
        }

        public static bool PushER(Terminal terminal)
        {
            bool result = false;
            //if using paramterminal remark below syntac
            //var tn = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
            terminal.Connect(); // physcial connection
            do
            {
                terminal.SendResponse("<ACTION>ER!</ACTION>", true); // send dir command
                result = true;
            }
            while (false);

            //if using paramterminal remark below syntac
            terminal.Close(); // physically close on TcpClient
            return result;
        }

        public bool PushER()
        {
            bool result = false;
            //if using paramterminal remark below syntac
            var terminal = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
            terminal.Connect(); // physcial connection
            do
            {
                terminal.SendResponse("<ACTION>ER!</ACTION>", true); // send dir command
                result = true;
            }
            while (false);

            //if using paramterminal remark below syntac
            terminal.Close(); // physically close on TcpClient
            return result;
        }

    }
}
