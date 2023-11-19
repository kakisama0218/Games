using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices.ComTypes;

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

        public string CurrentDataModelId { get; private set; } = "";
        public SaveManager saveManager;
        public DataModel dataModel
        {
            get => _dataModel ??= new DataModel(new Data());
            set => _dataModel = value;
        }
        private DataModel _dataModel;
        public DataListModel DataListModel;

        public Action<DataListModel> OnDataListUpdate;
        public Action<string> OnSelectData;

        public void Init()
        {

            saveManager = new SaveManager();
            DataListModel = new DataListModel(saveManager.Load<DataList>());
        }

        public void Save()
        {
            dataModel.setTime(TimeUtils.GetTimeStamp());
            DataListModel.GetData(CurrentDataModelId)?.CopyFrom(dataModel);
            DataListModel.Save();
        }

        public List<DataModel> GetAllDataModel()
        {
            return DataListModel.list;
        }

        public void SetDataID(int DataID)
        {
            dataModel.CopyFrom(DataListModel.GetData(DataID));
        }
        public void SetDataID(string DataID)
        {
            CurrentDataModelId = DataID;
            var model = DataListModel.GetData(DataID);
            if (model != null) 
            {
                
                dataModel.CopyFrom(model);
            }
            else
            {
                dataModel = null;
            }
            OnSelectData?.Invoke(DataID);
        }

        public void DeleteCurrentData()
        {
            if (string.IsNullOrEmpty(CurrentDataModelId))
            {
                return;
            }
            DataListModel.RemoveData(CurrentDataModelId);
        }

        public void Create()
        {
            DataListModel.NewData();
        }

        public void Restart()
        {
            var model = DataListModel.GetData(CurrentDataModelId);
            if (model != null)
            {

                dataModel.CopyFrom(model);
            }
            else
            {
                dataModel = null;
            }
        }

        public void UpdateCurrentDataTime()
        {
            var data = DataListModel.GetData(CurrentDataModelId);
            if (data != null)
            {
                data.setTime(TimeUtils.GetTimeStamp());
            }
        }
    }
}