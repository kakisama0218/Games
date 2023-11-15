using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Mojiex
{
    //CreateTime : 2022/8/27
    public class DataStatic
    {
        public static DataStatic Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new DataStatic();
                    _inst.Init();
                }
                return _inst;
            }
        }
        private static DataStatic _inst;

        public  SaveManager saveManager;
        public  DataModel dataModel;
        public DataListModel DataListModel;
        //public static PlayerModel player;
        //public static StatisticsInfo statistics;

        public void Init()
        {

            saveManager = new SaveManager();
            DataListModel = new DataListModel(saveManager.Load<DataList>());
            //dataModel = new DataModel(saveManager.Load<Data>());
            //player = new PlayerModel();
            //player.Init();
            //statistics = Mgr.saveMgr.Load<StatisticsInfo>();
            //MDebug.Log(player.GetInfo().name);
        }

        public void Save()
        {
            dataModel.setTime(TimeUtils.GetTimeStamp());
            DataListModel.Save();

            //player.Save();
            //Mgr.saveMgr.Save(statistics);
        }

        public void SetDataID(int DataID)
        {
            dataModel = DataListModel.GetData(DataID);
        }
    }
}