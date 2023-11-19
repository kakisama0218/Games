using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

//TODO 待优化
[DisallowMultipleComponent]
public class UGUIListAdapter : MonoBehaviour
{
    public GameObject itemObj;
    public Type itemAdpterType;
    public RectTransform Mask;
    private Coroutine updateViewCoroutine;

    public object[] Data
    {
        set
        {
            _data = value;
            UpdateView();
        }
        get => _data;
    }

    public object[] Data_anim
    {
        set
        {
            _data = value;
            if (updateViewCoroutine != null)
            {
                StopCoroutine(updateViewCoroutine);
            }
            updateViewCoroutine = StartCoroutine(UpdateView_Anim());
            //UpdateView();
        }
        get => _data;
    }

    public int SelectIndex
    {
        set
        {
            if (_selectIndex != value)
            {
                _selectIndex = value;
                UpdateView();
            }
        }
        get => _selectIndex;
    }

    public Action OnCompelete;
    public int CurrentSortLayer = -1;
    private int _selectIndex = -1;
    private object[] _data;
    private Dictionary<int, BaseItemAdapter> itemPool;
    private RectTransform _panel;
    private bool _inited = false;
    private Tween _moveTween;
    private bool _isVertical;

    private void Awake()
    {
        if (!_inited)
            Init();
    }

    private void Init()
    {
        itemPool = new Dictionary<int, BaseItemAdapter>();
        _panel = gameObject.GetComponent<RectTransform>();
        _isVertical = gameObject.GetComponent<VerticalLayoutGroup>() != null;
        _inited = true;
        itemObj.gameObject.SetActive(false);
    }

    private void UpdateView()
    {
        if (!_inited)
            Init();

        if (!itemObj)
        {
            Debug.LogError("Not Set ListAdapter Item!!!");
            return;
        }
        else if (itemAdpterType == null)
        {
            Debug.LogError("Not Set ListAdapter itemAdpterType!!!");
            return;
        }
        if (_data?.Length > 0)
        {
            BaseItemAdapter baseItemAdapter;

            for (int i = 0; i < _data.Length; i++)
            {
                if (!itemPool.ContainsKey(i))
                {
                    baseItemAdapter = Activator.CreateInstance(itemAdpterType, null) as BaseItemAdapter;
                    baseItemAdapter.currentSortLayer = CurrentSortLayer;
                    var go = Instantiate(itemObj);
                    go.SetActive(true);
                    baseItemAdapter.Init(go);

                    if (baseItemAdapter.gameObject.TryGetComponent(out Button button))
                    {
                        baseItemAdapter.SelfBtn = button;
                    }

                    if (baseItemAdapter.SelfBtn)
                    {
                        //TODO 待优化
                        int idx = i;
                        var adp = baseItemAdapter;
                        baseItemAdapter.SelfBtn.onClick.AddListener(() =>
                        {
                            _selectIndex = idx;
                            adp.OnClickItem();
                        });
                    }

                    itemPool.Add(i, baseItemAdapter);
                    baseItemAdapter.gameObject.transform.SetParent(_panel.transform, false);
                }
                else baseItemAdapter = itemPool[i];

                baseItemAdapter.gameObject.SetActive(true);
                baseItemAdapter.UpdateView(i, _data[i]);
                baseItemAdapter.SetSelect(i == _selectIndex);
                baseItemAdapter.SetSelect(_selectIndex);
            }

            if (itemPool.Count > _data.Length)
                for (int j = _data.Length; j < itemPool.Count; j++)
                    itemPool[j].gameObject.SetActive(false);
        }
        else
        {
            for (int j = 0; j < _panel.childCount; j++)
                _panel.GetChild(j).gameObject.SetActive(false);
        }
        OnCompelete?.Invoke();
        OnCompelete = null;
    }

    public void MoveIndex(int index, bool needTween = true, bool needCenter = false, bool isSpecial = false)
    {
        if (!itemPool.ContainsKey(index)) return;
        if (!itemPool.ContainsKey(index + 1))
        {
            needCenter = false;
        }

        Vector3 endPos = Vector3.zero;
        if (_isVertical)
        {
            if (needCenter)
            {
                if (isSpecial)
                {
                    //商店界面特殊处理了一下, +100是子物体间隔, 注意i+1不要越界
                    if (index < 1)
                    {
                        endPos.y = _panel.transform.GetChild(0).GetComponent<RectTransform>().rect.height + 100;
                    }
                    else
                    {
                        int jumpIndex = index;
                        endPos.y = _panel.transform.GetChild(0).GetComponent<RectTransform>().rect.height + 100;
                        for (int i = 0; i < jumpIndex; i++)
                        {
                            endPos.y += _panel.transform.GetChild(i + 1).GetComponent<RectTransform>().rect.height;
                        }
                        endPos.y += jumpIndex * 100;
                    }
                }
                else
                {
                    RectTransform childRect1 = itemPool[index].gameObject.GetComponent<RectTransform>();
                    RectTransform childRect2;
                    if (index + 1 >= itemPool.Count)
                    {
                        childRect2 = itemPool[index].gameObject.GetComponent<RectTransform>();
                    }
                    else
                    {
                        childRect2 = itemPool[index + 1].gameObject.GetComponent<RectTransform>();
                    }
                    float y1 = -childRect1.localPosition.y + childRect1.rect.height / 2 - Mask.rect.height;
                    float y2 = -childRect2.localPosition.y + childRect2.rect.height / 2 - Mask.rect.height;
                    endPos.y = (y1 + y2) / 2;
                }
            }
            else
            {
                RectTransform childRect = null;
                if (index + 1 >= itemPool.Count)
                {
                    childRect = itemPool[index].gameObject.GetComponent<RectTransform>();
                }
                else
                {
                    childRect = itemPool[needCenter ? index + 1 : index].gameObject.GetComponent<RectTransform>();
                }
                endPos.y = -childRect.localPosition.y + childRect.rect.height / 2 - Mask.rect.height;
            }
            if (endPos.y < 0) endPos.y = Mask.rect.height / 2;
            else
                endPos.y += Mask.rect.height / 2;
        }
        else
        {
            if (needCenter)
            {
                RectTransform childRect1 = itemPool[index].gameObject.GetComponent<RectTransform>();
                RectTransform childRect2;
                if (index + 1 >= itemPool.Count)
                {
                    childRect2 = itemPool[index].gameObject.GetComponent<RectTransform>();
                }
                else
                {
                    childRect2 = itemPool[index + 1].gameObject.GetComponent<RectTransform>();
                }
                float y1 = -childRect1.localPosition.x + childRect1.rect.width / 2 - Mask.rect.width;
                float y2 = -childRect2.localPosition.x + childRect2.rect.width / 2 - Mask.rect.width;
                endPos.x = (y1 + y2) / 2;
            }
            else
            {
                RectTransform childRect = null;
                if (index + 1 >= itemPool.Count)
                {
                    childRect = itemPool[index].gameObject.GetComponent<RectTransform>();
                }
                else
                {
                    childRect = itemPool[needCenter ? index + 1 : index].gameObject.GetComponent<RectTransform>();
                }
                endPos.x = -childRect.localPosition.x + childRect.rect.width / 2 - Mask.rect.width;
            }
            if (endPos.x < 0) endPos.x = Mask.rect.width / 2;
            else endPos.x += Mask.rect.width / 2;
        }
        if (needTween)
        {
            if (_moveTween != null)
                _moveTween.Kill();
            if (Vector3.Distance(endPos, gameObject.transform.localPosition) > 20)
            {
                _moveTween = gameObject.transform.DOLocalMove(endPos, 0.5f, true)
                        .OnComplete(() =>
                        {
                            SelectIndex = index;
                            SelectIndex = -1;
                        })
                        .SetAutoKill(true)
                        .OnKill(() => _moveTween = null)
                        .Play();
            }
            else
            {
                SelectIndex = index;
                SelectIndex = -1;
            }
        }
        else
        {
            _panel.localPosition = endPos;
            SelectIndex = index;
        }
    }

    public BaseItemAdapter GetItem(int index)
    {
        return itemPool[index];
    }

    public Dictionary<int, BaseItemAdapter> GetAllItems()
    {
        return itemPool;
    }

    IEnumerator UpdateView_Anim()
    {
        if (!_inited)
            Init();

        if (!itemObj)
        {
            Debug.LogError("Not Set ListAdapter Item!!!");
            yield return null;
        }
        else if (itemAdpterType == null)
        {
            Debug.LogError("Not Set ListAdapter itemAdpterType!!!");
            yield return null;
        }
        if (_data?.Length > 0)
        {
            BaseItemAdapter baseItemAdapter;

            for (int i = 0; i < _data.Length; i++)
            {
                if (!itemPool.ContainsKey(i))
                {
                    baseItemAdapter = Activator.CreateInstance(itemAdpterType, null) as BaseItemAdapter;
                    baseItemAdapter.currentSortLayer = CurrentSortLayer;
                    var go = Instantiate(itemObj);
                    go.SetActive(true);
                    baseItemAdapter.Init(go);
                    baseItemAdapter.SelfBtn = baseItemAdapter.gameObject.GetComponent<Button>();
                    if (baseItemAdapter.SelfBtn)
                    {
                        //TODO 待优化
                        int idx = i;
                        var adp = baseItemAdapter;
                        baseItemAdapter.SelfBtn.onClick.AddListener(() =>
                        {
                            _selectIndex = idx;
                            adp.OnClickItem();
                        });
                    }

                    itemPool.Add(i, baseItemAdapter);
                    baseItemAdapter.gameObject.transform.SetParent(_panel.transform, false);
                }
                else baseItemAdapter = itemPool[i];

                baseItemAdapter.gameObject.SetActive(true);
                baseItemAdapter.UpdateView(i, _data[i]);
                baseItemAdapter.SetSelect(i == _selectIndex);
                baseItemAdapter.SetSelect(_selectIndex);
                if (i == _data.Length - 1)
                {
                    baseItemAdapter.lastItem = true;
                }
                yield return new WaitForSeconds(0.15f);
            }

            if (itemPool.Count > _data.Length)
                for (int j = _data.Length; j < itemPool.Count; j++)
                    itemPool[j].gameObject.SetActive(false);
        }
        else
        {
            for (int j = 0; j < _panel.childCount; j++)
                _panel.GetChild(j).gameObject.SetActive(false);
        }
        OnCompelete?.Invoke();
        OnCompelete = null;
    }

    public void StopUpdateView_Anim()
    {
        if (updateViewCoroutine != null)
        {
            StopCoroutine(updateViewCoroutine);
        }
    }

    private void OnDestroy()
    {
        foreach (var info in itemPool)
            info.Value.Dispose();

        itemPool.Clear();
    }

}
