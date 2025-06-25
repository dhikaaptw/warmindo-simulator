using System;

namespace Warmindo_Simulator.src
{
    internal interface ICookable
    {
        void StartCooking(string menu);
        bool IsCooking { get; }
        void UpdateCooking(Action onFinished);
        string GetStatusText(string menu);
    }
}