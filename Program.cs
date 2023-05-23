namespace BeeboLevelPacker
{
    public class LevelMetadata
    {
        public int tileSize {get; set;}
        public string songId {get; set;}

        public LevelMetadata(int tileSize = 16, string songId = "bgm_placeholder")
        {
            this.tileSize = tileSize;
            this.songId = songId;
        }
    }

    public class Room
    {
        
    }

    public class Level
    {
        public LevelMetadata? metadata {get; set;}
        public Room[]? rooms {get; set;}
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "beeboLevelPacker (cmd) v1.1";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to the beeboLevelPacker tool");
            Console.WriteLine("------------------------------------");

            string levelid = ReadArg("levelid", "1a");

            int rooms = ReadArg("room count", 0);

            Level level = new Level
            {
                metadata = new LevelMetadata()
            };
            level.rooms = new Room[rooms];

            // string path = Environment.CurrentDirectory + "/output/lvl" + levelid + ".beebo";
            string path = Environment.CurrentDirectory;

            if(!Directory.Exists(path + "/output"))
            {
                Console.WriteLine("Creating missing output directory");
                Directory.CreateDirectory(path + "/output");
            }
            if(!Directory.Exists(path + "/input"))
            {
                Console.WriteLine("Creating missing input directory");
                Directory.CreateDirectory(path + "/input");
            }

            string json = "{\n  \"metadata\": {},\n  \"rooms\": [";

            Console.WriteLine("Reading input files...");
            for(int i = 0; i < rooms; i++)
            {
                string _file = path + "/input/lvl" + levelid + "_" + i + ".beebo";
                if(File.Exists(_file))
                {
                    using(StreamReader reader = new StreamReader(_file))
                    {
                        if(i > 0 && i < rooms - 1) json += ",";
                        json += "\n" + reader.ReadToEnd();

                        reader.Dispose();
                    }
                }
            }
            Console.WriteLine("Done.");

            json += "  ]\n}";

            File.WriteAllText(path + "/output/lvl" + levelid + ".beebo", json);
            Console.WriteLine("All tasks complete! Press any key to close this window.");

            Console.ReadKey();
        }

        static string ReadArg(string argName, string defaultValue)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Set Argument: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(argName + " (string)");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nleave blank for default (" + defaultValue + ")\n");
            Console.ForegroundColor = ConsoleColor.White;

            string? output = Console.ReadLine();
            if(output == null || output == "")
                output = defaultValue;
            return output;
        }

        static int ReadArg(string argName, int defaultValue)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Set Argument: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(argName + " (int)");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nleave blank for default (" + defaultValue.ToString() + ")\n");
            Console.ForegroundColor = ConsoleColor.White;

            string? output = Console.ReadLine();
            if(output == null || output == "")
                output = defaultValue.ToString();
            return Int32.Parse("0" + new String(output.Where(Char.IsDigit).ToArray()));
        }
    }
}
