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
            SerialPort mySerialPort = new SerialPort("COM4");

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
            //Console.WriteLine("Data Received:");            
            Console.Write(indata);
            
            //Console.WriteLine("Motion Detected at: " + time);                       
                        
                                  
            string id = "seancogley";
            AzureMobileServicesClient client = new AzureMobileServicesClient();
            client.UpdateAzureTable(time, id);
        }

        //private static void SpeakConsoleText(string speakThis)
        //{
        //    SpeechSynthesizer synth = new SpeechSynthesizer();
        //    //synth.SelectVoice("Microsoft Hazel Desktop");
        //    synth.SelectVoice("Microsoft David Desktop");
        //    //synth.SelectVoice("Microsoft Zira Desktop");
        //    //var voices = synth.GetInstalledVoices();
        //    //foreach (var voice in voices)
        //    //{
        //    //    Console.WriteLine(voice.ToString());
        //    //}

        //    //string speakThis = Console.ReadLine();                        
        //    //string speakThis = "Perimeter has been compromised. Lock on phasers and fire at will!!!!";
        //    //string speakThis = "I see you!";                        
        //    //string speakThis = "Intruder alert!!!! Activating defense protocol Alpha 1.";
        //    //string speakThis = "Halt! Who goes there?";
            
        //    synth.Speak(speakThis);

        //}

        //private static void launchPandora()
        //{
            
        //    Process[] pname = Process.GetProcessesByName("iexplore");
        //    if (pname.Length == 0)
        //    {
        //        string speakThis = "Greetings, master! Welcome Home. It is currently " + DateTime.Now.ToString("h:mm");
        //        SpeakConsoleText(speakThis);
        //        //speakThis = "Shawn loves you very much and asked me to play some nice music for you. Enjoy!";
        //        speakThis = "I have a message for you from Shawn. Rosa, I love your sexy petite little body and I want to fuck your brains out tonight!";
        //        SpeakConsoleText(speakThis);
        //        //System.Threading.Thread.Sleep(3000);
        //        Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe");
        //    }   
        //    else
        //    {
        //        Console.WriteLine("Pandora is already running, going back to sleep");
        //    }
        //}
    }
}
