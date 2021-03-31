using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	/// <summary>
	/// 条件类
	/// </summary>
	public abstract class FSMTrigger
	{
	    /// <summary>
	    /// 编号
	    /// </summary>
	    public FSMTriggerID TriggerID { get; set; }
	
	
	    public FSMTrigger()
	    {
	        Init();
	    }
	
	    /// <summary>
	    /// 初始化,给编号赋值
	    /// </summary>
	    public abstract void Init();
	
	
	    /// <summary>
	    /// 逻辑处理
	    /// </summary>
	    /// <returns></returns>
	    public abstract bool HandleTrigger(FSMBase fsm);
	
	}
	
}
