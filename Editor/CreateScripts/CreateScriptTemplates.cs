using System.IO;
using UnityEditor;
using UnityEngine;

namespace hrolgarUllr.Editor.CreateScripts
{
    public static class CreateScriptTemplates
    {
        private static string SetTemplatePath (string template)
        {
            var packagesPath = Application.dataPath.Replace("Assets", "Packages");
            var tempPath = $"com.hrolgarullr.tools/Editor/CreateScripts/Templates/{template}";
            return Path.Combine(packagesPath, tempPath);
        }
        
        [MenuItem("Assets/Create/Code/NewMonoBehaviour", priority = 40)]
        public static void CreateMonoBehaviour() => 
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(SetTemplatePath("MonoBehaviour.cs.txt"), "NewMono.cs");
        
        [MenuItem("Assets/Create/Code/ScriptableObject", priority = 40)]
        public static void CreateScriptableObject() => 
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(SetTemplatePath("ScriptableObject.cs.txt"), "NewSO.cs");
    }
}