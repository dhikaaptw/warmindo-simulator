using System;
using System.Windows.Forms;

namespace Warmindo_Simulator.src
{
    internal class Kitchen : ICookable
    {
        private int cookTimeLeft = 0;
        private bool isCooking = false;
        private int cookTickCounter = 0;

        public bool IsCooking
        {
            get { return isCooking; }
        }

        public void StartCooking(string menu)
        {
            if (menu == "Mie Instan")
            {
                cookTimeLeft = 10;
            }
            else if (menu == "Nasi Goreng")
            {
                cookTimeLeft = 12;
            }
            else if (menu == "Minuman Manis")
            {
                cookTimeLeft = 8;
            }
            else if (menu == "Es Teh Manis")
            {
                cookTimeLeft = 5;
            }
            

            isCooking = true;
            cookTickCounter = 0;
        }

        public void UpdateCooking(Action onFinished)
        {
            if (!isCooking)
            {
                return;
            }

            cookTickCounter++;

            if (cookTickCounter >= 33)
            {
                cookTickCounter = 0;
                cookTimeLeft--;

                if (cookTimeLeft <= 0)
                {
                    isCooking = false;

                    if (onFinished != null)
                    {
                        onFinished();
                    }
                }
            }
        }

        public string GetStatusText(string menu)
        {
            if (isCooking)
            {
                return "Memasak " + menu + " - sisa " + cookTimeLeft.ToString() + " detik...";
            }

            return "";
        }
    }
}
