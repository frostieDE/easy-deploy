using System;
using System.Collections.Generic;
using System.IO;

namespace EasyDeploy.Core.IO
{
    public class SpecialFoldersHelper : ISpecialFoldersHelper
    {
        private readonly Dictionary<string, Environment.SpecialFolder> folders;

        public SpecialFoldersHelper()
        {
            folders = new Dictionary<string, Environment.SpecialFolder>();
            folders.Add("Windows", Environment.SpecialFolder.Windows);
            folders.Add("System", Environment.SpecialFolder.System);
            folders.Add("ProgramFilesX86", Environment.SpecialFolder.ProgramFilesX86);
            folders.Add("ProgramFiles", Environment.SpecialFolder.ProgramFiles);
            folders.Add("CommonApplicationData", Environment.SpecialFolder.CommonApplicationData);
            folders.Add("CommonStartMenu", Environment.SpecialFolder.CommonStartMenu);
            folders.Add("CommonPrograms", Environment.SpecialFolder.CommonPrograms);
            folders.Add("CommonStartup", Environment.SpecialFolder.CommonStartup);
            folders.Add("CommonDesktopDirectory", Environment.SpecialFolder.CommonDesktopDirectory);
            folders.Add("CommonProgramFiles", Environment.SpecialFolder.CommonProgramFiles);
            folders.Add("CommonProgramFilesX86", Environment.SpecialFolder.CommonProgramFilesX86);
            folders.Add("CommonDocuments", Environment.SpecialFolder.CommonDocuments);
        }

        public string GetRealPath(string path)
        {
            if(!path.StartsWith("%"))
            {
                return path;
            }

            var pos = path.IndexOf("%");
            if(pos < 0)
            {
                return path;
            }

            var specialFolder = path.Substring(1, pos - 1);

            if(folders.ContainsKey(specialFolder))
            {
                return Path.Combine(specialFolder, path.Substring(pos + 1));
            }

            return path;
        }
    }
}
