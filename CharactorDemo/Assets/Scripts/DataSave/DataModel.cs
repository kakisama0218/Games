using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mojiex;
using System;

public class DataModel
{
    private Data m_data;
    public string Id => m_data.Id;
    public DataModel(Data data)
    {
        m_data = data;

    }
    public float GetMaxHealth()
    {
        return m_data.MaxHealth;
    }
    public void SetMaxHealth(float maxHealth)
    {
        m_data.MaxHealth = maxHealth;
        Save();
    }
    public int GetCoinNum()
    {
        return m_data.CoinNum;
    }
    public void SetCoinNum(int CoinNum)
    {
        m_data.CoinNum = CoinNum;
        Save();
    }public int GetBornFire()
    {
        return m_data.BornfireID;
    }
    public void SetBornFire(int BornfireID)
    {
        m_data.BornfireID = BornfireID;
        Save();
    }
    public bool isGot(int ItemID)
    {
        return m_data.ItemID.Contains(ItemID);
    }
    public void addItem(int itemID)
    {
        if(!isGot(itemID))
        {
            m_data.ItemID.Add(itemID);
        }
    }
    public void removeItem(int itemID)
    {
        m_data.ItemID.Remove(itemID);
    }
    public DateTime GetTime()
    {
        return TimeUtils.GetTime(m_data.Time);
    }
    public void setTime(long Time)
    {
        m_data.Time = Time;
    }
    public void Save()
    {
       // DataStatic.Inst.saveManager.Save(m_data);
    }

    public void CopyFrom(DataModel dataModel)
    {
        m_data.CopyFrom(dataModel.m_data);
    }

    public Data GetData() { return m_data; }
}
