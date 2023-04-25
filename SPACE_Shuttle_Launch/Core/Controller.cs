namespace SPACE_Shuttle_Launch.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using SPACE_Shuttle_Launch.Core.Contracts;
    using SPACE_Shuttle_Launch.Models;
    using SPACE_Shuttle_Launch.Models.Contracts;
    using SPACE_Shuttle_Launch.Models.Enums;
    using SPACE_Shuttle_Launch.Utilities.Messages;

    public class Controller : IController
    {
        IWeatherForecast weatherForecast;
        string CSV;
        char delimiter;

        public Controller()
        {
            weatherForecast = new WeatherForecast();
            delimiter = ';';
        }

        public string CheckIfFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format(ExceptionMessages.FileNotFound, filePath));
            }

            return OutputMessages.SuccessfullyFoundFile;
        }

        public string AccumulateData(IList<string> lines)
        {
            string[] parts0 = lines[0].Split(delimiter);
            string[] parts1 = lines[1].Split(delimiter);
            string[] parts2 = lines[2].Split(delimiter);
            string[] parts3 = lines[3].Split(delimiter);
            string[] parts4 = lines[4].Split(delimiter);
            string[] parts5 = lines[5].Split(delimiter);
            string[] parts6 = lines[6].Split(delimiter);
            int daysCount = parts0.Length - 1;

            for (int i = 0; i < daysCount; i++)
            {
                var day = new DayForecast(DateTime.ParseExact($"2023-07-{String.Format("{0:00}", int.Parse(parts0[i + 1]))}", "yyyy-MM-dd",
                    null), double.Parse(parts1[i + 1]),
                    double.Parse(parts2[i + 1]), double.Parse(parts3[i + 1]), double.Parse(parts4[i + 1]),
                    parts5[i + 1] == "Yes" ? true : false, (Clouds)Enum.Parse(typeof(Clouds), parts6[i + 1]));

                weatherForecast.AddDay(day);
            }
            
            return OutputMessages.SuccessfullyAddedData;
        }

        public string CreateCSVFile(string fileName)
        {
            CSV = weatherForecast.CreateCSV();
            File.WriteAllText(fileName, CSV);

            return OutputMessages.SuccessfullyCreatedCSV;
        }

        public string SentCSV(string senderEmail, string senderPassword, string receiverEmail, string outputFile)
        {
            if(!IsValid(senderEmail))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.SenderEmailInvalid, senderEmail));
            }
            else if(!IsValid(senderEmail))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ReceiverEmailInvalid, senderEmail));
            }

            SmtpClient SmtpServer = new SmtpClient("smtp.abv.bg");
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(senderEmail, senderPassword);
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Port = 465;
            SmtpServer.EnableSsl = true;
            SmtpServer.Timeout = 100000;

            MailMessage mail = new MailMessage(new MailAddress(senderEmail), new MailAddress(receiverEmail));
            mail.Subject = "SPACE mission - Weather report";
            mail.Body = $"The most appropriate date for the space shuttle launch is {weatherForecast.CalculateMostAppropriateDayForLaunch().Date.ToString("MM/dd/yyyy")}.";

            Attachment attachment;
            attachment = new Attachment(outputFile);
            mail.Attachments.Add(attachment);
            mail.IsBodyHtml = true;

            SmtpServer.Send(mail);  

            return OutputMessages.SuccessfullySentFile;
        }

        private static bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}