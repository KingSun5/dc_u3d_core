using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using DG.Tweening;

/// <summary>
/// 缩放
/// @author hannibal
/// @time 2016-3-26
/// </summary>
public class UIPanelScale : UIPanelAnimation
{
    public Vector2 m_FromScale;
    public Vector2 m_ToScale;

    public override void Awake()
    {
    }

    public override void Start()
    {
    }

    private float m_CurTick;
    public override void Update()
    {
        if (m_CurTick <= Time.time && m_CurTick != -1.0f)
        {
            PlayForward();
            m_CurTick = -1.0f;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        gameObject.transform.localScale = new Vector3(m_FromScale.x, m_FromScale.y, 1);
        m_CurTick = Time.time + m_Delay;
    }

    public override void OnDisable()
    {
        Reset();
        base.OnDisable();
    }

    public void Reset()
    {
        m_CurTick = Time.time + m_Delay;
        gameObject.transform.localScale = new Vector3(m_FromScale.x, m_FromScale.y, 1);
    }

    public void PlayForward()
    {
        gameObject.transform.DOScale(new Vector3(m_ToScale.x, m_ToScale.y, 1), m_Duration).SetEase(m_easeType);
    }
}

