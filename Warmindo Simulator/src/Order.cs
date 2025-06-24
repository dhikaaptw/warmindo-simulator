using System;
using System.Collections.Generic;

namespace Warmindo_Simulator.src
{
    internal class OrderSystem
    {
        private List<Customer> customerQueue = new List<Customer>();
        private Random random = new Random();

        public List<Customer> Customers
        {
            get { return customerQueue; }
        }

        public void AddCustomer()
        {
            string[] orders = { "Mie Instan", "Es Teh Manis" };
            string selectedOrder = orders[random.Next(orders.Length)];
            customerQueue.Add(new Customer(selectedOrder));
        }

        public void RemoveFirstCustomer()
        {
            if (customerQueue.Count > 0)
                customerQueue.RemoveAt(0);
        }

        public Customer GetFirstCustomer()
        {
            return customerQueue.Count > 0 ? customerQueue[0] : null;
        }

        public bool IsQueueFull => customerQueue.Count >= 5;

        public void Clear()
        {
            customerQueue.Clear();
        }
    }
}
