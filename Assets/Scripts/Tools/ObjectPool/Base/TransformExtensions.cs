using UnityEngine;
using System.Collections;

namespace Main 
{	
	public static class TransformExtensions
	{
		public static void Reset(this Transform t)
		{
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = new Vector3(1, 1, 1);
		}
		
		public static void ResetToParent(this Transform t, GameObject aParent)
		{
			t.parent = aParent.transform;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			//t.localScale = new Vector3(1, 1, 1);
		}
		
		public static void ResetExcludeScale(this Transform t)
		{
			t.position = Vector3.zero;
			t.localRotation = Quaternion.identity;
		}
	
	}
}
