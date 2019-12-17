# ProcessWatcher

This library provides some classes that help you to watch
for started/stopped processes.

## Classes

### TimedProcessWatcher

TimedProcessWatcher can be used if you don't care about short running processes
and don't want to be forced to use administrator rights.

### WmiProcessWatcher

WmiProcessWatcher uses the event interface from the
[Windows Management Instrumentation](https://docs.microsoft.com/en-us/windows/win32/wmisdk/about-wmi)
and is able to catch short running processes.

This comes with a lower resource usage as this doesn't require a constantly running timer
and list of all running processes.

However this class requires administrator rights.