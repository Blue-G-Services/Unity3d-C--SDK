using System;
using System.Diagnostics;
using System.Reflection;
using GameService.Client.Sdk.Models;

namespace GameService.Client.Sdk.Adapters.Utils.Logger
{
    public static class Logger
    {
        public static event EventHandler<DebugArgs> OnDebugReceived;
        
        public static void LogNormal(Type type,DebugLocation where,string callingMethod,string data)
        {
            if(!CanDebug(LogType.Normal,where)) return;
            
            var callingClass = type.Name;
            OnDebugReceived?.Invoke(null, new DebugArgs()
            {
                LogTypeType = LogType.Normal,
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Normal + " F:" + callingMethod +
                       "] : " + data
            });
        }
        
        
        public static void LogNormal<TClass>(DebugLocation where,string callingMethod,string data)
           where TClass : class
        {
            if(!CanDebug(LogType.Normal,where)) return;

            var callingClass = typeof(TClass).Name;
            OnDebugReceived?.Invoke(null, new DebugArgs()
            {
                LogTypeType = LogType.Normal,
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Normal + " F:" + callingMethod +
                       "] : " + data
            });
        }

        
        public static void LogError<TClass>(DebugLocation where,string callingMethod,string data) 
        {
            if(!CanDebug(LogType.Error,where)) return;

            var callingClass = typeof(TClass).Name;
            OnDebugReceived?.Invoke(null, new DebugArgs()
            {
                LogTypeType = LogType.Error, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Error + " F:" + callingMethod + "] : " + data
            });
        }
        
        public static void LogError(Type type,DebugLocation where,string callingMethod,Exception exception)
        {
            if(!CanDebug(LogType.Error,where)) return;

            var callingClass = type.Name;
            OnDebugReceived?.Invoke(null, new DebugArgs()
            {
                LogTypeType = LogType.Error, 
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Error + " F:" + callingMethod + "] : " + exception
            });
        }


        public static Exception LogException<TClass>(this Exception exception,DebugLocation where,string callingMethod) where TClass : class
        {
            if(!CanDebug(LogType.Exception,where)) return exception;
            
            var callingClass = typeof(TClass).Name;
            OnDebugReceived?.Invoke(null, new DebugArgs()
            {
                LogTypeType = LogType.Exception, 
                Exception = exception,
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Exception + " F:" +callingMethod + "] : " + exception.Message
            });
            
            return exception;
        }
        
        
        public static Exception LogException(this Exception exception,Type type,DebugLocation where,string callingMethod)
        {
            if(!CanDebug(LogType.Exception,where)) return exception;

            var callingClass = type.Name;
            OnDebugReceived?.Invoke(null, new DebugArgs()
            {
                LogTypeType = LogType.Exception, 
                Exception = exception,
                Where = where,
                Data = GetTime() + " - [" + callingClass + ".cs" + " ▶ " + LogType.Exception + " F:" +callingMethod + "] : " + exception.Message
            });
            
            return exception;
        }


        private static string GetTime()
        {
            return DateTime.Now.ToString("yyyy-M-d  h:mm:ss tt");
        }

        private static bool CanDebug(LogType type , DebugLocation where)
        {
            var can = false;
            switch (type)
            {
                case LogType.Normal:
                    if (DynamicPixels.VerboseMode) can = true;
                    break;
                case LogType.Error:
                    if (DynamicPixels.DebugMode || DynamicPixels.VerboseMode) can = true;
                    break;
                case LogType.Exception:
                    if (DynamicPixels.DebugMode || DynamicPixels.VerboseMode) can = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            // if (DynamicPixels.DebugConfiguration.DebugLocations == null)
            //     return can;
            // if(DynamicPixels.DebugConfiguration.DebugLocations.Contains(DebugLocation.All))
            //     return can;
            //
            // can &= DynamicPixels.DebugConfiguration.DebugLocations.ToList().Any(dl => dl == where);
            return can;
        }
        
        private static MethodBase GetCallingMethodBase(StackFrame stackFrame)
        {
            return stackFrame == null ? MethodBase.GetCurrentMethod() : stackFrame.GetMethod();
        }
        
        private static StackFrame FindStackFrame(string methodName)
        {
            var stackTrace = new StackTrace(true);
            for (var i = 0; i < stackTrace.GetFrames()?.Length; i++)
            {
                var methodBase = stackTrace.GetFrame(i).GetMethod();
                var name = MethodBase.GetCurrentMethod()?.Name;
                
                if (!methodBase.Name.Equals(methodName) && !methodBase.Name.Equals(name))
                    return new StackFrame(i, true);
            }
            return null;
        }
    }
}