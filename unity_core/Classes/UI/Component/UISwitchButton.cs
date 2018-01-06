using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UI状态按钮
/// @author hannibal
/// @time 2016-2-25
/// </summary>
[RequireComponent(typeof(Image))]
public class UISwitchButton : UIComponentBase
{
    public enum Status
    {
        Normal,
        Select,
    }
    public bool AutoSwitch = true;  //是否自动切换状态
    public Status BtnStatus = Status.Normal;
    
    public Sprite NormalBtn;
    public Sprite SelectBtn;
    public bool m_SetNativeSize = true;

    private Image m_ImgComponent;

    public override void Awake()
    {
        if (NormalBtn == null) Log.Error("没有设置按钮基础状态");
        m_ImgComponent = GetComponent<Image>();
    }

    public override void OnEnable()
    {
        SetStatus(BtnStatus);
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void RegisterEvent()
    {
        this.AddUIEventListener(gameObject, eUIEventType.Click, OnClick);
    }
    public override void UnRegisterEvent()
    {
        this.RemoveUIEventListener(gameObject, eUIEventType.Click, OnClick);
    }

    void OnClick(UIEvent args)
    {
        if (!AutoSwitch) return;
        switch (BtnStatus)
        {
            case Status.Normal:
                SetStatus(Status.Select);
                break;

            case Status.Select:
                SetStatus(Status.Normal);
                break;
        }
    }

    public void SetStatus(Status status)
    {
        switch (status)
        {
            case Status.Normal:
                if (NormalBtn != null)
                {
                    m_ImgComponent.sprite = NormalBtn;
                    if (m_SetNativeSize) m_ImgComponent.SetNativeSize();
                }
                BtnStatus = status;
                break;

            case Status.Select:
                if (SelectBtn != null)
                {
                    m_ImgComponent.sprite = SelectBtn;
                    if (m_SetNativeSize) m_ImgComponent.SetNativeSize();
                }
                BtnStatus = status;
                break;
        }
    }
}
