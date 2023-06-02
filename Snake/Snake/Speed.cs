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
            int sad;

            public int SpeedValue { get { return speedValue; } }
            public int das { get { return sad; } }

            public Speed()
            {
                speedValue = 135;
                sad = 1;
            }
            public void Speedy()
            {
                if (speedValue > minSpeed)
                {
                    speedValue -= faster;
                    sad++;
                }
            }
        }
    }
