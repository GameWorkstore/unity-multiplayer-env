using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameWorkstore.UnityMultiplayerEnvironment
{
    /// <summary>
    /// Provides Unity Editor window for ProjectCloner.
    /// </summary>
	public class UnityMultiplayerEnvironmentWindow : EditorWindow
    {
        /// <summary>
        /// True if currently open project is a clone.
        /// </summary>
        public bool isClone
        {
            get { return UnityMultiplayerEnvironmentTool.IsClone(); }
        }

        /// <summary>
        /// Returns true if project clone exists or currently open project is a clone itself.
        /// </summary>
        public bool isCloneCreated
        {
            get { return UnityMultiplayerEnvironmentTool.GetCloneProjectPath() != string.Empty; }
        }

        [MenuItem("Window/Unity Multiplayer Environment")]
        private static void InitWindow()
        {
            UnityMultiplayerEnvironmentWindow window = (UnityMultiplayerEnvironmentWindow)EditorWindow.GetWindow(typeof(UnityMultiplayerEnvironmentWindow));
            window.titleContent = new GUIContent(nameof(UnityMultiplayerEnvironmentWindow));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Clone Settings", EditorStyles.boldLabel);

            if (isClone)
            {
                /// If it is a clone project...
                string originalProjectPath = UnityMultiplayerEnvironmentTool.GetOriginalProjectPath();
                if (originalProjectPath == string.Empty)
                {
                    /// If original project cannot be found, display warning message.
                    string thisProjectName = UnityMultiplayerEnvironmentTool.GetCurrentProject().Name;
                    string supposedOriginalProjectName = UnityMultiplayerEnvironmentTool.GetCurrentProject().Name.Replace("_clone", "");
                    EditorGUILayout.HelpBox(
                        "This project is a clone, but the link to the original seems lost.\nYou have to manually open the original and create a new clone instead of this one.\nThe original project should have a name '" + supposedOriginalProjectName + "', if it was not changed.",
                        MessageType.Warning);
                }
                else
                {
                    /// If original project is present, display some usage info.
                    EditorGUILayout.HelpBox(
                        "This project is a clone of the project '" + Path.GetFileName(originalProjectPath) + "'.\nIf you want to make changes the project files or manage clones, please open the original project through Unity Hub.",
                        MessageType.Info);
                }
            }
            else
            {
                /// If it is an original project...
                if (isCloneCreated)
                {
                    /// If clone is created, we can either open it or delete it.
                    string cloneProjectPath = UnityMultiplayerEnvironmentTool.GetCloneProjectPath();
                    EditorGUILayout.TextField("Clone Project Path", cloneProjectPath, EditorStyles.textField);
                    EditorGUILayout.HelpBox("Find your cloned project side-by-side to this project with _clone as suffix. Open it using Unity Hub.", MessageType.Info);
                    if (GUILayout.Button("Open Unity Hub"))
                    {
                        UnityMultiplayerEnvironmentTool.OpenProject(cloneProjectPath);
                    }

                    if (GUILayout.Button("Delete Clone"))
                    {
                        bool delete = EditorUtility.DisplayDialog(
                            "Delete the clone?",
                            "Are you sure you want to delete the clone project '" + UnityMultiplayerEnvironmentTool.GetCurrentProject().Name + "_clone'? If so, you can always create a new clone from ProjectCloner window.",
                            "Delete",
                            "Cancel");
                        if (delete)
                        {
                            UnityMultiplayerEnvironmentTool.DeleteClone();
                        }
                    }
                }
                else
                {
                    /// If no clone created yet, we must create it.
                    EditorGUILayout.HelpBox("No project clones found. Create a new one!", MessageType.Info);
                    if (GUILayout.Button("Create new clone"))
                    {
                        UnityMultiplayerEnvironmentTool.CreateCloneFromCurrent();
                    }
                }
            }
        }
    }
}
