using System;
using UnityEngine;

namespace UnityLogWrapper
{
	public enum LogLevel
	{
		Debug = 0,
		Info = 1,
		Warning = 2,
		Error = 3,
		Exception = 4
	}
	
	/// <summary>
	/// Wrapper class that calls non generic Logger
	/// </summary>
	public static class Logger<T>
	{
		/// <summary>
		/// Log on info level
		/// </summary>
		/// <param name="message">Message to log</param>
		public static void Info(string message)
		{
			Logger.WriteLog(LogLevel.Info, typeof(T), message);
		}
		
		/// <summary>
		/// Log an exception
		/// </summary>
		/// <param name="message">Message to log</param>
		public static void Exception(Exception e)
		{
			Logger.WriteLog(LogLevel.Exception, typeof(T), $"{e.Message}\n{e.StackTrace}");
			if (e.InnerException != null)
			{
				Logger.WriteLog(LogLevel.Exception, typeof(T), $"{e.InnerException.Message}\n{e.InnerException.StackTrace}");
			}
		}

		/// <summary>
		/// Log on warning level
		/// </summary>
		/// <param name="message">Message to log</param>
		public static void Warn(string message)
		{
			Logger.WriteLog(LogLevel.Warning, typeof(T), message);
		}
		
		/// <summary>
		/// Log on error level
		/// </summary>
		/// <param name="message">Message to log</param>
		public static void Error(string message)
		{
			Logger.WriteLog(LogLevel.Error, typeof(T), message);
		}
		
		/// <summary>
		/// Log on debug level
		/// </summary>
		/// <param name="message">Message to log</param>
		public static void Debug(string message)
		{
			Logger.WriteLog(LogLevel.Debug, typeof(T), message);
		}
	}

	public static class Logger
	{
		public static ILogWriter LogWriter;

		public static LogLevel LogLevel = LogLevel.Info;

		internal static void WriteLog(LogLevel level, Type callerType, string message)
		{
			if (level < LogLevel)
				return;

			// checking for null instead of ?. operator since someone might inherit ILogWriter from monobehaviour
			if (LogWriter != null)
			{
				LogWriter.Write($"{level}:{callerType}: {message}");
			}

			LogUnity(level, message);
		}
		
		private static void LogUnity(LogLevel level, string message)
		{
			switch (level)
			{
				case LogLevel.Debug:
				case LogLevel.Info:
					Debug.Log(message);
					break;
				case LogLevel.Warning:
					Debug.LogWarning(message);
					break;
				case LogLevel.Error:
				case LogLevel.Exception:
					Debug.LogError(message);
					break;
			}
		}
	}
}