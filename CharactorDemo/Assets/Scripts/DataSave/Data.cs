using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mojiex;

public class Data:BaseSaveInfo
{
    public float MaxHealth;
    public int CoinNum;
    public int BornfireID;
    public List<int> ItemID;
    public long Time;

    public override void InitDefault()
    {
        MaxHealth = 100f;
        CoinNum = 0;
        BornfireID = 1;

        ItemID = new List<int>();

        Time = 1;
        //ItemID.Add(1);
    }
}
