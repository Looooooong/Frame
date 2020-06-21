using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


/// <summary>
/// 状态类
/// </summary>
public abstract class FSMState 
{
    /// <summary>
    /// 状态编号
    /// </summary>
    public FSMStateID StateID { get; set; }
    private Dictionary<FSMTriggerID, FSMStateID> map;
    //条件列表
    private List<FSMTrigger> Triggers;


    public FSMState()
    {
        map = new Dictionary<FSMTriggerID, FSMStateID>();
        Triggers = new List<FSMTrigger>();
        Init();
    }
    /// <summary>
    /// 初始化,为编号赋值
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 由状态机调用,添加映射表
    /// </summary>
    /// <param name="triggerID"></param>
    /// <param name="stateID"></param>
    public void AddMap(FSMTriggerID triggerID,FSMStateID stateID)
    {
        //添加映射
        map.Add(triggerID, stateID);
        //创建条件对象
        CreateTrigger(triggerID);
    }

    private void CreateTrigger(FSMTriggerID triggerID)
    {
        //创建条件对象
        //命名规范：命名空间 + 条件枚举 + Trigger
        Type type = Type.GetType(triggerID + "Trigger");
        FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
        Triggers.Add(trigger);
    }

    /// <summary>
    /// 检测当前状态是否满足
    /// </summary>
    public void Reason(FSMBase fsm)
    {
        for (int i = 0; i < Triggers.Count; i++)
        {
            if(Triggers[i].HandleTrigger(fsm))
            {
                //切换状态
                FSMStateID stateID = map[Triggers[i].TriggerID];
                fsm.ChangeActiveState(stateID);
                return;
            }
        }
    }

    public virtual void EnterState(FSMBase fsm) { }
    public virtual void ActionState(FSMBase fsm) { }
    public virtual void ExitState(FSMBase fsm) { }
}
