using System;
using System.Diagnostics;

namespace CookieProjects.ProcessWatcher.Events
{
    /// <summary>
    /// Represents the method that will handle the process event.
    /// </summary>
    /// <param name="sender">The sender of this event.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void ProcessEventHandler(object sender, ProcessEventArgs e);

    /// <summary>
    /// Represents a class containing all relevant process event data.
    /// </summary>
    public class ProcessEventArgs : EventArgs
    {
        private readonly string _processName;

        /// <summary>
        /// The name of the process catched by the event.
        /// </summary>
        public string ProcessName
        {
            get
            {
                if (this.Process != null)
                    return this.Process.ProcessName;
                return this._processName;
            }
        }

        private readonly uint _processId;

        /// <summary>
        /// The id of the process catched by the event.
        /// </summary>
        public uint ProcessID
        {
            get
            {
                if (this.Process != null)
                    return (uint)this.Process.Id;
                return this._processId;
            }
        }

        /// <summary>
        /// The actual process object catched by the event.
        /// 
        /// Warning: This property may be <c>null</c> depending on
        /// used Watcher or situation. Use <see cref="ProcessName"/>
        /// or <see cref="ProcessID"/> instead.
        /// </summary>
        public Process Process { get; }

        /// <summary>
        /// Construct a new ProcessEventArgs with <see cref="Process"/> unset.
        /// </summary>
        /// <param name="processName">The name of the catched process.</param>
        /// <param name="processId">The id of the catched process.</param>
        public ProcessEventArgs(string processName, uint processId)
        {
            this._processName = processName;
            this._processId = processId;
        }

        /// <summary>
        /// Construct a new ProcessEventArgs.
        /// </summary>
        /// <param name="process">The catched process.</param>
        public ProcessEventArgs(Process process) => this.Process = process;
    }
}
