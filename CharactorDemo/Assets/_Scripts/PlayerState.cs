#region Header
///Author:Mojiex
///Github:https://github.com/mojiex/Mojiex
///Create Time:2023/3/14
///Framework Description:This framework is developed based on unity2019LTS,Lower unity version may not supported.
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Mojiex
{
    public class PlayerState
    {
        public static PlayerState Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new PlayerState();
                }
                return _inst;
            }
        }
        private static PlayerState _inst;
        private PlayerState()
        {

        }
    }

}