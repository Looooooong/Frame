using UnityEngine;
using System.Collections;

/// <summary>
/// 加载资源类,对外提供加载资源方法
/// </summary>
public class Loader
{
    public static Object LoadObject(string aPathObjectName)
    {
        return Resources.Load(aPathObjectName);
    }
    /// <summary>
    /// 加载Rerouses/Prefabs/文件夹下的Prefabs
    /// </summary>
    /// <param name="aPathGameObjectName"></param>
    /// <returns></returns>
    public static GameObject LoadGameObject(string aPathGameObjectName)
    {
        Object tObject;
        tObject = Resources.Load("Prefabs/" + aPathGameObjectName, typeof(GameObject));

        if (tObject != null)
        {
            GameObject tGameObject = Object.Instantiate(tObject) as GameObject;
            tGameObject.name = aPathGameObjectName.Split('/')[1];
            return tGameObject;
        }
        else return null;
    }
    /// <summary>
    /// 加载Rerouses/Materials/文件夹下的Materials
    /// </summary>
    /// <param name="aPathMaterialName"></param>
    /// <returns></returns>
    public static Material LoadMaterial(string aPathMaterialName)
    {
        return Resources.Load("Materials/" + aPathMaterialName, typeof(Material)) as Material;
    }

    /// <summary>
    /// 加载Rerouses/Textures/文件夹下的Textures
    /// </summary>
    /// <param name="aPathTextureName"></param>
    /// <returns></returns>
    public static Texture LoadTexture(string aPathTextureName)
    {
        return Resources.Load("Textures/" + aPathTextureName, typeof(Texture)) as Texture;
    }

    public static TextAsset LoadTextFile(string aPathTextFileName)
    {
        return Resources.Load(aPathTextFileName, typeof(TextAsset)) as TextAsset;
    }

    public static AudioClip LoadAudio(string anAudioSourceName)
    {
        return Resources.Load("Audio/" + anAudioSourceName, typeof(AudioClip)) as AudioClip;
    }

    public static PhysicMaterial LoadPhysicMaterial(string aPathPhysicMaterialName)
    {
        return Resources.Load("PhysicMaterials/" + aPathPhysicMaterialName, typeof(PhysicMaterial)) as PhysicMaterial;
    }
}
