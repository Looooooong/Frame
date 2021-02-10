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
    unsafe class HotFixILManager_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::HotFixILManager);
            args = new Type[]{};
            method = type.GetMethod("get_Instance", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Instance_0);

            field = type.GetField("UpdateEventHandler", flag);
            app.RegisterCLRFieldGetter(field, get_UpdateEventHandler_0);
            app.RegisterCLRFieldSetter(field, set_UpdateEventHandler_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_UpdateEventHandler_0, AssignFromStack_UpdateEventHandler_0);
            field = type.GetField("LateUpdateEventHandler", flag);
            app.RegisterCLRFieldGetter(field, get_LateUpdateEventHandler_1);
            app.RegisterCLRFieldSetter(field, set_LateUpdateEventHandler_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_LateUpdateEventHandler_1, AssignFromStack_LateUpdateEventHandler_1);
            field = type.GetField("FixedUpdateEventHandler", flag);
            app.RegisterCLRFieldGetter(field, get_FixedUpdateEventHandler_2);
            app.RegisterCLRFieldSetter(field, set_FixedUpdateEventHandler_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_FixedUpdateEventHandler_2, AssignFromStack_FixedUpdateEventHandler_2);


        }


        static StackObject* get_Instance_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = global::HotFixILManager.Instance;

            object obj_result_of_this_method = result_of_this_method;
            if(obj_result_of_this_method is CrossBindingAdaptorType)
            {    
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_UpdateEventHandler_0(ref object o)
        {
            return ((global::HotFixILManager)o).UpdateEventHandler;
        }

        static StackObject* CopyToStack_UpdateEventHandler_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::HotFixILManager)o).UpdateEventHandler;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_UpdateEventHandler_0(ref object o, object v)
        {
            ((global::HotFixILManager)o).UpdateEventHandler = (System.Action)v;
        }

        static StackObject* AssignFromStack_UpdateEventHandler_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action @UpdateEventHandler = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::HotFixILManager)o).UpdateEventHandler = @UpdateEventHandler;
            return ptr_of_this_method;
        }

        static object get_LateUpdateEventHandler_1(ref object o)
        {
            return ((global::HotFixILManager)o).LateUpdateEventHandler;
        }

        static StackObject* CopyToStack_LateUpdateEventHandler_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::HotFixILManager)o).LateUpdateEventHandler;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_LateUpdateEventHandler_1(ref object o, object v)
        {
            ((global::HotFixILManager)o).LateUpdateEventHandler = (System.Action)v;
        }

        static StackObject* AssignFromStack_LateUpdateEventHandler_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action @LateUpdateEventHandler = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::HotFixILManager)o).LateUpdateEventHandler = @LateUpdateEventHandler;
            return ptr_of_this_method;
        }

        static object get_FixedUpdateEventHandler_2(ref object o)
        {
            return ((global::HotFixILManager)o).FixedUpdateEventHandler;
        }

        static StackObject* CopyToStack_FixedUpdateEventHandler_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((global::HotFixILManager)o).FixedUpdateEventHandler;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_FixedUpdateEventHandler_2(ref object o, object v)
        {
            ((global::HotFixILManager)o).FixedUpdateEventHandler = (System.Action)v;
        }

        static StackObject* AssignFromStack_FixedUpdateEventHandler_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Action @FixedUpdateEventHandler = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            ((global::HotFixILManager)o).FixedUpdateEventHandler = @FixedUpdateEventHandler;
            return ptr_of_this_method;
        }



    }
}
