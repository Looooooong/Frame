using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTriggerHelper : MonoBehaviour
{
    
    #region Application

    public Action<bool> OnApplicationFocusEventHandler;
    public Action<bool> OnApplicationPauseEventHandler;
    public Action OnApplicationQuitEventHandler;

    private void OnApplicationFocus(bool focus)
    {
        OnApplicationFocusEventHandler?.Invoke(focus);
    }

    private void OnApplicationPause(bool pause)
    {
        OnApplicationPauseEventHandler?.Invoke(pause);
    }

    private void OnApplicationQuit()
    {
        OnApplicationQuitEventHandler?.Invoke();
    }

    #endregion

    #region Collision3D
    
    public Action<Collision> OnCollisionEnterEventHandler;
    public Action<Collision> OnCollisionExitEventHandler;
    public Action<Collision> OnCollisionStayEventHandler;

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEventHandler?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExitEventHandler?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollisionStayEventHandler?.Invoke(collision);
    }

    #endregion
    
    #region Collision2D

    public Action<Collision2D> OnCollisionEnter2DEventHandler;
    public Action<Collision2D> OnCollisionExit2DEventHandler;
    public Action<Collision2D> OnCollisionStay2DEventHandler;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter2DEventHandler?.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit2DEventHandler?.Invoke(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionStay2DEventHandler?.Invoke(collision);
    }

    #endregion

    #region Enable/Disable/Destroy

    public Action OnDestroyEventHandler;
    public Action OnDisableEventHandler;
    public Action OnEnableEventHandler;
    
    private void OnDestroy()
    {
        OnDestroyEventHandler?.Invoke();
    }

    private void OnDisable()
    {
        OnDisableEventHandler?.Invoke();
    }

    private void OnEnable()
    {
        OnEnableEventHandler?.Invoke();
    }


    #endregion
    
    #region MouseEvent


    public Action OnMouseDownEventHandler;
    public Action OnMouseDragEventHandler;
    public Action OnMouseEnterEventHandler;
    public Action OnMouseExitEventHandler;
    public Action OnMouseOverEventHandler;
    public Action OnMouseUpEventHandler;
    public Action OnMouseUpAsButtonEventHandler;

    private void OnMouseDown()
    {
        OnMouseDownEventHandler?.Invoke();
    }

    private void OnMouseDrag()
    {
        OnMouseDragEventHandler?.Invoke();
    }

    private void OnMouseEnter()
    {
        OnMouseEnterEventHandler?.Invoke();
    }
    private void OnMouseExit()
    {
        OnMouseExitEventHandler?.Invoke();
    }

    private void OnMouseOver()
    {
        OnMouseOverEventHandler?.Invoke();
    }
    private void OnMouseUp()
    {
        OnMouseUpEventHandler?.Invoke();
    }

    private void OnMouseUpAsButton()
    {
        OnMouseUpAsButtonEventHandler?.Invoke();
    }

    #endregion

    #region Trigger3D

    public Action<Collider> OnTriggerEnterEventHandler;
    public Action<Collider> OnTriggerExitEventHandler;
    public Action<Collider> OnTriggerStayEventHandler;


    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEventHandler?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEventHandler?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayEventHandler?.Invoke(other);
    }

    #endregion

    #region Trigger2D

    public Action<Collider2D> OnTriggerEnter2DEventHandler;
    public Action<Collider2D> OnTriggerExit2DEventHandler;
    public Action<Collider2D> OnTriggerStay2DEventHandler;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter2DEventHandler?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit2DEventHandler?.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerStay2DEventHandler?.Invoke(collision);
    }

    #endregion



}


