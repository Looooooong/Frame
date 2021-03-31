using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	
	/// <summary>
	/// 影响算法
	/// </summary>
	public interface IImpactEffect 
	{
	    void Execute(SkillDeployer deployer);
	}
}
