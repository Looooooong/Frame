using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public class DamageImpact : IImpactEffect
	{
	
	    public void Execute(SkillDeployer deployer)
	    {
	        deployer.StartCoroutine(RepeatDamage(deployer));
	    }
	
	    private IEnumerator RepeatDamage(SkillDeployer deployer)
	    {
	        float atkTime = 0;
	        do
	        {
	            //伤害目标生命
	            OnceDamage(deployer.SkillData);
	            yield return new WaitForSeconds(deployer.SkillData.atkInterval);
	            atkTime += deployer.SkillData.atkInterval;
	            deployer.CalculateTargets();
	        } while (atkTime > deployer.SkillData.durationTime);
	    }
	
	    /// <summary>
	    /// 单次伤害
	    /// </summary>
	    private void OnceDamage(SkillData data)
	    {
	        for (int i = 0; i < data.attackTargets.Length; i++)
	        {
	            //float atk = data.atkRatio * data.owner.GetComponent<status>().atk;
	            //data.attackTargets[i].GetComponent<status>().hp -= atk;
	        }
	
	        //创建攻击特效
	    }
	
	}
}
