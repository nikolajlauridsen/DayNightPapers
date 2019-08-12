using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperLib.Properties;

namespace WallpaperLib
{
    public class WallpaperStore
    {
        private static string _cacheName = @"PaperCache";

        public string CacheFolder
        {
            get
            {
                if (Directory.Exists(_cacheName)) return _cacheName;
                Directory.CreateDirectory(_cacheName);
                return _cacheName;
            }
        }

        public string DayPaper
        {
            get
            {
                string dayPaperPath = GetAbsolutePath(Settings.Default.DayPaperName);
                if (File.Exists(dayPaperPath)) return dayPaperPath;
                else return null;

            }
            set
            {
                string newFileName = storePaper(Settings.Default.DayPaperName, value, "_day");
                Settings.Default.DayPaperName = newFileName;
                Settings.Default.Save();

            }
        }

        public string NightPaper
        {
            get
            {
                string nightPaperPath = GetAbsolutePath(Settings.Default.NightPaperName);
                if (File.Exists(nightPaperPath)) return nightPaperPath;
                else return null;
            }
            set
            {
                string newFileName = storePaper(Settings.Default.NightPaperName, value, "_night");
                Settings.Default.NightPaperName = newFileName;
                Settings.Default.Save();
            }
        }

        private string storePaper(string oldFileName, string newPath, string fileSuffix)
        {
            // Delete old cached wallpaper if it exists
            string oldPaperPath = Path.Combine(Environment.CurrentDirectory, CacheFolder, oldFileName);
            if (File.Exists(oldPaperPath)) File.Delete(oldPaperPath);

            // Copy new wallpaper
            string newFileName = Path.GetFileNameWithoutExtension(newPath) + fileSuffix + Path.GetExtension(newPath);
            File.Copy(newPath, Path.Combine(Environment.CurrentDirectory, CacheFolder, newFileName));
            return newFileName;
        }

        private string GetAbsolutePath(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, CacheFolder, fileName);
        }
    }
}
