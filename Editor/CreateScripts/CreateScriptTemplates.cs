using System.IO;
using System.Linq;
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
        
        // var packagesPath = Application.dataPath.Replace("Assets", "Packages");
        // const string tempPath = "com.hrolgarullr.tools/Editor/CreateScripts/Templates/MonoBehaviour.cs.txt";
        // var path = Path.Combine(packagesPath, tempPath);

    }
}