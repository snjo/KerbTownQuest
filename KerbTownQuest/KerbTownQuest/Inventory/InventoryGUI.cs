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
        public Rect windowRect = new Rect(10f, 100f, 300f, 100f);
        public Vector2 padding = new Vector2(4f, 4f);
        public Vector2 tileSize = new Vector2(64f, 64f);
        public Vector2 buttonSize = new Vector2(64f, 32f);
        public KTKerbal activeKerbal;
        public bool visible;        
        public IntVector2 gridSize = new IntVector2(6, 10);        
        public string iconRootURL = "FShousingProgram/icons/";
        public string modelRootURL = "FShousingProgram/models/";
        public float dropDistance = 1f;

        private GUI.GUIPopup popup;
        private int scrollLine;
        private List<PopupElement> itemElements = new List<PopupElement>();
        private List<InventoryTile> tiles = new List<InventoryTile>();
        private Section tileSection = new Section();
        private Section buttonSection = new Section();

        private PopupButton buttonDropItem;
        private PopupButton buttonUseItem;
        private PopupElement primaryButtonRow;
        private string buttonMode = "";


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
            Debug.Log("tile: " + tile.item.displayName + " / " + tile.item.name );
            switch (buttonMode)
            {
                case "drop":
                    dropItem(tile);                  
                    break;
                case "use":
                    Debug.Log("use item " + tile.item.displayName);
                    break;
                default:
                    Debug.Log("no button mode set");
                    break;
            }
        }

        private void dropItem(InventoryTile tile)
        {
            Transform dropLocation = new GameObject().transform;
            dropLocation.position = activeKerbal.transform.position;
            dropLocation.rotation = activeKerbal.transform.rotation;
            //dropLocation.LookAt(TranformTools.WorldUp(FlightGlobals.ActiveVessel));
            dropLocation.position += (activeKerbal.transform.forward + (TranformTools.WorldUp(FlightGlobals.ActiveVessel) * 0.5f)).normalized * dropDistance;
            Debug.Log("dropping at: " + dropLocation.position + ", kerbal: " + activeKerbal.transform.position + " , forward: " + dropLocation.forward.normalized * dropDistance);
            activeKerbal.backpack.DropItem(tile.item, dropLocation);
        }

        private InventoryTile createInventoryTile(BackPackItem item)
        {
            InventoryTile newTile = new InventoryTile(item, iconRootURL + item.iconName);
            newTile.function = tileClick;
            tiles.Add(newTile);
            return newTile;
        }

        public void updateGrid()
        {
            activeKerbal = KerbTownQuestLogic.activeKerbal;
            if (activeKerbal.backpack.contentsHaveChanged)
            {
                createInventoryGrid(activeKerbal.backpack);
                activeKerbal.backpack.contentsHaveChanged = false;
            }
        }

        public void createInventoryGrid(Backpack backpack)
        {
            tileSection.elements = new List<PopupElement>();
            //popup.elementList = new List<PopupElement>();
            int counter = scrollLine * gridSize.x;            
            for (int y = 0; y < gridSize.y; y++)
            {
                PopupElement newElement = new PopupElement();
                tileSection.AddElement(newElement, tileSize.y);
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

        private void setButtonMode(string newMode)
        {
            buttonMode = newMode;
            Debug.Log("Button mode set to " + newMode);
        }

        private void createButtons()
        {
            buttonDropItem = new PopupButton("Drop", buttonSize.x, setButtonMode, "drop");
            buttonUseItem = new PopupButton("Use", buttonSize.x, setButtonMode, "use");
            primaryButtonRow = new PopupElement("0/0 kg");
            primaryButtonRow.buttons.Add(buttonDropItem);
            primaryButtonRow.buttons.Add(buttonUseItem);
            buttonSection.AddElement(primaryButtonRow, buttonSize.y);

            //buttonSection.elements.Add(
        }

        private void createPopup()
        {
            popup = new GUIPopup(0, GUIwindowID.inventory, windowRect, "Inventory");

            createButtons();
            popup.sections.Add(buttonSection);
            popup.sections.Add(tileSection);            

            //popup.elementSize.y = tileSize.y;
            popup.hideMenuEvent = inventoryOff;
            popup.showMenu = true;
        }

        public void OnStart()
        {            
            createPopup();
            popup.subElementSpacing = padding.x;
            popup.lineSpacing = padding.y;
            popup.windowRect.width = (gridSize.x * tileSize.x) + ((gridSize.x - 1) * popup.subElementSpacing) + popup.marginLeft + popup.marginRight;
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
                //Debug.Log("KerbTownQuest InventoryGUI: Missing item icon texture " + imageURL);
                button.buttonText = item.displayName; // if the image is missing, default to text on the button
            }
        }
    }
}


