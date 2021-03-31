using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	public class Test : MonoBehaviour
	{
	    public Vector3 move;
	    public Vector3 euler;
	    public Vector3 scale;
	
	    public Matrix4x4 mat4x4;
	    // Start is called before the first frame update
	    void Start()
	    {
	        //Change();
	
	        BulletConfig b =  ConfigDataBase.GetConfigData<BulletConfig>("2");
	
	        Debug.Log(b.bulletName);
	        Debug.Log(b.killValue);
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        if(Input.GetKeyDown(KeyCode.A))
	            Change();
	    }
	
	
	    public void Change()
	    {
	        move = move + transform.position;
	        Quaternion q = Quaternion.Euler(euler);
	        print(transform.rotation.eulerAngles + "   " + q.eulerAngles);
	        q = transform.rotation * q;
	        mat4x4 = Matrix4x4.TRS(move, q, scale);
	        transform.FromMatrix4x4(mat4x4);
	    }
	
	}
}
