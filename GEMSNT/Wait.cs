using System;
using System.Collections.Generic;
using System.Text;

namespace GEMSNT.Wait
{
    public class Wait
    {
        public static void wait(uint ms)
        {
            Cosmos.HAL.Global.PIT.Wait(ms);
        }
    }
}
