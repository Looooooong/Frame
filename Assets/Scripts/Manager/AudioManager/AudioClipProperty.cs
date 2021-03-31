using UnityEngine.Audio;
using UnityEngine;

namespace Main 
{	
	[System.Serializable]
	public class AudioClipProperty
	{
	    public AudioClip _AudioClip;
	    public AudioClipType _ClipType;
	    [Range(0, 256)]
	    public int _Priority;
	    [Range(0, 1)]
	    public float _Volume;
	    [Range(0, 1)]
	    public float _Pitch;
	    [Range(0, 1)]
	    public float _SpatialBlend;
	    public bool _Loop;
	    public bool doNotSetDefaultValue;
	
	    public float clipTime { get { return _AudioClip.length; } }
	}
}
