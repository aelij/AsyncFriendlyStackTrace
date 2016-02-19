Example1
========
OLD
---
```
System.Exception: Crash! Boom! Bang!
   at AsyncFriendlyStackTrace.Test.Example1.<C>d__3.MoveNext() in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Example1.cs:line 26
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncFriendlyStackTrace.Test.Example1.<B>d__2.MoveNext() in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Example1.cs:line 20
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncFriendlyStackTrace.Test.Example1.<A>d__1.MoveNext() in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Example1.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncFriendlyStackTrace.Test.Example1.<Run>d__0.MoveNext() in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Example1.cs:line 10
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AsyncFriendlyStackTrace.Test.Program.Run[TExample](TextWriter writer) in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Program.cs:line 45
```

NEW
---
```
System.Exception: Crash! Boom! Bang!
   at async AsyncFriendlyStackTrace.Test.Example1.C(?) in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Example1.cs:line 26
   at async AsyncFriendlyStackTrace.Test.Example1.B(?) in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Example1.cs:line 20
   at async AsyncFriendlyStackTrace.Test.Example1.A(?) in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Example1.cs:line 15
   at async AsyncFriendlyStackTrace.Test.Example1.Run(?) in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Example1.cs:line 10
   at AsyncFriendlyStackTrace.Test.Program.Run[TExample](TextWriter writer) in C:\Source\Repos\AsyncFriendlyStackTrace\src\AsyncFriendlyStackTrace.Test\Program.cs:line 45
```


