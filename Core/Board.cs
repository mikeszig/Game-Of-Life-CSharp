using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game_of_life_csharp.DataUsage;

namespace game_of_life_csharp.Core
{
    public class Board
    {
        private static int _width;
        private static int _height;
        private static int _countGen = 1;

        private int[,] _soup;

        private TemporalMemory _tempMemory;
        private Rules _rules;
        private ScriptsHandler _sHandler;
        private FileHandler _fHandler;

        public Board(int width, int height)
        {
            _width = width;
            _height = height;

            Init();
        }

        public Board(string path)
        {
            _fHandler = new();
            _soup = _fHandler.LoadSoupFromFile(path);

            Init();
        }

        private void Init()
        {
            if (_soup == null)
                _soup = RandomState();

            if (_soup != null)
            {
                _rules = new();
                _sHandler = new();
                _tempMemory = new();
                _fHandler = new();
                _tempMemory.SaveSoup(_soup);
                _fHandler.SaveSoupOnFile(_soup);
            }
        }

        private static int[,] RandomState()
        {
            int[,] soup = new int[_width, _height];

            return Fill2DArray(soup);
        }

        private static int[,] Fill2DArray(int[,] soup)
        {
            Random r = new();

            for (int i = 0; i < soup.GetLength(0); ++i)
            {
                for (int j = 0; j < soup.GetLength(1); ++j)
                    soup[i, j] = r.Next(0, 2);
            }

            return soup;
        }

        public void Show()
        {
            while (true)
            {
                Render(_soup);
                NextBoardState(_soup);
            }
        }

        private static void Render(int[,] soup)
        {
            Console.Write(" ");
            for (int i = 0; i < soup.GetLength(1); i++)
                Console.Write("-");
            Console.WriteLine();
            for (int x = 0; x < soup.GetLength(0); x++)
            {
                Console.Write("|");
                for (int y = 0; y < soup.GetLength(1); y++)
                {
                    if (soup[x, y] == 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine("|");
            }
            Console.Write(" ");
            for (int i = 0; i < soup.GetLength(1); i++)
                Console.Write("-");
            Console.WriteLine($"\nGeneración: {_countGen}");
            _countGen++;
            Console.WriteLine(" ");
        }

        private void NextBoardState(int[,] newState)
        {
            int[,] soupWrapped = _sHandler.CallPython(newState);

            for (int x = 0; x < soupWrapped.GetLength(0); ++x)
            {
                for (int y = 0; y < soupWrapped.GetLength(1); ++y)
                {
                    if (x == 0 || y == 0 || x == soupWrapped.GetLength(0) - 1 || y == soupWrapped.GetLength(1) - 1)
                    {
                        continue;
                    }

                    if (soupWrapped[x, y] == 0)
                    {
                        int neighborhood = _rules.SumarizeNeighbor(x, y, soupWrapped);

                        soupWrapped[x, y] = _rules.NewValue(false, neighborhood);
                    }
                    else
                    {
                        int neighborhood = _rules.SumarizeNeighbor(x, y, soupWrapped);

                        soupWrapped[x, y] = _rules.NewValue(true, neighborhood);
                    }
                }
            }

            int x2 = 0;
            int y2 = 0;
            for (int x = 0; x < soupWrapped.GetLength(0); ++x)
            {
                for (int y = 0; y < soupWrapped.GetLength(1); ++y)
                {
                    if (x == 0 || y == 0 || x == soupWrapped.GetLength(0) - 1 || y == soupWrapped.GetLength(1) - 1)
                    {
                        continue;
                    }

                    if (x2 < newState.GetLength(0) - 1)
                        x2++;
                    else
                    {
                        x2 = 0;
                        if (y2 < newState.GetLength(1) - 1)
                            y2++;
                    }

                    newState[x2, y2] = soupWrapped[x, y];
                }
            }

            _tempMemory.SaveSoup(newState);
            _soup = newState;
        }
    }
}
