using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClientFanuc
{
    public class Counter
    {
        private List<int> Values;
        public Counter()
        {
            Values = new List<int>();
        }

        public List<int> GetValues(int minValue, int maxValue)
        {
            if(Values.Count>0)
            {
                Values.Clear();
            }
            for (int i = minValue; i <= maxValue; i++)
            {
                Values.Add(i);
            }
            return Values;
        }
    }
}
