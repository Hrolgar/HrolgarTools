using System;
using UnityEditor;
using UnityEngine;

namespace hrolgarUllr.Editor.GUI
{
    public class TextInputPrompt : EditorWindow
    {
        private static string _result;
        private static Action<string> _callback;
        private static string _title;

        public static void Prompt(string title, string defaultValue, Action<string> callback)
        {
            var window = GetWindow(typeof(TextInputPrompt), true, title);
            window.ShowUtility();
            _result = defaultValue;
            _title = title;
            _callback = callback;
        }

        private void OnGUI()
        {
            _result = EditorGUILayout.TextField(_title, _result);
            if (GUILayout.Button("OK"))
            {
                _callback(_result);
                Close();
            }
        }
    }
}
