using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI进度条
/// 1.需要设置image的锚点
/// @author hannibal
/// @time 2016-2-25
/// </summary>
public class UIProgressBar : UIComponentBase
{
    /// <summary>
    /// 进度条方向
    /// </summary>
    public eProgressType m_Type = eProgressType.Horizontal;
    
    /// <summary>
    /// 背景
    /// </summary>
    public Image m_BGImg;
    /// <summary>
    /// 进度条
    /// </summary>
    public Image m_ProgressImg;
    /// <summary>
    /// 进度
    /// </summary>
    public Text  m_HpText;

    [SerializeField]
    protected int m_Value = 1;
    [SerializeField]
    protected int m_TotalValue = 1;

    private Vector3 m_InitScale;
    /// <summary>
    /// 初始文本
    /// 1.如果有填，必须是String.Format格式
    /// 2.如果未填，默认显示hp/hp_max
    /// </summary>
    private string m_InitText = "";

	public override void Awake () 
    {
        m_InitScale = m_ProgressImg.transform.localScale;
        m_InitText = "";
        if (m_HpText != null)
        {
            m_InitText = m_HpText.text;
        }

        this.SetValue(m_Value);
        base.Awake();
	}

    private Vector3 tmpScale;
    public void SetValue(int value)
    {
        if (value < 0 || value > m_TotalValue) return;
        if (m_ProgressImg == null) return;

        m_Value = value;
        switch(m_Type)
        {
            case eProgressType.Horizontal:
                tmpScale = m_InitScale;
                tmpScale.x = (float)m_Value / (float)m_TotalValue;
                m_ProgressImg.transform.localScale = tmpScale;
                break;

            case eProgressType.Vertical:
                tmpScale = m_InitScale;
                tmpScale.y = (float)m_Value / (float)m_TotalValue;
                m_ProgressImg.transform.localScale = tmpScale;
                break;
        }

        if(m_HpText != null)
        {
            string str_hp = m_Value.ToString() + "/" + m_TotalValue.ToString();
            string str_txt = str_hp;
            if(!string.IsNullOrEmpty(m_InitText))
            {
                str_txt = string.Format(m_InitText, str_hp);
            }
            m_HpText.text = str_txt;
        }
    }

    public int Value
    {
        get { return m_Value; }
    }
    public int TotalValue
    {
        get { return m_TotalValue; }
        set 
        {
            if (m_TotalValue <= 0) return;
            m_TotalValue = value; 
        }
    }
}
/// <summary>
/// 进度条类型
/// </summary>
public enum eProgressType
{
    Horizontal,
    Vertical,
}
