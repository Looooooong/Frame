using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public class TestManager
    {
        public void Test()
        {
            //GameObject go = new GameObject();
            //go.name = "Test";

            //ILBehaviourManager.Instance.AddComponent<TestCom>(go);

            GameObject colliderGo = new GameObject();
            colliderGo.name = "ColliderTeset";
            BoxCollider c = colliderGo.AddComponent<BoxCollider>();
            
            ILBehaviourManager.Instance.AddComponent<TestUnityEvent>(colliderGo);
        }
    }
}
