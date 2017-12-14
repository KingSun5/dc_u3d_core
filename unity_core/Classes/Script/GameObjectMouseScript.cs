using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鼠标事件
/// @author hannibal
/// @time 2017-12-4
/// </summary>
public class GameObjectMouseScript : MonoBehaviour 
{
    public bool m_UpwardsMessage = true;
    public bool m_DownwardsMessage = false;
    public string m_Param = "";

    /// <summary>
    /// 当用户在GUIElement或Collider上按下鼠标按钮时，OnMouseDown被调用。
    /// </summary>
    void OnMouseDown()
    {
        if (m_DownwardsMessage) this.BroadcastMessage("OnMouseDownEvt", m_Param, SendMessageOptions.DontRequireReceiver);
        if (m_UpwardsMessage) this.SendMessageUpwards("OnMouseDownEvt", m_Param, SendMessageOptions.DontRequireReceiver);
    }
    /// <summary>
    /// 在用户释放鼠标按钮时被调用。
    /// </summary>
    void OnMouseUp()
    {
        if (m_DownwardsMessage) this.BroadcastMessage("OnMouseUpEvt", m_Param, SendMessageOptions.DontRequireReceiver);
        if (m_UpwardsMessage) this.SendMessageUpwards("OnMouseUpEvt", m_Param, SendMessageOptions.DontRequireReceiver);
    }
    /// <summary>
    /// 仅在按下和松开鼠标通过相同的GUIElement或Collider时触发。
    /// </summary>
    void OnMouseUpAsButton()
    {
        if (m_DownwardsMessage) this.BroadcastMessage("OnMouseUpAsButtonEvt", m_Param, SendMessageOptions.DontRequireReceiver);
        if (m_UpwardsMessage) this.SendMessageUpwards("OnMouseUpAsButtonEvt", m_Param, SendMessageOptions.DontRequireReceiver);
    }
    /// <summary>
    /// 当鼠标进入GUIElement或Collider时调用。
    /// </summary>
    void OnMouseEnter()
    {
        if (m_DownwardsMessage) this.BroadcastMessage("OnMouseEnterEvt", m_Param, SendMessageOptions.DontRequireReceiver);
        if (m_UpwardsMessage) this.SendMessageUpwards("OnMouseEnterEvt", m_Param, SendMessageOptions.DontRequireReceiver);
    }
    /// <summary>
    /// 当鼠标不再在GUIElement或Collider上时调用。
    /// </summary>
    void OnMouseExit()
    {
        if (m_DownwardsMessage) this.BroadcastMessage("OnMouseExitEvt", m_Param, SendMessageOptions.DontRequireReceiver);
        if (m_UpwardsMessage) this.SendMessageUpwards("OnMouseExitEvt", m_Param, SendMessageOptions.DontRequireReceiver);
    }
    /// <summary>
    /// 当鼠标悬停在GUIElement或Collider上时调用每一帧。
    /// </summary>
    void OnMouseOver()
    {
        if (m_DownwardsMessage) this.BroadcastMessage("OnMouseOverEvt", m_Param, SendMessageOptions.DontRequireReceiver);
        if (m_UpwardsMessage) this.SendMessageUpwards("OnMouseOverEvt", m_Param, SendMessageOptions.DontRequireReceiver);
    }
}
