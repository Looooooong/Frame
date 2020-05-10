using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAudioSource : MonoBehaviour {

    private AudioSource m_AS;

    private void Start()
    {
        m_AS = AudioManager.Instance.GetAudioSrouce(AudioClipType.BG_1,transform);
        m_AS.Play();
        Invoke("PlayBG", m_AS.clip.length + 2);
    }

    private void PlayBG()
    {
        m_AS.Stop();
        AudioClipProperty tempClip = AudioManager.Instance.GetClipByType((int)AudioClipType.BG_2);
        m_AS.clip = tempClip._AudioClip;
        m_AS.volume = tempClip._Volume;
        m_AS.pitch = tempClip._Pitch;
        m_AS.priority = tempClip._Priority;
        m_AS.spatialBlend = tempClip._SpatialBlend;
        m_AS.loop = tempClip._Loop;
        m_AS.Play();
    }
}
