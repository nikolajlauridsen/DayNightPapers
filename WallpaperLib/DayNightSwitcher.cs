using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperLib
{
    public class DayNightSwitcher
    {
        private WallpaperStore store = new WallpaperStore();
        private WallpaperChanger changer = new WallpaperChanger();

        public string DayPaper
        {
            get => store.DayPaper;
            set => store.DayPaper = value;
        }

        public string NightPaper
        {
            get => store.NightPaper;
            set => store.NightPaper = value;
        }

        public void ForcePaperCheck()
        {
            changer.SetWallpaper(DayPaper);
        }
    }
}
