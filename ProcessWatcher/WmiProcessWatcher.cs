using CookieProjects.ProcessWatcher.Events;

using System.Management;

namespace CookieProjects.ProcessWatcher
{
    /// <summary>
    /// ProcessWatcher implemented with <see cref="ManagementEventWatcher"/>
    /// using WMI Queries.
    /// 
    /// This ProcessWatcher will never set <see cref="ProcessEventArgs.Process"/>
    /// to any value.
    /// 
    /// This class requires admin rights but can also catch short running
    /// processes. However short running processes may not be reported 
    /// in-order. For example a process may be reported as stopped before
    /// it's getting reported as started.
    /// 
    /// If you don't want to use admin rights and don't care
    /// about short running processes <seealso cref="TimedProcessWatcher"/>.
    /// </summary>
    public class WmiProcessWatcher : IProcessWatcher
    {
        #region Members

        private ManagementEventWatcher StartWatch;

        private ManagementEventWatcher StopWatch;

        #endregion

        #region Events

        /// <inheritdoc/>
        public event ProcessEventHandler ProcessStarted;

        /// <inheritdoc/>
        public event ProcessEventHandler ProcessStopped;

        #endregion

        #region Methods

        /// <inheritdoc/>
        public void Start()
        {
            if (this.StartWatch != null) return;

            // subscribe to wmi events
            this.StartWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            this.StopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));

            this.StartWatch.EventArrived += (sender, e) =>
            {
                this.ProcessStarted?.Invoke(this, new ProcessEventArgs((string)e.NewEvent.Properties["ProcessName"].Value, (uint)e.NewEvent.Properties["ProcessID"].Value));
            };
            this.StopWatch.EventArrived += (sender, e) =>
            {
                this.ProcessStopped?.Invoke(this, new ProcessEventArgs((string)e.NewEvent.Properties["ProcessName"].Value, (uint)e.NewEvent.Properties["ProcessID"].Value));
            };

            this.StartWatch.Start();
            this.StopWatch.Start();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (this.StartWatch == null) return;

            this.StartWatch.Stop();
            this.StopWatch.Stop();

            this.StartWatch = null;
            this.StopWatch = null;
        }

        #endregion
    }
}
