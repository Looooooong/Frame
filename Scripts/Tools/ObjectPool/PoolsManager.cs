using UnityEngine;
using System.Collections;

/// <summary>
/// 对象池管理类，管理游戏中所有对象池
/// </summary>
public class PoolsManager : MonoBehaviour {

    //对象池的实例和路径
    
    //声音播放源
    [Header("AS")]
    public string audioSourcePath = "AudioSource/";
    public string audioSourceNanme = "AS";
    


    void Awake()
    {
        PoolManager.Initialize();
        InitAllPools();
    }
    
    private void InitAllPools()
    {
        //创建声音播放源
        PoolManager.CreatePool(audioSourcePath, audioSourceNanme, 15);

    }

}
