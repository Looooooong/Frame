using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public static class TransformHelper
	{
	    /// <summary>
	    /// 未知层级,查找子物体指定名称组件
	    /// </summary>
	    /// <param name="currentTF">当前组件</param>
	    /// <param name="childName">子物体名字</param>
	    /// <returns></returns>
	    public static Transform FindChildByName(this Transform currentTF, string childName)
	    {
	        //递归：方法内部调用自身
	        //1.在子物体中查找
	        Transform childTF = currentTF.Find(childName);
	        if (childTF != null) return childTF;
	
	        //2.没找到则交给子物体
	        for (int i = 0; i < currentTF.childCount; i++)
	        {
	            childTF = FindChildByName(currentTF.GetChild(i), childName);
	            if (childTF != null) return childTF;
	        }
	
	        return null;
	    }
	}
}
