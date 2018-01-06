using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弹簧变换器
/// @author hannibal
/// @time 2018-1-4
/// </summary>
public class SpringTransformer : Transformer
{
    public enum eDir { Down=-1,Up=1}

    /// <summary>
    /// 初始位置
    /// </summary>
    private Vector3 m_InitPosition;
    /// <summary>
    /// 方向
    /// </summary>
    private eDir m_Dir = eDir.Down;
    /// <summary>
    /// 震动速度
    /// </summary>
    private float m_Speed;
    /// <summary>
    /// 振幅
    /// </summary>
    private float m_Altitude;
    private float m_InitAltitude;
    /// <summary>
    /// 振幅衰减速度
    /// </summary>
    private float m_Attenuation;

    /// <summary>
    /// Y轴方向弹簧效果
    /// </summary>
    /// <param name="target"></param>
    /// <param name="init_dir">初始方向</param>
    /// <param name="speed">震动速度</param>
    /// <param name="altitude">振幅</param>
    /// <param name="attenuation">振幅衰减速度</param>
    /// <returns></returns>
    public static SpringTransformer springY(GameObject target, eDir init_dir, float speed, float altitude, float attenuation)
    {
        SpringTransformer transformer = new SpringTransformer();
        transformer.m_InitPosition = target.transform.localPosition;
        transformer.m_Dir = init_dir;
        transformer.m_Speed = speed;
        transformer.m_Altitude = altitude;
        transformer.m_Attenuation = attenuation;
        transformer.m_fTransformTime = float.MaxValue;
        transformer.target = target;
        return transformer;
    }
    public SpringTransformer()
    {
        m_Type = eTransformerID.Spring;
    }
    public override void OnTransformStarted()
    {
        m_InitAltitude = m_Altitude;
        base.OnTransformStarted();
    }
    public override void runTransform(float currTime)
    {
        if (m_Altitude <= 0) return;

        float time = (currTime - m_fStartTime) * m_Speed;
        float angle = time * Mathf.PI * 2;
        float offset_pos = Mathf.Sin(angle) * m_Altitude * (int)m_Dir;
        m_Target.transform.localPosition = m_InitPosition + new Vector3(0,offset_pos,0);

        m_Altitude = m_InitAltitude - (int)((currTime - m_fStartTime) * m_Speed) * m_Attenuation;
        if (m_Altitude <= 0)
        {
            m_Target.transform.localPosition = m_InitPosition;
            this.stop();
        }
    }
}
