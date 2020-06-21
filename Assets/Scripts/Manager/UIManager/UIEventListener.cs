using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public delegate void PointerEventHandler(PointerEventData eventData);

/// <summary>
/// UI事件监听类: 管理所有UGUI事件,提供事件参数
/// 附加到需要交互的UI上,监听用户操作
/// 类似于EventTrigger
/// </summary>
public class UIEventListener : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
{
    public event PointerEventHandler PointerClick;
    public event PointerEventHandler PointerUp;
    public event PointerEventHandler PointerDown;



    public static UIEventListener GetListener(Transform tf)
    {
        UIEventListener uiEvent = tf.GetComponent<UIEventListener>();
        if (uiEvent == null) uiEvent = tf.gameObject.AddComponent<UIEventListener>();
        return uiEvent;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PointerClick != null) PointerClick(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointerDown != null) PointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (PointerUp != null) PointerUp(eventData);
    }

}
