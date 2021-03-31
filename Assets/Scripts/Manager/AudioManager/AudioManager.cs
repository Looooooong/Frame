using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main 
{	
	
	
	public class AudioManager : MonoBehaviour {
	
	
	    private static AudioManager instance;
	    public static AudioManager Instance
	    {
	        get
	        {
	            return instance;
	        }
	    }
	
	    public bool isMute;                  //是否静音
	    public int priority = 128;           //默认优先级
	    public float volume = 1;               //默认音量
	    public float pitch = 1;                //默认播放速度
	    public float spatialBlend = 1;         //默认立体音效
	    public AudioClipProperty[] clips;    //声音文件
	    public Dictionary<int, AudioClipProperty> AudioClipDic = new Dictionary<int, AudioClipProperty>();
	
	
	    private GameObject _CurAudioPlayObj; //当前挂载声音播放源的游戏对象
	    private AudioSource _CurAudioSource; //当前使用的声音播放源
	    private AudioClipProperty _CurAudioClipProperty; //当前使用的音频文件
	
	    private void Awake()
	    {
	        instance = this;
	
	        InitAudioClip();
	    }
	
	    private void Start()
	    {
	
	    }
	
	    /// <summary>
	    /// 初始化所有的Clip的参数信息
	    /// </summary>
	    private void InitAudioClip()
	    {
	        for (int i = 0; i < clips.Length; i++)
	        {
	            AudioClipDic.Add((int)clips[i]._ClipType, clips[i]);
	            if (clips[i].doNotSetDefaultValue) continue;
	            clips[i]._Priority = priority;
	            clips[i]._Volume = volume;
	            clips[i]._Pitch = pitch;
	            clips[i]._SpatialBlend = spatialBlend;
	        }
	    }
	
	
	    /// <summary>
	    /// 直接播放声音
	    /// </summary>
	    /// <param name="type">音效的类型</param>
	    /// <param name="pos">播放的位置信息</param>
	    public void PlayAudioClip(AudioClipType type,Vector3 pos)
	    {
	        if (isMute) return;
	        _CurAudioClipProperty =  GetClipByType((int)type);
	        if (_CurAudioClipProperty != null)
	        {
	            _CurAudioPlayObj = PoolManager.GetObjectFromPool(ResourceType.AS.ToString());
	            _CurAudioPlayObj.transform.position = pos;
	
	            //赋值声音的播放参数
	            _CurAudioSource = _CurAudioPlayObj.GetComponent<AudioSource>();
	            _CurAudioSource.clip = _CurAudioClipProperty._AudioClip;
	            _CurAudioSource.priority = _CurAudioClipProperty._Priority;
	            _CurAudioSource.volume = _CurAudioClipProperty._Volume;
	            _CurAudioSource.pitch = _CurAudioClipProperty._Pitch;
	            _CurAudioSource.spatialBlend = _CurAudioClipProperty._SpatialBlend;
	            _CurAudioPlayObj.SetActive(true);
	            _CurAudioSource.Play();
	        }
	        //for (int i = 0; i < clips.Length; i++)
	        //{
	        //    if(clips[i]._ClipType == type)
	        //    {
	        //        _CurAudioPlayObj = PoolManager.GetObjectFromPool(ResourceType.AS.ToString());
	        //        _CurAudioPlayObj.transform.position = pos;
	
	        //        //赋值声音的播放参数
	        //        _CurAudioSource = _CurAudioPlayObj.GetComponent<AudioSource>();
	        //        _CurAudioSource.clip = clips[i]._AudioClip;
	        //        _CurAudioSource.priority = clips[i]._Priority;
	        //        _CurAudioSource.volume = clips[i]._Volume;
	        //        _CurAudioSource.pitch = clips[i]._Pitch;
	        //        _CurAudioSource.spatialBlend = clips[i]._SpatialBlend;
	        //        _CurAudioPlayObj.SetActive(true);
	        //        _CurAudioSource.Play();
	        //        break;
	        //    }
	        //}
	    }
	    
	    /// <summary>
	    /// 播放传入的audiosource中的音效
	    /// </summary>
	    /// <param name="audioSource"></param>
	    public void PlayAudioByAudioSource(AudioSource audioSource)
	    {
	        if (isMute) return;
	        audioSource.Play();
	    }
	
	
	    /// <summary>
	    /// 获取一个声音播放器不会还回到池子中
	    /// </summary>
	    /// <param name="type">需要播放的声音</param>
	    /// <param name="t">使用的声音播放器的Transform</param>
	    /// <returns></returns>
	    public AudioSource GetAudioSrouce(AudioClipType type ,Transform t = null)
	    {
	
	        _CurAudioPlayObj = PoolManager.GetObjectFromPool(ResourceType.AS.ToString());
	        _CurAudioPlayObj.transform.parent = t;
	        _CurAudioPlayObj.transform.localPosition = Vector3.zero;
	        _CurAudioPlayObj.GetComponent<AudioSourceRecyle>().isNeedRecycle = false;
	        _CurAudioSource = _CurAudioPlayObj.GetComponent<AudioSource>();
	        _CurAudioClipProperty = GetClipByType((int)type);
	        if(_CurAudioClipProperty != null)
	        {
	            _CurAudioSource.clip = _CurAudioClipProperty._AudioClip;
	            _CurAudioSource.volume = _CurAudioClipProperty._Volume;
	            _CurAudioSource.pitch = _CurAudioClipProperty._Pitch;
	            _CurAudioSource.priority = _CurAudioClipProperty._Priority;
	            _CurAudioSource.spatialBlend = _CurAudioClipProperty._SpatialBlend;
	            _CurAudioSource.loop = _CurAudioClipProperty._Loop;
	        }
	
	        _CurAudioPlayObj.SetActive(true);
	        return _CurAudioSource;
	        
	    }
	
	    /// <summary>
	    /// 获取一个声音播放器不会还回到池子中
	    /// </summary>
	    /// <param name="clip">音效资源</param>
	    /// <param name="t">父对象</param>
	    /// <returns></returns>
	    public AudioSource GetAudioSrouce(AudioClip clip, Transform t = null)
	    {
	
	        _CurAudioPlayObj = PoolManager.GetObjectFromPool(ResourceType.AS.ToString());
	        _CurAudioPlayObj.transform.parent = t;
	        _CurAudioPlayObj.transform.localPosition = Vector3.zero;
	        _CurAudioPlayObj.GetComponent<AudioSourceRecyle>().isNeedRecycle = false;
	        _CurAudioSource = _CurAudioPlayObj.GetComponent<AudioSource>();
	        _CurAudioSource.clip = clip;
	        _CurAudioSource.pitch = Random.Range(0.95f, 1f);
	        _CurAudioSource.volume = Random.Range(0.95f, 1f);
	        _CurAudioSource.loop = true;
	        _CurAudioSource.minDistance = 50f;
	        _CurAudioSource.spatialBlend  = 1;
	        _CurAudioPlayObj.SetActive(true);
	        return _CurAudioSource;
	
	
	    }
	
	    /// <summary>
	    /// 根据类型获取对应的音频文件
	    /// </summary>
	    /// <param name="type"></param>
	    /// <returns></returns>
	    public AudioClipProperty GetClipByType(int type)
	    {
	        AudioClipProperty audioTemp;
	        if(AudioClipDic.TryGetValue(type,out audioTemp))
	        {
	            return audioTemp;
	        }
	        //for (int i = 0; i < clips.Length; i++)
	        //{
	        //    if (clips[i]._ClipType == type)
	        //        return clips[i];
	        //}
	        return null;
	    }
	    
	}
}
