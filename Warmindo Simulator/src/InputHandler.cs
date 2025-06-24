using System.Windows.Forms;

namespace Warmindo_Simulator.src
{
    internal class InputHandler
    {
        private Keys? currentKey = null;

        public void KeyDown(Keys key)
        {
            if (key == Keys.W || key == Keys.A || key == Keys.S || key == Keys.D)
            {
                currentKey = key;
            }
        }

        public void KeyUp(Keys key)
        {
            if (key == currentKey)
            {
                currentKey = null;
            }
        }

        public Keys? GetActiveKey()
        {
            return currentKey;
        }

        public bool IsKeyPressed()
        {
            return currentKey.HasValue;
        }
    }
}