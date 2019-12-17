using CookieProjects.ProcessWatcher.Events;

namespace CookieProjects.ProcessWatcher
{
    /// <summary>
    /// Defines a basic ProcessWatcher.
    /// </summary>
    public interface IProcessWatcher
    {
        /// <summary>
        /// Event when a new process has been started.
        /// </summary>
        public event ProcessEventHandler ProcessStarted;

        /// <summary>
        /// Event when a process has stopped.
        /// </summary>
        public event ProcessEventHandler ProcessStopped;

        /// <summary>
        /// Start the watcher.
        /// </summary>
        public void Start();

        /// <summary>
        /// Stop the watcher.
        /// </summary>
        public void Stop();
    }
}
