# UnityLogWrapper

Have you've ever tried to write a wrapper for unity console?
Did double clicking a console message open your code editor to the wrapper itself instead of where the wrapper call is?

If you said yes to both then this little utility dll is exactly what you need!

This is a wrapper dll for unity console so that you can double click console logs to get to where the log is called from instead of going to the wrapper class where the Debug.Log call is in.
Also has callbacks from log to allow custom file loging etc.

## How to use: 
* Download as zip and grab the dll from bin. Drop that in your unity project /Assets/Plugins/
* Reference the dll in your asmdef if you have one.

```cs
using Log = UnityLogWrapper.Logger<MyClass>;

public class MyClass
{
  public void SomeMethod()
  {
    Log.Error("Some error occured");
    try
    {
      int crash = 1 / 0;
    }
    catch (Exception e)
    {    
      Log.Exception(e);
    }    
  }
}

// This is optional
public class MyLogWriter : ILogWriter
{
  // create a new MyLogWriter and call this method from somewhere in your code
  public void Register()
  {
    UnityLogWrapper.Logger.LogWriter = this;
  }

  private void Write(string message)
  {
    /* 
    * Log to file here or do other magic with the log message
    * 
    * Under the hood the message is formatted as $"{level}:{callerType}: {message}"
    * 
    * Example (from MyClass above):
    * Error:MyClass: Some error occured
    *
    * DivideByZeroException:MyClass: Divide by zero blahblah
    * stack trace
    * stack trace
    * stack trace
    */
  }
}
```
