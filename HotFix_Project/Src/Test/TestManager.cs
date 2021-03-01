﻿using System;
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


            //注册
            EventManager.Instance.AddEvent(MG_EventType.ZERO, TestEvent);
            EventManager.Instance.AddEvent<int>(MG_EventType.ONE, TestEvent1);
            EventManager.Instance.AddEvent<int,int>(MG_EventType.TWO, TestEvent2);
            EventManager.Instance.AddEvent<int, int, int>(MG_EventType.THREE, TestEvent3);

            //触发
            EventManager.Instance.DispatchEvent(MG_EventType.ZERO);
            EventManager.Instance.DispatchEvent(MG_EventType.ONE, 3);
            EventManager.Instance.DispatchEvent(MG_EventType.TWO, 3, 3);
            EventManager.Instance.DispatchEvent(MG_EventType.THREE, 3, 3, 3);

        }



        private void TestEvent()
        {
            Debug.LogError(100);
        }

        private void TestEvent1(int x)
        {
            Debug.Log(x);
        }


        private void TestEvent2(int x,int y)
        {
            Debug.Log($"{x}{y}");
        }

        private void TestEvent3(int x,int y,int z)
        {
            Debug.Log($"{x}{y}{z}");
        }


    }
}
