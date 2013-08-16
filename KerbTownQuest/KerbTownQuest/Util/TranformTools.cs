using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KerbTownQuest.Util
{
    public class TranformTools
    {
        public static Vector3 WorldUp(Vessel vessel)
        {
                return (vessel.rigidbody.position - vessel.mainBody.position).normalized;            
        }
    }
}
