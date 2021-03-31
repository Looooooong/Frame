using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public class NoHealthTrigger : FSMTrigger
	{
	
	    public override void Init()
	    {
	        TriggerID = FSMTriggerID.NoHealthTrigger;
	    }
	
	    public override bool HandleTrigger(FSMBase fsm)
	    {
	        //返回生命力是否为0
	        throw new System.NotImplementedException();
	    }
	
	
	}
}
