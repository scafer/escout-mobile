using System.Threading;

namespace escout.Helpers
{
    public static class StopWatch
    {
        private static int Minutes { get; set; }
        private static int Seconds { get; set; }
        private static Timer Timer = new Timer(new TimerCallback(Tick));

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
            Timer.Change(0, 1000);
        }

        public static void Stop()
        {
            Timer.Change(Timeout.Infinite, 1000);
        }

        public static void Reset()
        {
            Minutes = 0;
            Seconds = 0;
        }

        public static void Restart()
        {
            Reset();
            Start();
        }

        public static string ShowTime()
        {
            return Minutes.ToString("D2") + ":" + Seconds.ToString("D2");
        }
    }
}