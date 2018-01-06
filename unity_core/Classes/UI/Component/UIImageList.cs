using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UI图片列表，同一时刻，只显示一张
/// @author hannibal
/// @time 2016-2-25
/// </summary>
[RequireComponent(typeof(Image))]
public class UIImageList : UIComponentBase
{
    public Sprite[] ImageList;
    public bool m_SetNativeSize = true;

    private Image m_ImgComponent;

    public override void Awake()
    {
        if (ImageList == null || ImageList.Length == 0) Log.Error("没有设置基础数据");
        m_ImgComponent = GetComponent<Image>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }

    public void SetImage(int index)
    {
        if (index < 0 || index >= ImageList.Length)
        {
            m_ImgComponent.sprite = null;
            Log.Warning("图片索引超出范围:" + index);
        }
        else
        {
            m_ImgComponent.sprite = ImageList[index];
            if (m_SetNativeSize) m_ImgComponent.SetNativeSize();
        }
    }
}
