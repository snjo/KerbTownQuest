using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KerbTownQuest
{
    public class KTKerbalRoster
    {
        public List<KTKerbal> kerbals = new List<KTKerbal>();
        public string nodeName = "KTKerbalRoster";

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

        public void Save()
        {
            //Debug.Log("KTQ: KerbalRoster OnSave");
            ConfigNode rosterNode = new ConfigNode(nodeName);
            foreach (KTKerbal kerbal in kerbals)
            {                
                rosterNode.AddNode(kerbal.getNode());
            }

            rosterNode.Save(KerbTownQuestLogic.Instance.savePath + nodeName + ".cfg");
        }

        public void Load()
        {
            ConfigNode rosterNode = ConfigNode.Load(KerbTownQuestLogic.Instance.savePath + nodeName + ".cfg");
            if (rosterNode != null)
            {
                ConfigNode[] kerbalNodes = rosterNode.GetNodes("kerbal");
                foreach (ConfigNode kerbalNode in kerbalNodes)
                {
                    KTKerbal newKerbal = new KTKerbal();
                    newKerbal.setValues(kerbalNode);
                    kerbals.Add(newKerbal);
                }
            }
            else
            {
                Debug.Log("KTQ: rosterNode is null, using empty roster (Tried loading: " + KerbTownQuestLogic.Instance.savePath + nodeName + ".cfg");
            }
        }
    }
}
