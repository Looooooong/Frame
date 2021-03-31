using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	/// <summary>
	/// 封装技能系统,对外提供释放方法
	/// </summary>
	[RequireComponent(typeof(BaseSkillManager))]
	public class BaseSkillSystem : MonoBehaviour
	{
	
	    private BaseSkillManager skillManager;
	    private Animator anim;
	
	
	    private void Start()
	    {
	        skillManager = GetComponent<BaseSkillManager>();
	        anim = GetComponentInChildren<Animator>();
	        GetComponent<AnimationEventBehaviour>().attackHandler += DeploySkill;
	
	    }
	
	    //动作队列 ： 动作游戏适用
	    //当前采用限制 ： 限制只使用一个技能,如果在释放则调用时return
	    private SkillData skill;
	    private void DeploySkill()
	    {
	        skillManager.GenerateSkill(skill);
	    }
	
	    /// <summary>
	    /// 使用技能攻击
	    /// </summary>
	    public void AttackUseSkill(int skillID , bool isBatter = false)
	    {
	        //如果是连击,则获取技能连击技能
	        if (skill != null && isBatter)
	            skillID = skill.nextBatterId;
	
	        //准备技能
	        skill = skillManager.PrepareSkill(skillID);
	        if (skill == null) return;
	        //播放动画
	        anim.SetBool(skill.animationName, true);
	        //生成技能
	        DeploySkill();
	        //查找目标
	        Transform targetTF = SelectTarget();
	        transform.LookAt(targetTF);
	
	
	    }
	
	    private Transform SelectTarget()
	    {
	        Transform[] targets = new SectorAttackSelector().SelectTarget(skill, transform);
	        return targets.Length != 0 ? targets[0] : null;
	    }
	
	    /// <summary>
	    /// 使用随机技能（NPC）
	    /// </summary>
	    public void UseRandomSkill()
	    {
	        //随机选择技能释放
	        var usableSkill = skillManager.skills.FindAll(s => skillManager.PrepareSkill(s.skillID) != null);
	
	        if (usableSkill.Length == 0) return;
	
	        int rdm = UnityEngine.Random.Range(0, usableSkill.Length);
	
	        AttackUseSkill(usableSkill[rdm].skillID);
	    }
	}
}
