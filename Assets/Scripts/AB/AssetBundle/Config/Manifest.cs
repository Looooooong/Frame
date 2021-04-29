﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 功能：Manifest管理：提供对AssetBundleManifest类的封装
/// </summary>

namespace AssetBundles
{
    public class Manifest
    {
        const string assetName = "AssetBundleManifest";
        AssetBundleManifest manifest = null;
        byte[] manifestBytes = null;
        string[] emptyStringArray = new string[] { };
        
        public Manifest()
        {
            AssetbundleName = AssetBundleManager.ManifestBundleName;
            if (string.IsNullOrEmpty(AssetbundleName))
            {
                Debug.LogError("You should set ManifestBundleName first!");
            }
        }
        
        public AssetBundleManifest assetbundleManifest
        {
            get
            {
                return manifest;
            }
        }

        public string AssetbundleName
        {
            get;
            protected set;
        }
        
        public int Length
        {
            get
            {
                return manifest == null ? 0 : manifest.GetAllAssetBundles().Length;
            }
        }

        public void LoadFromAssetbundle(AssetBundle assetbundle)
        {
            if (assetbundle == null)
            {
                Debug.LogError("Manifest LoadFromAssetbundle assetbundle null!");
                return;
            }
            manifest = assetbundle.LoadAsset<AssetBundleManifest>(assetName);
        }

        public void SaveBytes(byte[] bytes)
        {
            manifestBytes = bytes;
        }

        public void SaveToDiskCahce()
        {
            if (manifestBytes != null && manifestBytes.Length > 0)
            {
                string path = AssetBundleUtility.GetPersistentDataPath(AssetbundleName);
                GameUtility.SafeWriteAllBytes(path, manifestBytes);
            }
        }

        public Hash128 GetAssetBundleHash(string name)
        {
            return manifest == null ? default(Hash128) : manifest.GetAssetBundleHash(name);
        }
        
        public string[] GetAllAssetBundleNames()
        {
            return manifest == null ? emptyStringArray : manifest.GetAllAssetBundles();
        }
        
        public string[] GetAllAssetBundlesWithVariant()
        {
            return manifest == null ? emptyStringArray : manifest.GetAllAssetBundlesWithVariant();
        }
        
        public string[] GetAllDependencies(string assetbundleName)
        {
            return manifest == null ? emptyStringArray : manifest.GetAllDependencies(assetbundleName);
        }
        
        public string[] GetDirectDependencies(string assetbundleName)
        {
            return manifest == null ? emptyStringArray : manifest.GetDirectDependencies(assetbundleName);
        }
        
        public List<string> CompareTo(Manifest manifest)
        {
            List<string> ret_list = new List<string>();
            if (manifest.assetbundleManifest == null)
            {
                return ret_list;
            }

            if (manifest == null )
            {
                ret_list.AddRange(manifest.GetAllAssetBundleNames());
                return ret_list;
            }

            string[] other_name_list = manifest.GetAllAssetBundleNames();
            string[] self_name_list = GetAllAssetBundleNames();
            foreach (string name in other_name_list)
            {
                int idx = System.Array.FindIndex(self_name_list, element => element.Equals(name));
                if (idx == -1)
                {
                    //对方有、自己无
                    ret_list.Add(name);
                }
                else if (!GetAssetBundleHash(self_name_list[idx]).Equals(manifest.GetAssetBundleHash(name)))
                {
                    //对方有，自己有，但是hash不同
                    ret_list.Add(name);
                }
                else
                {
                    //对方有，自己有，且hash相同：什么也不做
                    //donothing
                }
            }
            return ret_list;
        }
    }
}