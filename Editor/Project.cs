using System.Collections.Generic;
using System.Linq;

namespace GameWorkstore.UnityMultiplayerEnvironment
{
    public class Project : System.ICloneable
    {
        public string Name;
        public string ProjectPath;
        private string RootPath;
        public string AssetPath;
        public string ProjectSettingsPath;
        public string LibraryPath;
        public string PackagesPath;
        public string AutoBuildPath;
        private char[] _separator = new char[1] { '/' };


        /// <summary>
        /// Default constructor
        /// </summary>
        public Project()
        {
        }


        /// <summary>
        /// Initialize the project object by parsing its full path returned by Unity into a bunch of individual folder names and paths.
        /// </summary>
        /// <param name="path"></param>
        public Project(string path)
        {
            ParsePath(path);
        }


        /// <summary>
        /// Create a new object with the same settings
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Project newProject = new Project();
            newProject.RootPath = RootPath;
            newProject.ProjectPath = ProjectPath;
            newProject.AssetPath = AssetPath;
            newProject.ProjectSettingsPath = ProjectSettingsPath;
            newProject.LibraryPath = LibraryPath;
            newProject.Name = Name;
            newProject._separator = _separator;
            newProject.PackagesPath = PackagesPath;
            newProject.AutoBuildPath = AutoBuildPath;

            return newProject;
        }


        /// <summary>
        /// Update the project object by renaming and reparsing it. Pass in the new name of a project, and it'll update the other member variables to match.
        /// </summary>
        /// <param name="name"></param>
        public void UpdateNewName(string newName)
        {
            Name = newName;
            ParsePath(RootPath + "/" + Name + "/Assets");
        }


        /// <summary>
        /// Debug override so we can quickly print out the project info.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string printString = Name + "\n" +
                                 RootPath + "\n" +
                                 ProjectPath + "\n" +
                                 AssetPath + "\n" +
                                 ProjectSettingsPath + "\n" +
                                 PackagesPath + "\n" +
                                 AutoBuildPath + "\n" +
                                 LibraryPath;
            return (printString);
        }

        private void ParsePath(string path)
        {
            //Unity's Application functions return the Assets path in the Editor. 
            ProjectPath = path;

            //pop off the last part of the path for the project name, keep the rest for the root path
            List<string> pathArray = ProjectPath.Split(_separator).ToList<string>();
            Name = pathArray.Last();

            pathArray.RemoveAt(pathArray.Count() - 1);
            RootPath = string.Join(_separator[0].ToString(), pathArray);

            AssetPath = ProjectPath + "/Assets";
            ProjectSettingsPath = ProjectPath + "/ProjectSettings";
            LibraryPath = ProjectPath + "/Library";
            PackagesPath = ProjectPath + "/Packages";
            AutoBuildPath = ProjectPath + "/AutoBuild";

        }
    }
}
