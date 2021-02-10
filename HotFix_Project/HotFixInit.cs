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
        }

        public static void Update()
        {
            Debug.Log("Update");
        }

        public static void LateUpadte()
        {
            Debug.Log("LateUpadte");
        }

        public static void FixedUpdate()
        {
            Debug.Log("FixedUpdate");
        }



    }
}
