#if UNITY_EDITOR
 
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UnityToolbarExtender
{

    public class DrawToolBarConfig
    {
        public enum MODE { DEFAULT, DEBUG_VIEW }
        public static MODE mode;
    }
    public class DrawToolBarRightConfig
    {
        public enum MODE { DEFAULT, DEVELOP }
        public static MODE mode;
    }

    public class DrawItems
    {
        public string title;
        public System.Action callback;
    }

    public class DrawList
    { 
        public List<DrawItems> list = new List<DrawItems>();
        public DrawToolBarConfig.MODE mode;
        public int Max {get=>list.Count;}
        public int current = 0; 
        public void AddItem(string title, System.Action callback) { list.Add(new DrawItems() { callback = callback, title = title });}
        public void PrevPrev()
        {
            current = 0;
        }
        public void NextNext()
        {
            current = Max - 3;
        }
        public void Prev()
        {
            current = Mathf.Clamp(current - 1, 0, Max - 3);
        }
        public void Next()
        {
            current = Mathf.Clamp(current + 1, 0, Max - 3 );
        }
        public void Draw()
        {
            //DRAW ARROW
            if (Max > 3 && current > 0)
                DrawButton("<<", null, GUIStyleManager.GetCustomStyle(32, btnTexture: GUIStyleManager.ui_red_bt), PrevPrev);
            if (Max > 3 && current > 0)
                DrawButton("<", null, GUIStyleManager.GetCustomStyle(24, btnTexture: GUIStyleManager.ui_red_bt), Prev);



            //DRAW Coontent
            for (int i = current; i< current + 3; i++)
            {
                DrawButton(list[i].title,null, GUIStyleManager.GetCustomStyle(100, btnTexture: GUIStyleManager.ui_blue_bt), list[i].callback);
            }


            //DRAW ARROW
            if (Max > 3 && current+3 != Max )
                DrawButton(">", null, GUIStyleManager.GetCustomStyle(24, btnTexture: GUIStyleManager.ui_red_bt), Next);
            if (Max > 3 && current+3 != Max)
                DrawButton(">>", null, GUIStyleManager.GetCustomStyle(32, btnTexture: GUIStyleManager.ui_red_bt), NextNext);


            //DRAW Exit
            DrawButton("X", "TOOLTIP", GUIStyleManager.GetCustomStyle(32, btnTexture: GUIStyleManager.ui_red_bt, fontStyle: FontStyle.Bold), () =>
            {
                DrawToolBarConfig.mode = DrawToolBarConfig.MODE.DEFAULT;
            });
        }
        void DrawButton(string title, string tooltip, GUIStyle guiStyle, System.Action onClick)
        {
            if (GUILayout.Button(new GUIContent(title, tooltip), guiStyle))
            {
                onClick?.Invoke();
            }
        }
    }


 
    [InitializeOnLoad]
    public class DrawToolbar
    {  
        static DrawList draw_debug_view = new DrawList(); 
        static DrawToolbar()
        { 
            ToolbarExtender.LeftToolbarGUI.Add(OnLeftUI); 
            ToolbarExtender.RightToolbarGUI.Add(OnRightUI);


            draw_debug_view.AddItem("Menu 1", () => {Debug.Log("Test1"); });
            draw_debug_view.AddItem("Menu 2", () => {Debug.Log("Test2"); });
            draw_debug_view.AddItem("Menu 3", () => {Debug.Log("Test3"); });
            draw_debug_view.AddItem("Menu 4", () => {Debug.Log("Test4"); });
            draw_debug_view.AddItem("Menu 6", () => {Debug.Log("Test6"); });
            draw_debug_view.AddItem("Menu 7", () => {Debug.Log("Test7"); });
            draw_debug_view.AddItem("Menu 8", () => {Debug.Log("Test8"); });
            draw_debug_view.AddItem("Menu 9", () => {Debug.Log("Test9"); });
            draw_debug_view.AddItem("Menu 10", () =>{Debug.Log("Test10"); }); 
        }
         
        static void DrawButton(string title, string tooltip, GUIStyle guiStyle, System.Action onClick)
        {
            if (GUILayout.Button(new GUIContent(title, tooltip), guiStyle))
            {
                onClick?.Invoke();
            }
        }
        static void DrawCloseButton(System.Action onClick)
        {
            DrawButton(null, null, GUIStyleManager.GetCustomStyle(btnTexture: GUIStyleManager.ui_close, width: 25), onClick);
        }

        static void OnRightUI()
        {
            GUILayout.FlexibleSpace(); 
            if (DrawToolBarRightConfig.mode == DrawToolBarRightConfig.MODE.DEFAULT)
            {
                DrawButton("깃허브", "TOOLTIP", GUIStyleManager.GetCustomStyle(64, 11, FontStyle.Bold, GUIStyleManager.ui_red_bt, 0,255,0), () =>
                {
                    Application.OpenURL("http://github.com/shlifedev");
                });
                DrawButton("개발모드", "TOOLTIP", GUIStyleManager.GetCustomStyle(96, 11, FontStyle.Bold, GUIStyleManager.ui_blue_bt, 255, 255, 255), () =>
                {
                    DrawToolBarRightConfig.mode = DrawToolBarRightConfig.MODE.DEVELOP;
                }); 
            }
        }

        static void DrawDefault()
        { 

            if (DrawToolBarConfig.mode == DrawToolBarConfig.MODE.DEFAULT)
            {
                DrawButton("디버그 뷰", "TOOLTIP", GUIStyleManager.GetCustomStyle(100, btnTexture: GUIStyleManager.ui_red_bt, fontStyle: FontStyle.Bold), () =>
                {
                    DrawToolBarConfig.mode = DrawToolBarConfig.MODE.DEBUG_VIEW;
                });
            }

        }


        static void DrawUIDebugView()
        { 
            if (DrawToolBarConfig.mode == DrawToolBarConfig.MODE.DEBUG_VIEW)
            {
                draw_debug_view.Draw();
            } 
        }  


        static void OnLeftUI()
        {
            GUILayout.FlexibleSpace(); 
            DrawDefault(); 
            DrawUIDebugView();
        }
    }


} 
#endif