using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public class AnimationEventBehaviour : MonoBehaviour
	{
	
	    public Action attackHandler;
	
	
	
	
	    private void OnAttack()
	    {
	        if (attackHandler != null)
	            attackHandler();
	    }
	
	}
}
