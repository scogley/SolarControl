using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
//required to azure enable mobile services
//requires nuget package console Install-Package WindowsAzure.MobileServices -Version 1.2.1
//this enables client connection to Azure Mobile Web Service. see this: https://www.nuget.org/packages/WindowsAzure.MobileServices/1.2.1
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json; //used for JSON .net reference here: http://json.codeplex.com/


namespace MotionSerialTalker
{
    class Program
    {
        static void Main(string[] args)
        {           
            // Arduino Mega 2560 defaults to COM4 on my PC
            // serial port sample code: http://msdn.microsoft.com/en-us/library/system.io.ports.serialport.datareceived(v=vs.110).aspx
            //Console.WriteLine("enter serial port number for Arduino");
            //string serialPort = Console.ReadLine();
            //serialPort = "COM" + serialPort;
            //SerialPort mySerialPort = new SerialPort(serialPort);
            SerialPort mySerialPort = new SerialPort("COM3");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mySerialPort.Open();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Solar Monitor: Reading analog port 0 for voltage generated from the Sun");
            Console.WriteLine("Press any key to quit...");
            Console.WriteLine();
            Console.ReadKey();
            mySerialPort.Close();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            string time = DateTime.Now.ToString("h:mm:ss");            
            Console.Write(indata);
                                                          
            //string id = "seancogley";
            //AzureMobileServicesClient client = new AzureMobileServicesClient();
            //client.UpdateAzureTable(time, id);
        }
    }
}
