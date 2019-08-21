using System;
using System.Runtime.InteropServices;

namespace WallpaperLib
{
    internal class WallpaperChanger
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private static readonly int MAX_PATH = 260;
        private static readonly int SPI_GETDESKWALLPAPER = 0x73;
        private static readonly int SPI_SETDESKWALLPAPER = 0x14;
        private static readonly int SPIF_UPDATEINIFILE = 0x01;
        private static readonly int SPIF_SENDWININICHANGE = 0x02;

        static string GetDesktopWallpaper()
        {
            // Sets up string with the max path length filled with null chars
            string wallpaper = new string('\0', MAX_PATH);
            // Calls the function from the DLL
            SystemParametersInfo(SPI_GETDESKWALLPAPER, (int) wallpaper.Length, wallpaper, 0);
            // Removes all null chars and returns the path (the chars that has been replaced)
            return wallpaper.Substring(0, wallpaper.IndexOf('\0'));
        }

        public string GetCurrentWallpaperPath()
        {
            return GetDesktopWallpaper();
        }

        static void SetDesktopWallpaper(string path)
        {
            //                      Action (Set WP)                 ?  Path to wp       Update ini file     Apply updated ini
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        public void SetWallpaper(string path)
        {
            SetDesktopWallpaper(path);
        }
    }
}
