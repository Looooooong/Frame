using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Main 
{	
	/// <summary>
	/// 对象池管理类
	/// </summary>
	public static class PoolManager
	{
		public static GameObject poolHolder; // 所有对象池的holder
		public static Dictionary<string, PoolData> availablePools; // 所有可用的资源池字典
		public static PoolVisualizer poolVisualizer; // 可视化类,方便在editor中显示
		public static bool debug = false; //是否显示debug
	
		public static void Initialize()
		{
			GameObject tPoolHolder = GameObject.Find("Pools"); 
			if (tPoolHolder == null) poolHolder = new GameObject("Pools"); 
			else poolHolder = tPoolHolder; 
			poolVisualizer = poolHolder.AddComponent<PoolVisualizer>(); // 添加可视化类
	        poolHolder.transform.position = new Vector3(0, -200, 0);
	        DontDestroyOnLoad.DontDestroyOnLoad(poolHolder);
			availablePools = new Dictionary<string, PoolData>(); // 初始化存储对象池的字典
		}
	
		/// <summary>
	    /// 创建一个资源对象池
	    /// </summary>
	    /// <param name="aPath"></param>
	    /// <param name="aPrefab"></param>
	    /// <param name="anAmount"></param>
	    /// <returns></returns>
		public static PoolData CreatePool(string aPath, string aPrefab, int anAmount)
		{
			PoolData poolData = new PoolData(); 
			poolData.Initialize(aPath, aPrefab, anAmount); 
			availablePools[aPrefab] = poolData; // 储存到字典中
			poolVisualizer.allPoolData.Add(poolData); // 添加到列表中,可以在editor上显示
			return poolData;
		}
	
		/// <summary>
		/// 对象池是否存在
		/// </summary>
		/// <returns><c>true</c>, 存在, <c>false</c> 不存在.</returns>
		/// <param name="aPoolName">对象池名字</param>
		public static bool DoesPoolExist(string aPoolName){
	
			return availablePools.ContainsKey(aPoolName); 
		}
	
		/// <summary>
		/// 从对象池中获取一个对象
		/// </summary>
		/// <returns>GameObject</returns>
		/// <param name="aPoolName">对象池名字</param>
		public static GameObject GetObjectFromPool(string aPoolName){
	
			if(!availablePools.ContainsKey(aPoolName)){if(debug) Debug.Log("[PoolManager] GetObjectFromPool. This is a last resort fallback! There is no pool with this name: " + aPoolName); return null;} 
			return availablePools[aPoolName].GetObject();
		}
	
		/// <summary>
		/// 将对象返回到它所对应的池中
		/// </summary>
		/// <param name="aPoolName">对象池名字</param>
		/// <param name="anObject">对象</param>
		public static void ReturnObjectToPool(string aPoolName, GameObject anObject){
			if(!availablePools.ContainsKey(aPoolName)|| availablePools ==null)
	        {
				if(debug)Debug.Log("[PoolManager] ReturnObjectToPool. This is a last resort fallback! There is no pool with this name: " + aPoolName + ". This object will be destroyed instead.");
				Object.Destroy(anObject);
			} else availablePools[aPoolName].ReturnObject(anObject);
		}
	
		/// <summary>
		/// 清空该实例下的所有对象池
		/// </summary>
		public static void Reset(){
			// 清空所有对象池
			List<PoolData> allPoolData = new List<PoolData>();
			allPoolData.AddRange(availablePools.Values);
			PoolData poolData;
			for (int i = allPoolData.Count-1; i >= 0; i--) {
				poolData = allPoolData[i];
				allPoolData.RemoveAt(i);
				poolData.Destroy();
			}
	
	        // 初始化availablePools
	        availablePools = new Dictionary<string, PoolData>();
		}
	}
	
}
