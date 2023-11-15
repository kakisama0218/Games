using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int BasicDamage;
    public int Damage;
    public int BasicToughDamage;
    public int ToughDamage;
    public float attackRange;
    public float attackRate;

    public bool isSkill=false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //if(other.tag=="Enemy"||other.tag=="Player")
/*
            if( other.tag == "Enemy")
        {
            if (anim.GetInteger("ComboStep") == 3)
            {
                Damage = (int)(BasicDamage * 1.35);
                ToughDamage = BasicToughDamage * 2;
            }
           
            else*/
           // {
                Damage = BasicDamage;
                ToughDamage = BasicToughDamage;
           // }
            other.GetComponent<Charactor>()?.TakeDamage(this);
           // Debug.Log(Damage);
        }
       

       /* if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("1");
        }*/
    //}
    public void Skill()
    {
        isSkill = true;
    }
}
