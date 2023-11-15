﻿#region Header
///Author:Mojiex
///Github:https://github.com/mojiex/Mojiex
///Create Time:2022/9/17
///Framework Description:This framework is developed based on unity2019LTS,Lower unity version may not supported.
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Mojiex.Ease
{
    public class QuintOutIn : EaseBase
    {
        private static readonly QuintIn QuintIn = new QuintIn();
        private static readonly QuintOut QuintOut = new QuintOut();
        public override Color GetEasedValue(Color start, Color end, float step)
        {
            step = Mathf.Clamp01(step);
            if (step < .5f)
                return QuintOut.GetEasedValue(start, start + (end - start) / 2, step * 2);
            return QuintIn.GetEasedValue(start + (end - start) / 2, end, step * 2 - 1);
        }

        public override float GetEasedValue(float start, float end, float step)
        {
            step = Mathf.Clamp01(step);
            if (step < .5f)
                return QuintOut.GetEasedValue(start, start + (end - start) / 2, step * 2);
            return QuintIn.GetEasedValue(start + (end - start) / 2, end, step * 2 - 1);
        }

        public override Vector2 GetEasedValue(Vector2 start, Vector2 end, float step)
        {
            step = Mathf.Clamp01(step);
            if (step < .5f)
                return QuintOut.GetEasedValue(start, start + (end - start) / 2, step * 2);
            return QuintIn.GetEasedValue(start + (end - start) / 2, end, step * 2 - 1);
        }

        public override Vector3 GetEasedValue(Vector3 start, Vector3 end, float step)
        {
            step = Mathf.Clamp01(step);
            if (step < .5f)
                return QuintOut.GetEasedValue(start, start + (end - start) / 2, step * 2);
            return QuintIn.GetEasedValue(start + (end - start) / 2, end, step * 2 - 1);
        }
    }
}