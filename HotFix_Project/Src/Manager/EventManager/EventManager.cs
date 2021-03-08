using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotFix_Project
{

    /// <summary>
    /// 注册事件的类型
    /// </summary>
    public enum MG_EventType
    {
        ZERO,
        ONE,
        TWO,
        THREE,


        END,
    }


    public class EventManager
    {
        private static EventManager instance;
        public static EventManager Instance
        {
            get
            {
                if (instance == null) instance = new EventManager();
                return instance;
            }
        }

        private EventManager()
        {
            m_ListenersDic = new Dictionary<MG_EventType, Delegate>();
        }



        /// <summary>
        /// 事件的监听池，存放所有需要监听的事件
        /// </summary>
        private Dictionary<MG_EventType, Delegate> m_ListenersDic;

        /// <summary>
        /// 在添加事件时,校验事件的合法性
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应的事件委托</param>
        private void CheckEventWhenAdding(MG_EventType eType, Delegate callBack)
        {
            if (!m_ListenersDic.ContainsKey(eType))
            {
                m_ListenersDic.Add(eType, null);
            }
            Delegate tempD = m_ListenersDic[eType];
            if (tempD != null && tempD.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("error"));
            }
        }

        /// <summary>
        /// 移除事件时,校验事件的合法性
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应的事件委托</param>
        private void CheckEventWhenRemoving(MG_EventType eType, Delegate callBack)
        {
            if (!m_ListenersDic.ContainsKey(eType))
            {
                throw new Exception(string.Format("移除监听事件错误,事件池中不包含该类型的事件类型：{0}", eType));
            }
            Delegate tempD = m_ListenersDic[eType];
            if (tempD == null)
            {
                throw new Exception(string.Format("移除监听事件错误,该事件类型{0}中未注册对应的事件.", eType));
            }
            else if (tempD.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除监听事件错误,移除的事件类型{0}和当前类型事件类型不一致{1}", callBack.GetType(), tempD.GetType()));
            }
        }


        /// <summary>
        /// 添加事件（0个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应事件委托</param>
        public void AddEvent(MG_EventType eType, Action callBack)
        {
            CheckEventWhenAdding(eType, callBack);
            m_ListenersDic[eType] = (Action)m_ListenersDic[eType] + callBack;
        }


        /// <summary>
        /// 添加事件（1个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应事件委托</param>
        public void AddEvent<T>(MG_EventType eType, Action<T> callBack)
        {
            CheckEventWhenAdding(eType, callBack);
            m_ListenersDic[eType] = (Action<T>)m_ListenersDic[eType] + callBack;
        }


        /// <summary>
        /// 添加事件（2个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应事件委托</param>
        public void AddEvent<T, X>(MG_EventType eType, Action<T, X> callBack)
        {
            CheckEventWhenAdding(eType, callBack);
            m_ListenersDic[eType] = (Action<T, X>)m_ListenersDic[eType] + callBack;

        }


        /// <summary>
        /// 添加事件（3个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应事件委托</param>
        public void AddEvent<T, X, Y>(MG_EventType eType, Action<T, X, Y> callBack)
        {
            CheckEventWhenAdding(eType, callBack);
            m_ListenersDic[eType] = (Action<T, X, Y>)m_ListenersDic[eType] + callBack;
        }


        /// <summary>
        /// 移除事件（0个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应事件委托</param>
        public void RemoveEvent(MG_EventType eType, Action callBack)
        {
            CheckEventWhenRemoving(eType, callBack);
            m_ListenersDic[eType] = (Action)m_ListenersDic[eType] - callBack;
            if (m_ListenersDic[eType] == null)
            {
                m_ListenersDic.Remove(eType);
            }
        }


        /// <summary>
        /// 移除事件（1个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应事件委托</param>
        public void RemoveEvent<T>(MG_EventType eType, Action<T> callBack)
        {
            CheckEventWhenRemoving(eType, callBack);
            m_ListenersDic[eType] = (Action<T>)m_ListenersDic[eType] - callBack;
            if (m_ListenersDic[eType] == null)
            {
                m_ListenersDic.Remove(eType);
            }
        }


        /// <summary>
        /// 移除事件（2个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应事件委托</param>
        public void RemoveEvent<T, X>(MG_EventType eType, Action<T, X> callBack)
        {
            CheckEventWhenRemoving(eType, callBack);
            m_ListenersDic[eType] = (Action<T, X>)m_ListenersDic[eType] - callBack;
            if (m_ListenersDic[eType] == null)
            {
                m_ListenersDic.Remove(eType);
            }
        }


        /// <summary>
        /// 移除事件（3个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        /// <param name="callBack">对应事件委托</param>
        public void RemoveEvent<T, X, Y>(MG_EventType eType, Action<T, X, Y> callBack)
        {
            CheckEventWhenRemoving(eType, callBack);
            m_ListenersDic[eType] = (Action<T, X, Y>)m_ListenersDic[eType] - callBack;
            if (m_ListenersDic[eType] == null)
            {
                m_ListenersDic.Remove(eType);
            }
        }


        /// <summary>
        /// 根据事件,类型调度触发对应的事件（0个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        public void DispatchEvent(MG_EventType eType)
        {
            Delegate tempD;
            if (m_ListenersDic.TryGetValue(eType, out tempD))
            {
                Action callBack = tempD as Action;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    Debug.LogWarningFormat("该类型{0}未添加相应的事件委托", eType);
                }
            }
            else
            {
                throw new Exception(string.Format("调度事件错误：不包含该类型的事件委托：{0}", eType));
            }
        }

        /// <summary>
        /// 根据事件,类型调度触发对应的事件（1个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        public void DispatchEvent<T>(MG_EventType eType, T arg0)
        {
            Delegate tempD;
            if (m_ListenersDic.TryGetValue(eType, out tempD))
            {
                Action<T> callBack = tempD as Action<T>;
                if (callBack != null)
                {
                    callBack(arg0);
                }
                else
                {
                    Debug.LogWarningFormat("该类型{0}未添加相应的事件委托", eType);
                }
            }
            else
            {
                throw new Exception(string.Format("调度事件错误：不包含该类型的事件委托：{0}", eType));
            }
        }


        /// <summary>
        /// 根据事件,类型调度触发对应的事件（2个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        public void DispatchEvent<T, X>(MG_EventType eType, T arg0, X arg1)
        {
            Delegate tempD;
            if (m_ListenersDic.TryGetValue(eType, out tempD))
            {
                Action<T, X> callBack = tempD as Action<T, X>;
                if (callBack != null)
                {
                    callBack(arg0, arg1);
                }
                else
                {
                    Debug.LogWarningFormat("该类型{0}未添加相应的事件委托", eType);
                }
            }
            else
            {
                throw new Exception(string.Format("调度事件错误：不包含该类型的事件委托：{0}", eType));
            }
        }


        /// <summary>
        /// 根据事件,类型调度触发对应的事件（3个参数事件）
        /// </summary>
        /// <param name="eType">事件类型</param>
        public void DispatchEvent<T, X, Y>(MG_EventType eType, T arg0, X arg1, Y arg2)
        {
            Delegate tempD;
            if (m_ListenersDic.TryGetValue(eType, out tempD))
            {
                Action<T, X, Y> callBack = tempD as Action<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg0, arg1, arg2);
                }
                else
                {
                    Debug.LogWarningFormat("该类型{0}未添加相应的事件委托", eType);
                }
            }
            else
            {
                throw new Exception(string.Format("调度事件错误：不包含该类型的事件委托：{0}", eType));
            }
        }


        public void RemoveAllEvent()
        {
            m_ListenersDic.Clear();
        }

    }
}
