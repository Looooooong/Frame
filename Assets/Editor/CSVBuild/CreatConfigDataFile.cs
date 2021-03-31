using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorTool 
{	
	public class CreatConfigDataFile : EditorWindow
	{
	
	    static string writePath = "/Scripts/Configs/";
	    static Object selectObj;
	
	
	    [MenuItem("CSV/打开配置CSV表格的解析窗口")]
	    static void ByWindow()
	    {
	        CreatConfigDataFile window = EditorWindow.GetWindow<CreatConfigDataFile>();
	    }
	
	    private void OnGUI()
	    {
	        GUILayout.Label("设置配置数据文件的生成路径");
	        writePath = GUILayout.TextField(writePath);
	        GUILayout.Label("请选择一个合法的CSV文件");
	
        if (GUILayout.Button("生成C#协议文件"))
	        {
            Debug.Log("生成C#协议文件----------");
	            if (selectObj != null)
	            {
	                CreatConfigUitl.CreatConfigFile(selectObj, writePath);
	            }
	
	        }
	
	        if (Selection.activeObject != null)
	        {
	            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
	            if (path.ToLower().Substring(path.Length - 4, 4) == ".csv")
	            {
	                selectObj = Selection.activeObject;
	                GUILayout.Label(path);
	
	            }
	        }
	    }
	
	    private void OnSelectionChange()
	    {
	        Repaint();
	    }
	
	
	
	}
}
