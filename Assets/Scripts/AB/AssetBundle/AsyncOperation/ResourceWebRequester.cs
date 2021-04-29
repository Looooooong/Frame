using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 功能：资源异步请求，本地、远程
/// 
/// </summary>

namespace AssetBundles
{
    public class ResourceWebRequester : ResourceAsyncOperation
    {
        static Queue<ResourceWebRequester> pool = new Queue<ResourceWebRequester>();
        static int sequence = 0;
        protected WWW www = null;
        protected bool isOver = false;

        public static ResourceWebRequester Get()
        {
            if (pool.Count > 0)
            {
                return pool.Dequeue();
            }
            else
            {
                return new ResourceWebRequester(++sequence);
            }
        }

        public static void Recycle(ResourceWebRequester creater)
        {
            pool.Enqueue(creater);
        }

        public ResourceWebRequester(int sequence)
        {
            Sequence = sequence;
        }

        public void Init(string name, string url, bool noCache = false)
        {
            assetbundleName = name;
            this.url = url;
            this.noCache = noCache;
            www = null;
            isOver = false;
        }

        public int Sequence
        {
            get;
            protected set;
        }

        public bool noCache
        {
            get;
            protected set;
        }

        public string assetbundleName
        {
            get;
            protected set;
        }

        public string url
        {
            get;
            protected set;
        }

        public AssetBundle assetbundle
        {
            get
            {
                return www.assetBundle;
            }
        }

        public byte[] bytes
        {
            get
            {
                return www.bytes;
            }
        }

        public string text
        {
            get
            {
                return www.text;
            }
        }

        public string error
        {
            get
            {
                // 注意：不能直接判空
                // 详见：https://docs.unity3d.com/530/Documentation/ScriptReference/WWW-error.html
                return string.IsNullOrEmpty(www.error) ? null : www.error;
            }
        }

        public override bool IsDone()
        {
            return isOver;
        }

        public void Start()
        {
            www = new WWW(url);
            if (www == null)
            {
                Debug.LogError("New www failed!!!");
                isOver = true;
            }
            else
            {
                //Logger.Log("Downloading : " + url);
            }
        }
        
        public override float Progress()
        {
            if (isDone)
            {
                return 1.0f;
            }

            return www != null ? www.progress : 0f;
        }

        public override void Update()
        {
            if (isDone)
            {
                return;
            }
            
            isOver = www != null && (www.isDone || !string.IsNullOrEmpty(www.error));
            if (!isOver)
            {
                return;
            }

            if (www != null && !string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error + "     " + url);
            }
        }

        public override void Dispose()
        {
            if (www != null)
            {
                www.Dispose();
                www = null;
            }
            Recycle(this);
        }
    }
}
