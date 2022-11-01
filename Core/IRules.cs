using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_of_life_csharp.Core
{
    public interface IRules
    {
        int SumarizeNeighbor(int x, int y, int[,] initialState);
        int NewValue(bool alive, int neighbor);
    }
}
