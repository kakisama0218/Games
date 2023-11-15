using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mojiex;

public class DataList:BaseSaveInfo
{
    public List<Data> Data;

    public override void InitDefault()
    {
        Data = new List<Data>();
       /* for (int i = 0; i < 3; i++)
        {
            Data.Add(new Data());
        }*/
    }
    public void AddNewData()
    {
        Data.Add(new Data());
    }
    public void RemoveData(Data data)
    {
        Data.Remove(data);
    }



}
