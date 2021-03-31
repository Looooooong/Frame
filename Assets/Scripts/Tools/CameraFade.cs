using UnityEngine;
using System.Collections;

namespace Main 
{	
	public class CameraFade : MonoBehaviour {
	
	    public static CameraFade instance;
	
	    public Material fadeMaterial;
	
	    private Color currentColor = new Color(0f, 0f, 0f, 0f);
	    private Color targetColor = new Color(0f, 0f, 0f, 0f);
	    private Color deltaColor = new Color(0f, 0f, 0f, 0f);
	
	
	    public float fadeTime = 1f;
	    public float fadeInterval = 2.0f;
	    public static void Start(Color newColor, float duration)
	    {
	        if (instance)
	        {
	            instance.StartFade(newColor, duration);
	        }
	    }
	
	    public void StartFade(Color newColor, float duration)
	    {
	        if (duration > 0.0f)
	        {
	            targetColor = newColor;
	            deltaColor = (targetColor - currentColor) / duration;
	        }
	        else
	        {
	            currentColor = newColor;
	        }
	    }
	    /// <summary>
	    /// 颜色渐入渐出
	    /// </summary>
	    /// <param name="turnColor">需要渐入的颜色</param>
	    public virtual void MovingFade(Color turnColor)
	    {
	        FadeIn(turnColor);
	        Invoke("FadeOut", fadeInterval);
	    }
	    
	
	    public virtual void FadeIn(Color color)
	    {
	        Start(color, fadeTime);
	    }
	    
	    public virtual void FadeOutNoTeleport(Color color)
	    {
	        Start(Color.clear, fadeTime);
	    }
	
	    protected virtual void FadeOut()
	    {
	        Start(Color.clear, fadeTime);
	    }
	
	    protected virtual void Awake()
	    {
	        //fadeMaterial = new Material(Shader.Find("Unlit/TransparentColor"));
	        instance = this;
	    }
	
	    protected virtual void OnPostRender()
	    {
	        if (currentColor != targetColor)
	        {
	            if (Mathf.Abs(currentColor.a - targetColor.a) < Mathf.Abs(deltaColor.a) * Time.deltaTime)
	            {
	                currentColor = targetColor;
	                deltaColor = new Color(0, 0, 0, 0);
	            }
	            else
	            {
	                currentColor += deltaColor * Time.deltaTime;
	            }
	        }
	
	        if (currentColor.a > 0 && fadeMaterial)
	        {
	            fadeMaterial.color = currentColor;
	            fadeMaterial.SetPass(0);
	            GL.PushMatrix();
	            GL.LoadOrtho();
	            GL.Color(fadeMaterial.color);
	            GL.Begin(GL.QUADS);
	            GL.Vertex3(0f, 0f, 0.9999f);
	            GL.Vertex3(0f, 1f, 0.9999f);
	            GL.Vertex3(1f, 1f, 0.9999f);
	            GL.Vertex3(1f, 0f, 0.9999f);
	            GL.End();
	            GL.PopMatrix();
	        }
	    }
	}
	
}
