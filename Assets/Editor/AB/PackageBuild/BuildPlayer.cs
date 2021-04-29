using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using GameChannel;
using AssetBundles;
using System;
using System.Text;

public class BuildPlayer : Editor
{
    public const string ApkOutputPath = "vAPK";
    public const string XCodeOutputPath = "vXCode";

    public static void WritePackageNameFile(BuildTarget buildTarget, string channelName)
    {
        var outputPath = PackageUtils.GetBuildPlatformOutputPath(buildTarget, channelName);
        GameUtility.SafeWriteAllText(Path.Combine(outputPath, BuildUtils.PackageNameFileName), channelName);
    }


    /// <summary>
    /// 打包版本文件,做资源的热更
    /// </summary>
    /// <param name="buildTarget"></param>
    /// <param name="channelName"></param>
    public static void WriteAssetBundleMD5AndSize(BuildTarget buildTarget, string channelName)
    {
        var outputPath = PackageUtils.GetBuildPlatformOutputPath(buildTarget, channelName);
        //var allAssetbundles = GameUtility.GetSpecifyFilesInFolder(outputPath, new string[] { ".assetbundle" });
        //TODO 子文件夹内的文件未包含
        var allAssetbundleFiles = GameUtility.GetSpecifyFilesInFolder(outputPath, new string[] { ".manifest" },true);
        StringBuilder sb = new StringBuilder();
        VersionConfig vc = new VersionConfig();
        vc.Version = PackageTool.resVersion;
        if (allAssetbundleFiles != null && allAssetbundleFiles.Length > 0)
        {
            foreach (var file in allAssetbundleFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Name == BuildUtils.AppVersionFileName) continue;
                string md5 = AssetBundleUtility.FileMD5(file);
                int size = (int)(fileInfo.Length / 1024) + 1; //kb
                var path = GameUtility.FormatToUnityPath(file.Substring(outputPath.Length + 1));
                vc.FileInfoDict.Add(path, new FileVersionInfo
                {
                    File = path,
                    MD5 = md5,
                    Size = size
                });
                vc.TotalSize += size;
            }
        }
        byte[] bytes = DataUtils.StringToBytes(LitJson.JsonMapper.ToJson(vc));
        GameUtility.SafeWriteAllBytes(Path.Combine(outputPath, BuildUtils.AppVersionFileName), bytes);



        //if (allAssetbundles != null && allAssetbundles.Length > 0)
        //{
        //    foreach (var assetbundle in allAssetbundles)
        //    {
        //        FileInfo fileInfo = new FileInfo(assetbundle);
        //        string md5 = AssetBundleUtility.FileMD5(assetbundle);
        //        int size = (int)(fileInfo.Length / 1024) + 1; //kb
        //        var path = assetbundle.Substring(outputPath.Length + 1);
        //        //sb.AppendFormat("{0}{1}{2}\n", GameUtility.FormatToUnityPath(path), AssetBundleConfig.CommonMapPattren, size);
        //        sb.AppendFormat("{0}{1}{2}{3}{4}\n", GameUtility.FormatToUnityPath(path), AssetBundleConfig.CommonMapPattren, md5, AssetBundleConfig.CommonMapPattren, size);
        //    }
        //}
        //string context = sb.ToString().Trim();
        //GameUtility.SafeWriteAllText(Path.Combine(outputPath, BuildUtils.AppVersionFileName), context);


    }

    private static void InnerBuildAssetBundles(BuildTarget buildTarget, string channelName, bool writeConfig)
    {
        //BuildAssetBundleOptions buildOption = BuildAssetBundleOptions.IgnoreTypeTreeChanges | BuildAssetBundleOptions.DeterministicAssetBundle;
        BuildAssetBundleOptions buildOption = BuildAssetBundleOptions.IgnoreTypeTreeChanges | BuildAssetBundleOptions.DeterministicAssetBundle | 
                                              BuildAssetBundleOptions.ChunkBasedCompression;
        var outputPath = PackageUtils.GetBuildPlatformOutputPath(buildTarget, channelName);
        AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath, buildOption, buildTarget);
        if (manifest != null && writeConfig)
        {
            AssetsPathMappingEditor.BuildPathMapping(manifest);
            VariantMappingEditor.BuildVariantMapping(manifest);
            BuildPipeline.BuildAssetBundles(outputPath, buildOption, buildTarget);
        }
        WritePackageNameFile(buildTarget, channelName);
        WriteAssetBundleMD5AndSize(buildTarget, channelName);
        AssetDatabase.Refresh();
    }


    //private static void InnerBuildAssetBundles(BuildTarget buildTarget, string channelName, bool writeConfig)
    //{
    //    //BuildAssetBundleOptions buildOption = BuildAssetBundleOptions.IgnoreTypeTreeChanges | BuildAssetBundleOptions.DeterministicAssetBundle;
    //    BuildAssetBundleOptions buildOption = BuildAssetBundleOptions.IgnoreTypeTreeChanges | BuildAssetBundleOptions.DeterministicAssetBundle |
    //                                          BuildAssetBundleOptions.ChunkBasedCompression;
    //    var outputPath = PackageUtils.GetBuildPlatformOutputPath(buildTarget, channelName);
    //    AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath, buildOption, buildTarget);
    //    if (manifest != null && writeConfig)
    //    {
    //        AssetsPathMappingEditor.BuildPathMapping(manifest);
    //        VariantMappingEditor.BuildVariantMapping(manifest);
    //        BuildPipeline.BuildAssetBundles(outputPath, buildOption, buildTarget);
    //    }
    //    WritePackageNameFile(buildTarget, channelName);
    //    WriteAssetBundleMD5AndSize(buildTarget, channelName);
    //    AssetDatabase.Refresh();
    //}



    public static void BuildAssetBundles(BuildTarget buildTarget, string channelName)
    {
        
        var start = DateTime.Now;
        CheckAssetBundles.RunCheck();
        Debug.Log("Finished CheckAssetBundles.Run! use " + (DateTime.Now - start).TotalSeconds + "s");
        /*
        var start = DateTime.Now;
        CheckAssetBundles.SwitchChannel(channelName.ToString());
        Debug.Log("Finished CheckAssetBundles.SwitchChannel! use " + (DateTime.Now - start).TotalSeconds + "s");
        */
        start = DateTime.Now;
        InnerBuildAssetBundles(buildTarget, channelName, true);
        Debug.Log("Finished InnerBuildAssetBundles! use " + (DateTime.Now - start).TotalSeconds + "s");

        var targetName = PackageUtils.GetPlatformName(buildTarget);
        Debug.Log(string.Format("Build assetbundles for platform : {0} and channel : {1} done!", targetName, channelName));
    }


    public static void BuildAssetBundlesWithoutCheck(BuildTarget buildTarget, string channelName)
    {
        /*
        var start = DateTime.Now;
        CheckAssetBundles.RunCheck();
        Debug.Log("Finished CheckAssetBundles.Run! use " + (DateTime.Now - start).TotalSeconds + "s");
        
        start = DateTime.Now;
        CheckAssetBundles.SwitchChannel(channelName.ToString());
        Debug.Log("Finished CheckAssetBundles.SwitchChannel! use " + (DateTime.Now - start).TotalSeconds + "s");
        */
        var start = DateTime.Now;
        InnerBuildAssetBundles(buildTarget, channelName, true);
        Debug.Log("Finished InnerBuildAssetBundles! use " + (DateTime.Now - start).TotalSeconds + "s");

        var targetName = PackageUtils.GetPlatformName(buildTarget);
        Debug.Log(string.Format("Build assetbundles for platform : {0} and channel : {1} done!", targetName, channelName));
    }

    public static void BuildAssetBundlesForAllChannels(BuildTarget buildTarget)
    {
        var targetName = PackageUtils.GetPlatformName(buildTarget);

        var start = DateTime.Now;
        CheckAssetBundles.RunCheck();
        Debug.Log("Finished CheckAssetBundles.Run! use " + (DateTime.Now - start).TotalSeconds + "s");

        int index = 0;
        double switchChannel = 0;
        double buildAssetbundles = 0;
        foreach (var current in (ChannelType[])Enum.GetValues(typeof(ChannelType)))
        {
            start = DateTime.Now;
            var channelName = current.ToString();
            CheckAssetBundles.SwitchChannel(channelName);
            switchChannel = (DateTime.Now - start).TotalSeconds;

            start = DateTime.Now;
            InnerBuildAssetBundles(buildTarget, channelName, index == 0);
            buildAssetbundles = (DateTime.Now - start).TotalSeconds;

            index++;
            Debug.Log(string.Format("{0}.Build assetbundles for platform : {1} and channel : {2} done! use time : switchChannel = {3}s , build assetbundls = {4} s", index, targetName, channelName, switchChannel, buildAssetbundles));
        }

        Debug.Log(string.Format("Build assetbundles for platform : {0} for all {1} channels done!", targetName, index));
    }

    public static void BuildAssetBundlesForCurSetting()
    {
        var buildTarget = EditorUserBuildSettings.activeBuildTarget;
        var outputPath = PackageUtils.GetCurBuildSettingOutputPath();
        BuildAssetBundles(buildTarget, outputPath);
    }

    public static void BuildAndroid(string channelName, bool isTest)
    {
        BuildTarget buildTarget = BuildTarget.Android;
        PackageUtils.CopyAssetBundlesToStreamingAssets(buildTarget, channelName);
        if (!isTest)
        {
            CopyAndroidSDKResources(channelName);
        }

        string buildFolder = Path.Combine(System.Environment.CurrentDirectory, ApkOutputPath);
        GameUtility.CheckDirAndCreateWhenNeeded(buildFolder);
        BaseChannel channel = ChannelManager.instance.CreateChannel(channelName);
        PlayerSettings.applicationIdentifier = channel.GetBundleID();
        PlayerSettings.productName = channel.GetPackageName();

        string savePath = null;
        if (channel.IsGooglePlay())
        {
            savePath = Path.Combine(buildFolder, channelName);
            GameUtility.SafeDeleteDir(savePath);
            BuildPipeline.BuildPlayer(GetBuildScenes(), savePath, buildTarget, BuildOptions.AcceptExternalModificationsToPlayer);
        }
        else
        {
            savePath = Path.Combine(buildFolder, channel.GetPackageName() + ".apk");
            BuildPipeline.BuildPlayer(GetBuildScenes(), savePath, buildTarget, BuildOptions.None);
        }
        Debug.Log(string.Format("Build android player for : {0} done! output ：{1}", channelName, savePath));
    }
    
    public static void BuildXCode(string channelName, bool isTest)
    {
        BuildTarget buildTarget = BuildTarget.iOS;
        PackageUtils.CopyAssetBundlesToStreamingAssets(buildTarget, channelName);

        string buildFolder = Path.Combine(System.Environment.CurrentDirectory, XCodeOutputPath);
        buildFolder = Path.Combine(buildFolder, channelName);
        GameUtility.CheckDirAndCreateWhenNeeded(buildFolder);

        string iconPath = "Assets/Editor/icon/ios/{0}/{1}.png";
        string[] iconSizes = new string[] { "180", "167","152", "144", "120", "114", "76", "72", "57" };
        List<Texture2D> iconList = new List<Texture2D>();
        for (int i = 0; i < iconSizes.Length; i++)
        {
            Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(string.Format(iconPath, channelName, iconSizes[i]), typeof(Texture2D));
            iconList.Add(texture);
        }
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.iOS, iconList.ToArray());

        BaseChannel channel = ChannelManager.instance.CreateChannel(channelName);
        PlayerSettings.applicationIdentifier = channel.GetBundleID();
        PlayerSettings.productName = channel.GetPackageName();
        PackageUtils.CheckAndAddSymbolIfNeeded(buildTarget, channelName);
        BuildPipeline.BuildPlayer(GetBuildScenes(), buildFolder, buildTarget, BuildOptions.None);
    }
	
	static string[] GetBuildScenes()
	{
		List<string> names = new List<string>();
		foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e != null && e.enabled)
            {
                names.Add(e.path);
            }
        }
        return names.ToArray();
    }


    public static void CopyAndroidSDKResources(string channelName)
    {
        string targetPath = Path.Combine(Application.dataPath, "/Plugins/Android");
        GameUtility.SafeDeleteDir(targetPath);

        string resPath = Path.Combine(Environment.CurrentDirectory, "/Channel/UnityCallAndroid_" + channelName);
        if (!Directory.Exists(resPath))
        {
            resPath = Path.Combine(Environment.CurrentDirectory, "/Channel/UnityCallAndroid" + channelName);
        }

        EditorUtility.DisplayProgressBar("提示", "正在拷贝SDK资源，请稍等", 0f);
        PackageUtils.CopyJavaFolder(resPath + "/assets", targetPath + "/assets");
        EditorUtility.DisplayProgressBar("提示", "正在拷贝SDK资源，请稍等", 0.3f);
        PackageUtils.CopyJavaFolder(resPath + "/libs", targetPath + "/libs");
        EditorUtility.DisplayProgressBar("提示", "正在拷贝SDK资源，请稍等", 0.6f);
        PackageUtils.CopyJavaFolder(resPath + "/res", targetPath + "/res");
        if (File.Exists(resPath + "/bin/UnityCallAndroid.jar"))
        {
            File.Copy(resPath + "/bin/UnityCallAndroid.jar", targetPath + "/libs/UnityCallAndroid.jar", true);
        }
        if (File.Exists(resPath + "/AndroidManifest.xml"))
        {
            File.Copy(resPath + "/AndroidManifest.xml", targetPath + "/AndroidManifest.xml", true);
        }
        if (File.Exists(resPath + "/icon/icon.png"))
        {
            File.Copy(resPath + "/icon/icon.png", Application.dataPath + "/icon.png", true);
        }

        EditorUtility.DisplayProgressBar("提示", "正在拷贝SDK资源，请稍等", 1f);
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
    }
}
