using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public class BaseSkillManager : MonoBehaviour
	{
	    //技能列表
	    public SkillData[] skills;
	
	
	    private void Start()
	    {
	        foreach (var data in skills)
	        {
	            InitSkill(data);
	        }
	    }
	
	    /// <summary>
	    /// 初始化技能
	    /// </summary>
	    /// <param name="data"></param>
	    private void InitSkill(SkillData data)
	    {
	        //data.prefabName --> data.skillPrefab  通过资源管理器加载
	        data.skillPrefab = Resources.Load(data.skillprefabName) as GameObject;
	        data.owner = gameObject;
	    }
	
	
	    public SkillData PrepareSkill(int id)
	    {
	        //根据 id 查找技能数据
	        SkillData data = skills.Find(s => s.skillID == id);
	
	        float sp = 0;
	        //判断条件
	        if (data != null && data.coolRemain <= 0 && sp >= data.costSP)
	        {
	            //返回技能数据
	            return data;
	        }
	        else
	        {
	            return null;
	        }
	
	    }
	
	    /// <summary>
	    /// 生成技能
	    /// </summary>
	    public void GenerateSkill(SkillData data)
	    {
	        //技能释放条件 ： 冷却/法力
	
	
	        //创建技能预制件
	        GameObject skillGo = Instantiate(data.skillPrefab, transform.position, transform.rotation);
	        //传递技能数据
	        SkillDeployer deployer = skillGo.GetComponent<SkillDeployer>();
	        deployer.SkillData = data;  //内部创建算法对象
	        deployer.DeploySkill();     //内部执行算法随想
	
	        //销毁技能
	        Destroy(skillGo, data.durationTime);
	
	        //开启技能冷却
	        StartCoroutine(CoolTimeDown(data));
	    }
	
	    private IEnumerator CoolTimeDown(SkillData data)
	    {
	        data.coolRemain = data.coolTime;
	        while(data.coolRemain > 0)
	        {
	            yield return new WaitForSeconds(1f);
	            data.coolRemain--;
	        }
	    }
	
	}
}
