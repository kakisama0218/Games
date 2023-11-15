using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;

    public  IdelState(FSM manager)
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

        if(parameter.target!=null&&
            parameter.target.position.x>=parameter.chasePoints[0].position.x&&
            parameter.target.position.x<=parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }

        if (timer >= parameter.idleTime)
        {
            //Debug.Log("nb");
            manager.TransitionState(StateType.Patrol);
        }
    }
    public void OnExit()
    {
        timer = 0;
    }
}

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("Attack");
    }
    public void OnUpdate()
    {
        manager.FilpTo(parameter.target);
        if(parameter.target)
        {

        }
        if(parameter.target==null||manager.transform.position.x<parameter.chasePoints[0].position.x|| manager.transform.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);
        }
    }
    public void OnExit()
    {

    }
}

public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;

    private int patrolPosition;

    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("walk");
    }
    public void OnUpdate()
    {
        manager.FilpTo(parameter.patrolPoints[patrolPosition]);

        manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);

        if (parameter.target != null &&
            parameter.target.position.x >= parameter.chasePoints[0].position.x &&
            parameter.target.position.x <= parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.React);
        }

        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position )< .1f)
        {
            //Debug.Log("nb");
            manager.TransitionState(StateType.Idle);
        }
    }
    public void OnExit()
    {
        patrolPosition++;

        if(patrolPosition>=parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }
}
public class ReactState : IState
{
    private FSM manager;
    private Parameter parameter;

    private AnimatorStateInfo info;
    

    public ReactState(FSM manager)
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
        info = parameter.anim.GetCurrentAnimatorStateInfo(0);

        if(info.normalizedTime>=.50f)
        {
            manager.TransitionState(StateType.Attack);
        }
    }
    public void OnExit()
    {
       
    }
}


