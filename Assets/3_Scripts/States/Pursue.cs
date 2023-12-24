using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    public Pursue(GameObject _npc, NavMeshAgent _agent, AnimationController _animationController, Transform _player)
        : base(_npc, _agent, _animationController, _player)
    {
        name = STATE.PURSUE;
        base.agent.speed = 5;
        base.agent.isStopped = false;
    }

    public override void Enter()
    {
        animationController.PlayAnim(AnimList.RUN);
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, animationController, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, animationController, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        animationController.PlayAnim(AnimList.IDLE);
        base.Exit();
    }
}