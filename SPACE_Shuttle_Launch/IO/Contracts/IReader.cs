using System.IO;

namespace SPACE_Shuttle_Launch.IO.Contracts
{
    public interface IReader
    {
        string ReadLine();

        string ReadLineFromFile(StreamReader sr);
    }
}