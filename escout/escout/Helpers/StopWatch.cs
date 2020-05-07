using System.Threading;

namespace escout.Helpers
{
    public static class StopWatch
    {
        private static int minutes { get; set; }
        private static int seconds { get; set; }
        private static Timer timer = new Timer(new TimerCallback(Tick));

        private static void Tick(object state)
        {
            if (seconds == 59)
            {
                seconds = 0;
                minutes++;
            }
            else
                seconds++;
        }

        public static void Start()
        {
            timer.Change(0, 1000);
        }

        public static void Stop()
        {
            timer.Change(Timeout.Infinite, 1000);
        }

        public static void Reset()
        {
            minutes = 0;
            seconds = 0;
        }

        public static void Restart()
        {
            Reset();
            Start();
        }

        public static string ShowTime()
        {
            return minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }
    }
}