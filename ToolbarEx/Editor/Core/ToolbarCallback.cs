#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEngine.UIElements;

namespace UnityToolbarExtender
{
    public static class ToolbarCallback
    {
        //어셈블리 강제로 가져옴
        static Type m_toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");

        static Type m_guiViewType = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");

        static PropertyInfo m_viewVisualTree = m_guiViewType.GetProperty("visualTree",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        static FieldInfo m_imguiContainerOnGui = typeof(IMGUIContainer).GetField("m_OnGUIHandler",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        static ScriptableObject m_currentToolbar;

        /// <summary>
        /// Callback for toolbar OnGUI method.
        /// </summary>
        public static Action OnToolbarGUI;

        static ToolbarCallback()
        { 
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
        }

        static void OnUpdate()
        { 
            if (m_currentToolbar == null)
            { 
                var toolbars     = Resources.FindObjectsOfTypeAll(m_toolbarType); 
                m_currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;


                if (m_currentToolbar != null)
                { 
                    var visualTree = (VisualElement) m_viewVisualTree.GetValue(m_currentToolbar, null); 
                    var container  = (IMGUIContainer) visualTree[0]; 
                    var handler = (Action) m_imguiContainerOnGui.GetValue(container);
                    handler -= OnGUI;
                    handler += OnGUI;
                    m_imguiContainerOnGui.SetValue(container, handler);
                }
            }
        }

        static void OnGUI()
        { 
            var handler = OnToolbarGUI;
            if (handler != null) handler();
        }
    }
} 
#endif