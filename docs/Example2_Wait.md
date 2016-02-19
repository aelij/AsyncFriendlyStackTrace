Example2_Wait
=============
OLD
---
```
System.AggregateException: One or more errors occurred. ---> System.Exception: Crash! Boom! Bang!
   at AsyncStackTrace.Test.Example2_Wait.<C>d__3.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 27
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example2_Wait.<B>d__2.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 21
   --- End of inner exception stack trace ---
   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)
   at System.Threading.Tasks.Task.Wait(Int32 millisecondsTimeout, CancellationToken cancellationToken)
   at AsyncStackTrace.Test.Example2_Wait.<A>d__1.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 0
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example2_Wait.<Run>d__0.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 10
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Program.Run[TExample](TextWriter writer) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Program.cs:line 45
---> (Inner Exception #0) System.Exception: Crash! Boom! Bang!
   at AsyncStackTrace.Test.Example2_Wait.<C>d__3.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 27
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example2_Wait.<B>d__2.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 21<---

```

NEW
---
```
System.AggregateException: One or more errors occurred. ---> Crash! Boom! Bang!
   at System.Threading.Tasks.Task.ThrowIfExceptional(Boolean includeTaskCanceledExceptions)
   at System.Threading.Tasks.Task.Wait(Int32 millisecondsTimeout, CancellationToken cancellationToken)
   at async AsyncStackTrace.Test.Example2_Wait.A(?) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 0
   at async AsyncStackTrace.Test.Example2_Wait.Run(?) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 10
   at AsyncStackTrace.Test.Program.Run[TExample](TextWriter writer) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Program.cs:line 45
---> (Inner Exception #0) System.Exception: Crash! Boom! Bang!
   at async AsyncStackTrace.Test.Example2_Wait.C(?) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 27
   at async AsyncStackTrace.Test.Example2_Wait.B(?) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example2_Wait.cs:line 21<---

```


