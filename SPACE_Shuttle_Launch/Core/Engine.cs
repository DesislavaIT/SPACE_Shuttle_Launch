namespace SPACE_Shuttle_Launch.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using SPACE_Shuttle_Launch.Core.Contracts;
    using SPACE_Shuttle_Launch.IO;
    using SPACE_Shuttle_Launch.IO.Contracts;
    using SPACE_Shuttle_Launch.Utilities.Messages;

    public class Engine : IEngine
    {
        private IWriter writer;
        private IReader reader;
        private IController controller;
        private StringBuilder log;
        private string outputFile;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new Controller();
            this.log = new StringBuilder();
            outputFile = "WeatherReport.csv";
        }

        public void Run()
        {
            writer.Write("Enter CSV file path: ");
            string filePath = reader.ReadLine();
            writer.Write("Enter sender email address: ");
            string senderEmail = reader.ReadLine();
            writer.Write("Enter sender password: ");
            string senderPassword = reader.ReadLine();
            writer.Write("Enter receiver email address: ");
            string receiverEmail = reader.ReadLine();

            try
            {
                log.AppendLine(controller.CheckIfFileExists(filePath));
            }
            catch (FileNotFoundException ex)
            {
                writer.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            IList<string> lines = new List<string>();
            try
            {
                StreamReader sr = new StreamReader(filePath);
                string line = reader.ReadLineFromFile(sr);
                while (line != null)
                {
                    lines.Add(line);
                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception)
            {
                writer.WriteLine(string.Format(ExceptionMessages.ErrorReadingFile, filePath));
                Environment.Exit(2);
            }

            try
            {
                log.AppendLine(controller.AccumulateData(lines));
            }
            catch (Exception)
            {
                writer.WriteLine(string.Format(ExceptionMessages.ErrorProcessingFile, filePath));
                Environment.Exit(3);
            }

            try
            {
                log.AppendLine(controller.CreateCSVFile(outputFile));
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.Message);
                Environment.Exit(4);
            }

            try
            {
                log.AppendLine(controller.SentCSV(senderEmail, senderPassword, receiverEmail, outputFile));
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.Message);
                Environment.Exit(5);
            }

            writer.WriteLine(log.ToString());
        }
    }
}