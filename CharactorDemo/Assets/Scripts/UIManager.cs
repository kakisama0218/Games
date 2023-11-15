using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public BloodStrip bloodStrip;
    public Text Hp;

    public BloodStrip BossBloodStrip;
    public Text BossHp;

    //public Text Coin;
    
    [Header("�¼�����")]

    public CharactorEventSO healthEvent;
    //public CharactorEventSO CoinEvent;
    public CharactorEventSO healEvent;
    public CharactorEventSO BossHpEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        //CoinEvent.OnEventRaised += OnCoinEvent;
        healEvent.OnEventRaised += OnHealEvent;
        BossHpEvent.OnEventRaised += OnBossHpEvent;

    }
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        //CoinEvent.OnEventRaised -= OnCoinEvent;
        healEvent.OnEventRaised -= OnHealEvent;
        BossHpEvent.OnEventRaised -= OnBossHpEvent;
    }

    private void OnHealEvent(Charactor charactor)
    {
       // charactor.CurrentHealth = charactor.MaxHealth;
        
        bloodStrip.BornFireHeal(charactor.CurrentHealth);
        Hp.text = charactor.CurrentHealth.ToString();
    }

    /*private void OnCoinEvent(Charactor charactor)
    {
       
    }*/
    private void OnBossHpEvent(Charactor charactor)
    {
        var damage = charactor.hurtdamage;
        BossBloodStrip.GetDamage(damage);
        BossHp.text = charactor.CurrentHealth.ToString();
    }

    private void OnHealthEvent(Charactor charactor)
    {
        var damage = charactor.hurtdamage;
        bloodStrip.GetDamage(damage);
        Hp.text = charactor.CurrentHealth.ToString();
    }

   
}
