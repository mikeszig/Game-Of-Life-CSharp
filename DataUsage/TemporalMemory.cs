using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_of_life_csharp.DataUsage
{
    public class TemporalMemory
    {
        private List<int[,]> _pottage;

        public TemporalMemory()
        {
            _pottage = new List<int[,]>();
        }

        public void SaveSoup(int[,] soup)
        {
            if (soup != null)
                _pottage.Add(soup);
        }
    }
}
