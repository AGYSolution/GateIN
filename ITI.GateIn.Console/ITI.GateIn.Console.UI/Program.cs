using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateIn.Console.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InputDataGate();
        }

        static void InputDataGate()
        {
            var terminal = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
            //var terminal = new Terminal("192.168.43.99", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height

            long contCardID;
            string location;

            System.Console.WriteLine("---");
            System.Console.Write("Please input container card ID: ");
            contCardID = Convert.ToInt64(System.Console.ReadLine());

            var vehicle = DAL.GateINDal.CheckKendaraan(contCardID);
            if (vehicle == null)
            {
                PushCommand.PushER(terminal);
                System.Console.WriteLine("Data Kendaraan tidak ditemukan!");
            }
            else
            {
                List<DAL.ContCard> data = new List<DAL.ContCard>();
                data.Add(vehicle);
                System.Console.WriteLine("Data Kendaraan:");
                System.Console.WriteLine(data.ToStringTable(
                    new[] { "ID ContCard", "Card Mode", "Ref Mode", "Cont Count", "Cont Size", "Cont Type" },
                    a => a.ContCardID, a => a.CardMode, a => a.RefID, a => a.Cont, a => a.Size, a => a.Type));
                System.Console.WriteLine("---");
                System.Console.Write("Please input location: ");
                location = System.Console.ReadLine();
                if (DAL.GateINDal.UpdateContCardGateIn(contCardID, location))
                {
                    System.Console.WriteLine("Data Kendaraan berhasil diupdate!");
                    System.Console.WriteLine("Press enter to continue..");
                    System.Console.ReadLine();
                    PushCommand.PushOK(terminal);
                }
                else
                {

                    System.Console.WriteLine("Data Kendaraan gagal diupdate!");
                    System.Console.WriteLine("Press enter to continue..");
                    System.Console.ReadLine();
                    PushCommand.PushER(terminal);
                }
            }


            InputDataGate();
        }
    }
}
