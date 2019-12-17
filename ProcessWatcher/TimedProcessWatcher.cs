using CookieProjects.ProcessWatcher.Events;
using CookieProjects.ProcessWatcher.Extensions;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace CookieProjects.ProcessWatcher
{
    /// <summary>
    /// ProcessWatcher implemented with a <see cref="Timer"/>.
    /// 
    /// This may miss some short running processes but don't
    /// require admin rights and should serve most purposes.
    /// 
    /// If you need those short running processes <seealso cref="WmiProcessWatcher"/>.
    /// </summary>
    public class TimedProcessWatcher : IProcessWatcher
    {
        #region Members

        private readonly Timer Timer;

        private Dictionary<ProcessCacheEntry, Process> ProcessCache;

        #endregion

        #region Events

        /// <inheritdoc/>
        public event ProcessEventHandler ProcessStarted;

        /// <inheritdoc/>
        public event ProcessEventHandler ProcessStopped;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new TimedProcessWatcher.
        /// </summary>
        /// <param name="interval">Interval to scan for started/stopped processes.</param>
        public TimedProcessWatcher(double interval)
        {
            this.Timer = new Timer(interval);
            this.Timer.Elapsed += this.Tick;
        }

        #endregion

        #region Methods

        private void UpdateProcessCache() =>
            this.ProcessCache = new Dictionary<ProcessCacheEntry, Process>(
                Process.GetProcesses().Select(p => new KeyValuePair<ProcessCacheEntry, Process>(new ProcessCacheEntry(p), p)));

        private void Tick(object sender, ElapsedEventArgs e)
        {
            var oldCache = this.ProcessCache;
            this.UpdateProcessCache();
            var (added, deleted) = oldCache.GetFullDifferenceTo(this.ProcessCache);

            foreach (var add in added)
                ProcessStarted?.Invoke(this, new ProcessEventArgs(add.Value));
            foreach (var del in deleted)
                ProcessStopped?.Invoke(this, new ProcessEventArgs(del.Value));
        }

        /// <inheritdoc/>
        public void Start()
        {
            if (this.Timer.Enabled) return;
            this.UpdateProcessCache();
            this.Timer.Start();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (!this.Timer.Enabled) return;
            this.Timer.Stop();
            this.ProcessCache = null;
        }

        #endregion
    }
}
