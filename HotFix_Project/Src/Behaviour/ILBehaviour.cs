using UnityEngine;

namespace HotFix_Project
{
    public class ILBehaviour
    {
        public bool isStart = false;

        public GameObject GameObject { get; set; }
        public Transform Transform { get; set; }


        public virtual void Awake()
        {

        }

        public virtual void Start()
        {
            isStart = true;
        }

        public virtual void Update()
        {

        }

        public virtual void LateUpdate()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void OnDestroy()
        {

        }

        
    }
}
