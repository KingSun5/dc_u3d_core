using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件控制器
/// @author hannibal
/// @time 2016-9-18
/// </summary>
public class EventDispatcher
{
    public delegate void RegistFunction(GameEvent evt);
    private Dictionary<string, RegistFunction> m_dispathcerMap;

    public EventDispatcher()
    {
        m_dispathcerMap = new Dictionary<string, RegistFunction>();
    }

    public void AddEventListener(string EventID, RegistFunction pFunction)
    {
        if (!m_dispathcerMap.ContainsKey(EventID))
        {
            m_dispathcerMap.Add(EventID, pFunction);
        }
        else
        {
            m_dispathcerMap[EventID] += pFunction;
        }
    }
    public void RemoveEventListener(string EventID, RegistFunction pFunction)
    {
        if (m_dispathcerMap.ContainsKey(EventID))
        {
            m_dispathcerMap[EventID] -= pFunction;
        }
    }
    public void TriggerEvent(string EventID, GameEvent info)
    {
        info.type = EventID;
        if (m_dispathcerMap.ContainsKey(EventID) && m_dispathcerMap[EventID] != null)
        {
            m_dispathcerMap[EventID](info);
        }
    }
    public void TriggerEvent(string EventID, params object[] list)
    {
        GameEvent info = new GameEvent();
        info.Init(list);
        info.type = EventID;
        if (m_dispathcerMap.ContainsKey(EventID) && m_dispathcerMap[EventID] != null)
        {
            m_dispathcerMap[EventID](info);
        }
    }
    public void Cleanup()
    {
        m_dispathcerMap.Clear();
    }
}