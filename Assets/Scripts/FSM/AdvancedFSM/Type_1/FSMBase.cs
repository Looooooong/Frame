using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Main 
{	
	/// <summary>
	/// 状态机
	/// </summary>
	public class FSMBase : MonoBehaviour
	{
    #region 状态机自身成员
	    //状态列表
	    private List<FSMState> states;
	
	    [Tooltip("默认状态编号")]
	    public FSMStateID defaultStateID;
	
	    public FSMState currentState;
	
	    private FSMState defaultState;
	
	    [Tooltip("状态机配置文件")]
	    public string fileName = "AI_01.txt";
	
	    //配置状态机
	    private void ConfigFSM()
	    {
	
	        //states = new List<FSMState>();
	        ////-- 创建状态对象
	        //IdleState idle = new IdleState();
	        //DeadState dead = new DeadState();
	        ////-- 设置状态(AddMap)
	        //idle.AddMap(FSMTriggerID.NoHealthTrigger, FSMStateID.Dead);
	        ////-- 添加状态
	        //states.Add(idle);
	        //states.Add(dead);
	
	
	        //读取配置文件
	        states = new List<FSMState>();
	        var map = AIConfigurationReaderFactory.GetMap(fileName);
	
	        //大字典 -> 状态
	        //小字典 -> 映射
	        foreach (var state in map)
	        {
	            //state.key     状态名
	            //state.vaule   映射
	            Type type = Type.GetType(state.Key + "State"); //如果有命名空间的话需要加上
	            FSMState s = Activator.CreateInstance(type) as FSMState;
	            states.Add(s);
	            foreach (var dic in state.Value)
	            {
	                //dic.key   条件编号
	                //dic.value 状态编号
	                FSMTriggerID triggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), dic.Key);
	                FSMStateID stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), dic.Value);
	                //添加映射
	                s.AddMap(triggerID, stateID);
	            }
	
	        }
	    }
	
	
	    /// <summary>
	    /// 初始化默认状态
	    /// </summary>
	    private void InitDefaultState()
	    {
	        defaultState = states.Find(s => s.StateID == defaultStateID);
	        currentState = defaultState;
	        //进入状态
	        currentState.EnterState(this);
	    }
	
	
	
	    public void ChangeActiveState(FSMStateID stateID)
	    {
	        //离开上一个状态
	        currentState.ExitState(this);
	        //切换状态
	        currentState = stateID == FSMStateID.None ? defaultState : states.Find(s => s.StateID == stateID);
	        //进入下一个状态
	        currentState.EnterState(this);
	    }
	
    #endregion
	
	
    #region 为状态和条件提供的成员
	
	    public Animator anim;
	    public Transform targetTF;
	    public NavMeshAgent navAgent;
	    [Tooltip("攻击目标标签")]
	    public string[] targetTags = { "Player" };
	    [Tooltip("视野距离")]
	    public float sightDistance = 10f;
	    public void InitComponent()
	    {
	        anim = GetComponentInChildren<Animator>();
	    }
	
	    /// <summary>
	    /// 查找目标
	    /// </summary>
	    public void SearchTarget()
	    {
	        // 根据需求使用data
	        SkillData data = new SkillData()
	        {
	            attackTargetTags = targetTags,
	            attackDistance = sightDistance,
	            attackAngle = 360,
	            attackType = SkillAttackType.Single
	        };
	        //end
	        Transform[] targetArr = new SectorAttackSelector().SelectTarget(data, transform);
	        targetTF = targetArr.Length == 0 ? null:targetArr[0];
	    }
	
	    /// <summary>
	    /// 移动到目的位置
	    /// </summary>
	    /// <param name="pos">目标</param>
	    /// <param name="stopDistance">停止距离</param>
	    /// <param name="moveSpeed">移动速度</param>
	    public void MoveToTarget(Vector3 pos,float stopDistance,float moveSpeed)
	    {
	        navAgent.SetDestination(pos);
	        navAgent.stoppingDistance = stopDistance;
	        navAgent.speed = moveSpeed;
	    }
	
	    public void StopMove()
	    {
	        navAgent.enabled = false;
	        navAgent.enabled = true;
	    }
	
    #endregion
	
	
    #region 脚本生命周期
	    private void Start()
	    {
	        ConfigFSM();
	        InitDefaultState();
	        InitComponent();
	    }
	
	
	    //每帧处理的逻辑
	    public void Update()
	    {
	        //判断条件是否满足
	        currentState.Reason(this);
	        //执行当前状态逻辑
	        currentState.ActionState(this);
	        //搜索目标
	        SearchTarget();
	    }
	
	
    #endregion
	
	
	
	
	
	   
	}
}
