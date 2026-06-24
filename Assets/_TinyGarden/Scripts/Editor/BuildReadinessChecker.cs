#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

namespace TinyGarden.Editor
{
    public class BuildReadinessChecker
    {
        private static readonly string[] RequiredScenes = new string[]
        {
            "Assets/_TinyGarden/Scenes/Boot.unity",
            "Assets/_TinyGarden/Scenes/MainMenu.unity",
            "Assets/_TinyGarden/Scenes/Garden.unity",
            "Assets/_TinyGarden/Scenes/ColorMatch.unity",
            "Assets/_TinyGarden/Scenes/CountingFruits.unity",
            "Assets/_TinyGarden/Scenes/ShapeSort.unity"
        };

        private static readonly string[] ForbiddenTerms = new string[]
        {
            "ads", "analytics", "firebase", "crashlytics", "admob", "applovin",
            "ironsource", "facebook", "meta", "adjust", "appsflyer", "onesignal",
            "gameanalytics", "unity.ads", "unity.analytics"
        };

        [MenuItem("Tiny Garden/Check Build Readiness")]
        public static void CheckReadiness()
        {
            Debug.Log("--- Starting Build Readiness Check ---");
            bool passed = true;

            passed &= CheckScenes();
            passed &= CheckForbiddenSDKs();
            passed &= CheckPlayerSettings();

            if (passed)
            {
                Debug.Log("<color=green>SUCCESS: Project is clean, child-safe, and ready for Build.</color>");
            }
            else
            {
                Debug.LogError("<color=red>FAILED: Build Readiness Check failed. See above logs.</color>");
            }
            Debug.Log("--- End Build Readiness Check ---");
        }

        private static bool CheckScenes()
        {
            var buildScenes = EditorBuildSettings.scenes.Select(s => s.path).ToList();
            bool allFound = true;

            foreach (var req in RequiredScenes)
            {
                if (!buildScenes.Contains(req))
                {
                    Debug.LogError($"[Build Check] Missing Required Scene in Build Settings: {req}");
                    allFound = false;
                }
            }
            
            // Check order
            if (allFound && buildScenes.Count >= 6)
            {
                if (buildScenes[0] != RequiredScenes[0])
                {
                    Debug.LogError("[Build Check] Boot scene must be at index 0 in Build Settings.");
                    allFound = false;
                }
            }

            return allFound;
        }

        private static bool CheckForbiddenSDKs()
        {
            bool clean = true;
            string projectPath = Directory.GetParent(Application.dataPath).FullName;

            // Directories and files to scan
            var searchPaths = new string[] 
            {
                Path.Combine(projectPath, "Assets"),
                Path.Combine(projectPath, "Packages"),
                Path.Combine(projectPath, "ProjectSettings")
            };

            foreach (var path in searchPaths)
            {
                if (!Directory.Exists(path)) continue;

                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                    .Where(f => f.EndsWith(".json") || f.EndsWith(".asmdef") || f.EndsWith(".dll") || 
                                f.EndsWith(".aar") || f.EndsWith(".framework") || f.EndsWith(".xcframework") ||
                                f.EndsWith(".xml") || f.EndsWith(".gradle"))
                    .ToList();

                // Add directories as well
                var dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories).ToList();

                // Check directory names
                foreach (var dir in dirs)
                {
                    string dirName = new DirectoryInfo(dir).Name.ToLower();
                    if (ForbiddenTerms.Any(term => dirName.Contains(term)))
                    {
                        Debug.LogError($"[Build Check] FORBIDDEN SDK TERM IN PATH: {dir}\nThis violates Apple Kids/Google Play Families policy.");
                        clean = false;
                    }
                }

                // Check file names
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file).ToLower();
                    if (ForbiddenTerms.Any(term => fileName.Contains(term)))
                    {
                        Debug.LogError($"[Build Check] FORBIDDEN SDK TERM IN FILE: {file}\nThis violates Apple Kids/Google Play Families policy.");
                        clean = false;
                    }
                }
            }

            return clean;
        }

        private static bool CheckPlayerSettings()
        {
            bool ok = true;
            
            if (PlayerSettings.defaultInterfaceOrientation != UIOrientation.Portrait)
            {
                Debug.LogError("[Build Check] Player Settings: Orientation must be set to Portrait only.");
                ok = false;
            }

            // Check Company and Product Name
            if (PlayerSettings.companyName == "DefaultCompany")
            {
                Debug.LogError("[Build Check] Player Settings: DefaultCompany is not allowed. Please set a real company name.");
                ok = false;
            }

            if (PlayerSettings.productName == "" || PlayerSettings.productName.Contains("Default"))
            {
                Debug.LogError("[Build Check] Player Settings: Invalid product name.");
                ok = false;
            }

            // Check Permissions (No microphone, camera, location)
            #if UNITY_ANDROID
            if (PlayerSettings.Android.forceInternetPermission)
            {
                Debug.LogError("[Build Check] Android Settings: forceInternetPermission is true. MVP requires NO internet.");
                ok = false;
            }
            #endif

            #if UNITY_IOS
            if (!string.IsNullOrEmpty(PlayerSettings.iOS.cameraUsageDescription) || 
                !string.IsNullOrEmpty(PlayerSettings.iOS.locationUsageDescription) ||
                !string.IsNullOrEmpty(PlayerSettings.iOS.microphoneUsageDescription))
            {
                Debug.LogError("[Build Check] iOS Settings: Unnecessary permissions requested (Camera/Location/Mic).");
                ok = false;
            }
            #endif

            // Check IL2CPP
            var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            if (PlayerSettings.GetScriptingBackend(targetGroup) != ScriptingImplementation.IL2CPP)
            {
                Debug.LogWarning("[Build Check] Note: IL2CPP is recommended for Release builds (required for iOS).");
            }

            return ok;
        }
    }
}
#endif
