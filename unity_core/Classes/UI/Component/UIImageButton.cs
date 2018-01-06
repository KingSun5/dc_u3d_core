using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UI图片按钮
/// @author hannibal
/// @time 2016-2-25
/// </summary>
[RequireComponent(typeof(Image))]
public class UIImageButton : UIComponentBase
{
    public enum Status
    {
        Normal,
        Select,
        Disable,
    }
    public Status BtnStatus = Status.Normal;

    public Sprite NormalBtn;
    public Sprite SelectBtn;
    public Sprite DisableBtn;
    public bool m_SetNativeSize = true;

    private Image m_ImgComponent;

    private bool m_IsVisible = true;

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
        this.AddUIEventListener(gameObject, eUIEventType.Down, OnMouseDown);
        this.AddUIEventListener(gameObject, eUIEventType.Up, OnMouseUp);
        this.AddUIEventListener(gameObject, eUIEventType.Exit, OnMouseUp);
    }
    public override void UnRegisterEvent()
    {
        this.RemoveUIEventListener(gameObject, eUIEventType.Down, OnMouseDown);
        this.RemoveUIEventListener(gameObject, eUIEventType.Up, OnMouseUp);
        this.RemoveUIEventListener(gameObject, eUIEventType.Exit, OnMouseUp);
    }

    void OnMouseDown(UIEvent args)
    {
        if (!m_IsVisible) return;
        SetStatus(Status.Select);
    }
    void OnMouseUp(UIEvent args)
    {
        if (!m_IsVisible) return;
        SetStatus(Status.Normal);
    }

    public override void SetVisible(bool is_Show)
    {
        m_IsVisible = is_Show;
        if(!m_IsVisible)
        {
            this.SetStatus(Status.Disable);
        }
        else
        {
            this.SetStatus(Status.Normal);
        }
    }

    public void SetStatus(Status status)
    {
        switch(status)
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

            case Status.Disable:
                if (DisableBtn != null)
                {
                    m_ImgComponent.sprite = DisableBtn;
                    if (m_SetNativeSize) m_ImgComponent.SetNativeSize();
                }
                BtnStatus = status;
                break;
        }
    }
}
