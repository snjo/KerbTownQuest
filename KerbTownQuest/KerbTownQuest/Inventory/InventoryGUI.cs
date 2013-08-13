using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KerbTownQuest.GUI;

namespace KerbTownQuest.Inventory
{
    public class InventoryGUI
    {
        private GUI.GUIPopup popup;
        public Rect windowRect = new Rect(10f, 100f, 300f, 500f);
        public KTKerbal activeKerbal;
        private List<GUI.PopupElement> itemElements = new List<PopupElement>();

        public void deleteItem(string itemName)
        {

        }

        public void OnStart()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                itemElements.Add(new PopupElement("Fake beard", new PopupButton("X", 15f, deleteItem, "fakeBeard")));
                popup = new GUIPopup("InventoryGUI", 0, GUIwindowID.inventory, windowRect, "Inventory", itemElements[0]);
            }
        }

        public void OnGUI()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                if (popup != null)
                    popup.popup();
            }
        }        
    }
}
