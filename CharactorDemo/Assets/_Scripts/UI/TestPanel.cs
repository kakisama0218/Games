#region Header
///Author:Mojiex
///Github:https://github.com/mojiex/Mojiex
///Create Time:2023/3/14
///Framework Description:This framework is developed based on unity2019LTS,Lower unity version may not supported.
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mojiex;

public class TestPanel : UIObject
{
    private TestPanelCom m_panel;
    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        m_panel = m_go.GetComponent <TestPanelCom>();
        m_panel.Text.text = "你好";
        m_panel.Button.onClick.AddListener(CloseText);
    }
    protected void CloseText()
    {
        Mgr.uiMgr.Close<TestPanel>();
    }

    public override void Close()
    {
        base.Close();
    }
}