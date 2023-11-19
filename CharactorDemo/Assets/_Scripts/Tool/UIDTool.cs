using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIDTool
{
    public static string GetUID()
    {
        return Guid.NewGuid().ToString("N");
    }
}
