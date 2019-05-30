#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
    public class GUIStyleManager
    {
        public static Texture2D ui_btn_01 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/GameUI/Sprite_V2/UI_Btn_00.png");
        public static Texture2D ui_btn_02 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/GameUI/Sprite_V2/UI_Btn_01.png");
        public static Texture2D ui_btn_03 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/GameUI/Sprite_V2/UI_Panel_00.png");
        public static Texture2D ui_leftArrow = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/GameUI/Sprite_V2/UI_Btn_Back_00.png");
        public static Texture2D ui_close = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/GameUI/Sprite_V2/UI_Btn_Close_00.png");
        public static Texture2D ui_black_bt = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/Editor/Black-Button-Rounded.png"); 
        public static Texture2D ui_red_bt = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/Editor/Red-Button-Rounded.png");
        public static Texture2D ui_green_bt = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/Editor/Green-Button-Rounded.png");
        public static Texture2D ui_blue_bt = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resource/UIResource/Editor/Blue-Button-Rounded.png");


        /// preset. 
        public static readonly GUIStyle commandButtonStyle;
        public static readonly GUIStyle middleSize;
        public static readonly GUIStyle largeSize;
        /// <summary>
        /// Get Custom Style.
        /// </summary> 
        public static GUIStyle GetCustomStyle(float width = 50, int fontSize = 11, FontStyle fontStyle = FontStyle.Normal, Texture2D btnTexture = null, float r = -1, float g = -1, float b = -1)
        {
            GUIStyle style = new GUIStyle("Command")
            {
                fixedWidth = width,
                fontSize = 11,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = fontStyle
            };

            if (btnTexture != null)
                style.normal.background = btnTexture;

            if(r != -1 || g != -1 || b != -1)
            {
                style.normal.textColor = new Color(r/255f, g/255f, b/255f);
            }
            return style;
        }

        /// <summary>
        /// GUIStyle make 
        /// </summary>
        static GUIStyleManager()
        {
            commandButtonStyle = new GUIStyle("Command") {
                fixedWidth = 50,
                fontSize = 11,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Normal
            };
            middleSize = new GUIStyle("Command") {
                fixedWidth = 70,
                fontSize = 11,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Normal
            };
            largeSize = new GUIStyle("Command") {
                fixedWidth = 100,
                fontSize = 11,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Normal
            };
        }
    }
} 
#endif