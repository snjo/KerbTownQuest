using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerbTownQuest
{
    public class KTKerbalRoster
    {
        public List<KTKerbal> kerbals = new List<KTKerbal>();

        public KTKerbal findKerbalByName(string kerbalName)
        {
            foreach (KTKerbal k in kerbals)
            {
                if (k.name == kerbalName)
                    return k;
            }
            return null;
        }

        public KTKerbal Add(KTKerbal kerbal)
        {
            if (findKerbalByName(kerbal.name) == null)
            {
                kerbals.Add(kerbal);
                return kerbal;
            }
            else
            {
                return null;
            }
        }

        public KTKerbal AddbyName(string kerbalName)
        {
            if (findKerbalByName(kerbalName) == null)
            {
                KTKerbal newKerbal = new KTKerbal();
                newKerbal.name = kerbalName;
                kerbals.Add(newKerbal);
                return newKerbal;
            }
            else
            {
                return null;
            }
        }
    }
}
