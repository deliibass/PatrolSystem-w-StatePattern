using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, AnimationController _animationController, Transform _player)
        : base(_npc, _agent, _animationController, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        animationController.PlayAnim(AnimList.IDLE);
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, animationController, player);
            stage = EVENT.EXIT;
        }
        else if (UnityEngine.Random.Range(0, 100) < 2)
        {
            nextState = new Patrol(npc, agent, animationController, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animationController.PlayAnim(AnimList.IDLE);
        base.Exit();
    }
}