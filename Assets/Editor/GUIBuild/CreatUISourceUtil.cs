using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace EditorTool
{	
	
	public class CreatUISourceUtil {
	
	    //创建UISource文件的函数
	    public static void CreatUISourceFile(GameObject selectGameObject)
	    {
	        string gameObjectName = selectGameObject.name;
	        string className = gameObjectName + "UICtrl";
	        StreamWriter sw = null;
	        
	        if (File.Exists(Application.dataPath + "/Scripts/Game/UICtrl/" + className + ".cs")) {
	            return;
	        }
	
	        sw = new StreamWriter(Application.dataPath + "/Scripts/Game/UICtrl/" + className + ".cs");
	        sw.WriteLine(
            "using UnityEngine;\nusing System.Collections;\nusing UnityEngine.UI;\nusing System.Collections.Generic;\n");
	
	        sw.WriteLine("public class " + className + " : UICtrl {" + "\n");
	
	        sw.WriteLine("\t" + "public override void Awake() {");
	        sw.WriteLine("\t\t" + "base.Awake();" + "\n");
	        sw.WriteLine("\t" + "}" + "\n");
	
	        sw.WriteLine("\t" + "void Start() {");
	        sw.WriteLine("\t" + "}" + "\n");
	        sw.WriteLine("}");
	        sw.Flush();
	        sw.Close();
	
	        Debug.Log("Gen: " + Application.dataPath + "/Scripts/Game/UICtrl/" + className + ".cs");
	    }
	}
}
