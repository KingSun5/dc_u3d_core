﻿using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// UI动画效果工具类：在本地坐标系下移动
/// @author hannibal
/// @time 2016-12-2
/// </summary>
public class UIMoveTargetAction : MonoBehaviour
{
    /// <summary>
    /// 移动到
    /// </summary>
    public Vector2 m_TargetPos = Vector2.zero;
    /// <summary>
    /// 速度
    /// </summary>
    public float m_Duration = 1;
    /// <summary>
    /// 循环次数
    /// </summary>
    public int m_LoopCount = -1;
    public LoopType m_LoopType = LoopType.Yoyo;

    private bool m_Active = false;
    private Vector3 m_InitPos = Vector3.zero;

    void Awake()
    {
        m_InitPos = transform.localPosition;
    }

    void OnEnable()
    {
        transform.localPosition = m_InitPos;
        Start();
    }

    void OnDisable()
    {
        Stop();
    }

    void OnMoveTo()
    {
        if (!m_Active) return;
        Tween tween = gameObject.transform.DOLocalMove(new Vector3(m_TargetPos.x, m_TargetPos.y, m_InitPos.z), m_Duration);
        tween.SetLoops(m_LoopCount, m_LoopType);
    }

    public void Start()
    {
        if (m_Active) return;
        m_Active = true;
        OnMoveTo();
    }

    public void Stop()
    {
        if (!m_Active) return;
        m_Active = false;
        UIEffectTools.MoveStop(gameObject);
    }
}
