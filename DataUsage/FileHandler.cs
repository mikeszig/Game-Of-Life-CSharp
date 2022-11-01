using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_of_life_csharp.DataUsage
{
    public class FileHandler
    {
        public FileHandler()
        {

        }

        public int[,] LoadSoupFromFile(string path)
        {
            char[] chars;
            string[] lines = File.ReadAllLines(path);
            int[,] soupLoaded = new int[lines.Length, lines[0].Length];

            for (int x = 0; x < lines.Length; x++)
            {
                chars = lines[x].ToCharArray();
                for (int y = 0; y < chars.Length; y++)
                {
                    soupLoaded[x, y] = Convert.ToInt32(chars[y].ToString());
                }
            }

            return soupLoaded;
        }

        public void SaveSoupOnFile(int[,] soup)
        {
            int cont = 1;
            string fileName = $"soup{cont}.txt";
            string pathName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Sopas\{fileName}");

            while (File.Exists(pathName))
            {
                cont++;
                fileName = $"soup{cont}.txt";
                pathName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Sopas\{fileName}");
            }

            using StreamWriter file = new(pathName, append: true);

            for (int x = 0; x < soup.GetLength(0); x++)
            {
                for (int y = 0; y < soup.GetLength(1); y++)
                {
                    file.Write(soup[x, y]);
                }
                file.WriteLine();
            }
        }
    }
}
