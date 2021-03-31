using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public class EventTest : MonoBehaviour
	{
	
	    // Use this for initialization
	    void Start()
	    {
	
	        EventManager.AddEvent<int>(MG_EventType.Test, Test);
	    }
	
	
	    // Update is called once per frame
	    void Update()
	    {
	
	        //dispatch event
	        if (Input.GetKeyDown(KeyCode.A))
	        {
	            EventManager.DispatchEvent<int>(MG_EventType.Test, 5);
	        }
	    }
	
	
	    private void Test(int a)
	    {
	        print(a);
	    }
	}
	
}
