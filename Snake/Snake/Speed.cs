using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Snake
{
    public class Speed
    {
        int speedValue;
        int maxSnakeSpeed = 45;
        int boost = 15;

        public int SpeedValue { get { return speedValue; } }
        public Speed()
        {
            speedValue = 100;

        }
        public void speedy()
        {
            if (speedValue > maxSnakeSpeed)
            {
                speedValue -= boost;
            }
        }
    }
}
