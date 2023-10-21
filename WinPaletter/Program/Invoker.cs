using System;
using System.ComponentModel;
using System.Threading;

namespace WinPaletter
{
    internal partial class Program : ISynchronizeInvoke
    {
        private static readonly SynchronizationContext _currentContext = SynchronizationContext.Current;
        private static readonly object _invokeLocker = new();

        private static bool ISynchronizeInvoke_InvokeRequired
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ISynchronizeInvoke.InvokeRequired { get => ISynchronizeInvoke_InvokeRequired; }

        [Obsolete("This method Is Not supported!", true)]
        private IAsyncResult ISynchronizeInvoke_BeginInvoke(Delegate method, object[] args)
        {
            throw new NotSupportedException("The method Or operation Is Not implemented.");
        }

        [Obsolete("This method Is Not supported!", true)]
        IAsyncResult ISynchronizeInvoke.BeginInvoke(Delegate method, object[] args) => ISynchronizeInvoke_BeginInvoke(method, args);

        [Obsolete("This method Is Not supported!", true)]
        private object ISynchronizeInvoke_EndInvoke(IAsyncResult result)
        {
            throw new NotSupportedException("The method Or operation Is Not implemented.");
        }

        [Obsolete("This method Is Not supported!", true)]
        object ISynchronizeInvoke.EndInvoke(IAsyncResult result) => ISynchronizeInvoke_EndInvoke(result);

        static object Invoke(Delegate method, object[] args)
        {
            if (method is null)
            {
                throw new ArgumentNullException("method");
            }

            lock (_invokeLocker)
            {
                object objectToGet = null;
                var invoker = new SendOrPostCallback((data) => Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(objectToGet, method.DynamicInvoke(args), false));
                _currentContext.Send(invoker, method.Target);
                return objectToGet;
            }
        }

        object ISynchronizeInvoke.Invoke(Delegate method, object[] args) => Invoke(method, args);

        public static object Invoke(Delegate method)
        {
            return Invoke(method, null);
        }
    }
}
