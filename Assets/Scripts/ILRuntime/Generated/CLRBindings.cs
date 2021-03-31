using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }

        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3> s_UnityEngine_Vector3_Binding_Binder = null;

        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            Main_HotFixILManager_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Delegate_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Array_Binding.Register(app);
            System_String_Binding.Register(app);
            System_Exception_Binding.Register(app);
            System_Action_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_List_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_List_1_Int32_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            System_Activator_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            System_Linq_Enumerable_Binding.Register(app);
            System_Collections_Generic_List_1_List_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            UnityEngine_Resources_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            UnityEngine_UI_Text_Binding.Register(app);
            Main_UnityTriggerHelper_Binding.Register(app);
            UnityEngine_Collision_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_UIEventListener_Binding.Register(app);
            UnityEngine_CanvasGroup_Binding.Register(app);
            Main_TransformHelper_Binding.Register(app);
            Main_UIEventListener_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Int32_Binding.Register(app);
            LitJson_JsonMapper_Binding.Register(app);
            System_Diagnostics_Stopwatch_Binding.Register(app);
            System_Boolean_Binding.Register(app);
            System_Single_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            UnityEngine_Vector2_Binding.Register(app);
            UnityEngine_Time_Binding.Register(app);
            CoroutineDemo_Binding.Register(app);
            UnityEngine_WaitForSeconds_Binding.Register(app);
            System_NotSupportedException_Binding.Register(app);
            CLRBindingTestClass_Binding.Register(app);
            System_Int32_Binding.Register(app);
            TestClassBase_Binding.Register(app);
            TestDelegateMethod_Binding.Register(app);
            TestDelegateFunction_Binding.Register(app);
            System_Action_1_String_Binding.Register(app);
            DelegateDemo_Binding.Register(app);
            System_Threading_Monitor_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Vector3));
            s_UnityEngine_Vector3_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3>;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            s_UnityEngine_Vector3_Binding_Binder = null;
        }
    }
}
