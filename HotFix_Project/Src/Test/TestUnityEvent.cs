using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public class TestUnityEvent : ILBehaviour
    {
        public override void Awake()
        {
            base.Awake();
            UnityTriggerHelper eventHelper = GameObject.AddComponent<UnityTriggerHelper>();
            InitUnityEvent(eventHelper);
        }

        
        private void InitUnityEvent(UnityTriggerHelper eventHelper)
        {
            eventHelper.OnCollisionEnterEventHandler += this.OnTriggerEnter;
        }

        private void OnTriggerEnter(Collision collision)
        {
            Debug.Log("碰撞到物体：" + collision.gameObject.name);
        }
        


    }
}
