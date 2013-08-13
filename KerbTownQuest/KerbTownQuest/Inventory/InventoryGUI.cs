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
        public bool visible;
        private List<GUI.PopupElement> itemElements = new List<PopupElement>();

        public void toggleInventory()
        {
            setInventoryState(!visible);
        }

        public void inventoryOff()
        {
            setInventoryState(false);
        }

        public void inventoryOn()
        {
            setInventoryState(true);
        }

        public void setInventoryState(bool _visible)
        {
            visible = _visible;
            popup.showMenu = _visible;
        }

        public void deleteItem(string itemName)
        {

        }

        public void OnStart()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                itemElements.Add(new PopupElement("Fake beard", new PopupButton("X", 15f, deleteItem, "fakeBeard")));
                popup = new GUIPopup(0, GUIwindowID.inventory, windowRect, "Inventory", itemElements[0]);
                popup.hideMenuEvent = inventoryOff;
                popup.showMenu = true;
            }
        }

        public void OnGUI()
        {
            if (visible)
            {
                if (popup != null)
                    popup.popup();
            }
        }        
    }
}
