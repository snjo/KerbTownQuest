using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FShousingProgram
{
    public class InventoryGUI
    {
        private FSHPGUIPopup popup;
        public Rect windowRect = new Rect(10f, 100f, 300f, 500f);
        public KTKerbal activeKerbal;
        private List<FSHPPopupElement> itemElements = new List<FSHPPopupElement>();

        public void deleteItem(string itemName)
        {

        }

        public void OnStart()
        {
            itemElements.Add( new FSHPPopupElement("Fake beard", new FSHPPopupButton("X", 15f, deleteItem, "fakeBeard")));
            popup = new FSHPGUIPopup("InventoryGUI", 0, FSHPGUIwindowID.inventory, windowRect, "Inventory", itemElements[0]);
        }

        public void OnUpdate()
        {

        }

        public void OnFixedUpdate()
        {

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
