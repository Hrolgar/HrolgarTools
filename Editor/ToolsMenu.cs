using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using static System.IO.Path;
using static System.IO.Directory;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using static hrolgarUllr.Editor.GUI.TextInputPrompt;

namespace hrolgarUllr.Editor
{
    public static class ToolsMenu
    {
        [MenuItem("HrolTools/Reset Project")]
        private static void ResetPrefs() => EditorPrefs.SetInt("EditorPrefsInitializedProject", 0);

        [MenuItem("HrolTools/Setup/Initialize Project", true)]
        private static bool ValidateInitializeProject() => EditorPrefs.GetInt("EditorPrefsInitializedProject") != 1;

        [MenuItem("HrolTools/Setup/Initialize Project")]
        private static void InitializeProject()
        {
            Prompt(
                "Enter your root namespace",
                "RootNamespace",
                root =>
                {
                    if (string.IsNullOrEmpty(root)) return;
                    EditorSettings.projectGenerationRootNamespace = root;
                    EditorPrefs.SetInt("EditorPrefsInitializedProject", 1);
                    
                    CreateDefaultFolders();
                });
        }
        
        public static void CreateDefaultFolders()
        {
            var allFolders = new Dictionary<string, Dictionary<string, List<string>>>
            {
                {
                    "_Project", new Dictionary<string, List<string>>
                    {
                        {
                            "_Code", new List<string>
                                { "_Scripts", "Editor", "Shaders" }
                        },
                        {
                            "Art", new List<string>
                                { "Materials", "Models", "Textures" }
                        },
                        {
                            "Audio", new List<string>
                                { "Music", "Sound" }
                        },
                        {
                            "Level", new List<string>
                                { "_Prefabs", "_Scenes" }
                        }
                    }
                }
            };
            DirWithDict(allFolders);
            Refresh();
        }

        public static void DirWithDict (Dictionary<string, Dictionary<string, List<string>>> allFolders)
        {
            foreach (var folder in allFolders)
            {
                var fullPath = Combine(dataPath, folder.Key);
                foreach (var dir in folder.Value)
                {
                    var subDir = Combine(fullPath, dir.Key);
                    foreach (var newDir in dir.Value)
                        CreateDirectory(Combine(subDir, newDir));
                }
            }
        }

        private static AddAndRemoveRequest _requests;
        private static string[] _packages = { "com.unity.cinemachine" };
        
        [MenuItem("HrolTools/Setup/Download Assets")]
        public static void DownloadAssets()
        {
            foreach (var package in _packages)
            {
                if (PackageInfo.GetAllRegisteredPackages().All(p => p.name != package)) continue;
                var a = package;
                _packages = _packages?.Where(v => v != a).ToArray();
                Debug.Log($"{package} is already installed.");
            }

            if (_packages?.Length < 1) return;
            _requests = Client.AddAndRemove(_packages);
            EditorApplication.update += Progress;

            if (!File.Exists("Assets/TextMesh Pro/Resources/TMP Settings.asset"))
                ImportPackage("Packages/com.unity.textmeshpro/Package Resources/TMP Essential Resources.unitypackage", false);
            Refresh();
        }

        private static void Progress()
        {
            if (_requests is not null && !_requests.IsCompleted) return;
            if (_requests?.Status == StatusCode.Success)
            {
                foreach (var request in _requests.Result)
                {
                    if (PackageInfo.GetAllRegisteredPackages().All(p => p.name != request.name) && _packages.Any(p => p == request.name))
                    {
                        Debug.Log($"Successfully installed {request.displayName}, version {request.version}");
                    }
                }
            }
            else
            {
                Debug.Log(_requests?.Error.message);
            }
            EditorApplication.update -= Progress;
        }
    }
}