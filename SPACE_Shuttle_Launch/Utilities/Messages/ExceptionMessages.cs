namespace SPACE_Shuttle_Launch.Utilities.Messages
{
    public static class ExceptionMessages
    {
        public const string NegativeWind = "Wind can not be negative.";

        public const string NegativeHumidity = "Humidity can not be negative."; 

        public const string NegativePrecipitation = "Precipitation can not be negative.";

        public const string FileNotFound = "File {0} can not be found.";

        public const string SenderEmailInvalid = "Sender email {0} is not valid.";

        public const string ReceiverEmailInvalid = "Receiver email {0} is not valid.";

        public const string ErrorReadingFile = "Error while reading file {0}.";

        public const string ErrorProcessingFile = "Error while processing file {0}.";
    }
}