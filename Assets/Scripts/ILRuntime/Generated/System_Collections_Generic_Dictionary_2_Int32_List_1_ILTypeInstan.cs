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
    unsafe class System_Collections_Generic_Dictionary_2_Int32_List_1_ILTypeInstance_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("ContainsKey", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, ContainsKey_0);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("get_Item", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Item_1);
            args = new Type[]{typeof(System.Int32), typeof(System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>)};
            method = type.GetMethod("Add", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Add_2);
            args = new Type[]{};
            method = type.GetMethod("get_Values", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Values_3);
            args = new Type[]{typeof(System.Int32)};
            method = type.GetMethod("Remove", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Remove_4);

            args = new Type[]{};
            method = type.GetConstructor(flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Ctor_0);

        }


        static StackObject* ContainsKey_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @key = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>> instance_of_this_method = (System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>)typeof(System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.ContainsKey(@key);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* get_Item_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @key = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>> instance_of_this_method = (System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>)typeof(System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method[key];

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Add_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance> @value = (System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>)typeof(System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Int32 @key = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>> instance_of_this_method = (System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>)typeof(System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.Add(@key, @value);

            return __ret;
        }

        static StackObject* get_Values_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>> instance_of_this_method = (System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>)typeof(System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Values;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Remove_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Int32 @key = ptr_of_this_method->Value;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>> instance_of_this_method = (System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>)typeof(System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Remove(@key);

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }


        static StackObject* Ctor_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);

            var result_of_this_method = new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.List<ILRuntime.Runtime.Intepreter.ILTypeInstance>>();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


    }
}
