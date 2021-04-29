using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 功能： Assetbundle相关的Asset路径映射解析，每次在构建Assetbunlde完成自动生成，每次有资源更新时需要强行下载一次
/// 说明： 映射规则：
/// 1）对于Asset：Asset加载路径（相对于Assets文件夹）到Assetbundle名与Asset名的映射
/// 2）对于带有Variant的Assetbundle，做通用替换处理
/// 注意：Assets路径带文件类型后缀，且区分大小写
/// 使用说明：
/// 1）asset加载：
///     假定AssetBundleConfig设置为AssetsFolderName = "AssetsPackage"，且：
///         A）assetbundle名称：assetspackage/ui/prefabs/view/uiloading_prefab.assetbundle
///         B）assetbundle中资源：UILoading.prefab
///         C）Assets路径为：Assets/AssetsPackage/UI/Prefabs/View/UILoading.prefab
///     则代码中需要的加载路径为：UI/Prefabs/View/UILoading.prefab
/// </summary>

namespace AssetBundles
{
    public class ResourcesMapItem
    {
        public string assetbundleName;
        public string assetName;
    }

    public class AssetsPathMapping
    {
        protected const string PATTREN = AssetBundleConfig.CommonMapPattren;
        protected Dictionary<string, ResourcesMapItem> pathLookup = new Dictionary<string, ResourcesMapItem>();
        protected Dictionary<string, List<string>> assetsLookup = new Dictionary<string, List<string>>();
        protected Dictionary<string, string> assetbundleLookup = new Dictionary<string, string>();
        protected List<string> emptyList = new List<string>();

        public AssetsPathMapping()
        {
            AssetName = AssetBundleUtility.PackagePathToAssetsPath(AssetBundleConfig.AssetsPathMapFileName);
            AssetbundleName = AssetBundleUtility.AssetBundlePathToAssetBundleName(AssetName);
        }

        public string AssetbundleName
        {
            get;
            protected set;
        }

        public string AssetName
        {
            get;
            protected set;
        }

        public void Initialize(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                Debug.LogError("ResourceNameMap empty!!");
                return;
            }

            content = content.Replace("\r\n", "\n");
            string[] mapList = content.Split('\n');
            foreach (string map in mapList)
            {
                if (string.IsNullOrEmpty(map))
                {
                    continue;
                }

                string[] splitArr = map.Split(new[] { PATTREN }, System.StringSplitOptions.None);
                if (splitArr.Length < 2)
                {
                    Debug.LogError("splitArr length < 2 : " + map);
                    continue;
                }

                ResourcesMapItem item = new ResourcesMapItem();
                // 如：ui/prefab/assetbundleupdaterpanel_prefab.assetbundle
                item.assetbundleName = splitArr[0];
                // 如：UI/Prefab/AssetbundleUpdaterPanel.prefab
                item.assetName = splitArr[1];
                
                var assetPath = item.assetName;
                pathLookup.Add(assetPath, item);
                List<string> assetsList = null;
                assetsLookup.TryGetValue(item.assetbundleName, out assetsList);
                if (assetsList == null)
                {
                    assetsList = new List<string>();
                }
                if (!assetsList.Contains(item.assetName))
                {
                    assetsList.Add(item.assetName);
                }
                assetsLookup[item.assetbundleName] = assetsList;
                assetbundleLookup.Add(item.assetName, item.assetbundleName);
            }
        }
        
        public bool MapAssetPath(string assetPath, out string assetbundleName, out string assetName)
        {
            assetbundleName = null;
            assetName = null;
            ResourcesMapItem item = null;
            if (pathLookup.TryGetValue(assetPath, out item))
            {
                assetbundleName = item.assetbundleName;
                assetName = item.assetName;
                return true;
            }
            return false;
        }
        
        public List<string> GetAllAssetNames(string assetbundleName)
        {
            List<string> allAssets = null;
            assetsLookup.TryGetValue(assetbundleName, out allAssets);
            return allAssets == null ? emptyList : allAssets;
        }

        public string GetAssetBundleName(string assetName)
        {
            string assetbundleName = null;
            assetbundleLookup.TryGetValue(assetName, out assetbundleName);
            return assetbundleName;
        }
    }
}
