using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerbTownQuest.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;

    public class GUIPopup
    {
        public bool showMenu = false;
        public Rect windowRect = new Rect(500f, 300f, 150f, 100f);
        public Vector2 elementSize = new Vector2(130f, 25f);
        public float lastElementTop = 0f;
        public float marginLeft = 10f;
        public float marginRight = 10f;
        public float marginTop = 22f;
        public float marginBottom = 10f;
        public float subElementSpacing = 8f;
        //public bool optionEnabled = false;
        public bool showCloseButton = true;
        public string optionEnabledString = "On";
        public string optionDisabledString = "Off";
        public string windowTitle;
        //public Part parentPart;
        public string moduleName;
        public int GUIlayer = 5600;
        public int moduleID = 0;

        public delegate void HideMenuEvent();
        public HideMenuEvent hideMenuEvent;

        //public bool useInActionEditor = true;
        //public bool useInFlight = false;
        //public delegate void RunFunction();
        //private RunFunction run;

        public List<PopupElement> elementList = new List<PopupElement>();

        //private string currentOptionString;    

        /// <summary>
        /// Create the editor popup adn define its settings. Example use at the end of this modules' source.
        /// </summary>
        /// <param name="part">The calling part, just use this.part</param>
        /// <param name="module">The class name of the module where this is being used</param>
        /// <param name="ID">If you have more than one of a module on a part the ID should be unique to prevent window stealing and overlap. Should be passed on from the cfg.</param>
        /// <param name="Windowlayer">the GUI windows layer. Should be unique for each class, possibly with a span of numbers reserved for duplicate modules. the final layer used is layer + ID</param>
        /// <param name="windowDimensions">Left, Top, Width and Height of the GUI window. These are not static, but changed by dragging the window and by resizing functions.</param>
        public GUIPopup(int ID, int Windowlayer, Rect windowDimensions, string windowName, PopupElement defaultElement)
        {
            elementList.Add(defaultElement);            
            //moduleName = module;
            moduleID = ID;
            GUIlayer = Windowlayer;
            windowRect = windowDimensions;
            windowTitle = windowName;
            windowRect.y += (moduleID * windowRect.height) + 20;            
        }

        public GUIPopup(int ID, int Windowlayer, Rect windowDimensions, string windowName)
        {                        
            //moduleName = module;
            moduleID = ID;
            GUIlayer = Windowlayer;
            windowRect = windowDimensions;
            windowTitle = windowName;
            windowRect.y += (moduleID * windowRect.height) + 20;            
        }

        private void drawElement(PopupElement element)
        {
            int activeElements = 0;
            if (element.useTitle) activeElements++;
            if (element.useInput) activeElements++;
            activeElements += element.buttons.Count;

            if (activeElements < 1)
                return;

            Rect subElementRect = new Rect(marginLeft, marginTop + lastElementTop, element.titleSize, elementSize.y);

            if (element.useTitle)
            {
                if (subElementRect.width == 0)
                    subElementRect.width = (elementSize.x / activeElements) - (subElementSpacing);
                GUI.Label(subElementRect, element.titleText);
                subElementRect.x += subElementRect.width + subElementSpacing;
            }

            if (element.useInput)
            {
                subElementRect.width = element.inputSize;
                if (subElementRect.width == 0)
                    subElementRect.width = (elementSize.x / activeElements) - (subElementSpacing);
                element.inputText = GUI.TextField(subElementRect, element.inputText);
                subElementRect.x += subElementRect.width + subElementSpacing;
            }

            for (int i = 0; i < element.buttons.Count; i++)
            {
                subElementRect.width = element.buttons[i].buttonWidth;
                if (subElementRect.width == 0)
                    subElementRect.width = (elementSize.x / activeElements) - (subElementSpacing);

                //if (element.buttons[i].style == null) 
                //{
                //    element.buttons[i].style = new GUIStyle(GUI.skin.button); // This has to happen in a the OnGUI, annoyingly! Should create it from scratch elsewhere.
                //    if (element.buttons[i].texture != null)
                //        element.buttons[i].style.normal.background = element.buttons[i].texture;
                //}

                if (GUI.Button(subElementRect, element.buttons[i].buttonText, element.buttons[i].style))
                {
                    if (element.buttons[i].genericFunction != null)
                        element.buttons[i].genericFunction();
                    if (element.buttons[i].buttonSpecificFunction != null)
                        element.buttons[i].buttonSpecificFunction(element.buttons[i]);
                    if (element.buttons[i].IDfunctionInt != null)
                        element.buttons[i].IDfunctionInt(element.buttons[i].buttonIDInt);
                }
                subElementRect.x += subElementRect.width + subElementSpacing;
            }

            lastElementTop += elementSize.y + subElementSpacing;
        }

        private void drawWindow(int windowID)
        {
            lastElementTop = 0f;
            elementSize.x = windowRect.width - marginLeft - marginRight + subElementSpacing;
            windowRect.height = ((float)elementList.Count * (elementSize.y + subElementSpacing)) + marginTop + marginBottom;
            for (int i = 0; i < elementList.Count; i++)
            {
                drawElement(elementList[i]);
            }
            if (showCloseButton)
            {
                if (GUI.Button(new Rect(windowRect.width - 18f, 2f, 16f, 16f), ""))
                {
                    showMenu = false;
                    hideMenuEvent();
                }
            }
            GUI.DragWindow();
        }

        public void popup()
        {            
            if (HighLogic.LoadedSceneIsFlight)
            {
                if (showMenu)
                {
                    windowRect = GUI.Window(GUIlayer, windowRect, drawWindow, windowTitle);
                }
            }
            //return optionEnabled;        
        }
    }

    public class PopupElement
    {
        public string titleText = "";
        public string inputText = "";
        public List<PopupButton> buttons = new List<PopupButton>();

        public float titleSize = 0f;
        public float inputSize = 0f;


        public bool useTitle = false;
        public bool useInput = false;

        public PopupElement()
        {
        }

        public PopupElement(string title, PopupButton button)
        {
            titleText = title;
            useTitle = true;
            buttons.Add(button);
        }

        public PopupElement(PopupButton button)
        {
            buttons.Add(button);
        }

        public PopupElement(string title)
        {
            titleText = title;
            useTitle = true;
        }

        public PopupElement(string title, string input)
        {
            titleText = title;
            useTitle = true;
            inputText = input;
            useInput = true;
        }

        public PopupElement(string title, string input, PopupButton button)
        {
            titleText = title;
            useTitle = true;
            inputText = input;
            useInput = true;
            buttons.Add(button);
        }
    }

    public class PopupButton
    {
        public delegate void RunFunction();
        public RunFunction genericFunction;
        public delegate void RunButtonSpecificFunction(PopupButton button);        
        public RunButtonSpecificFunction buttonSpecificFunction;
        public delegate void RunIDFunctionInt(int ID);
        public RunIDFunctionInt IDfunctionInt;
        public delegate void RunIDFunctionString(string ID);
        public RunIDFunctionString IDfunctionString;
        public int buttonIDInt;
        public string buttonIDString;
        public string buttonText;
        public string buttonTextOn;
        public string buttonTextOff;
        public Texture2D texture;
        public float buttonWidth;
        public bool isToggleButton;
        public bool toggleState;
        public PopupElement popupElement;

        public GUIStyle style;
        Color selectedColor = Color.red;
        Color normalColor = Color.white;
        Color disabledColor = Color.gray;

        public void setupGUIStyle()
        {
            style = new GUIStyle(); //GUI.skin.GetStyle("Button"));
            //style.normal.background = texture;
            //style.border = new RectOffset(1,1,1,1);
            style.alignment = TextAnchor.LowerCenter;
        }

        public void setTexture(Texture2D normalTexture, Texture2D hoverTexture, Texture2D focusedTexture, Texture2D activeTexture)
        {            
            style.normal.background = normalTexture;
            style.hover.background = hoverTexture;
            style.focused.background = focusedTexture;
            style.active.background = activeTexture;            
        }

        public void setTexture(Texture2D allStatesTextures)
        {
            setTexture(allStatesTextures, allStatesTextures, allStatesTextures, allStatesTextures);
        }

        private bool _styleSelected;
        public bool styleSelected
        {
            set
            {
                _styleSelected = value;
                if (value)
                {
                    style.normal.textColor = selectedColor;
                }
                else
                {
                    style.normal.textColor = normalColor;
                }
            }
            get
            {
                return _styleSelected;
            }
        }

        private bool _styleDisabled;
        public bool styleDisabled
        {
            set
            {
                _styleDisabled = value;
                if (value)
                {
                    style.normal.textColor = disabledColor;
                }
                else
                {
                    style.normal.textColor = normalColor;
                }
            }
            get
            {
                return _styleDisabled;
            }
        }

        public PopupButton()
        {
            setupGUIStyle();
        }

        public PopupButton(string text, float width, RunFunction function)
        {
            buttonText = text;
            buttonWidth = width;
            genericFunction = function;
            isToggleButton = false;
            setupGUIStyle();
        }

        public PopupButton(string textOn, string textOff, float width, RunFunction function)
        {
            buttonText = textOff;
            buttonTextOn = textOn;
            buttonTextOff = textOff;
            isToggleButton = true;
            buttonWidth = width;
            genericFunction = function;
            setupGUIStyle();
        }

        public PopupButton(string text, float width, RunButtonSpecificFunction function)
        {
            buttonText = text;
            buttonWidth = width;
            buttonSpecificFunction = function;
            setupGUIStyle();
        }

        public PopupButton(string text, float width, RunIDFunctionInt function, int ID)
        {
            buttonText = text;
            buttonWidth = width;
            IDfunctionInt = function;
            buttonIDInt = ID;
            setupGUIStyle();
        }

        public PopupButton(string text, float width, RunIDFunctionString function, string ID)
        {
            buttonText = text;
            buttonWidth = width;
            IDfunctionString = function;
            buttonIDString = ID;
            setupGUIStyle();
        }

        public void toggle(bool newState)
        {
            toggleState = newState;
            if (newState)
            {
                buttonText = buttonTextOn;
            }
            else
            {
                buttonText = buttonTextOff;
            }
        }
    }
}
