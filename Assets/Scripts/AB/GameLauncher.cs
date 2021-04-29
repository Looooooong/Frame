using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssetBundles;
using GameChannel;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;


public enum VersionCheckEnum
{
    Error = 0, //检测失败
    Hotfix = 1, //热更新
    NewPack = 2, //整包更新
    Pass = 3, //检测通过
}


public class GameLauncher : UnitySingleton<GameLauncher>
{
    private Queue<string> bundles = new Queue<string>();
    private long totalSize;

    private VersionCheckEnum updateState = VersionCheckEnum.Error;

    private string hotfixFold = "Hotfix/";
    private string hotfixSuffix = ".zip";

    public string Host { get; set; }
    public string Voice { get; set; }

    private Queue<string> downloadUrlQueue = new Queue<string>();

    public override void Awake()
    {
        base.Awake();
        // 初始化游戏框架
        //this.gameObject.AddComponent<ResMgr>();

        //this.gameObject.AddComponent<SoundMgr>();
        //this.gameObject.AddComponent<EventMgr>();
        //EventMgr.Instance.init();
        //this.gameObject.AddComponent<UI_manager>();
        //this.gameObject.AddComponent<NetMgr>();

        //#if RELEASE_BUILD
        //#else
        //        this.gameObject.AddComponent<show_fps>();
        //        if (Application.platform == RuntimePlatform.Android ||
        //            Application.platform == RuntimePlatform.IPhonePlayer)
        //        {
        //            this.gameObject.AddComponent<DebugManager>();
        //        }
        //#endif
        //        // end 

        //        this.gameObject.AddComponent<Game>();

    }

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
        //ChannelManager.instance.Init(packageName);
        Debug.Log(string.Format("packageName = {0}", packageName));
        yield break;
    }

    IEnumerator CheckAndDownload()
    {
        string outputPath = AssetBundleUtility.GetPersistentDataPath() + "/";

        //从资源服务器拉取资源版本文件
        var downloadRequest = AssetBundleManager.Instance.DownloadAssetBundleAsync("app_version.bytes");
        yield return downloadRequest;
        VersionInfo remoteVersion = LitJson.JsonMapper.ToObject<VersionInfo>(downloadRequest.text);
        downloadRequest.Dispose();

        //Host = remoteVersion.Host;
        //Voice = remoteVersion.Voice;

        //比较版本文件
        VersionInfo localVersion = null;
        using (ResourceWebRequester request = AssetBundleManager.Instance.RequestAssetFileAsync("app_version.bytes"))
        {
            yield return request;
            localVersion = LitJson.JsonMapper.ToObject<VersionInfo>(request.text);
        }

        CheckVersion(remoteVersion, localVersion);

        #region 增量更新

        //保存有差异的ab包名
        // 对比MD5
        // foreach (FileVersionInfo fileVersionInfo in remoteVersion.FileInfoDict.Values)
        // {
        //     // 对比md5
        //     string localFileMD5 = GetBundleMD5(localVersion, fileVersionInfo.File);
        //
        //     if (fileVersionInfo.MD5 == localFileMD5)
        //     {
        //         continue;
        //     }
        //
        //     bundles.Enqueue(fileVersionInfo.File);
        //     totalSize += fileVersionInfo.Size;
        // }


        // size = 0;
        // //替换新的版本文件,做增量更新
        //             while (bundles.Count != 0)
        //             {
        //                 string bundle = bundles.Dequeue();
        //                 downloadRequest = AssetBundleManager.Instance.DownloadAssetBundleAsync(bundle);
        //                 yield return downloadRequest;
        //                 GameUtility.SafeWriteAllBytes(outputPath + bundle, downloadRequest.bytes);
        //                 size += (downloadRequest.bytes.Length / 1024) + 1; //kb 保存下载文件的大小
        //                 downloadRequest.Dispose();
        //             }
        //
        //             //保存新的版本文件
        //             GameUtility.SafeWriteAllText(outputPath + "app_version.bytes", LitJson.JsonMapper.ToJson(remoteVersion));

        #endregion

        //var downloadRequest = AssetBundleManager.Instance.DownloadAssetBundleAsync("charactors.assetbundle");
        //yield return downloadRequest;
        //GameUtility.SafeWriteAllBytes(AssetBundleUtility.GetPersistentDataPath() + "/charactors.assetbundle", downloadRequest.bytes);
        //downloadRequest.Dispose();
    }

    IEnumerator GameStart()
    {
        //yield break;
        //StartCoroutine(TestMessage());

        //启动检测更新
        var start = DateTime.Now;
        yield return CheckAndDownload();
        Debug.Log(string.Format("CheckAndDownload use {0}ms", (DateTime.Now - start).Milliseconds));

        //EventManager.DispatchEvent(EventName.Hotfix, (int) updateState);
        yield break;


        //start = DateTime.Now;
        //yield return InitPackageName();
        //Debug.Log(string.Format("InitPackageName use {0}ms", (DateTime.Now - start).Milliseconds));


        // 启动资源管理模块
        //start = DateTime.Now;
        //yield return AssetBundleManager.Instance.Initialize();
        //Debug.Log(string.Format("AssetBundleManager Initialize use {0}ms", (DateTime.Now - start).Milliseconds));

        //end

        //加载资源

        //string[] res = { "charactors.assetbundle" , "sounds.assetbundle" , "excels.assetbundle" , "maps.assetbundle" , "gui.assetbundle" , "channel/channel.assetbundle"
        //,"channel/charactors_bytes.assetbundle"};
        //ResMgr.Instance.LoadAssetBundleAsync(res, () => SceneManager.LoadScene(1));


        // string scene = "test1.u3d";
        // var url = AssetBundleUtility.GetAssetBundleFileUrl(scene);
        // ResourceWebRequester wr = AssetBundleManager.Instance.RequestAssetBundleAsync(scene);
        // yield return wr;
        // if (wr.error == null)
        // {
        //     AssetBundle ab = wr.assetbundle; //将场景通过AssetBundle方式加载到内存中 
        //     SceneManager.LoadScene("LightMapTest1");
        //     ab = null;
        //     //AsyncOperation asy = SceneManager.LoadSceneAsync("LightMapTest1"); //sceneName不能加后缀,只是场景名称
        //     //yield return asy;
        // }


        //var loader1 = AssetBundleManager.Instance.LoadAssetBundleAsync("charactors.assetbundle");
        //var loader2 = AssetBundleManager.Instance.LoadAssetBundleAsync("sounds.assetbundle");
        //var loader3 = AssetBundleManager.Instance.LoadAssetBundleAsync("excels.assetbundle");
        //var loader4 = AssetBundleManager.Instance.LoadAssetBundleAsync("maps.assetbundle");
        //var loader5 = AssetBundleManager.Instance.LoadAssetBundleAsync("gui.assetbundle");
        //var loader6 = AssetBundleManager.Instance.LoadAssetBundleAsync("channel/channel.assetbundle");
        //var loader7 = AssetBundleManager.Instance.LoadAssetBundleAsync("channel/charactors_bytes.assetbundle");
        //yield return loader1;
        //yield return loader2;
        //yield return loader3;
        //yield return loader4;
        //yield return loader5;
        //yield return loader6;
        //yield return loader7;

        //loader1.Dispose();
        //loader2.Dispose();
        //loader3.Dispose();
        //loader4.Dispose();
        //loader5.Dispose();
        //loader6.Dispose();
        //loader7.Dispose();

        //Game.Instance.EnterGame();
    }



    void Start()
    {
        StartUpdate();
    }


    public string GetBundleMD5(VersionConfig streamingVersionConfig, string bundleName)
    {
        if (streamingVersionConfig != null && streamingVersionConfig.FileInfoDict.ContainsKey(bundleName))
        {
            return streamingVersionConfig.FileInfoDict[bundleName].MD5;
        }

        return "";
    }

    /// <summary>
    /// 比照版本,返回相应结果
    /// </summary>
    /// <param name="remote"></param>
    /// <param name="local"></param>
    /// <returns></returns>
    private void CheckVersion(VersionInfo remote, VersionInfo local)
    {
        if (remote != null && local != null)
        {
            if (remote.MainVersion == local.MainVersion)
            {
                if (remote.SubVersion == local.SubVersion)
                {
                    if (remote.ResVersion == local.ResVersion)
                        updateState = VersionCheckEnum.Pass;
                    else
                        updateState = VersionCheckEnum.Hotfix;
                }
                else
                    updateState = VersionCheckEnum.NewPack;
            }
            else
                updateState = VersionCheckEnum.NewPack;
        }
        else
            updateState = VersionCheckEnum.Error;
    }





    public void StartUpdate()
    {
        this.StartCoroutine(this.GameStart());
    }


}