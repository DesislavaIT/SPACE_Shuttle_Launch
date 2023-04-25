namespace SPACE_Shuttle_Launch.Core.Contracts
{
    using System.Collections.Generic;

    public interface IController
    {
        string CheckIfFileExists(string filePath);

        string AccumulateData(IList<string> lines);

        string CreateCSVFile(string fileName);

        string SentCSV(string senderEmail, string senderPassword, string receiverEmail, string outputFile);
    }
}