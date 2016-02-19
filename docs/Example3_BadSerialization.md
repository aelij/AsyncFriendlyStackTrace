Example3_BadSerialization
=========================
OLD
---
```
System.AggregateException: One or more errors occurred. ---> System.Exception: Crash! Boom! Bang!
   at AsyncStackTrace.Test.Example1.<C>d__3.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 26
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<B>d__2.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 20
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<A>d__1.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<Run>d__0.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 10
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example3_BadSerialization.<Run>d__0.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example3_BadSerialization.cs:line 14
   --- End of inner exception stack trace ---
   at AsyncStackTrace.Test.Example3_BadSerialization.<Run>d__0.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example3_BadSerialization.cs:line 20
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Program.Run[TExample](TextWriter writer) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Program.cs:line 45
---> (Inner Exception #0) System.Exception: Crash! Boom! Bang!
   at AsyncStackTrace.Test.Example1.<C>d__3.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 26
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<B>d__2.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 20
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<A>d__1.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<Run>d__0.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 10
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example3_BadSerialization.<Run>d__0.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example3_BadSerialization.cs:line 14<---

```

NEW
---
```
System.AggregateException: One or more errors occurred. ---> Crash! Boom! Bang!
   at async AsyncStackTrace.Test.Example3_BadSerialization.Run(?) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example3_BadSerialization.cs:line 20
   at AsyncStackTrace.Test.Program.Run[TExample](TextWriter writer) in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Program.cs:line 45
---> (Inner Exception #0) System.Exception: Crash! Boom! Bang!
   at AsyncStackTrace.Test.Example1.<C>d__3.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 26
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<B>d__2.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 20
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<A>d__1.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example1.<Run>d__0.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example1.cs:line 10
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncStackTrace.Test.Example3_BadSerialization.<Run>d__0.MoveNext() in C:\Source\Repos\asyncstacktrace\src\AsyncStackTrace.Test\Example3_BadSerialization.cs:line 14<---

```


