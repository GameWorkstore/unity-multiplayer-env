using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameWorkstore.UnityMultiplayerEnvironment
{
    /**
	 *	An editor utility for easily creating symlinks in your project.
	 *
	 *	Adds a Menu item under `Assets/Create/Folder (Symlink)`, and 
	 *	draws a small indicator in the Project view for folders that are
	 *	symlinks.
	 */
    /*
    [InitializeOnLoad]
    public static class SymlinkUtility
    {
        // FileAttributes that match a junction folder.
        private const FileAttributes k_FOLDER_SYMLINK_ATTRIBS = FileAttributes.Directory | FileAttributes.ReparsePoint;

        // Style used to draw the symlink indicator in the project view.
        private static GUIStyle s_SymlinkMarkerStyle = null;
        private static GUIStyle symlinkMarkerStyle
        {
            get
            {
                if (s_SymlinkMarkerStyle == null)
                {
                    s_SymlinkMarkerStyle = new GUIStyle(EditorStyles.label);
                    ColorUtility.TryParseHtmlString("#FFFF00", out Color color);
                    s_SymlinkMarkerStyle.normal.textColor = color;
                    s_SymlinkMarkerStyle.alignment = TextAnchor.MiddleRight;
                }
                return s_SymlinkMarkerStyle;
            }
        }

        // Static constructor subscribes to projectWindowItemOnGUI delegate.
        static SymlinkUtility()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        // Draw a little indicator if folder is a symlink
        private static void OnProjectWindowItemGUI(string guid, Rect r)
        {
            try
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                if (!string.IsNullOrEmpty(path))
                {
                    FileAttributes attribs = File.GetAttributes(path);

                    if ((attribs & k_FOLDER_SYMLINK_ATTRIBS) == k_FOLDER_SYMLINK_ATTRIBS)
                        GUI.Label(r, "<link>", symlinkMarkerStyle);
                }
            }
            catch { }
        }

        [MenuItem("Assets/Link/(Symlink to Selected Folder)", false, 10)]
        private static void SymlinkToSelectedFolder()
        {
            string sourceFolderPath = EditorUtility.OpenFolderPanel("Select Folder Source", "", "");

            if (string.IsNullOrEmpty(sourceFolderPath))
                return;

            if (sourceFolderPath.Contains(Application.dataPath))
            {
                UnityEngine.Debug.LogWarning("Cannot create a symlink to folder in your project!");
                return;
            }

            Object uobject = Selection.activeObject;

            string targetPath = uobject != null ? AssetDatabase.GetAssetPath(uobject) : null;

            SymlinkInternal(sourceFolderPath, targetPath);
        }

        [MenuItem("Assets/Link/(Symlink to Packages)", false, 10)]
        private static void SymlinkToPackagesFolder()
        {
            string sourceFolderPath = EditorUtility.OpenFolderPanel("Select Folder Source", "", "");

            if (string.IsNullOrEmpty(sourceFolderPath))
                return;

            if (sourceFolderPath.Contains(Application.dataPath))
            {
                UnityEngine.Debug.LogWarning("Cannot create a symlink to folder in your project!");
                return;
            }

            SymlinkInternal(sourceFolderPath, "packages");
        }

        private static void SymlinkInternal(string sourceFolderPath, string targetPath)
        {
            if (string.IsNullOrEmpty(targetPath))
            {
                return;
            }

            // use absolute path only
            string sourceFolderName = sourceFolderPath.Split(new char[] { '/', '\\' }).LastOrDefault();

            if (string.IsNullOrEmpty(sourceFolderName))
            {
                UnityEngine.Debug.LogWarning("Couldn't deduce the folder name?");
                return;
            }

            FileAttributes attribs = File.GetAttributes(targetPath);

            if ((attribs & FileAttributes.Directory) != FileAttributes.Directory)
                targetPath = Path.GetDirectoryName(targetPath);

            // Get path to project.
            string pathToProject = Application.dataPath.Split(new string[1] { "/Assets" }, System.StringSplitOptions.None)[0];

            targetPath = string.Format("{0}/{1}/{2}", pathToProject, targetPath, sourceFolderName);

            if (Directory.Exists(targetPath))
            {
                UnityEngine.Debug.LogError(string.Format("A folder already exists at this location, aborting link.\n{0} -> {1}", sourceFolderPath, targetPath));
                return;
            }

#if UNITY_EDITOR_WIN
            string linkOption = "/D";// use Symlink only
            string command = string.Format("mklink {0} \"{1}\" \"{2}\"", linkOption, targetPath, sourceFolderPath);
            ExecuteCmdCommand(command, true); // Symlinks require admin privilege on windows, junctions do not.
#elif UNITY_EDITOR_OSX
            // For some reason, OSX doesn't want to create a symlink with quotes around the paths, so escape the spaces instead.
            sourcePath = sourcePath.Replace(" ", "\\ ");
            targetPath = targetPath.Replace(" ", "\\ ");
            string command = string.Format("ln -s {0} {1}", sourcePath, targetPath);
            ExecuteBashCommand(command);
#elif UNITY_EDITOR_LINUX
            // Is Linux the same as OSX?
#endif

            UnityEngine.Debug.LogError(string.Format("Created symlink: {0} <=> {1}", targetPath, sourceFolderPath));

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        private static void ExecuteCmdCommand(string command, bool asAdmin)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "CMD.exe",
                Arguments = "/C " + command,
                UseShellExecute = asAdmin,
                RedirectStandardError = !asAdmin,
                CreateNoWindow = true,
            };
            if (asAdmin)
            {
                startInfo.Verb = "runas"; // Runs process in admin mode. See https://stackoverflow.com/questions/2532769/how-to-start-a-process-as-administrator-mode-in-c-sharp
            }
            var proc = new Process()
            {
                StartInfo = startInfo
            };

            using (proc)
            {
                proc.Start();
                proc.WaitForExit();

                if (!asAdmin && !proc.StandardError.EndOfStream)
                {
                    UnityEngine.Debug.LogError(proc.StandardError.ReadToEnd());
                }
            }
        }

        private static void ExecuteBashCommand(string command)
        {
            command = command.Replace("\"", "\"\"");

            var proc = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            using (proc)
            {
                proc.Start();
                proc.WaitForExit();

                if (!proc.StandardError.EndOfStream)
                {
                    UnityEngine.Debug.LogError(proc.StandardError.ReadToEnd());
                }
            }
        }
    }
    */
}
