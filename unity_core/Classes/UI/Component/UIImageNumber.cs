using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(GridLayoutGroup))]

/// <summary>
/// 数字排列组件：需要配合GridLayoutGroup一块使用
/// @author hannibal
/// @time 2015-1-6
/// </summary>
public class UIImageNumber : UIComponentBase
{
    public Sprite[] m_TemplateSprite;
    public string m_TemplateText;

    public bool m_SetNativeSize = true;
    public string m_NumValue = "";
    private List<GameObject> m_NumImage = new List<GameObject>();

    public override void OnEnable()
	{
        SetData(m_NumValue);
        base.OnEnable();
	}
    public override void OnDisable()
	{
        Clear();
        m_NumImage.Clear();
        base.OnDisable();
	}

	public void SetData(string num)
    {
        Clear();
		m_NumValue = num;
        string arr = m_NumValue.ToString();
		for(int i = 0; i < arr.Length; ++i)
		{
            if (m_NumImage.Count <= i )
            {
                GameObject obj = new GameObject();
                obj.AddComponent<Image>();
                m_NumImage.Add(obj);
            }
            Image image = m_NumImage[i].GetComponent<Image>();
            image.transform.SetParent(transform, false);
            image.transform.localScale = Vector3.one;
            image.gameObject.SetActive(true);
            image.sprite = this.GetSpriteByNumber(arr[i]);
            if (m_SetNativeSize) image.SetNativeSize();
		}
	}

    private Sprite GetSpriteByNumber(char num)
    {
        int index = m_TemplateText.IndexOf(num);
        if(index >= 0 && index < m_TemplateSprite.Length)
        {
            return m_TemplateSprite[index];
        }
        return null;
    }

	private void Clear()
	{
        for (int i = 0; i < m_NumImage.Count; i++)
        {
            if (m_NumImage[i] == null)
                continue;
            m_NumImage[i].gameObject.SetActive(false);
        }
	}
}
