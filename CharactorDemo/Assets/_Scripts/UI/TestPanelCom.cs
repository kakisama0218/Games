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

public class TestPanelCom : MonoBehaviour
{
	public UnityEngine.Transform TestPanel;
	public UnityEngine.UI.Image Root;
	public UnityEngine.UI.Text Text;
	public UnityEngine.UI.Button Button;
	public UnityEngine.UI.Text Text_1;

    private void Awake()
    {
		TestPanel = GetComponent<UnityEngine.RectTransform>();
		Root = transform.Find("Root/").GetComponent<UnityEngine.UI.Image>();
		Text = transform.Find("Root/Text/").GetComponent<UnityEngine.UI.Text>();
		Button = transform.Find("Root/Button/").GetComponent<UnityEngine.UI.Button>();
		Text_1 = transform.Find("Root/Button/Text/").GetComponent<UnityEngine.UI.Text>();

    }
}