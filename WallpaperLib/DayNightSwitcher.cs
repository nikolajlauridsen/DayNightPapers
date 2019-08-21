using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SunData;

namespace WallpaperLib
{
    public class DayNightSwitcher
    {
        private WallpaperStore store = new WallpaperStore();
        private WallpaperChanger changer = new WallpaperChanger();
        private SunTime sunTime = new SunTime();

        public Action PaperCheck;

        private object sunriseLock = new object();
        private object sunsetLock = new object();
        private object actionLock = new object();

        public DayNightSwitcher()
        {
            PaperCheck += paperCheckAction;
        }

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

        public DateTime SunRise
        {
            get
            {
                lock (sunriseLock)
                {
                    return sunTime.Sunrise;
                }
            }
        }

        public DateTime SunSet
        {
            get
            {
                lock (sunsetLock)
                {
                    return sunTime.Sunset;
                }
            }
        }

        public void ForcePaperCheck()
        {
            Thread thread = new Thread(paperCheckAction);
            thread.Start();
        }

        private void paperCheckAction()
        {
            // TODO: Refresh sun data if it's no longer the same day
            lock (actionLock)
            {
                DateTime currentTime = DateTime.Now;

                if (currentTime >= SunRise && currentTime < SunSet) {
                    changer.SetWallpaper(store.DayPaper);
                } else {
                    changer.SetWallpaper(store.NightPaper);
                }
            } 
        }
    }
}
