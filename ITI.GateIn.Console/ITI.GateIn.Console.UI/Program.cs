using AGY.Solution.Helper.Common;
using ITI.GateIn.Console.DAL;
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
            GateService.GateServicesSoapClient service = new GateService.GateServicesSoapClient();

            //    string userid;
            //    string password;

            //STARTED:
            //    System.Console.WriteLine("--- Login ---");
            //    System.Console.Write("Please input username: ");
            //    userid = System.Console.ReadLine();
            //    System.Console.Write("Please input password: ");
            //    password = System.Console.ReadLine();
            //    if (!service.Login(userid, password))
            //    {
            //        goto STARTED;
            //    }

            System.Console.WriteLine("--- Welcome to gate app ---");
            InputDataGate(service);
        }

        static void InputDataGate(GateService.GateServicesSoapClient service)
        {
            var terminal = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
            //var terminal = new Terminal("192.168.43.99", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height

            long contCardID;
            string location;

            System.Console.Write("Please input container card ID: ");
            contCardID = Convert.ToInt64(System.Console.ReadLine());

            //var vehicle = DAL.GateINDal.CheckKendaraan(contCardID);
            var vehicle = service.CheckKendaraan(contCardID);
            if (vehicle.Length == 0 || vehicle.Contains("Error"))
            {
                PushCommand.PushER(terminal);
                System.Console.WriteLine("Data Kendaraan tidak ditemukan!");
            }
            else
            {
                var dataSetContCard = Converter.ConvertXmlToDataSet(vehicle);
                var data = dataSetContCard.Tables[0].ToList<ContCard>();
                System.Console.WriteLine("Data Kendaraan:");
                System.Console.WriteLine(data.ToStringTable(
                    new[] { "ID ContCard", "Card Mode", "Ref Mode", "Cont Count", "Cont Size", "Cont Type" },
                    a => a.ContCardID, a => a.CardMode, a => a.RefID, a => a.Cont, a => a.Size, a => a.Type));
                System.Console.WriteLine("---");
                System.Console.Write("Please input location: ");
                location = System.Console.ReadLine();
                if (service.UpdateContCardGateIn(contCardID, location))
                {
                    if (service.UpdateContInOutGateIn(contCardID, location))
                    {
                        System.Console.WriteLine("Data Kendaraan berhasil diupdate!");
                        System.Console.WriteLine("Press enter to continue..(OPEN GATE)");
                        System.Console.ReadLine();
                        PushCommand.PushOK(terminal);
                        System.Console.WriteLine("---");
                    }
                }
            }


            InputDataGate(service);
        }
    }
}
