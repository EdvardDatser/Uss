using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Speed
    {
        int speedValue;
        int minSpeed = 45;
        int faster = 15;
        int text;

        public int SpeedValue { get { return speedValue; } }
        public int TXT { get { return text; } }

        public Speed()
        {
            speedValue = 135;
            text = 1;
        }
        public void plusSpeed()
        {
            if (speedValue > minSpeed)
            {
                speedValue -= faster;
                text++;
            }
        }
    }
}
