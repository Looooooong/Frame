using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 挂在在技能预制体上
/// </summary>
public abstract class SkillDeployer : MonoBehaviour
{
    private SkillData skillData;
    //有技能管理器提供
    public SkillData SkillData
    {
        get
        {
            return skillData;
        }
        set
        {
            skillData = value;
            //创建算法对象
            InitDeployer();
        }
    }
    //选区算法
    private IAttackSelector selector;
    //影响算法
    private IImpactEffect[] impactArray;

    /// <summary>
    /// 初始化释放器
    /// </summary>
    private void InitDeployer()
    {
        //创建选区对象  
        selector = DeployerConfigFactory.CreateAttackSelector(SkillData);


        //创建影响对象
        impactArray = DeployerConfigFactory.CreateImpactEffects(SkillData);
        
    }

    //执行算法

    /// <summary>
    /// 获取选区中的目标
    /// </summary>
    public void CalculateTargets()
    {
        skillData.attackTargets = selector.SelectTarget(skillData, transform);
    }

    /// <summary>
    /// 影响目标算法
    /// </summary>
    public void ImpactTargets()
    {
        for (int i = 0; i < impactArray.Length; i++)
        {
            impactArray[i].Execute(this);
        }
    }

    /// <summary>
    /// 释放方式 供技能管理器调用
    /// </summary>
    public abstract void DeploySkill();

}
