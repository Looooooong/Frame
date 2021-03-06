using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	
	
	/// <summary>
	/// UI窗口基类：定义所有窗口共有成员
	/// </summary>
	public class UIWindow : MonoBehaviour
	{
	    private Dictionary<string, UIEventListener> uiEventDic;
	    private CanvasGroup canvasGroup;
	    //private VR_UICanvas uiCanvas;
	
	    private void Awake()
	    {
	        canvasGroup = GetComponent<CanvasGroup>();
	        if (canvasGroup == null)
	            canvasGroup = gameObject.AddComponent<CanvasGroup>();
	        uiEventDic = new Dictionary<string, UIEventListener>();
	    }
	
	    /// <summary>
	    /// 设置窗口是否可使
	    /// </summary>
	    /// <param name="state">是否可见</param>
	    public void SetVisible(bool state)
	    {
	        //
	        canvasGroup.alpha = state ? 1 : 0;
	
	        //VR_UICanvas
	
	    }
	
	    public UIEventListener GetUIEventListener(string name)
	    {
	        if (!uiEventDic.ContainsKey(name))
	        {
	            UIEventListener eventListener = UIEventListener.GetListener(transform.FindChildByName(name));
	            uiEventDic.Add(name, eventListener);
	        }
	        return uiEventDic[name];
	    }
	}
	
	
}
