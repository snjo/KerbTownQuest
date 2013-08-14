using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KerbTownQuest.GUI;
using KerbTownQuest.Util;

namespace KerbTownQuest.Inventory
{
    public class InventoryGUI
    {        
        public Rect windowRect = new Rect(10f, 100f, 300f, 500f);
        public Vector2 padding = new Vector2(4f, 4f);
        public Vector2 tileSize = new Vector2(64f, 64f);
        public KTKerbal activeKerbal;
        public bool visible;        
        public IntVector2 gridSize = new IntVector2(6, 10);        
        public string iconRootURL = "FShousingProgram/icons/";
        public string modelRootURL = "FShousingProgram/models/";

        private GUI.GUIPopup popup;
        private int scrollLine;
        private List<PopupElement> itemElements = new List<PopupElement>();
        private List<InventoryTile> tiles = new List<InventoryTile>();

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

        public void tileClick(InventoryTile tile)
        {
            Debug.Log("tile clicked");
            Debug.Log("tile: " + tile.item.displayName + " / " + tile.item.name );
        }

        private InventoryTile createInventoryTile(BackPackItem item)
        {
            InventoryTile newTile = new InventoryTile(item, iconRootURL + item.iconName);
            newTile.function = tileClick;
            tiles.Add(newTile);
            return newTile;
        }

        public void createInventoryGrid(Backpack backpack)
        {
            int counter = scrollLine * gridSize.x;            
            for (int y = 0; y < gridSize.y; y++)
            {
                PopupElement newElement = new PopupElement();
                popup.elementList.Add(newElement);
                for (int x = 0; x < gridSize.x; x++)
                {
                    if (counter < backpack.items.Count)
                    {
                        newElement.buttons.Add(createInventoryTile(backpack.items[counter]).button);                        
                    }
                    counter++;
                }
            }            
        }

        private void createPopup()
        {
            popup = new GUIPopup(0, GUIwindowID.inventory, windowRect, "Inventory");

            popup.elementSize.y = tileSize.y;
            popup.hideMenuEvent = inventoryOff;
            popup.showMenu = true;
        }

        public void OnStart()
        {            
            createPopup();
            popup.windowRect.width = (gridSize.x * tileSize.x) + popup.marginLeft + popup.marginRight;
            windowRect.width = popup.windowRect.width;
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


    public class InventoryTile
    {
        public PopupButton button;
        public BackPackItem item;
        public delegate void Function(InventoryTile tile);
        public Function function;

        private void runFunction()
        {
            function(this);
        }

        public InventoryTile(BackPackItem _item, string imageURL)
        {
            item = _item;
            button = new PopupButton(string.Empty, 64f, runFunction);
            if (GameDatabase.Instance.ExistsTexture(imageURL))
            {
                button.setTexture(GameDatabase.Instance.GetTexture(imageURL, false));
            }
            else
            {
                Debug.Log("KerbTownQuest InventoryGUI: Missing item icon texture " + imageURL);
                button.buttonText = item.displayName; // if the image is missing, default to text on the button
            }
        }
    }
}


