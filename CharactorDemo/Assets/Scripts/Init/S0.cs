#region Header
///Author:Mojiex
///Github:https://github.com/mojiex/Mojiex
///Create Time:2023/3/6
///Framework Description:This framework is developed based on unity2019LTS,Lower unity version may not supported.
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Mojiex
{
    public class S0 : MonoBehaviour
    {
	    private void Awake()
        {
            Mgr.Init(OnInitFinish);
        }

        private void OnInitFinish()
        {
            Mgr.sceneMgr.LoadScene("SampleScene",null,()=> {
                //到场景之后执行的第一个方法
                Mgr.uiMgr.Add<TestPanel>();
            });
            //EventSystem<GameEvents>.AddListener(GameEvents.SaveData, SaveData);
            //EventSystem<GameEvents>.RemoveListener(GameEvents.SaveData, SaveData);
            //EventSystem<GameEvents>.Trigger(GameEvents.SaveData, 1, 1, 1);
        }

        private void SaveData(object[] obj)
        {
            throw new NotImplementedException();
        }
    }
}