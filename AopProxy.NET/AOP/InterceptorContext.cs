using System.Reflection;

namespace AopProxy.AOP
{
    public class InterceptorContext
    {
        public object TargetInstance { get; internal set; }

        public MethodInfo TargetMethodInfo { get; internal set; }

        public MethodInfo MethodInfo { get; internal set; }

        public object[] Args { get; internal set; }

        public object ReturnValue { get;  internal set; }

        public bool IsInvoked { get; internal set; }

        private static object locker = new object();

        public object Invoke()
        {
            lock (locker)
            {
                if (!IsInvoked && ReturnValue == null)
                {
                    ReturnValue = MethodInfo.Invoke(TargetInstance, Args);
                    IsInvoked = true;
                }
            }

            return ReturnValue;
        }
    }
}
