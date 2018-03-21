using System;
using System.IO;
using System.Linq;
using Demo.Helpers.Extensions;
using Demo.Reflection.Extensions;

namespace Web.Core.Web
{
    public class WebContentDirectoryFinder
    {
        public static string CalculateContentRootFolder()
        {
            var coreAssemblyDirectoryPath = Path.GetDirectoryName(typeof(WebContentDirectoryFinder).GetAssembly().Location);
            if (coreAssemblyDirectoryPath == null)
            {
                throw new Exception("Could not find location of Demo assembly!");
            }

            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);

            while (directoryInfo.GetFiles("appsettings.json").IsNullOrEmpty() && !Directory.Exists(Path.Combine(directoryInfo.FullName, "Demo.Web")))
            {
                if (directoryInfo.Parent == null)
                {
                    throw new Exception("Could not find content root folder!");
                }

                directoryInfo = directoryInfo.Parent;
            }

            var webMvcFolder = Path.Combine(directoryInfo.FullName, "Demo.Web");
            if (Directory.Exists(webMvcFolder))
            {
                return webMvcFolder;
            }

            throw new Exception("Could not find root folder of the web project!");
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
