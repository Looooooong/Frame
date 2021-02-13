using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class UnityTriggerHelper_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::UnityTriggerHelper);

            field = type.GetField("OnCollisionEnterEventHandler", flag);
            app.RegisterCLRFieldGetter(field, get_OnCollisionEnterEventHandler_0);
            app.RegisterCLRFieldSetter(field, set_OnCollisionEnterEventHandler_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_OnCollisionEnterEventHandler_0, AssignFromStack_OnCollisionEnterEventHandler_0);


        }



        static object get_OnCollisionEnterEventHandler_0(ref object o)
        {
            return ((global::UnityTriggerHelper)o).OnCollisionEnterEventHandler;
        }

        static StackObject* CopyToStack_OnCollisionEnterEventHandler_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::UnityTriggerHelper)o).OnCollisionEnterEventHandler;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_OnCollisionEnterEventHandler_0(ref object o, object v)
        {
            ((global::UnityTriggerHelper)o).OnCollisionEnterEventHandler = (System.Action<UnityEngine.Collision>)v;
        }

        static StackObject* AssignFromStack_OnCollisionEnterEventHandler_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action<UnityEngine.Collision> @OnCollisionEnterEventHandler = (System.Action<UnityEngine.Collision>)typeof(System.Action<UnityEngine.Collision>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::UnityTriggerHelper)o).OnCollisionEnterEventHandler = @OnCollisionEnterEventHandler;
            return ptr_of_this_method;
        }



    }
}
