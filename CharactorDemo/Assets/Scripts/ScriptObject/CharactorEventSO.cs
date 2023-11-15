using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/CharactorEventSO")]
public class CharactorEventSO : ScriptableObject
{
    public UnityAction<Charactor> OnEventRaised;

    public void RaiseEvent(Charactor charactor)
    {
        OnEventRaised.Invoke(charactor);
    }
}
