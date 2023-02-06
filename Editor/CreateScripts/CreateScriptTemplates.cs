using UnityEditor;
using UnityEngine;

namespace hrolgarUllr.Editor.CreateScripts
{
    public static class CreateScriptTemplates
    {
        [MenuItem("Assets/Create/Code/NewMonoBehaviour", priority = 40)]
        public static void CreateMonoBehaviour()
        {
            var path = "../../Unity Packages/Tools/Editor/CreateScripts/Templates/MonoBehaviour.cs.txt";
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewMono.cs");
        }
    }
}