using System.Threading;

namespace escout.Helpers
{
    public static class StopWatch
    {
        private static int Minutes { get; set; }
        private static int Seconds { get; set; }
        private static Timer timer = new Timer(new TimerCallback(Tick));

        private static void Tick(object state)
        {
            if (Seconds == 59)
            {
                Seconds = 0;
                Minutes++;
            }
            else
                Seconds++;
        }

        public static void Start()
        {
            timer.Change(0, 1000);
        }

        public static void Stop()
        {
            timer.Change(Timeout.Infinite, 1000);
        }

        public static void Restart()
        {
            Reset();
            Start();
        }

        public static void SetTimer(int m, int s)
        {
            Minutes = m;
            Seconds = s;
        }

        public static string ShowTime()
        {
            return Minutes.ToString("D2") + ":" + Seconds.ToString("D2");
        }

        private static void Reset()
        {
            Minutes = 0;
            Seconds = 0;
        }
    }
}
