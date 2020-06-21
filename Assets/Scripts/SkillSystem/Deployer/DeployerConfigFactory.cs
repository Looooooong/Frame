using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// 释放器配置工厂 : 提供释放器各种算法创建
/// </summary>
public class DeployerConfigFactory
{
    private static Dictionary<string, object> cache = new Dictionary<string, object>();
    public static IAttackSelector CreateAttackSelector(SkillData skillData)
    {

        //选取对象命名规则 ：命名空间.+ 枚举名 + AttackSelector
        //例如扇形选区： 命名空间.SectorAttackSelector
        string className = string.Format("{0}AttackSelector", skillData.selectorType);
        return CreateObject<IAttackSelector>(className);
    }


    public static IImpactEffect[] CreateImpactEffects(SkillData skillData)
    {

        IImpactEffect[] impactArray = new IImpactEffect[skillData.impactType.Length];
        //创建影响对象
        //影响效果命名规范 ： 命名空间.+ impactType[?] + Impact
        //例如消耗法力： 命名空间.CostSPImpact
        for (int i = 0; i < skillData.impactType.Length; i++)
        {
            string classNameImpact = string.Format("{0}Impact", skillData.impactType[i]);
            impactArray[i] = CreateObject<IImpactEffect>(classNameImpact);
        }

        return impactArray;
    }


    private static T CreateObject<T>(string className) where T : class
    {
        if(!cache.ContainsKey(className))
        {
            Type type = Type.GetType(className);
            object instance = Activator.CreateInstance(type);
            cache.Add(className, instance);
        }
        return cache[className] as T;
    }
}
