using UnityEngine;
using System.Collections;

/// <summary>
/// 位移变换器
/// @author hannibal
/// @time 2016-2-14
/// </summary>
public class Move2DTransformer : Transformer
{
    private int m_nStartType;
    private float m_fStartX;
    private float m_fStartY;
    private float m_fSpeedX;
    private float m_fSpeedY;
    private float m_fTargetX;
    private float m_fTargetY;
    private bool m_isWorld;

    /// <summary>
    /// 移动到目标点
    /// </summary>
    /// <param name="target">目标对象</param>
    /// <param name="x">目标位置：x</param>
    /// <param name="y">目标位置：y</param>
    /// <param name="time">变换时长</param>
    /// <returns></returns>
    public static Move2DTransformer moveTo(GameObject target, float x, float y, float time, bool is_world = false)
    {
        Move2DTransformer transformer = new Move2DTransformer();
        transformer.m_nStartType = 0;
        transformer.m_fTargetX = x;
        transformer.m_fTargetY = y;
        transformer.m_fTransformTime = time;
        transformer.m_isWorld = is_world;
        transformer.target = target;
        return transformer;
    }
    /// <summary>
    /// 基于当前点相对移动
    /// </summary>
    /// <param name="target">目标对象</param>
    /// <param name="relative_x">x方向移动量</param>
    /// <param name="relative_y">y方向移动量</param>
    /// <param name="time">变换时长</param>
    /// <returns></returns>
    public static Move2DTransformer moveBy(GameObject target, float relative_x, float relative_y, float time, bool is_world = false)
    {
        Vector3 position = target.transform.localPosition;
        Move2DTransformer transformer = new Move2DTransformer();
        transformer.m_nStartType = 0;
        transformer.m_fTargetX = position.x + relative_x;
        transformer.m_fTargetY = position.y + relative_y;
        transformer.m_fTransformTime = time;
        transformer.m_isWorld = is_world;
        transformer.target = target;
        return transformer;
    }
    /// <summary>
    /// 基于当前点移动：按速度
    /// </summary>
    /// <param name="target">目标对象</param>
    /// <param name="speedX">x方向速度</param>
    /// <param name="speedY">y方向速度</param>
    /// <param name="time">变换时长</param>
    /// <returns></returns>
    public static Move2DTransformer moveSpeed(GameObject target, float speedX, float speedY, float time, bool is_world = false)
    {
        Move2DTransformer transformer = new Move2DTransformer();
        transformer.m_nStartType = 1;
        transformer.m_fSpeedX = speedX;
        transformer.m_fSpeedY = speedY;
        transformer.m_fTransformTime = time;
        transformer.m_isWorld = is_world;
        transformer.target = target;
        return transformer;
    }
    public Move2DTransformer()
    {
        m_Type = eTransformerID.Move2D;
    }
    public override void OnTransformStarted()
    {
        //获得当前对象的缩放
        Vector3 position = m_isWorld ? target.transform.position : target.transform.localPosition;
        m_fStartX = position.x;
        m_fStartY = position.y;
        if (m_nStartType == 0)
        {
            m_fSpeedX = (m_fTargetX - position.x) / m_fTransformTime;
            m_fSpeedY = (m_fTargetY - position.y) / m_fTransformTime;
        }
        else if (m_nStartType == 1)
        {
            m_fTargetX = position.x + m_fSpeedX * m_fTransformTime;
            m_fTargetY = position.y + m_fSpeedY * m_fTransformTime;
        }
        base.OnTransformStarted();
    }
    public override void runTransform(float currTime)
    {
        Vector3 pos = new Vector3();
        if (currTime >= m_fEndTime)
        {
            pos.x = m_fTargetX;
            pos.y = m_fTargetY;
        }
        else
        {
            float timeElapased = currTime - m_fStartTime;
            pos.x = m_fStartX + m_fSpeedX * timeElapased;
            pos.y = m_fStartY + m_fSpeedY * timeElapased;
        }
        if (m_isWorld)
            target.transform.position = new Vector3(pos.x, pos.y, target.transform.position.z);
        else
            target.transform.localPosition = new Vector3(pos.x, pos.y, target.transform.localPosition.z);
    }
}
