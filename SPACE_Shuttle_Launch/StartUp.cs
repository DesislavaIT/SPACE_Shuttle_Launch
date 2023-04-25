namespace SPACE_Shuttle_Launch
{
    using System;
    using SPACE_Shuttle_Launch.Core;
    using SPACE_Shuttle_Launch.Core.Contracts;

    public class StartUp
    {
        public static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}