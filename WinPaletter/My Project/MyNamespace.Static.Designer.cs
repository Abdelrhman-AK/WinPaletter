// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WinPaletter.My
{
    [System.CodeDom.Compiler.GeneratedCode("MyTemplate", "11.0.0.0")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]

    internal partial class MyApplication : Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
    {
        [STAThread()]
        [DebuggerHidden()]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static void Main(string[] Args)
        {
            try
            {
                Application.SetCompatibleTextRenderingDefault(UseCompatibleTextRendering);
            }
            finally
            {
            }
            MyProject.Application.Run(Args);
        }
     }

    [System.CodeDom.Compiler.GeneratedCode("MyTemplate", "11.0.0.0")]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]

    internal partial class MyComputer : Microsoft.VisualBasic.Devices.Computer
    {
        [DebuggerHidden()]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public MyComputer() : base()
        {
        }
    }

    [HideModuleName()]
    [System.CodeDom.Compiler.GeneratedCode("MyTemplate", "11.0.0.0")]
    internal static partial class MyProject
    {

        [System.ComponentModel.Design.HelpKeyword("My.Computer")]
        internal static MyComputer Computer
        {
            [DebuggerHidden()]
            get
            {
                return m_ComputerObjectProvider.GetInstance;
            }
        }

        private readonly static ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new ThreadSafeObjectProvider<MyComputer>();

        [System.ComponentModel.Design.HelpKeyword("My.Application")]
        internal static MyApplication Application
        {
            [DebuggerHidden()]
            get
            {
                return m_AppObjectProvider.GetInstance;
            }
        }
        private readonly static ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new ThreadSafeObjectProvider<MyApplication>();

        [System.ComponentModel.Design.HelpKeyword("My.User")]
        internal static Microsoft.VisualBasic.ApplicationServices.User User
        {
            [DebuggerHidden()]
            get
            {
                return m_UserObjectProvider.GetInstance;
            }
        }
        private readonly static ThreadSafeObjectProvider<Microsoft.VisualBasic.ApplicationServices.User> m_UserObjectProvider = new ThreadSafeObjectProvider<Microsoft.VisualBasic.ApplicationServices.User>();

        [System.ComponentModel.Design.HelpKeyword("My.Forms")]
        internal static MyForms Forms
        {
            [DebuggerHidden()]
            get
            {
                return m_MyFormsObjectProvider.GetInstance;
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
        internal sealed partial class MyForms
        {
            [DebuggerHidden()]
            private static T Create__Instance__<T>(T Instance) where T : Form, new()
            {
                if (Instance is null || Instance.IsDisposed)
                {
                    if (m_FormBeingCreated is not null)
                    {
                        if (m_FormBeingCreated.ContainsKey(typeof(T)) == true)
                        {
                            throw new InvalidOperationException(Microsoft.VisualBasic.CompilerServices.Utils.GetResourceString("WinForms_RecursiveFormCreate"));
                        }
                    }
                    else
                    {
                        m_FormBeingCreated = new Hashtable();
                    }
                    m_FormBeingCreated.Add(typeof(T), null);
                    try
                    {
                        return new T();
                    }
                    catch (System.Reflection.TargetInvocationException ex) when (ex.InnerException is not null)
                    {
                        string BetterMessage = Microsoft.VisualBasic.CompilerServices.Utils.GetResourceString("WinForms_SeeInnerException", ex.InnerException.Message);
                        throw new InvalidOperationException(BetterMessage, ex.InnerException);
                    }
                    finally
                    {
                        m_FormBeingCreated.Remove(typeof(T));
                    }
                }
                else
                {
                    return Instance;
                }
            }

            [DebuggerHidden()]
            private void Dispose__Instance__<T>(ref T instance) where T : Form
            {
                instance.Dispose();
                instance = null;
            }

            [DebuggerHidden()]
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public MyForms() : base()
            {
            }

            [ThreadStatic()]
            private static Hashtable m_FormBeingCreated;

            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override bool Equals(object o)
            {
                return base.Equals(o);
            }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            internal new Type GetType()
            {
                return typeof(MyForms);
            }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public override string ToString()
            {
                return base.ToString();
            }
        }

        private static ThreadSafeObjectProvider<MyForms> m_MyFormsObjectProvider = new ThreadSafeObjectProvider<MyForms>();

        [System.ComponentModel.Design.HelpKeyword("My.WebServices")]
        internal static MyWebServices WebServices
        {
            [DebuggerHidden()]
            get
            {
                return m_MyWebServicesObjectProvider.GetInstance;
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
        internal sealed class MyWebServices
        {

            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [DebuggerHidden()]
            public override bool Equals(object o)
            {
                return base.Equals(o);
            }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [DebuggerHidden()]
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [DebuggerHidden()]
            internal new Type GetType()
            {
                return typeof(MyWebServices);
            }
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [DebuggerHidden()]
            public override string ToString()
            {
                return base.ToString();
            }

            [DebuggerHidden()]
            private static T Create__Instance__<T>(T instance) where T : new()
            {
                if (instance is null)
                {
                    return new T();
                }
                else
                {
                    return instance;
                }
            }

            [DebuggerHidden()]
            private void Dispose__Instance__<T>(ref T instance)
            {
                instance = default;
            }

            [DebuggerHidden()]
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public MyWebServices() : base()
            {
            }
        }

        private readonly static ThreadSafeObjectProvider<MyWebServices> m_MyWebServicesObjectProvider = new ThreadSafeObjectProvider<MyWebServices>();

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Runtime.InteropServices.ComVisible(false)]
        internal sealed class ThreadSafeObjectProvider<T> where T : new()
        {
            internal T GetInstance
            {
                [DebuggerHidden()]
                get
                {
                    if (m_ThreadStaticValue is null)
                        m_ThreadStaticValue = new T();
                    return m_ThreadStaticValue;
                }
            }

            [DebuggerHidden()]
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public ThreadSafeObjectProvider() : base()
            {
            }

            [System.Runtime.CompilerServices.CompilerGenerated()]
            [ThreadStatic()]
            private static T m_ThreadStaticValue;

        }
    }
}