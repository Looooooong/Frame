using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Main 
{	
	/// <summary>
	/// 配置文件读取器
	/// </summary>
	public class ConfigurationReader 
	{
	
	    public static string GetConfigFile(string fileName)
	    {
	        string url;
        #region 分平台判断 streamassets 路径
	
#if UNITY_EDITOR || UNITY_STANDALONE
	        url = "file://" + Application.dataPath + "/SteamingAssets" + fileName;
#elif UNITY_IPHONE
	
#elif UNITY_ANDROID
	        url = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
#endif
        #endregion
	
	        WWW www = new WWW(url);
	        while(true)
	        {
	            if (www.isDone)
	                return www.text;
	        }
	
	    }
	
	
	    /// <summary>
	    /// 配置读取（按行读取）
	    /// </summary>
	    /// <param name="fileContent">文件内容</param>
	    /// <param name="handler">处理逻辑</param>
	    public static void Reader(string fileContent , Action<string> handler)
	    {
        using (StringReader reader = new StringReader(fileContent))
	        {
	            string line;
	            while((line = reader.ReadLine())!= null)
	            {
	                handler(line);
	            }
	        }
	    }
	
	
	}
}
