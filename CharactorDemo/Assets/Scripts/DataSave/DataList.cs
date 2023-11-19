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
    }
    public Data AddNewData(string dataId)
    {
        var data = new Data();
        data.Id = dataId;
        Data.Add(data);
        return data;
    }
    public void RemoveData(Data data)
    {
        Data.Remove(data);
    }



}
