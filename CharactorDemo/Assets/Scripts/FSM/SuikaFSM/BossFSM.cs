using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossStateType
{
    BossIdle, BossDash,BossSpell,BossRest,BossHit,BossDeath
}

[Serializable]
public class BossParameter
{
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;
    public float dashTime;
    public float slowTime;

    public bool getHit;
    public bool isDead;

    public float jumpspeed;
    public float jumpforce;
    public Transform[] patrolPoints;
    public Transform[] chasePoints;
    public Animator anim;
    public Transform target;
    public Transform StartDialog;
    public ParticleSystem dust;
    public Rigidbody2D rg;
    public Transform AreaLimit;
    public AudioSource SuikaBGM;
    public Transform HpImage;
}

public class BossFSM : MonoBehaviour
{
    private IState currentState;
    private Dictionary<BossStateType, IState> states = new Dictionary<BossStateType, IState>();
    public BossParameter parameter;
    // Start is called before the first frame update
    void Start()
    {
        states.Add(BossStateType.BossIdle, new BossIdelState(this));
        states.Add(BossStateType.BossDash, new BossDashState(this));
        states.Add(BossStateType.BossSpell, new BossSpellState(this));
        states.Add(BossStateType.BossRest, new BossRestState(this));
        states.Add(BossStateType.BossHit, new BossHitState(this));
        states.Add(BossStateType.BossDeath, new BossDeathState(this));

        /*states.Add(StateType.Idle, new IdelState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.React, new ReactState(this));
        states.Add(StateType.Attack, new AttackState(this));*/

        TransitionState(BossStateType.BossIdle) ;

        parameter.anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();

    }
    public void GetHit()
    {
        parameter.getHit = true;
    }
    public void GetDied()
    {
        parameter.isDead = true;
    }
    public void TransitionState(BossStateType type)
    {
        if (currentState != null)

            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }
    public void FilpTo(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    public void Jump(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                parameter.rg.velocity = new Vector2(parameter.jumpspeed, parameter.jumpforce);
            }
            else if (transform.position.x < target.position.x)
            {
                parameter.rg.velocity = new Vector2(-parameter.jumpspeed, parameter.jumpforce);
            }
        }
    }

   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = null;
        }
    }*/
}
