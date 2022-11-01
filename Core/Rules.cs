using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_of_life_csharp.Core
{
    /// <summary>
    /// This class groups the rules of Life. Open to new rules or changes.
    /// </summary>
    public class Rules : IRules
    {
        public Rules()
        {

        }

        public int SumarizeNeighbor(int x, int y, int[,] soupWrapped)
        {
            int neighborhood = 0;

            neighborhood += soupWrapped[x - 1, y - 1];
            neighborhood += soupWrapped[x, y - 1];
            neighborhood += soupWrapped[x + 1, y - 1];
            neighborhood += soupWrapped[x - 1, y];
            neighborhood += soupWrapped[x + 1, y];
            neighborhood += soupWrapped[x - 1, y + 1];
            neighborhood += soupWrapped[x, y + 1];
            neighborhood += soupWrapped[x + 1, y + 1];

            return neighborhood;
        }

        public int NewValue(bool alive, int neighbor)
        {
            if (!alive)
            {
                if (neighbor == 3)
                    return 1;
                else return 0;
            }

            if (neighbor == 2 || neighbor == 3)
                return 1;
            else return 0;
        }
    }
}
