using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillDeployer : SkillDeployer
{
    public override void DeploySkill()
    {

        CalculateTargets();

        //执行影响算法
        ImpactTargets();
    }

}
