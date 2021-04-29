using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssetBundles;

public class ABLoadTest : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            //同步加载
            GameObject s = ResMgr.Instance.GetAssetSync<GameObject>("TestAssets/Cube.prefab");

            GameObject.Instantiate(s);


            ////异步加载缓存
            //ResMgr.Instance.GetAssetAsync("TestAssets/Cube.prefab", () =>
            //{
            //    GameObject s = ResMgr.Instance.GetAssetSync<GameObject>("TestAssets/Cube.prefab");

            //    GameObject.Instantiate(s);
            //});


            ////异步加载不缓存
            //StartCoroutine(LoadObj());



        }

        //卸载缓存
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AssetBundleManager.Instance.ClearAssetsCache();
        }
        //清除没有引用的assets
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Resources.UnloadUnusedAssets();
        }

    }

    public IEnumerator LoadObj()
    {
        //1,2是3的依赖ab
        var loader1 = ResMgr.Instance.LoadBundleAsync("shader/inbuildshader.assetbundle");
        var loader2 = ResMgr.Instance.LoadBundleAsync("testassets/cube_mat.assetbundle");
        //
        var loader3 = ResMgr.Instance.LoadBundleAsync("testassets/cube_prefab.assetbundle");

        yield return loader1;
        yield return loader2;
        yield return loader3;

        loader1.assetbundle.LoadAllAssets();
        loader2.assetbundle.LoadAllAssets();
        AssetBundle ab = loader3.assetbundle;

        GameObject go = ab.LoadAsset<GameObject>("Assets/AssetsPackage/TestAssets/Cube.prefab");

        Instantiate(go);

        loader1.Dispose();
        loader2.Dispose();
        loader3.Dispose();
    }


}
