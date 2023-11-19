using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class LongPressButton : Button
{
    public enum AnimationType
    {
        None,
        Scale,
    }
    [SerializeField]
    [Tooltip("How long the button be pressed to trigger a long press")]
    private float pressSeconds = 0.15f;

    public UnityEvent onLongPressBegin = new UnityEvent();
    public UnityEvent onLongPressEnd = new UnityEvent();
    public UnityEvent onClickDis = new UnityEvent();
    [SerializeField] private AnimationType animationType = AnimationType.Scale;
    [SerializeField] private float targetScale = 0.9f;
    [SerializeField] private float btnAnimateTime = 0.15f;

    private bool _longPressedInSession;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        _longPressedInSession = false;
        Invoke(nameof(OnLongPress), pressSeconds);
        switch (animationType)
        {
            case AnimationType.Scale:
                transform.DOKill();
                transform.DOScale(targetScale, btnAnimateTime);break;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (_longPressedInSession)
        {
            onLongPressEnd.Invoke();
        }

        _longPressedInSession = false;
        CancelInvoke(nameof(OnLongPress));
        switch (animationType)
        {
            case AnimationType.Scale:
                transform.DOKill();
                transform.DOScale(Vector3.one, btnAnimateTime); break;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        switch (animationType)
        {
            case AnimationType.Scale:
                transform.DOKill();
                transform.DOScale(Vector3.one, btnAnimateTime); break;
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (_longPressedInSession)
        {
            onLongPressEnd.Invoke();
        }

        _longPressedInSession = false;
        CancelInvoke(nameof(OnLongPress));
        switch (animationType)
        {
            case AnimationType.Scale:
                transform.DOKill();
                transform.DOScale(Vector3.one, btnAnimateTime); break;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (_longPressedInSession)
        {
            return;
        }

        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (!IsActive() || !IsInteractable())
        {
            if (IsActive() && !IsInteractable())
            {
                onClickDis?.Invoke();
            }
            return;
        }

        base.OnPointerClick(eventData);
    }

    private void OnLongPress()
    {
        onLongPressBegin.Invoke();
        _longPressedInSession = true;
    }
}
