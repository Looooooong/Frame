using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	
	public class UIManager : UnitySingleton<UIManager>
	{
	    //key 窗口类名称 value 窗口对象引用
	    private Dictionary<string, UIWindow> uiWindowDic;
	
	    protected override void Init()
	    {
	        base.Init();
	        uiWindowDic = new Dictionary<string, UIWindow>();
	        UIWindow[] uiWindowArr = FindObjectsOfType<UIWindow>();
	        for (int i = 0; i < uiWindowArr.Length; i++)
	        {
	            //隐藏窗口
	            uiWindowArr[i].SetVisible(false);
	            //保存窗口
	            AddWindow(uiWindowArr[i]);
	        }
	    }
	
	    /// <summary>
	    /// 添加新窗口,动态创建时使用
	    /// </summary>
	    /// <param name="window"></param>
	    public void AddWindow(UIWindow window)
	    {
	        string windowName = window.GetType().Name;
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
