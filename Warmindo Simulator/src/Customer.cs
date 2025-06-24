using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warmindo_Simulator.src
{
    internal class Customer
    {
        public string Order { get; set; }
        public int WaitTime { get; set; }
        public bool IsServed { get; set; }
        public bool IsAngry { get; set; } = false;


        public Customer(string order)
        {
            Order = order;
            WaitTime = 30; // in seconds
            IsServed = false;
        }

        public void DecreaseWaitTime()
        {
            if (WaitTime > 0)
                WaitTime--;
        }
    }
}
