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
            Debug.Log("saving roster, kerbals: " + kerbals.Count);
            foreach (KTKerbal kerbal in kerbals)
            {
                Debug.Log("adding kerbal: " + kerbal.name);
                rosterNode.AddNode(kerbal.getNode());
            }            
            rosterNode.Save(KerbTownQuestLogic.Instance.savePath + nodeName + ".cfg");
        }

        public void Load()
        {
            kerbals = new List<KTKerbal>();
            Debug.Log("loading roster: " + KerbTownQuestLogic.Instance.savePath + nodeName + ".cfg");
            bool rosterFileError = false;
            ConfigNode rosterNode = new ConfigNode();
            try
            {
                rosterNode = ConfigNode.Load(KerbTownQuestLogic.Instance.savePath + nodeName + ".cfg");
            }
            catch
            {
                rosterFileError = true;
                Debug.Log("Roster file read error");
            }
            if (rosterNode != null && !rosterFileError)
            {
                Debug.Log("roster not null");
                ConfigNode[] kerbalNodes = rosterNode.GetNodes("kerbal");
                Debug.Log("loading roster, kerbals: " + kerbalNodes.Length);
                foreach (ConfigNode kerbalNode in kerbalNodes)
                {                    
                    KTKerbal newKerbal = new KTKerbal();
                    newKerbal.setValues(kerbalNode);
                    kerbals.Add(newKerbal);
                    Debug.Log("loading kerbal: " + newKerbal.name);
                }
            }
            else
            {
                Debug.Log("KTQ: rosterNode is null, using empty roster (Tried loading: " + KerbTownQuestLogic.Instance.savePath + nodeName + ".cfg");
            }
        }
    }
}
