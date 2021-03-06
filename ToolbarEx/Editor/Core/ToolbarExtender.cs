﻿#if UNITY_EDITOR

﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{
    [InitializeOnLoad]
    public static class ToolbarExtender
    {
        static int m_toolCount;
        static GUIStyle m_commandStyle = null;

        public static readonly List<Action> LeftToolbarGUI = new List<Action>();
        public static readonly List<Action> RightToolbarGUI = new List<Action>();

        static ToolbarExtender()
        {
            ToolInit();
        }

        private static void ToolInit()
        {
            Type toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
            FieldInfo toolIcons = toolbarType.GetField("s_ShownToolIcons",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            var arr =  ((Array) toolIcons.GetValue(null));
            int mToolIconsCount = 7;
            if (arr != null)
            {
                mToolIconsCount = arr.Length;
            }
            else
            {
#if UNITY_2019
                mToolIconsCount = 7;
#else
                mToolIconsCount = 6;
#endif

            }
            m_toolCount = toolIcons != null ? mToolIconsCount : 6;
            //Add OnToolbarGUI Update
            ToolbarCallback.OnToolbarGUI -= OnGUI;
            ToolbarCallback.OnToolbarGUI += OnGUI;
        }

        private static void OnGUI()
        {
            if (m_commandStyle == null)
            {
                m_commandStyle = new GUIStyle("CommandLeft");
            }
            var screenWidth = EditorGUIUtility.currentViewWidth;

            // 플레이버튼의 위치 (400은 플레이버튼 영역 rect가 차지하는 가로사이즈 같음)
            float playButtonsPosition = (screenWidth - 140) / 2;
            Rect leftRect = new Rect(0, 0, screenWidth, Screen.height);

            ///// 이곳은 왼쪽 부분에 있는 유니티 기본 GUI들 사이즈를 계산해서 그릴 요소들을 밀어주는 곳   /////
            leftRect.xMin += 10; // Spacing left
            leftRect.xMin += 32 * m_toolCount; // Tool buttons
            leftRect.xMin += 20; // Spacing between tools and pivot
            leftRect.xMin += 64 * 2; // Pivot buttons
            leftRect.xMax = playButtonsPosition;

            Rect rightRect = new Rect(0, 0, screenWidth, Screen.height);
            rightRect.xMin = playButtonsPosition;
            rightRect.xMin += m_commandStyle.fixedWidth * 3; // Play buttons
            rightRect.xMax = screenWidth;











            ///// 이곳은 오른쪽 부분에 있는 유니티 기본 GUI들 사이즈를 계산해서 그릴 요소들을 밀어주는 곳인듯 함 /////
            rightRect.xMax -= 10; // Spacing right
            rightRect.xMax -= 80; // Layout
            rightRect.xMax -= 10; // Spacing between layout and layers
            rightRect.xMax -= 80; // Layers
            rightRect.xMax -= 20; // Spacing between layers and account
            rightRect.xMax -= 80; // Account
            rightRect.xMax -= 10; // Spacing between account and cloud
            rightRect.xMax -= 32; // Cloud
            rightRect.xMax -= 10; // Spacing between cloud and collab
            rightRect.xMax -= 78; // Colab


            // Add spacing around existing controls
            leftRect.xMin += 10;
            leftRect.xMax -= 10;
            rightRect.xMin += 10;
            rightRect.xMax -= 10;

            // Add top and bottom margins
            leftRect.y = 5;
            leftRect.height = 24;
            rightRect.y = 5;
            rightRect.height = 24;


            //Draw Left Elements.
            if (leftRect.width > 0)
            {
                GUILayout.BeginArea(leftRect);
                GUILayout.BeginHorizontal();
                foreach (var handler in LeftToolbarGUI)
                {
                    handler();
                }

                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }

            //Draw Right Elements.
            if (rightRect.width > 0)
            {
                GUILayout.BeginArea(rightRect);
                GUILayout.BeginHorizontal();
                foreach (var handler in RightToolbarGUI)
                {
                    handler();
                }

                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
        }
    }
}
#endif