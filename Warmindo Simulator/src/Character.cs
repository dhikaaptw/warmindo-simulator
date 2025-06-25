using System.Drawing;

namespace Warmindo_Simulator.src
{
    internal abstract class Character
    {
        public int X { get; set; }
        public int Y { get; set; }

        public abstract void Draw(Graphics g);
        public abstract void Move();
        public abstract void Animate();
    }
}
