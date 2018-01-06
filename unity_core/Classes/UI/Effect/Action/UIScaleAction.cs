using UnityEngine;
using System.Collections;

/// <summary>
/// 缩放
/// @author hannibal
/// @time 2016-4-8
/// </summary>
public class UIScaleAction : MonoBehaviour
{
    /// <summary>
    /// 缩放到
    /// </summary>
    public float m_ToScale = 0.8f;
    /// <summary>
    /// 缩放速度
    /// </summary>
    public float m_Duration = 1;
    /// <summary>
    /// 是否循环
    /// </summary>
    public bool m_Repeat = true;

    private bool m_Active = false;

    void OnEnable()
    {
        Start();
    }

    void OnDisable()
    {
        Stop(1);
    }

    void OnScaleOut()
    {
        if (!m_Active) return;
        UIEffectTools.ScaleTo(gameObject, m_Duration, OnScaleIn, 1);
    }

    void OnScaleIn()
    {
        if (!m_Active) return;
        UIEffectTools.ScaleTo(gameObject, m_Duration, OnScaleOut, m_ToScale);
    }

    public void Start()
    {
        if (m_Active) return;
        m_Active = true;
        OnScaleIn();
    }

    public void Stop(float alpha)
    {
        if (!m_Active) return;
        m_Active = false;
        UIEffectTools.ScaleStop(gameObject);
        UIEffectTools.ScaleTo(gameObject, 0, null, alpha);
    }
}
