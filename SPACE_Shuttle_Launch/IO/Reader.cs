namespace SPACE_Shuttle_Launch.IO.Contracts
{
    using System;
    using System.IO;

    public class Reader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public string ReadLineFromFile(StreamReader sr)
        {
            return sr.ReadLine();
        }
    }
}