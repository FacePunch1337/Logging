using System;
using System.IO;
using System.Text;
using NLog;


namespace Logging
{
    internal class Program
    {
        private static NLog.Config.LoggingConfiguration config;

        private static Logger logger;


        static void Main(string[] args)
        {
            logger = NLog.LogManager.GetCurrentClassLogger();

            Console.Write("Enter file name: ");
            String path = Console.ReadLine();
            Console.Write("\n");

            try
            {
                using (FileStream fstream = File.OpenRead(path))
                {
                    byte[] data = new byte[fstream.Length];
                    fstream.Read(data, 0, data.Length);
                    // декодируем байты в строку
                    string textFromFile = Encoding.Default.GetString(data);
                    Console.WriteLine($"File data: {textFromFile}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                Console.WriteLine("файл не найден.");
            }

        }




        static void Main2(string[] args)
        {

            logger = NLog.LogManager.GetCurrentClassLogger();



            Console.Write("Enter number: ");
            String str = Console.ReadLine();
            try
            {
                Console.WriteLine($"2 x {str} = {2 * Convert.ToInt32(str)}");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString(), str);
                Console.WriteLine("error");
            }


        }


        static void Main_Runtime_Config(string[] args)
        {
            config = new NLog.Config.LoggingConfiguration();

            var con = new NLog.Targets.ConsoleTarget("con");
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, con);

            var file = new NLog.Targets.FileTarget("file")
            {
                FileName = "log.txt"
            };

            config.AddRule(LogLevel.Trace, LogLevel.Fatal, file);

            NLog.LogManager.Configuration = config;

            logger = NLog.LogManager.GetCurrentClassLogger();



            logger.Trace("Trace msg");
            NLog.LogManager.Shutdown();


        }

    }
}
