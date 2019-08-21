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
    internal class WallpaperStore
    {
        private static string _cacheName = @"PaperCache";

        private object dayLock = new object();
        private object nightLock = new object();
        private object storeLock = new object();
        private object pathLock = new object();

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
                lock (dayLock)
                {
                    string dayPaperPath = GetAbsolutePath(Settings.Default.DayPaperName);
                    if (File.Exists(dayPaperPath)) return dayPaperPath;
                    else return null;
                }

            }
            set
            {
                lock (dayLock)
                {
                    string newFileName = storePaper(Settings.Default.DayPaperName, value, "_day");
                    Settings.Default.DayPaperName = newFileName;
                    Settings.Default.Save();
                }

            }
        }

        public string NightPaper
        {
            get
            {
                lock (nightLock)
                {
                    string nightPaperPath = GetAbsolutePath(Settings.Default.NightPaperName);
                    if (File.Exists(nightPaperPath)) return nightPaperPath;
                    else return null;
                }
                
            }
            set
            {
                lock (nightLock)
                {
                    string newFileName = storePaper(Settings.Default.NightPaperName, value, "_night");
                    Settings.Default.NightPaperName = newFileName;
                    Settings.Default.Save();
                }
            }
        }

        private string storePaper(string oldFileName, string newPath, string fileSuffix)
        {
            lock (storeLock)
            {
                // Delete old cached wallpaper if it exists
                string oldPaperPath = Path.Combine(Environment.CurrentDirectory, CacheFolder, oldFileName);
                if (File.Exists(oldPaperPath)) File.Delete(oldPaperPath);

                // Copy new wallpaper
                string newFileName = Path.GetFileNameWithoutExtension(newPath) + fileSuffix + Path.GetExtension(newPath);
                File.Copy(newPath, Path.Combine(Environment.CurrentDirectory, CacheFolder, newFileName));
                return newFileName;
            }
            
        }

        private string GetAbsolutePath(string fileName)
        {
            lock (pathLock)
            {
                return Path.Combine(Environment.CurrentDirectory, CacheFolder, fileName);
            }
            
        }
    }
}
