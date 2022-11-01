using game_of_life_csharp.Core;

namespace game_of_life_csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!CheckCommands(args))
            {
                Environment.Exit(0);
            }

            if (args.Length == 2)
            {
                Board board = new(Int32.Parse(args[0]), Int32.Parse(args[1]));
                board.Show();
            }
            else if (args.Length == 1)
            {
                Board board = new(args[0]);
                board.Show();
            }
        }

        private static bool CheckCommands(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Type Life --help for list of commands");
                return false;
            }

            if (args.Length == 1 && args[0].Equals("--help"))
            {
                Console.WriteLine("Life width_map height_map <path_map>:\n" +
                    "- width_map: number that represents width of the map to be generated\n" +
                    "- height_map: number that represents height of the map to be generated\n" +
                    "- path_map: path of a map to load it (optional). If there's no value declared, map is randomly generated");
                return false;
            }

            if (args.Length == 2 && int.TryParse(args[0], out _) && int.TryParse(args[1], out _))
                return true;
            else if (args.Length == 1 && File.Exists(args[0]))
                return true;
            else
            {
                Console.WriteLine("Type Life --help for list of commands");
                return false;
            }
        }
    }
}
