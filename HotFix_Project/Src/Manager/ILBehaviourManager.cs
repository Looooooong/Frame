using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HotFix_Project
{
    public class ILBehaviourManager
    {
        public static ILBehaviourManager Instance = new ILBehaviourManager();

        private Dictionary<int, List<ILBehaviour>> behaviourDic = null;
        
        private List<List<ILBehaviour>> updateList = null;
        private List<List<ILBehaviour>> lateUpdateList = null;
        private List<List<ILBehaviour>> fixedUpdateList = null;

        private List<int> removeList = null;

        public void Init()
        {
            this.behaviourDic = new Dictionary<int, List<ILBehaviour>>();
            this.removeList = new List<int>();
        }

        public void DestroyGameObject(GameObject go)
        {
            int id = go.GetInstanceID();
            if(this.behaviourDic.ContainsKey(id))
            {
                List<ILBehaviour> components = this.behaviourDic[id];
                for (int i = 0; i < components.Count; i++)
                {
                    components[i].OnDestroy();
                }
                this.removeList.Add(id);
            }

            GameObject.DestroyImmediate(go);
        }

        public T AddComponent<T>(GameObject go) where T : new()
        {
            int id = go.GetInstanceID();
            T t = new T();
            ILBehaviour il = t as ILBehaviour;

            List<ILBehaviour> components = null;
            if(!this.behaviourDic.ContainsKey(id))
            {
                components = new List<ILBehaviour>();
                this.behaviourDic.Add(id, components);
            }
            else
            {
                components = this.behaviourDic[id];
            }

            components.Add(il);
            il.GameObject = go;
            il.Transform = go.transform;

            il.Awake();
            il.Start();
            il.isStart = true;

            return t;
        }

        public T GetComponent<T>(GameObject go) where T : ILBehaviour
        {
            int id = go.GetInstanceID();

            if (!this.behaviourDic.ContainsKey(id))
                return null;

            List<ILBehaviour> components = this.behaviourDic[id];

            for (int i = 0; i < components.Count; i++)
            {
                if(components[i] is T)
                {
                    return components[i] as T;
                }
            }

            return null;
        }


        public T RemoveComponent<T>(GameObject go) where T : ILBehaviour
        {
            int id = go.GetInstanceID();

            if (!this.behaviourDic.ContainsKey(id))
                return null;

            List<ILBehaviour> components = this.behaviourDic[id];

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                {
                    components[i].OnDestroy();
                    components[i] = null;
                }
            }
            return null;
        }

        #region Update

        public void Update()
        {
            updateList = this.behaviourDic.Values.ToList();
            int goCount, comCount;
            goCount = updateList.Count;
            for (int i = 0; i < goCount; i++)
            {
                comCount = updateList[i].Count;
                for (int j = 0; j < comCount; j++)
                {
                    if (updateList[i][j] == null)
                        continue;
                    
                    updateList[i][j].Update();
                }
            }

        }

        public void LateUpdate()
        {
            lateUpdateList = this.behaviourDic.Values.ToList();
            int goCount = lateUpdateList.Count;
            int comCount = 0;
            for (int i = 0; i < goCount; i++)
            {
                comCount = lateUpdateList[i].Count;
                for (int j = 0; j < comCount; j++)
                {
                    if (lateUpdateList[i][j] == null)
                        continue;

                    lateUpdateList[i][j].LateUpdate();
                }
            }
            int removeCount = this.removeList.Count;
            for (int i = 0; i < removeCount; i++)
            {
                this.behaviourDic.Remove(this.removeList[0]);
            }
            this.removeList.Clear();
        }

        public void FixedUpdate()
        {
            fixedUpdateList = this.behaviourDic.Values.ToList();
            int goCount, comCount;
            goCount = fixedUpdateList.Count;
            for (int i = 0; i < goCount; i++)
            {
                comCount = fixedUpdateList[i].Count;
                for (int j = 0; j < comCount; j++)
                {
                    if (fixedUpdateList[i][j] == null)
                        continue;

                    fixedUpdateList[i][j].FixedUpdate();
                }
            }
        }

        #endregion


    }
}
