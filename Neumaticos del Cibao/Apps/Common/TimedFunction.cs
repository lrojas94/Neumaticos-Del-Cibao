using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Neumaticos_del_Cibao.Apps.Common
{
    public class TimedFunction
    {
        private Action action;
        private DispatcherTimer timer;

        public TimeSpan TimerInterval
        {
            get
            {
                return TimerInterval;
            }

            set
            {
                TimerInterval = value;
                timer.Interval = TimerInterval;
            }
        }

        public TimedFunction(Action action,DispatcherTimer timer = null)
        {
            if(action == null)
                throw new ArgumentNullException("Action cannot be null");

            this.action = action;
            this.timer = timer;

            if (this.timer == null)
                this.timer = new DispatcherTimer();

            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            this.timer.Tick += timer_Tick;
            
             
        }

        private void timer_Tick(object sender, object e) {
            action();
            timer.Stop();
        }

        public void Run()
        {
            if (!timer.IsEnabled)
                timer.Start();
        }   
    }
}
