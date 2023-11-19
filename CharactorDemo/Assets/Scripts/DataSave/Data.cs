using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mojiex;

public class Data:BaseSaveInfo
{
    public string Id;
    public float MaxHealth;
    public int CoinNum;
    public int BornfireID;
    public List<int> ItemID;
    public long Time;

    public override void InitDefault()
    {
        Id = "";
        MaxHealth = 100f;
        CoinNum = 0;
        BornfireID = 1;

        ItemID = new List<int>();

        Time = TimeUtils.GetTimeStamp();
    }

    public void CopyFrom(Data data)
    {
        Id = data.Id;
        MaxHealth = data.MaxHealth;
        CoinNum = data.CoinNum;
        BornfireID = data.BornfireID;
        ItemID = data.ItemID;
        Time = data.Time;
    }
}
