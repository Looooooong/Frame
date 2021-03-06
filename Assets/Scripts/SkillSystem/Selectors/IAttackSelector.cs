using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	/// <summary>
	/// 攻击选区
	/// </summary>
	public interface IAttackSelector
	{
	    /// <summary>
	    /// 搜索目标
	    /// </summary>
	    /// <param name="data">技能数据</param>
	    /// <param name="skillTF">技能释放者</param>
	    /// <returns></returns>
	    Transform[] SelectTarget(SkillData data,Transform skillTF);
	}
}
