using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public static class MathFunction
	{
	    const float pi = Mathf.PI;
	    public static Vector3 SineFunction(float x, float y, float z, float t,float amplitude = 1)
	    {
	        Vector3 p;
	        p.x = x;
	        p.y = amplitude * Mathf.Sin(pi * (x + t)) + y;
	        p.z = z;
	        return p;
	    }
	
	
	    public static Vector3 SineFunctionX(float x, float y, float z, float t, float amplitude = 1)
	    {
	        Vector3 p;
	        p.x = amplitude * Mathf.Sin(pi * (x + t)) + x;
	        p.y = y;
	        p.z = z;
	        return p;
	    }
	
	    public static Vector3 SineFunctionY(float x, float y, float z, float t, float amplitude = 1)
	    {
	        Vector3 p;
	        p.x = x;
	        p.y = amplitude * Mathf.Sin(pi * (x + t)) + y;
	        p.z = z;
	        return p;
	    }
	
	    public static Vector3 SineFunctionZ(float x, float y, float z, float t, float amplitude = 1)
	    {
	        Vector3 p;
	        p.x = x;
	        p.y = y;
	        p.z = amplitude * Mathf.Sin(pi * (x + t)) + z;
	        return p;
	    }
	
	
	}
}
