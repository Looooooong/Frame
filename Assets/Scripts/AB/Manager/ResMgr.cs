using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssetBundles;

public class ResMgr : UnitySingleton<ResMgr> {

    private Queue<string> bundles = new Queue<string>();

    public override void Awake() {

        //base.Awake();
        this.gameObject.AddComponent<AssetBundleManager>();
        StartCoroutine(InitResManager());
    }

    /// <summary>
    /// 同步加载Asset资源,处理依赖,同时缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    public T GetAssetSync<T>(string assetPath) where T : UnityEngine.Object
    {
#if UNITY_EDITOR
        if (AssetBundleConfig.IsEditorMode)
        {
            string path = AssetBundleUtility.PackagePathToAssetsPath(assetPath);
            UnityEngine.Object target = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            return target as T;
        }
#endif
        UnityEngine.Object obj = AssetBundleManager.Instance.GetAssetCache(assetPath);
        if (obj is T)
        {
            return obj as T;
        }
        else
        {
            Debug.LogError($"该资源类型是 ：{obj.GetType()}");
            return null;
        }
    }
    /// <summary>
    /// 异步加载Asset资源,处理依赖,同时缓存
    /// </summary>
    /// <param name="assetPath">加载ab包名</param>
    /// <param name="endFunc">完成后回调</param>
    public void GetAssetAsync(string assetPath, Action endFunc = null)
    {
        this.StartCoroutine(this.IE_LoadAssetAsync(assetPath, endFunc));
    }


    IEnumerator IE_LoadAssetAsync(string assetPath, Action endFunc)
    {
        var loader = AssetBundleManager.Instance.LoadAssetAsync(assetPath,typeof(UnityEngine.Object));
        yield return loader;
        loader.Dispose();

        if (endFunc != null)
            endFunc();
    }

    /// <summary>
    /// 异步加载ab包,处理依赖,资源缓存
    /// </summary>
    /// <param name="assetbundleName">加载ab包名数组</param>
    /// <param name="endFunc">完成后回调</param>
    public void LoadAssetBundleAsync(string[] assetbundleName, Action endFunc = null)
    {
        this.StartCoroutine(this.IE_LoadAssetBundleAsync(assetbundleName, endFunc));
    }

    IEnumerator IE_LoadAssetBundleAsync(string[] assetbundleName, Action endFunc)
    {
        int length = assetbundleName.Length;
        for (int i = 0; i < length; i++)
        {
            var loader = AssetBundleManager.Instance.LoadAssetBundleAsync(assetbundleName[i]);
            yield return loader;
            loader.Dispose();
        }

        if (endFunc != null)
            endFunc();
    }

    /// <summary>
    /// 异步加载bundle,不缓存资源
    /// 注意,使用完成后释放下载器(.Dispose())
    /// </summary>
    /// <param name="assetbundleName">ab名字</param>
    /// <returns></returns>
    public ResourceWebRequester LoadBundleAsync(string assetbundleName)
    {
        var loader = AssetBundleManager.Instance.RequestAssetBundleAsync(assetbundleName);
        return loader;

    }


    public IEnumerator InitResManager()
    {

        var start = DateTime.Now;
        ////启动检测更新
        //yield return CheckAndDownload();
        //Debug.Log(string.Format("CheckAndDownload use {0}ms", (DateTime.Now - start).Milliseconds));


        start = DateTime.Now;
        yield return InitPackageName();
        Debug.Log(string.Format("InitPackageName use {0}ms", (DateTime.Now - start).Milliseconds));


        // 启动资源管理模块
        start = DateTime.Now;
        yield return AssetBundleManager.Instance.Initialize();
        Debug.Log(string.Format("AssetBundleManager Initialize use {0}ms", (DateTime.Now - start).Milliseconds));


        //Tip.Show("资源更新成功.", uiCanvas);
        //uiCanvas.GetComponent<LoginUIManager>().enabled = true;

    }

    /// <summary>
    /// 清空asset缓存,切换场景时使用
    /// </summary>
    public void ClearAssets()
    {
        AssetBundleManager.Instance.ClearAssetsCache();
        AssetBundleManager.Instance.UnloadUnusedAssetBundles();
    }


    /// <summary>
    /// 检查资源更新
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckAndDownload()
    {
        string outputPath = AssetBundleUtility.GetPersistentDataPath() + "/";

        //从资源服务器拉取资源版本文件
        var downloadRequest = AssetBundleManager.Instance.DownloadAssetBundleAsync("app_version.bytes");
        yield return downloadRequest;
        VersionConfig remoteVersion = LitJson.JsonMapper.ToObject<VersionConfig>(downloadRequest.text);
        downloadRequest.Dispose();

        //if(remoteVersion == null)
        //{
        //    Tip.Show("连接资源服务器失败,请重新登录！", uiCanvas);
        //}

        //比较版本文件
        VersionConfig localVersion;
        using (ResourceWebRequester request = AssetBundleManager.Instance.RequestAssetFileAsync("app_version.bytes"))
        {
            yield return request;
            localVersion = LitJson.JsonMapper.ToObject<VersionConfig>(request.text);
        }

        //版本一致,直接进入游戏
        if (localVersion != null && remoteVersion.Version == localVersion.Version)
        {
            yield break;
        }
        else
        {
            if(localVersion != null)
            {   
                //删除本地在服务器上没有的bundles
                foreach (FileVersionInfo fileVersionInfo in localVersion.FileInfoDict.Values)
                {
                    //对比文件
                    if (remoteVersion.FileInfoDict.ContainsKey(fileVersionInfo.File))
                    {
                        continue;
                    }


                    bundles.Enqueue(fileVersionInfo.File);
                }
                while (bundles.Count != 0)
                {
                    string bundle = bundles.Dequeue();
                    GameUtility.SafeDeleteFile(outputPath + bundle);
                    Debug.Log("Delete file : " + outputPath + bundle);
                }
            }
         


            //保存有差异的ab包名
            long totalSize = 0;
            // 对比MD5
            foreach (FileVersionInfo fileVersionInfo in remoteVersion.FileInfoDict.Values)
            {
                // 对比md5
                string localFileMD5 = GetBundleMD5(localVersion, fileVersionInfo.File);

                if (fileVersionInfo.MD5 == localFileMD5)
                {
                    continue;
                }

                bundles.Enqueue(fileVersionInfo.File);
                totalSize += fileVersionInfo.Size;
            }
            int size = 0;
            //替换新的版本文件,做增量更新
            while (bundles.Count != 0)
            {
                if(bundles.Count >= 3)
                {
                    string bundle1 = bundles.Dequeue();
                    string bundle2 = bundles.Dequeue();
                    string bundle3 = bundles.Dequeue();
                    var downloadRequest1 = AssetBundleManager.Instance.DownloadAssetBundleAsync(bundle1);
                    var downloadRequest2 = AssetBundleManager.Instance.DownloadAssetBundleAsync(bundle2);
                    var downloadRequest3 = AssetBundleManager.Instance.DownloadAssetBundleAsync(bundle3);
                    yield return downloadRequest1;
                    GameUtility.SafeWriteAllBytes(outputPath + bundle1, downloadRequest1.bytes);
                    size += (downloadRequest1.bytes.Length / 1024) + 1; //kb 保存下载文件的大小
                    downloadRequest1.Dispose();
                    yield return downloadRequest2;
                    GameUtility.SafeWriteAllBytes(outputPath + bundle2, downloadRequest2.bytes);
                    size += (downloadRequest2.bytes.Length / 1024) + 1; //kb 保存下载文件的大小
                    downloadRequest2.Dispose();
                    yield return downloadRequest3;
                    GameUtility.SafeWriteAllBytes(outputPath + bundle3, downloadRequest3.bytes);
                    size += (downloadRequest3.bytes.Length / 1024) + 1; //kb 保存下载文件的大小
                    downloadRequest3.Dispose();
                }
                else
                {
                    string bundle = bundles.Dequeue();
                    downloadRequest = AssetBundleManager.Instance.DownloadAssetBundleAsync(bundle);
                    yield return downloadRequest;
                    GameUtility.SafeWriteAllBytes(outputPath + bundle, downloadRequest.bytes);
                    size += (downloadRequest.bytes.Length / 1024) + 1; //kb 保存下载文件的大小
                    downloadRequest.Dispose();
                }
            }

            //保存新的版本文件
            GameUtility.SafeWriteAllText(outputPath + "app_version.bytes", LitJson.JsonMapper.ToJson(remoteVersion));
            yield break;
        }
    }

    /// <summary>
    /// 请求并且初始化AB总包包名
    /// </summary>
    /// <returns></returns>
    IEnumerator InitPackageName()
    {
#if UNITY_EDITOR
        if (AssetBundleConfig.IsEditorMode)
        {
            yield break;
        }
#endif
        var packageNameRequest = AssetBundleManager.Instance.RequestAssetFileAsync(BuildUtils.PackageNameFileName);
        yield return packageNameRequest; // 中断当前协程，直到请求结束;
        var packageName = packageNameRequest.text;
        packageNameRequest.Dispose(); // 释放请求;

        AssetBundleManager.ManifestBundleName = packageName; // 包名字;
        Debug.Log(string.Format("packageName = {0}", packageName));
        yield break;
    }


    string GetBundleMD5(VersionConfig streamingVersionConfig, string bundleName)
    {
        if (streamingVersionConfig != null && streamingVersionConfig.FileInfoDict.ContainsKey(bundleName))
        {
            return streamingVersionConfig.FileInfoDict[bundleName].MD5;
        }
        return "";
    }



}
