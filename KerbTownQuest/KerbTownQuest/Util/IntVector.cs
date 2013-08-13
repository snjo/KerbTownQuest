using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KerbTownQuest.Util
{
    public struct IntVector2
    {
        int x;
        int y;        

        public IntVector2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public IntVector2(float _x, float _y)
        {
            x = (int)_x;
            y = (int)_y;
        }

        public Vector2 toVector2()
        {
            return new Vector2((float)x, (float)y);
        }

        public float magnitude()
        {
            return new Vector2((float)x, (float)y).magnitude;
        }

        public override string ToString()
        {
            return ("(" + x + ", " + y + ")");
        }
    }
}
