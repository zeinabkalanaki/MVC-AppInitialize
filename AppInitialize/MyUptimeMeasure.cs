using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppInitialize
{
    public class MyUptimeMeasure
    {
        private readonly Stopwatch stopWatch;
        public MyUptimeMeasure()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public long GetUptime()
        {
            return stopWatch.ElapsedMilliseconds;
        }
    }
}
