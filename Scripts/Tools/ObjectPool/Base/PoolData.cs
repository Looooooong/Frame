using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 单个对象池数据
/// </summary>
[System.Serializable]
public class PoolData
{
    public string prefab = ""; 
    public GameObject holder; // 对象池的holder
    public List<GameObject> freeObjects; // 可用的对象列表
    public List<GameObject> inUseObjects; // 正在使用的对象列表

    // 当对象池无可用对象时,允许创建新的对象
    public bool allowNew = true;

    // 当对象池无可用对象时,回收最先使用的对象,在使用。
    public bool forceReturn = false;

    private GameObject source; 
    private GameObject lastClone; 

    //返回池子中需要忽略的Component
    public List<string> ignoreComponents = new List<string>() { "Transform", "MeshFilter", "MeshRenderer", "ParticleSystem", "MeshCollider", "Animator", "CapsuleCollider", "Rigidbody", "FSM", "StateController", "NavMeshAgent", "ZoombiesBase" };

    public void Initialize(string aPath, string aPrefab, int anAmount)
    {
        prefab = aPrefab; // store prefab name
        holder = new GameObject(aPrefab + "_Pool"); 
        holder.transform.ResetToParent(PoolManager.poolHolder); 
        freeObjects = new List<GameObject>(); 
        inUseObjects = new List<GameObject>(); 
        // 创建source
        //source = Loader.LoadGameObject(aPath + aPrefab + "_Prefab");
        source = Loader.LoadGameObject(aPath + aPrefab);

        source.transform.ResetToParent(holder);
        //不要将source加入freeobject
        //freeObjects.Add(source);
        for (int i = 0; i < anAmount - 1; i++)
        { 
            CreateObject(source);
        }
        holder.SetActive(false); 
    }

    // 初始化对象池
    void CreateObject(GameObject aSourceObject)
    {
        lastClone = GameObject.Instantiate(aSourceObject);
        lastClone.name = source.name;
        lastClone.transform.ResetToParent(holder);
        freeObjects.Add(lastClone);
    }

    /// <summary>
    /// 从对象池中返回一个对象
    /// </summary>
    /// <returns>The object.</returns>
    public GameObject GetObject()
    {
        GameObject tObject = null;
        if (freeObjects.Count == 0)
        { 
            //没有空余的Obj
            if (allowNew)
            { 
                //允许创建新的对象时处理
                CreateObject(source);
                tObject = GetObject();
            }
            else if (forceReturn)
            {
                //强制使用inuselist中的对象
                ReturnObject(inUseObjects[0]);
                tObject = GetObject();
            }
        }
        else
        { // there are free ones, return the first
            tObject = freeObjects[0];
            freeObjects.RemoveAt(0);
        }
        // 初始化tObject
        if (tObject != null)
        {
            tObject.transform.parent = null;
            //tObject.SetActive(true); // 设置好位置时在active
            inUseObjects.Add(tObject); // 添加到inuseList
        }
        return tObject;
    }

    /// <summary>
    /// 将对象返回到池子中
    /// </summary>
    /// <param name="anObject"></param>
    public void ReturnObject(GameObject anObject)
    {
        // 从inuseList中移除
        inUseObjects.Remove(anObject);
        // 清除组建
        //Component[] components = anObject.GetComponents(typeof(Component));
        //foreach (Component component in components)
        //{
        //    string name = component.GetType().Name;
        //    if (!ignoreComponents.Contains(name)) Object.Destroy(component);
        //}


        // 送回到holder
        anObject.SetActive(false);
        anObject.transform.ResetToParent(holder);

        // 添加到freeList中
        freeObjects.Add(anObject);
    }


    public void Destroy()
    {
        // 将所有对象返回到池子中
        for (int i = inUseObjects.Count - 1; i >= 0; i--)
        {
            ReturnObject(inUseObjects[i]);
        }
        // 删除holder
        Object.Destroy(holder);
    }
}

