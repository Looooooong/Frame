using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceRecyle : MonoBehaviour
{


    public ResourceType RType;
    public float lifeTime;
    public bool isNeedRecycle = true;
    private AudioSource _audioSrouce;


    private void OnEnable()
    {
        if (_audioSrouce == null) _audioSrouce = GetComponent<AudioSource>();
        if (_audioSrouce.clip)
            lifeTime = _audioSrouce.clip.length + 0.5f;
        if (isNeedRecycle)
            Invoke("Recycle", lifeTime);
    }

    private void Recycle()
    {
        PoolManager.ReturnObjectToPool(RType.ToString(), gameObject);
    }


    private void OnDisable()
    {
        _audioSrouce.clip = null;
    }
}
