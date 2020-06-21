using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIConfigurationReader 
{
    //数据结构
    //大字典 ： key 状态      value 映射
    //小字典 ： key 条件编号  value 状态编号
    public Dictionary<string, Dictionary<string, string>> Map { get; private set; }

    public AIConfigurationReader(string fileName)
    {
        Map = new Dictionary<string, Dictionary<string, string>>();
        //读取配置文件
        string configFile = ConfigurationReader.GetConfigFile(fileName);
        //解析配置文件
        ConfigurationReader.Reader(configFile, ReadMap);
    }


    private string mainKey;
    /// <summary>
    /// 读每行数据
    /// </summary>
    /// <param name="line"></param>
    private void ReadMap(string line)
    {
        line = line.Trim();
        if(!string.IsNullOrEmpty(line))
        {
            //状态
            if(line.StartsWith("["))
            {
                //[Idle] --> Idle
                mainKey = line.Substring(1, line.Length - 2);
                Map.Add(line, new Dictionary<string, string>());
            }
            else
            {
                //映射
                string[] keyValue = line.Split('>');
                Map[mainKey].Add(keyValue[0], keyValue[1]);
            }
        }
    }
}
