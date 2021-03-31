using System;
using System.Collections.Generic;

namespace Main 
{	
	/// <summary>
	/// 控制AI的所有状态切换
	/// </summary>
	public class StateMachine<T>
	{
	    //保存当前所有的状态
	    public Dictionary<int, StateBase<T>> statesDic = new Dictionary<int, StateBase<T>>();
	
	    //前一个状态
	    public StateBase<T> preState;
	
	    //当前状态
	    public StateBase<T> curState;
	
	    public StateMachine(StateBase<T> beginState)
	    {
	        preState = null;
	        curState = beginState;
	
	        AddState(beginState);
	    }
	
	    public void AddState(StateBase<T> state)
	    {
	        if (statesDic.ContainsKey(state.ID)) return;
	
	        statesDic.Add(state.ID, state);
	        state.machine = this;
	    }
	
	    /// <summary>
	    /// 状态切换
	    /// </summary>
	    /// <param name="id"></param>
	    public void TransitionState(int id)
	    {
	        if (!statesDic.ContainsKey(id)) return;
	        
	        preState = curState;
	        curState.OnExit();
	        curState = statesDic[id];
	        curState.OnEnter();
	    }
	
	    public void UpdateState()
	    {
	        if (curState != null)
	            curState.OnStay();
	    }
	}
}
