using UnityEngine;
using System.Collections;

/// <summary>
/// 位移变换器
/// @author hannibal
/// @time 2016-2-14
/// </summary>
public class MoveTransformer : Transformer 
{
    private int m_nStartType;
    private float m_fStartX;
    private float m_fStartY;
    private float m_fStartZ;
    private float m_fSpeedX;
    private float m_fSpeedY;
    private float m_fSpeedZ;
    private float m_fTargetX;
    private float m_fTargetY;
    private float m_fTargetZ;
    private bool m_isWorld;
    /// <summary>
    /// 移动到目标点
    /// </summary>
    /// <param name="target">目标对象</param>
    /// <param name="x">目标位置：x</param>
    /// <param name="y">目标位置：y</param>
    /// <param name="time">变换时长</param>
    /// <returns></returns>
    public static MoveTransformer moveTo(GameObject target, float x, float y, float z, float time, bool is_world=false)
    {
        MoveTransformer transformer = new MoveTransformer();
        transformer.m_nStartType = 0;
        transformer.m_fTargetX = x;
        transformer.m_fTargetY = y;
        transformer.m_fTargetZ = z;
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
    public static MoveTransformer moveBy(GameObject target, float relative_x, float relative_y, float relative_z, float time, bool is_world = false)
    {
        Vector3 position = is_world ? target.transform.position : target.transform.localPosition;
        MoveTransformer transformer = new MoveTransformer();
        transformer.m_nStartType = 0;
        transformer.m_fTargetX = position.x + relative_x;
        transformer.m_fTargetY = position.y + relative_y;
        transformer.m_fTargetZ = position.z + relative_z;
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
    public static MoveTransformer moveSpeed(GameObject target, float speedX, float speedY, float speedZ, float time, bool is_world = false)
    {
        MoveTransformer transformer = new MoveTransformer();
        transformer.m_nStartType = 1;
        transformer.m_fSpeedX = speedX;
        transformer.m_fSpeedY = speedY;
        transformer.m_fSpeedZ = speedZ;
        transformer.m_fTransformTime = time;
        transformer.m_isWorld = is_world;
        transformer.target = target;
        return transformer;
    }
    public MoveTransformer()
    {
        m_Type = eTransformerID.Move;
    }
    public override void OnTransformStarted()
    {
        //获得当前对象的缩放
        Vector3 position = m_isWorld ? target.transform.position : target.transform.localPosition;
        m_fStartX = position.x;
        m_fStartY = position.y;
        m_fStartZ = position.z;
        if (m_nStartType == 0)
        {
            m_fSpeedX = (m_fTargetX - position.x) / m_fTransformTime;
            m_fSpeedY = (m_fTargetY - position.y) / m_fTransformTime;
            m_fSpeedZ = (m_fTargetZ - position.z) / m_fTransformTime;
        }
        else if (m_nStartType == 1)
        {
            m_fTargetX = position.x + m_fSpeedX * m_fTransformTime;
            m_fTargetY = position.y + m_fSpeedY * m_fTransformTime;
            m_fTargetZ = position.z + m_fSpeedZ * m_fTransformTime;
        }
        base.OnTransformStarted();
    }
    public override void runTransform(float currTime)
    {
        Vector3 pos;
        if (currTime >= m_fEndTime)
        {
            pos = new Vector3(m_fTargetX, m_fTargetY, m_fTargetZ);
        }
        else
        {
            float timeElapased = currTime - m_fStartTime;
            pos = new Vector3(m_fStartX + m_fSpeedX * timeElapased, m_fStartY + m_fSpeedY * timeElapased, m_fStartZ + m_fSpeedZ * timeElapased);
        }
        if (m_isWorld)
            target.transform.position = pos;
        else
            target.transform.localPosition = pos;
    }
}
