using UnityEngine;
namespace HotFix_Project
{
    public class HotFixInit
    {
        public static void Init()
        {
            HotFixILManager.Instance.UpdateEventHandler += Update;
            HotFixILManager.Instance.LateUpdateEventHandler += LateUpadte;
            HotFixILManager.Instance.FixedUpdateEventHandler += FixedUpdate;

            ILBehaviourManager.Instance.Init();


            TestManager t = new TestManager();

            t.Test();
        }

        public static void Update()
        {
            //Debug.Log("Upadte");
            ILBehaviourManager.Instance.Update();
        }

        public static void LateUpadte()
        {
            //Debug.Log("LateUpadte");
            ILBehaviourManager.Instance.LateUpdate();
        }

        public static void FixedUpdate()
        {
            //Debug.Log("FixedUpdate");
            ILBehaviourManager.Instance.FixedUpdate();
        }



    }
}
