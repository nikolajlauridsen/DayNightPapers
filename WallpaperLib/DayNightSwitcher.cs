using System;
using System.Threading;

using SunData;

namespace WallpaperLib
{

    // This is basically a facade/controller.
    public class DayNightSwitcher
    {
        private WallpaperStore store = new WallpaperStore();
        private WallpaperChanger changer = new WallpaperChanger();
        private SunTime sunTime = new SunTime();

        public Action PaperCheck;

        private object sunriseLock = new object();
        private object sunsetLock = new object();
        private object actionLock = new object();

        private bool running = false;

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

        public double Latitude
        {
            get => sunTime.Latitude;
            set => sunTime.Latitude = value;
        }

        public double Longtitude
        {
            get => sunTime.Longtitude;
            set => sunTime.Longtitude = value;
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

        public int CheckDelay
        {
            set => _checkSpan = new TimeSpan(0, 0, value);
        }

        private TimeSpan _checkSpan = new TimeSpan(0,0,60);

        public void ForcePaperCheck()
        {
            Thread thread = new Thread(new ThreadStart(PaperCheck));
            thread.Start();
        }

        public void Start()
        {
            if (running == false)
            {
                running = true;
                Thread thread = new Thread(paperCheckWorker);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        public void Stop()
        {
            if (running) running = false;
        }

        private void paperCheckAction()
        {
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

        private void paperCheckWorker()
        {
            while (running)
            {
                PaperCheck();
                Thread.Sleep(_checkSpan);
            }
        }
    }
}
