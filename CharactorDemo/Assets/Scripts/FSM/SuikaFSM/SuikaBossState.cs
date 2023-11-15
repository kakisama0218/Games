using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mojiex;

public class BossIdelState : IState
{
    private BossFSM manager;
    private BossParameter parameter;
    private float timer;
    private int dashCount;
    private bool isMusic=false;

    public BossIdelState(BossFSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Idle");
    }
    public void OnUpdate()
    {
        if (DataStatic.Inst.dataModel.isGot(21)==true)
        {
            parameter.AreaLimit.gameObject.SetActive(true);
            parameter.HpImage.gameObject.SetActive(true);
            if(!isMusic)
            {
                parameter.SuikaBGM.Play();
                isMusic = true;
            }
            
            timer += Time.deltaTime;
        if (parameter.isDead)
        {
            manager.TransitionState(BossStateType.BossDeath);
        }

            if (parameter.getHit)
            {
                manager.TransitionState(BossStateType.BossHit);
            }

            if (timer>parameter.idleTime&&dashCount<3)
            {
                manager.TransitionState(BossStateType.BossDash);
                dashCount++;
            }
            else if(timer>parameter.idleTime&&dashCount==3)
            {
                manager.TransitionState(BossStateType.BossRest);
                dashCount = 0;
            }
        }
    }
    public void OnExit()
    {
        timer = 0;
    }
}

public class BossDashState : IState
{
    private BossFSM manager;
    private BossParameter parameter;
    private float timer;

    private Vector2 direction;

    public BossDashState(BossFSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Dash");
        parameter.dust.Play();
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if (parameter.isDead)
        {
            manager.TransitionState(BossStateType.BossDeath);
        }
        if (parameter.getHit)
        {
            manager.TransitionState(BossStateType.BossHit);
        }
        if (parameter.isDead)
        {
            manager.TransitionState(BossStateType.BossDeath);
        }
        if (timer<=parameter.dashTime)
        {
            if (timer <= 0.02)
            {
                manager.FilpTo(parameter.target);



                direction = -(new Vector2(manager.transform.position.x, 0) - new Vector2(parameter.target.transform.position.x, 0)).normalized;
            }
                parameter.rg.velocity = direction * parameter.moveSpeed;
            
        }
        

       /* manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.target.position, parameter.moveSpeed * Time.deltaTime);*/


        if (timer>=parameter.slowTime)
        {
            manager.TransitionState(BossStateType.BossIdle);
        }
    }
    public void OnExit()
    {
        timer = 0;
    }
}
public class BossSpellState : IState
{
    private BossFSM manager;
    private BossParameter parameter;
    private AnimatorStateInfo info;
    //private Rigidbody2D rg;

    public BossSpellState(BossFSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Spell");
        manager.Jump(parameter.target);
        manager.FilpTo(parameter.target);
        
    }
    public void OnUpdate()
    {
        if (parameter.isDead)
        {
            manager.TransitionState(BossStateType.BossDeath);
        }
        if (parameter.getHit)
        {
            manager.TransitionState(BossStateType.BossHit);
        }
        if (parameter.isDead)
        {
            manager.TransitionState(BossStateType.BossDeath);
        }
        info = parameter.anim.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= .95f)
        {
            manager.TransitionState(BossStateType.BossIdle);
        }
    }
    public void OnExit()
    {

    }
}
public class BossRestState : IState
{
    private BossFSM manager;
    private BossParameter parameter;
    private float timer;

    public BossRestState(BossFSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("idle");
    }
    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if (parameter.isDead)
        {
            manager.TransitionState(BossStateType.BossDeath);
        }
        if (parameter.getHit)
        {
            manager.TransitionState(BossStateType.BossHit);
        }
        if (parameter.isDead)
        {
            manager.TransitionState(BossStateType.BossDeath);
        }
        if (timer > parameter.idleTime)
        {
            manager.TransitionState(BossStateType.BossSpell);
            
        }
        
    }
    public void OnExit()
    {

    }
}
public class BossHitState : IState
{
    private BossFSM manager;
    private BossParameter parameter;
    private AnimatorStateInfo info;
    

    public BossHitState(BossFSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Failed");
    }
    public void OnUpdate()
    {
        info = parameter.anim.GetCurrentAnimatorStateInfo(0);

      if(parameter.isDead)
        {
            manager.TransitionState(BossStateType.BossDeath);
        }
        if (!parameter.isDead&& info.normalizedTime >= .95f)
        {
            manager.TransitionState(BossStateType.BossIdle);
        }
    }
    public void OnExit()
    {
        parameter.getHit = false;
    }
}

public class BossDeathState : IState
{
    private BossFSM manager;
    private BossParameter parameter;


    public BossDeathState(BossFSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Failed");
        parameter.AreaLimit.gameObject.SetActive(false);
        parameter.HpImage.gameObject.SetActive(false);
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}

