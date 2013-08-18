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

        public void OnSave()
        {
            Debug.Log("KTQ: KerbalRoster OnSave");
            ConfigNode rosterNode = new ConfigNode(nodeName);
            foreach (KTKerbal kerbal in kerbals)
            {                
                rosterNode.AddNode(kerbal.getNode());
            }

            rosterNode.Save(KerbTownQuestLogic.Instance.savePath + "KTKerbalRoster.cfg");
        }

        public void OnLoad()
        {            
            //items = new Dictionary<string, BackPackItem>();
            //ConfigNode libraryNode = ConfigNode.Load(KerbTownQuestLogic.Instance.savePath + "KTKerbalRoster.cfg");
            //if (libraryNode != null)
            //{
            //    ConfigNode[] itemNodes = libraryNode.GetNodes("item");
            //    foreach (ConfigNode itemNode in itemNodes)
            //    {
            //        BackPackItem newItem = new BackPackItem();
            //        newItem.setValues(itemNode);
            //        items.Add(newItem.name, newItem);
            //    }
            //}
            //else
            //{
            //    Debug.Log("KTQ: libraryNode is null, have you put the mode in the wrong folder? Tried loading: " + KerbTownQuestLogic.modulePath + "/Configs/inv.cfg");
            //}
        }
    }
}
