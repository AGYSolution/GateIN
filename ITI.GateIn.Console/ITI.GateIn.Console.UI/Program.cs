using ITI.GateIn.Console.DAL;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace ITI.GateIn.Console.UI
{
    class Program
    {
        public static string _SecureGateLocName = "DefaultSGate";
        public static string _SecureGateTelnetAddress = string.Empty;
        public static string _SecureGateTelnetPort = string.Empty;

        public static string _CaptureImageExeFile = "";
        public static string _LastProcessedInput = "";
        public static string _CaptureFile = "";

        static void Main(string[] args)
        {

            SecureGateLog log = new SecureGateLog();
            _SecureGateTelnetAddress = ConfigurationSettings.AppSettings["TelnetAddress"];
            _SecureGateTelnetPort = ConfigurationSettings.AppSettings["TelnetPort"];
            _SecureGateLocName = ConfigurationSettings.AppSettings["sgatelocname"];
            _CaptureImageExeFile = ConfigurationSettings.AppSettings["sgatecaptureexefile"];
            _CaptureFile = ConfigurationSettings.AppSettings["capturefile"];
            if ((_CaptureImageExeFile == null) || (_CaptureImageExeFile == string.Empty))
            {
                _CaptureImageExeFile = @"capture\capture.exe";
            }
            if ((_CaptureFile == null) || (_CaptureFile == string.Empty))
            {
                _CaptureFile = @"capture\capture.jpg";
            }

            System.Console.WriteLine("welcome to Secure Gate In System " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ", the Secure Gate In Terminal." + System.Console.Out.NewLine + System.Console.Out.NewLine + @"type: \c for copyright info" + System.Console.Out.NewLine + @"    : \h for help with Secure Gate IN command" + System.Console.Out.NewLine + @"    : \q to quit" + System.Console.Out.NewLine + System.Console.Out.NewLine + "warning: make sure barcode scanner & impact printer are connected and " + System.Console.Out.NewLine + "working properly. See Secure Gate IN manual page \"Installation and Setup\" for " + System.Console.Out.NewLine + "instruction.");
            string str = ConfigurationSettings.AppSettings["loginserver"];
            if (string.IsNullOrEmpty(str))
            {
                str = "localhost";

            }
            System.Console.WriteLine(System.Console.Out.NewLine + System.Console.Out.NewLine + "info: Secure Gate IN location: " + _SecureGateLocName + System.Console.Out.NewLine + "info: logging in..." + System.Console.Out.NewLine + "server: '" + str + "'");
            Random random = new Random();

            Label_0271:
            try
            {

                //login logic
                var terminal = new Terminal(_SecureGateTelnetAddress, Convert.ToInt32(_SecureGateTelnetPort), 10, 80, 40); // hostname, port, timeout [s], width, height
                terminal.Connect();//tambahan grafik 17-04-2018
                System.Console.Write("info: logged in" + System.Console.Out.NewLine + "info: waiting for input..." + System.Console.Out.NewLine);
                string input = string.Empty;
                bool flag2 = false;
                do
                {
                    System.Console.Write("SCGIN> ");
                    //if (string.IsNullOrEmpty(input)) input = terminal.getResponse();//tambahan grafik 17-04-2018
                    int countLoop = 0;
                    Label_0101:
                    for (countLoop = 0; countLoop <= 50; countLoop++)
                    {
                        input = terminal.getResponse();
                        
                    }
                    if (countLoop > 50)
                        goto Label_0102; 
                    if (string.IsNullOrEmpty(input))
                        goto Label_0101;
                    else
                    {
                        System.Console.ReadLine();
                    }
                    Label_0102:
                    System.Console.WriteLine("Manually Input Container Card Number Secure Gate IN terminal " +
                        System.Console.Out.NewLine + @"type: \r to to retry process get barcode from System"
                                                   );
                    input = System.Console.ReadLine();
                    //if (string.IsNullOrEmpty(input)) input = terminal.getResponse();//tambahan grafik 17-04-2018
                    flag2 = false;
                    switch (input.ToLower())
                    {
                        case @"\r":
                            countLoop = 0;
                            goto Label_0101;

                        case @"\c":
                            System.Console.WriteLine(System.Console.Out.NewLine + "Secure Gate IN terminal"
                                                    + System.Console.Out.NewLine
                                                    + System.Console.Out.NewLine + "Copyright (c) 2018, Intercon International Terminal. All rights reserved."
                                                    + System.Console.Out.NewLine
                                                    + System.Console.Out.NewLine + "Portions Copyright (c) 2018, AGY Solutions."
                                                    + System.Console.Out.NewLine);
                            break;

                        case @"\q":
                        case "quit":
                        case "exit":
                            flag2 = true;
                            break;

                        case @"\o":
                        case "openup":
                        case "open":
                            try
                            {
                                OpenGate("openup command", log, new ContCard(), terminal);
                            }
                            catch (Exception exception)
                            {
                                System.Console.WriteLine("error: " + exception.Message);
                                _LastProcessedInput = string.Empty;
                            }
                            break;

                        case @"\h":
                        case @"\?":
                        case "help":
                        case "?":
                            System.Console.WriteLine(System.Console.Out.NewLine + "type: {container card number}[terminal code] scan container card barcode or " + System.Console.Out.NewLine + "enter the number manually" + System.Console.Out.NewLine + @"    : \o to open the gate" + System.Console.Out.NewLine + @"    : \c for copyright info" + System.Console.Out.NewLine + @"    : \h for help with SCGIN command" + System.Console.Out.NewLine + @"    : \q to quit" + System.Console.Out.NewLine);
                            break;

                        default:
                            try
                            {
                                ProcessInput(input, log, terminal);
                            }
                            catch (Exception exception2)
                            {
                                System.Console.WriteLine("error: " + exception2.Message);
                                _LastProcessedInput = string.Empty;
                            }
                            break;
                    }
                }
                while (!flag2);
                System.Console.WriteLine("info: logging out...");
                System.Console.WriteLine("info: logged out");
                terminal.Close();
                return;
            }
            catch (Exception)
            {
                System.Console.WriteLine("error: unable to login / connect to Gate");
                if (random.Next(2) > 0)
                {
                    System.Console.WriteLine("info: make sure the database server is online and configured correctly. "
                                            + System.Console.Out.NewLine + "See SCGIN manual page \"Installation and Setup\" for details.");
                }
                else
                {
                    System.Console.WriteLine("info: make sure the Secure Gate IN is configured properly. "
                                            + System.Console.Out.NewLine + "See SCGIN manual page \"Installation and Setup\" for instruction.");
                }
                System.Console.WriteLine(System.Console.Out.NewLine + @"type: \c to connect"
                                            + System.Console.Out.NewLine + @"    : \h for help with SCGIN command"
                                            + System.Console.Out.NewLine + @"    : \q to quit"
                                            + System.Console.Out.NewLine);
            }
            string str2 = string.Empty;
            bool flag = false;
            Label_0340:
            System.Console.Write("SCGIN> ");
            str2 = System.Console.ReadLine();
            flag = false;
            string str4 = str2.ToLower();
            if (str4 != null)
            {
                if (str4 != @"\c")
                {
                    if (str4 == @"\q")
                    {
                        return;
                    }
                    if ((str4 == @"\h") || (str4 == @"\?"))
                    {
                    }
                }
                else
                {
                    flag = true;
                    goto Label_0403;
                }
            }
            System.Console.WriteLine(System.Console.Out.NewLine + @"type: \c to connect"
                                    + System.Console.Out.NewLine + @"    : \h for help with SCGIN command"
                                    + System.Console.Out.NewLine + @"    : \q to quit"
                                    + System.Console.Out.NewLine);
            Label_0403:
            if (!flag)
            {
                goto Label_0340;
            }
            goto Label_0271;
        }

        private static void OpenGate(string openedBy, SecureGateLog log, ContCard contCard, Terminal terminal)
        {
            try
            {
                if (_CaptureImageExeFile.Length > 0)
                {
                    System.Console.WriteLine("info: capturing container picture...");
                    Process.Start(_CaptureImageExeFile);
                    System.Console.WriteLine("info: container picture captured");
                    Thread.Sleep(1000);
                }
                else
                {
                    System.Console.WriteLine("warning: container picture not captured");
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("error: " + exception.Message);
            }
            try
            {
                PushCommand.PushOK(terminal);

                Thread.Sleep(1000);


            }
            catch (Exception exception2)
            {
                System.Console.WriteLine("error: " + exception2.Message);
            }
            try
            {
                SecureGateLogDAL logDal = new SecureGateLogDAL();
                log.Loc1 = _SecureGateLocName;
                log.LogCat = "GATE OPEN EVENT";
                log.LogRemark = openedBy;
                log.RefID = contCard.ContCardID;
                if (log.SecureGateLogID <= 0)
                {
                    logDal.DeleteSecureGateLog(log);
                    logDal.InsertSecureGateLog(log);
                }
                else
                    logDal.UpdateSecureGateLog(log);
            }
            catch (Exception exception3)
            {
                System.Console.WriteLine("error: " + exception3.Message);
            }
        }

        private static void ProcessInput(string input, SecureGateLog log, Terminal terminal)
        {
            if (input.Length > 0)
            {
                System.Console.WriteLine("container card: " + input);
                _LastProcessedInput = input;
                ContCard contCard = new ContCard();
                ContCardDal cardDAL = new ContCardDal();
                contCard = cardDAL.GetContCardByContCardId(Convert.ToInt64(input));
                if (contCard.ContCardID <= 0)
                {
                    System.Console.WriteLine("INPUT NOT RECOGNIZED !!!");
                }
                else if (contCard.Dtm1.Length == 0)
                {
                    //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    contCard.Dtm1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    contCard.Loc1 = _SecureGateLocName;
                    cardDAL.UpdateContCardGateIn(contCard.ContCardID, contCard.Loc1);

                    System.Console.WriteLine("DTM1 USED");
                    OpenGate(input, log, contCard, terminal);

                    if ((_CaptureFile.Length > 0) && File.Exists(_CaptureFile))
                    {
                        FileStream stream = File.OpenRead(_CaptureFile);
                        BinaryReader reader = new BinaryReader(stream);
                        ContCardPic pic = new ContCardPic();
                        ContCardPICDal contCardPICDal = new ContCardPICDal();

                        pic.ContCardID = contCard.ContCardID;
                        pic.PicName = "IN";
                        pic.PicData = reader.ReadBytes((int)stream.Length);
                        reader.Close();
                        stream.Close();
                        contCardPICDal.InsertContCardPIC(pic);
                        File.Move(_CaptureFile, string.Concat(new object[] { _CaptureFile, ".", CtsCounter.NextValCtsCounter("CONTCARDPICIN_SEQ"), ".jpg" }));
                        System.Console.WriteLine("Picture Captured To Database.");
                    }
                }
                else
                {
                    System.Console.WriteLine("INPUT IS USED UP OR INVALID !!!");
                }
            }
        }
    }
}
