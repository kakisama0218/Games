using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Charactor : MonoBehaviour
{
    [Header("角色数值")]
    public float MaxHealth;
    public float CurrentHealth;
    public float hurtdamage;

    public float MaxTough;
    public float CurrentTough;
    public float Toughdamage;


    public WaitForSeconds HealTime;
    [Header("无敌时间")]
    public float InvulnerableDuration;
    private float InvulnerableCounter;
    public bool Invulnerable=false;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent<Transform> OnDie;
    public UnityEvent<Transform> OnBreak;

    public UnityEvent<Charactor> OnHealthChange;
    public UnityEvent<Charactor> OnHeal;

    public ParticleSystem hurtEffect;
    private bool isDead=false;
    public GameObject GameOverPanel;

    
    private string Attacktype;

    Coroutine Healcoroutine;


   
    [SerializeField] private Transform pfDamagePopup;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentTough = MaxTough;
        HealTime = new WaitForSeconds(0.01f);

    }

    private void Update()
    {
        if(Invulnerable)
        {
            InvulnerableCounter -= Time.deltaTime;
            if(InvulnerableCounter<=0)
            {
                Invulnerable = false;
            }
        }
        if(CurrentTough<MaxTough)
        {
            CurrentTough += 0.1f * Time.deltaTime;
            if(CurrentTough>MaxTough)
            {
                CurrentTough = MaxTough;
            }
        }

    }
    public virtual void RestoreHealth(float value)
    {
        if (CurrentHealth == MaxHealth)
            return;
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0f, MaxHealth);
        OnHeal.Invoke(this);
    }
    public IEnumerator HealthRestore(WaitForSeconds waittime,float percent)
    {
      
            while(CurrentHealth<MaxHealth)
            {
                yield return waittime;

                RestoreHealth(MaxHealth * percent);
            }
     
    }
    public void TakeDamage(Attack Attacker)
    {
        if (Invulnerable)
            return;
       

        if(CurrentHealth-Attacker.Damage>0)
        {
         
            CurrentHealth -= Attacker.Damage;
            CurrentTough -= Attacker.ToughDamage;
            hurtdamage = Attacker.Damage;
            TriggerInvulnerable();
            if(Attacker.Damage>0)
            {
                OnTakeDamage?.Invoke(Attacker.transform);
            }
            if(CurrentTough-Attacker.ToughDamage<=0)
            {
                OnBreak?.Invoke(Attacker.transform);
                CurrentTough = MaxTough;
            }

            Transform damagePopupTransform = Instantiate(pfDamagePopup, transform.position, Quaternion.identity);
            DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            damagePopup.SetUp(Attacker.Damage);
        }
       else
        {
            Transform damagePopupTransform = Instantiate(pfDamagePopup, transform.position, Quaternion.identity);
            DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            damagePopup.SetUp(Attacker.Damage);
            TriggerInvulnerable();
            CurrentHealth = 0;
            OnDie?.Invoke(Attacker.transform);
            isDead = true;
            Invoke("gameOver", 2.5f);
        }
        OnHealthChange?.Invoke(this);
        if(!isDead)
        {
            hurtEffect.Play();
        }
        
    }

    public void gameOver()
    {
        GameOverPanel.gameObject.SetActive(true);
    }
    public void GetHeal()
    {
        /*OnHeal.Invoke(this);*/
        if(Healcoroutine!=null)
        {
            StopCoroutine(Healcoroutine);
        }
        Healcoroutine= StartCoroutine(HealthRestore(HealTime,0.01f));
        
    }

    private void TriggerInvulnerable()
    {
        if(!Invulnerable)
        {
            Invulnerable = true;
            InvulnerableCounter = InvulnerableDuration;
        }
    }
    public void SkillInvulnerableUp()
    {
        if(!Invulnerable)
        {
            Invulnerable = true;
            InvulnerableCounter = InvulnerableDuration-0.5f;
        }
    }
    public void SkillInvulnerableDown()
    {
        if (Invulnerable)
        {
            Invulnerable = false;
        }
    }
}
