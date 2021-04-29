using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 说明：Assetbundle检测器，由于Unity中的AssetBundle名字标签很不好管理，这里做一层检测以防漏
/// 注意：
/// 1、每个Assetbundle对应配置一个Checker，Checker对应的PackagePath及为Assetbundle所在路径
/// 2、每个Checker可以检测多个目录或者文件，这些目录或者文件被打入一个Assetbundle包
/// </summary>

namespace AssetBundles
{
    public class AssetBundleCheckerFilter
    {
        public string RelativePath;
        public string ObjectFilter;
        
        public AssetBundleCheckerFilter(string relativePath, string objectFilter)
        {
            RelativePath = relativePath;
            ObjectFilter = objectFilter;
        }
    }

    public class AssetBundleCheckerConfig
    {
        public string PackagePath = string.Empty;
        public List<AssetBundleCheckerFilter> CheckerFilters = null;

        public AssetBundleCheckerConfig()
        {
        }

        public AssetBundleCheckerConfig(string packagePath, List<AssetBundleCheckerFilter> checkerFilters)
        {
            PackagePath = packagePath;
            CheckerFilters = checkerFilters;
        }
    }

    public class AssetBundleChecker
    {
        string assetsPath;
        AssetBundleImporter importer;
        AssetBundleCheckerConfig config;

        public AssetBundleChecker(AssetBundleCheckerConfig config)
        {
            this.config = config;
            assetsPath = AssetBundleUtility.PackagePathToAssetsPath(config.PackagePath);
            importer = AssetBundleImporter.GetAtPath(assetsPath);
        }

        public void CheckAssetBundleName()
        {
            if (!importer.IsValid)
            {
                return;
            }
            var checkerFilters = config.CheckerFilters;
            if (checkerFilters == null || checkerFilters.Count == 0)
            {
                importer.assetBundleName = assetsPath;
            }
            else
            {
                foreach (var checkerFilter in checkerFilters)
                {
                    var relativePath = assetsPath;
                    if (!string.IsNullOrEmpty(checkerFilter.RelativePath))
                    {
                        relativePath = Path.Combine(assetsPath, checkerFilter.RelativePath);
                    }
                    var imp = AssetBundleImporter.GetAtPath(relativePath);
                    if (imp == null)
                    {
                        continue;
                    }
                    if (imp.IsFile)
                    {
                        importer.assetBundleName = assetsPath;
                        continue;
                    }
                    string[] objGuids = AssetDatabase.FindAssets(checkerFilter.ObjectFilter, new string[] { relativePath });
                    foreach (var guid in objGuids)
                    {
                        var path = AssetDatabase.GUIDToAssetPath(guid);
                        imp = AssetBundleImporter.GetAtPath(path);
                        imp.assetBundleName = assetsPath;
                    }
                }
            }
        }

        public void CheckChannelName()
        {
            string channelAssetPath = Path.Combine(AssetBundleConfig.ChannelFolderName, config.PackagePath);
            channelAssetPath = AssetBundleUtility.PackagePathToAssetsPath(channelAssetPath) + ".bytes";
            if (!File.Exists(channelAssetPath))
            {
                GameUtility.SafeWriteAllText(channelAssetPath, "None");
                AssetDatabase.Refresh();
            }

            var imp = AssetBundleImporter.GetAtPath(channelAssetPath);
            imp.assetBundleName = assetsPath;
        }

        public static void Run(AssetBundleCheckerConfig config)
        {
            var checker = new AssetBundleChecker(config);
            checker.CheckAssetBundleName();
            //checker.CheckChannelName();
            AssetDatabase.Refresh();
        }
    }
}
