using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;


public class ConfigDataBase
{
    protected virtual string getFilePath()
    {
        return "";
    }

    static Dictionary<string, Dictionary<string, ConfigDataBase>> dataDic = new Dictionary<string, Dictionary<string, ConfigDataBase>>();


    public static T GetConfigData<T>(string key, string fileName = null) where T : ConfigDataBase
    {
        Type setT = typeof(T);
        if (fileName == null)
        {
            fileName = setT.Name;
        }

        if (!dataDic.ContainsKey(fileName))
        {
            ReadConfigData<T>(fileName);
        }
        Dictionary<string, ConfigDataBase> objDic = dataDic[fileName];
        Debug.Log("test  (" + key + ")" + objDic.Count);
        if (!objDic.ContainsKey(key))
        {
            throw new Exception("no this config");
        }
        return (T)(objDic[key]);
    }

    public static List<T> GetConfigDatas<T>(string fileName = null) where T : ConfigDataBase
    {
        List<T> returnList = new List<T>();
        Type setT = typeof(T);
        if (fileName == null)
        {
            fileName = setT.Name;
        }

        if (!dataDic.ContainsKey(fileName))
        {
            ReadConfigData<T>(fileName);
        }
        Dictionary<string, ConfigDataBase> objDic = dataDic[fileName];
        foreach (KeyValuePair<string, ConfigDataBase> kvp in objDic)
        {
            returnList.Add((T)(kvp.Value));
        }
        return returnList;
    }

    static void ReadConfigData<T>(string fileName = null) where T : ConfigDataBase
    {
        T obj = Activator.CreateInstance<T>();
        if (fileName == null)
        {
            fileName = obj.getFilePath();
        }
        // string 
        Debug.Log(fileName);
        string getString = Resources.Load<TextAsset>("Configs/" + fileName).text;
        //string getString = resource_manager.load_csv(fileName).text;

        CsvReaderByString csr = new CsvReaderByString(getString);

        Dictionary<string, ConfigDataBase> objDic = new Dictionary<string, ConfigDataBase>();

        FieldInfo[] fis = new FieldInfo[csr.ColCount];
        for (int colNum = 1; colNum < csr.ColCount + 1; colNum++)
        {
            fis[colNum - 1] = typeof(T).GetField(csr[1, colNum]);
        }

        for (int rowNum = 3; rowNum < csr.RowCount + 1; rowNum++)
        {
            T configObj = Activator.CreateInstance<T>();
            for (int i = 0; i < fis.Length; i++)
            {
                string fieldValue = csr[rowNum, i + 1];
                object setValue = new object();
                switch (fis[i].FieldType.ToString())
                {
                    case "System.Int32":
                        setValue = int.Parse(fieldValue);
                        break;
                    case "System.Int64":
                        setValue = long.Parse(fieldValue);
                        break;
                    case "System.String":
                        setValue = fieldValue;
                        break;
                    default:
                        Debug.Log("error data type");
                        break;
                }
                fis[i].SetValue(configObj, setValue);
                if (fis[i].Name == "key" || fis[i].Name == "id")
                {
                    //只检测key和id的值，然后添加到objDic 中
                    objDic.Add(setValue.ToString(), configObj);
                }
            }
        }
        dataDic.Add(fileName, objDic);    //可以作为参数
    }
}
