# Async Stack Trace

Async-friendly format for stack traces and exceptions

## Usage

To format exceptions, use the extension methods in `ExceptionExtensions`:

```csharp

exception.ToAsyncString();

```

This produces an async-friendly format, as you can see in the examples below. There is also special handling for `AggregateException`s.

The main formatting work is done by the `StackTraceExtensions.ToAsyncString` extension method. The async-friendly formatting is archieved by:
* Skipping all awaiter frames (all methods in types implementing `INotifyCompletion`).
* Inferring the original method name from the async machine state class (`IAsyncStateMachine`)
  and removing the "MoveNext" - currently only for C#.
* Adding the "async" prefix after "at" on each line for async invocations.
* Appending "(?)" to the method signature to indicate that parameter information is missing.
* Removing the "End of stack trace from previous location..." text.

## Example outputs

* [Example 1](docs/Example1.md): A simple 3 async method chain.
* [Example 2](docs/Example2_Wait.md): Async invocations with a synchronous `Wait()` in the middle, causing an `AggregateException`.
* [Example 3](docs/Example3_BadSerialization.md): Bad Serialization - When exception is serialized and deserialized, its stack trace is saved as string. So we can't reformat the stack trace. The "new" stack trace is still a bit shorter due to an improved `AggregateException` formatting (the first inner exception isn't repeated twice).  
* [Example 4](docs/Example4_GoodSerialization.md): Good Serialization - We use the `PrepareForAsyncSerialization` *before* serializing the exception. This saves the async-friendly stack trace *as a string* in the `Data` dictionary of the exception. This has two downsides:
  * The serialized data will now contain both stack trace formats.
  * When using the `DataContractSerializer`, you must include `exception.Data.GetType()` as a known type. This is because its concrete type (`ListDictionaryInternal`) is internal.
