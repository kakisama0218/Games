using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mojiex;
using System;
using System.Runtime.InteropServices.ComTypes;

public class DataListModel
{
    private DataList m_datalist;
    public List<DataModel> list;
    public DataListModel(DataList datalist)
    {
        m_datalist = datalist;
        list = new List<DataModel>();
        for(int i=0; i<m_datalist.Data.Count;i++)
        {
            list.Add(new DataModel(m_datalist.Data[i]));
        }
    }

    public DataModel NewData()
    {
        var data = new DataModel( m_datalist.AddNewData(UIDTool.GetUID()));
        list.Add(data);
        Save();
        DataStatic.Inst.OnDataListUpdate?.Invoke(this);
        return data;
    }
    public void RemoveData(string guid)
    {
        var data = list.Find(a => a.Id.Equals(guid));
        if(data == null)
        {
            return;
        }
        m_datalist.RemoveData(data.GetData());
        list.Remove(data);
        Save();
        DataStatic.Inst.OnDataListUpdate?.Invoke(this);
    }

    [Obsolete("May Use RemoveData(string guid)")]
    public void RemoveData(int DataID)
    {
        m_datalist.RemoveData(m_datalist.Data[DataID]);
        list.Remove(GetData(DataID));
        Save();
        DataStatic.Inst.OnDataListUpdate?.Invoke(this);
    }
    public DataModel GetData(string guid)
    {
        return list.Find(a => a.Id == guid);
    }
    public DataModel GetData(int DataID)
    {
        if(DataID+1<=m_datalist.Data.Count)
        {
            return list[DataID];
        }
        return null;
    }
    public void Save()
    {
        DataStatic.Inst.saveManager.Save(m_datalist);
    }

}
