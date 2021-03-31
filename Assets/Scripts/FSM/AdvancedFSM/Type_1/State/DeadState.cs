using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public class DeadState : FSMState
	{
	    public override void Init()
	    {
	        StateID = FSMStateID.Dead;
	    }
	
	    public override void EnterState(FSMBase fsm)
	    {
	        base.EnterState(fsm);
	        fsm.enabled = false;
	    }
	
	}
}
