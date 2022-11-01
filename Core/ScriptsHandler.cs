using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_of_life_csharp.Core
{
    public class ScriptsHandler
    {
        //private readonly string? _path = @"game-of-life-csharp\Scripts\WrapSoup.py";
        private readonly string? _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Scripts\WrapSoup.py");

        public ScriptsHandler()
        {

        }

        public ScriptsHandler(string path)
        {
            _path = path;
        }

        public int[,] CallPython(int[,] soup)
        {
            string soupToString = FormatSoupStringInput(soup);
            return WrapSoup(_path, soupToString, soup.GetLength(0).ToString(), soup.GetLength(1).ToString());
        }

        private static int[,] WrapSoup(string path, string soupToString, string width, string height)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = string.Format("{0} {1} {2} {3}", path, soupToString, width, height);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            using Process process = Process.Start(startInfo: start);
            using StreamReader reader = process.StandardOutput;

            return FormatScriptOutput(reader.ReadToEnd());
        }

        private static string FormatSoupStringInput(int[,] soup)
        {
            string soupToString = "";
            int count = 1;
            int width = soup.GetLength(0);

            foreach (int x in soup)
            {
                if (count % width == 0)
                {
                    soupToString += x.ToString() + "\n";
                    count++;
                    continue;
                }

                soupToString += x.ToString() + ",";
                count++;
            }

            return soupToString;
        }

        private static int[,] FormatScriptOutput(string soupText)
        {
            string[] soupStringSplitted = soupText.Trim().Split("\r\n");
            int[,] newSoupWrapped = new int[Int32.Parse(soupStringSplitted[0]), Int32.Parse(soupStringSplitted[1])];
            int count = 2;
            for (int x = 0; x < newSoupWrapped.GetLength(0); x++)
            {
                for (int y = 0; y < newSoupWrapped.GetLength(1); y++)
                {
                    newSoupWrapped[x, y] = Int32.Parse(soupStringSplitted[count]);
                    count++;
                }
            }

            return newSoupWrapped;
        }
    }
}
