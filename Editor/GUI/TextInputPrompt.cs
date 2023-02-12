using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace hrolgarUllr.Editor.GUI
{
    public class TextInputPrompt : EditorWindow
    {
        private static string _result;
        private static Action<string> _callback;
        private static string _title;
        public static void Prompt (string title, string defaultValue, Action<string> callback)
        {
            var window = GetWindow(typeof(TextInputPrompt), true, title);
            window.ShowUtility();
            window.minSize = new Vector2(300, 180);
            window.maxSize = new Vector2(300, 180);
            _result = defaultValue;
            _title = title;
            _callback = callback;
        }

        private void OnGUI()
        {
            var shared = new GUIStyle()
            {
                fontSize = 10,
                normal = { textColor = Color.white },
                alignment = TextAnchor.MiddleCenter,
            };

            var boldLabelStyle = new GUIStyle(shared)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
            };

            var normalLabelStyle = new GUIStyle(shared)
            {
                wordWrap = true,
                margin = new RectOffset(20, 20, 0, 0)
            };

            var helpBoxStyle = new GUIStyle(EditorStyles.helpBox)
            {
                margin = new RectOffset(50, 50, 0, 0)
            };
            
            GUILayout.Space(10);
            GUILayout.Label("Welcome to Hrolgar's Tools!", boldLabelStyle);
            GUILayout.Space(15);
            GUILayout.Label(
                "This is a one time setup for your project. After you write the Root Namespace, we will initialize the reccomended folder structure.",
                normalLabelStyle);
            GUILayout.Space(10);
            GUILayout.BeginVertical(helpBoxStyle, GUILayout.MaxWidth(minSize.x - 100));
            GUILayout.Space(10);
            GUILayout.Label(_title, normalLabelStyle);
            _result = EditorGUILayout.TextField("", _result, GUILayout.MaxWidth(minSize.x - 100));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Cancel", new GUIStyle(shared)
                    {padding = new RectOffset(2,10,1,1)}))
            {
                Close();
            }
            if (GUILayout.Button("OK", new GUIStyle(shared)
                    {padding = new RectOffset(2,2,1,1)}))
            {
                _callback(_result);
                Close();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}
