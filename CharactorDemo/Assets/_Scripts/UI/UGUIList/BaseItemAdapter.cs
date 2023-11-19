using UnityEngine;
using System;
using UnityEngine.UI;

public class BaseItemAdapter
{
    public GameObject gameObject;
    public bool canTouch = true;
    public Button SelfBtn;
    public int currentSortLayer = -1;
    public bool lastItem = false;
    public int index = -1;
    public virtual void SetSelect(bool isSelect) { }
    public virtual void SetSelect(int index) { }
    public virtual void Init(GameObject gameObject) { this.gameObject = gameObject; }
    public virtual void UpdateView(int index, object param) { this.index = index; }
    public virtual void OnClickItem() { }

    public virtual void Dispose()
    {
        if (SelfBtn)
            SelfBtn.onClick.RemoveAllListeners();
    }
}
