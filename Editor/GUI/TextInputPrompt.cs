using UnityEditor;
using UnityEngine;

namespace hrolgarUllr.Editor.GUI
{
    public class TextInputPrompt : EditorWindow
    {
        private static string result;
        private static bool accepted;

        
        
        public static string Prompt(string title, string defaultValue, string message)
        {
            var window = GetWindow(typeof(TextInputPrompt), true, title);
            window.ShowUtility();
            result = defaultValue;
            
            return result;
        }

        private void OnGUI()
        {
            result = EditorGUILayout.TextField("", result);
            if (GUILayout.Button("OK"))
            {
                accepted = true;
                Close();
            }
        }
    }
}
