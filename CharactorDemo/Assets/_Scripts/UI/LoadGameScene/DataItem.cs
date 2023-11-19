using Mojiex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataItem : BaseItemAdapter
{
    private DataItemCom _panel;
    private DataModel _model;
    public override void Init(GameObject gameObject)
    {
        base.Init(gameObject);
        _panel = gameObject.GetComponent<DataItemCom>();
        AddEvents();
    }

    public override void UpdateView(int index, object param)
    {
        base.UpdateView(index, param);
        _model = (DataModel)param;

        _panel.tittle.text = _model == null ? "" : _model.Id;
        _panel.mainText.text = _model == null ? "Create New" : _model.GetTime().ToString("g");
    }

    private void AddEvents()
    {
        _panel.pressButton.onClick.AddListener(OnClickDataItem);
    }

    private void RemoveEvents()
    {

    }

    private void OnClickDataItem()
    {
        if (_model != null) 
        {
            DataStatic.Inst.SetDataID(_model.Id);
        }
        else
        {
            DataStatic.Inst.SetDataID("0");
        }
    }

    public override void Dispose()
    {
        RemoveEvents();
    }
}
