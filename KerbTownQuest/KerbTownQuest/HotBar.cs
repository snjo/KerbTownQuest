using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KerbTownQuest.GUI;

namespace KerbTownQuest
{
    public class HotBar
    {
        public Vector2 hotBarSize = new Vector2(500f, 100f);
        public Vector2 hotBarPosition = new Vector2(100f, -200f);
        public bool visible = true;
        private Rect windowRect;
        private Vector2 actualHotBarPosition;
        private Vector2 displaySize;
        private GUIPopup popup;

        public delegate void ToggleInventory();
        public ToggleInventory toggleInventory;

        private void runToggleInventory()
        {
            toggleInventory();
        }

        public void UpdateContents()
        {

        }

        private void createWindowRect()
        {
            displaySize = new Vector2(Display.main.renderingWidth, Display.main.renderingHeight);
            actualHotBarPosition = hotBarPosition;
            if (actualHotBarPosition.x < 0f)
                actualHotBarPosition.x = displaySize.x - actualHotBarPosition.x;
            if (actualHotBarPosition.y < 0f)
                actualHotBarPosition.y = displaySize.y - actualHotBarPosition.y;
            windowRect = new Rect(100f, Display.main.renderingHeight - hotBarSize.y, hotBarSize.x, hotBarSize.y);
        }

        public void OnStart()
        {
            createWindowRect();
            popup = new GUIPopup(0, GUIwindowID.hotBar, windowRect, "Hotbar");
            popup.elementList.Add(new PopupElement(new PopupButton("Inventory", 50f, runToggleInventory)));
            popup.showMenu = true;
        }

        public void OnGUI()
        {
            if (visible)
            {
                if (popup != null)
                    popup.popup();
                else
                    Debug.Log("popup is null");
            }
            else
            {
                Debug.Log("hotBar is hidden");
            }
        }
    }
}
