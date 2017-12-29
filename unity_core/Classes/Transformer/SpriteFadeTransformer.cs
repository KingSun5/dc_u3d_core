using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sprite渐变动画
/// 1.动态增减的子节点，不受变化影响
/// @author hannibal
/// @time 2017-12-29
/// </summary>
public class SpriteFadeTransformer : Transformer
{
    private enum eFadeType { In, Out };
    private eFadeType m_FadeType;
    private float m_StartAlpha;
    private float m_SpeedAlpha;
    private float m_TargetAlpha;

    private SpriteRenderer[] m_Images = null;
    private TextMesh[] m_Texts = null;

    public static SpriteFadeTransformer FadeIn(GameObject target, float destAlpha, float time)
    {
        destAlpha = Mathf.Clamp(destAlpha, 0, 1);
        SpriteFadeTransformer transformer = new SpriteFadeTransformer();
        transformer.m_FadeType = eFadeType.In;
        transformer.m_TargetAlpha = destAlpha;
        transformer.m_fTransformTime = time;
        transformer.target = target;
        return transformer;
    }
    public static SpriteFadeTransformer FadeOut(GameObject target, float destAlpha, float time)
    {
        destAlpha = Mathf.Clamp(destAlpha, 0, 1);
        SpriteFadeTransformer transformer = new SpriteFadeTransformer();
        transformer.m_FadeType = eFadeType.Out;
        transformer.m_TargetAlpha = destAlpha;
        transformer.m_fTransformTime = time;
        transformer.target = target;
        return transformer;
    }
    public SpriteFadeTransformer()
    {
        m_Type = eTransformerID.SpriteFade;
    }
    public override void OnTransformStarted()
    {
        m_Images = target.GetComponentsInChildren<SpriteRenderer>();
        m_Texts = target.GetComponentsInChildren<TextMesh>();

        if (m_FadeType == eFadeType.In)
        {
            m_StartAlpha = 0f;
            m_SpeedAlpha = (m_TargetAlpha) / m_fTransformTime;
            this.SetAlpha(0);
        }
        else if (m_FadeType == eFadeType.Out)
        {
            m_StartAlpha = 1f;
            m_SpeedAlpha = -(m_StartAlpha - m_TargetAlpha) / m_fTransformTime;
            this.SetAlpha(1);
        }
    }
    public override void runTransform(float currTime)
    {
        if (m_Images == null || m_Images.Length == 0) return;

        float alpha = 1;
        if (currTime >= m_fEndTime)
        {
            alpha = m_TargetAlpha;
        }
        else
        {
            float timeElapased = currTime - m_fStartTime;
            alpha = (m_StartAlpha + m_SpeedAlpha * timeElapased);
        }
        this.SetAlpha(alpha);
	}
    private void SetAlpha(float alpha)
    {
        alpha = Mathf.Clamp(alpha, 0, 1);
        if (m_Images != null && m_Images.Length > 0)
        {
            foreach (SpriteRenderer vRenderer in m_Images)
            {
                if (vRenderer == null) continue;
                vRenderer.color = new Color(vRenderer.color.r, vRenderer.color.g, vRenderer.color.b, alpha);
            }
        }
        if (m_Texts != null && m_Texts.Length > 0)
        {
            foreach (TextMesh vRenderer in m_Texts)
            {
                if (vRenderer == null) continue;
                vRenderer.color = new Color(vRenderer.color.r, vRenderer.color.g, vRenderer.color.b, alpha);
            }
        }
    }
}
