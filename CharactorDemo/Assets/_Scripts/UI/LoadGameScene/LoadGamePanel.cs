using Mojiex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGamePanel : MonoBehaviour
{
    [SerializeField] private UGUIListAdapter listAdapter;
    [SerializeField] private LongPressButton confirm;
    [SerializeField] private LongPressButton delete;
    [SerializeField] private LongPressButton creat;

    private void Start()
    {
        AddEvents();
        listAdapter.itemAdpterType = typeof(DataItem);
        UpdateView(DataStatic.Inst.DataListModel);
        OnSelectData(DataStatic.Inst.CurrentDataModelId);
    }

    private void AddEvents()
    {
        confirm.onClick.AddListener(OnClickConfirm);
        delete.onClick.AddListener(OnClickDelete);
        creat.onClick.AddListener(OnClickCreat);
        DataStatic.Inst.OnSelectData += OnSelectData;
        DataStatic.Inst.OnDataListUpdate += UpdateView;
    }

    private void RemoveEvents()
    {
        DataStatic.Inst.OnSelectData -= OnSelectData;
        DataStatic.Inst.OnDataListUpdate -= UpdateView;
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    #region Call Back
    private void UpdateView(DataListModel listModel)
    {
        var list = new List<DataModel>(listModel.list);
        list.Add(null);
        listAdapter.Data = list.ToArray();
    }

    private void OnSelectData(string dataId)
    {
        var isNull = string.IsNullOrEmpty(dataId);
        var isCreate = dataId.Equals("0");
        confirm.gameObject.SetActive(!isNull && !isCreate);
        delete.gameObject.SetActive(!isNull && !isCreate);
        creat.gameObject.SetActive(!isNull && isCreate);
    }
    private void OnClickConfirm()
    {
        DataStatic.Inst.UpdateCurrentDataTime();
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    private void OnClickDelete()
    {
        DataStatic.Inst.DeleteCurrentData();
    }
    private void OnClickCreat()
    {
        DataStatic.Inst.Create();
    }
    #endregion
}
