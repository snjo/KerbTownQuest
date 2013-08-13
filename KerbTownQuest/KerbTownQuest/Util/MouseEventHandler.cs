using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KerbTownQuest.Util
{    
    public class MouseEventHandler : MonoBehaviour
    {
        public delegate void GenericDelegate();
        public GenericDelegate mouseDownEvent;

        public void OnMouseDown()
        {
            mouseDownEvent();
        }
    }
}
