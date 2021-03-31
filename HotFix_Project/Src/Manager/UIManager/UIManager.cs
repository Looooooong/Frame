using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotFix_Project
{
    public class UIManager : Singleton<UIManager>
    {
        //key 窗口类名称 value 窗口对象引用
        private Dictionary<string, UIWindow> uiWindowDic;

        public override void Init()
        {
            base.Init();
            uiWindowDic = new Dictionary<string, UIWindow>();
        }

        /// <summary>
        /// 添加新窗口,动态创建时使用
        /// </summary>
        /// <param name="window"></param>
        public void AddWindow(UIWindow window)
        {
            string windowName = window.GetType().Name;
            Debug.Log(windowName);
            //保存窗口
            if (uiWindowDic.ContainsKey(windowName))
            {
                uiWindowDic[windowName] = window;
            }
            else
            {
                uiWindowDic.Add(windowName, window);
            }
        }

        public T GetWindow<T>() where T : class
        {
            string key = typeof(T).Name;
            if (uiWindowDic.ContainsKey(key))
            {
                return uiWindowDic[key] as T;
            }
            else
            {
                Debug.LogError($"不存在该窗口：{key}");
                return null;
            }
        }

    }

}


