using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public enum FSMStateID
	{
	    None = 0,
	    Idle = 1,
	    Patrolling = 2,
	    Chasing = 3,
	    Attacking = 4,
	    Dead = 5,
	}
}
