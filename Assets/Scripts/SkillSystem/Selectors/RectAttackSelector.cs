using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectAttackSelector : IAttackSelector
{
    public Transform[] SelectTarget(SkillData data, Transform skillTF)
    {
        List<Transform> targets = new List<Transform>();
        //根据敌人标签获取所有目标
        for (int i = 0; i < data.attackTargetTags.Length; i++)
        {
            GameObject[] goArray = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);

            targets.AddRange(goArray.Select(g => g.transform));
        }

        //判断攻击范围（矩形）
        targets.FindAll(t =>
            Vector3.Dot(skillTF.forward, t.transform.position - skillTF.transform.position) > 0 && //目标在前方
            Vector3.Dot(skillTF.forward, t.transform.position - skillTF.transform.position) <= data.attackDistance && //前方距离小于攻击距离
            Mathf.Abs(Vector3.Dot(skillTF.right, t.transform.position - skillTF.transform.position)) <= data.attackDistance / 2 //左右攻击距离小于攻击距离一半
        ) ;
        //筛选角色
        //targets.FindAll(t => t.GetComponent<status>().hp > 0);

        Transform[] result = targets.ToArray();
        //返回目标（单攻/群攻）
        if (data.attackType == SkillAttackType.Group || result.Length == 0)
            return result;

        //距离最近
        Transform min = result.GetMin(t => Vector3.Distance(t.position, skillTF.position));
        return new Transform[] { min };
    }
}
