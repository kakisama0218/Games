using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mojiex;

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

    public void NewData()
    {
        m_datalist.AddNewData();
        list.Add(new DataModel(m_datalist.Data[m_datalist.Data.Count-1]));
        Save();
    }
    public void RemoveData(int DataID)
    {
        m_datalist.RemoveData(m_datalist.Data[DataID]);
        list.Remove(GetData(DataID));
        Save();
        
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
