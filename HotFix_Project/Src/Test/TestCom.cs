using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public class TestCom  : ILBehaviour
    {
        public override void Awake()
        {
            base.Awake();
            
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            Transform.position += new Vector3(1f, 0, 0);
            if (Transform.position.x > 300f)
                ILBehaviourManager.Instance.DestroyGameObject(GameObject);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
