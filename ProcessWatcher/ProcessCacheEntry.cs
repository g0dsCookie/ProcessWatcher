using System.Diagnostics;

namespace CookieProjects.ProcessWatcher
{
    /// <summary>
    /// Simple struct to composite processname with it's process id
    /// used for <see cref="System.Collections.Generic.HashSet{T}"/>
    /// or <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>.
    /// </summary>
    internal struct ProcessCacheEntry
    {
        public string ProcessName { get; }

        public uint ProcessID { get; }

        public ProcessCacheEntry(Process process)
            : this(process.ProcessName, (uint)process.Id)
        {
        }

        public ProcessCacheEntry(string processName, uint processId)
        {
            this.ProcessName = processName;
            this.ProcessID = processId;
        }

        public override int GetHashCode() =>
            this.ProcessName.GetHashCode() ^ this.ProcessID.GetHashCode();
    }
}
