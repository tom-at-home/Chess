using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Chess
{
    public class Timer
    {

        Label display;
        DateTime start_time;
        DateTime played_time;
        DispatcherTimer dt;
        int played_seconds;

        private bool isInitialized = false;

        public Timer(Label display)
        {
            this.display = display;
        }

        public void Init()
        {
            start_time = new DateTime();
            played_seconds = 0;
            dt = new DispatcherTimer();
            dt.Tick += new EventHandler(Timer_Tick);
            dt.Interval = TimeSpan.FromSeconds(1);
            // auch möglich:
            // dt.Interval = new TimeSpan(0, 0, 1);           
        }

        public void Start()
        {
            if (!isInitialized)
            {
                this.Init();
                this.isInitialized = true;
            }

            dt.Start();
        }

        public void Stop()
        {
            dt.Stop();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            played_seconds++;
            RefreshDisplay();

            // Aus der MSDN Referenz: Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        public void RefreshDisplay()
        {
            played_time = start_time.AddSeconds(played_seconds);
            display.Content = played_time.ToString("HH:mm:ss");
        }


    }
}
