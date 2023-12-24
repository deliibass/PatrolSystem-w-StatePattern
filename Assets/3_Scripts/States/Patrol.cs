using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int currentIndex = -1;
    public Patrol(GameObject _npc, NavMeshAgent _agent, AnimationController _animationController, Transform _player)
        : base(_npc, _agent, _animationController, _player)
    {
        name = STATE.PATROL;
        base.agent.speed = 2;
        base.agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDist = Mathf.Infinity;
        for (var i = 0; i < GameEnvironment.Instance.Checkpoints.Count; i++)
        {
            GameObject thisWP = GameEnvironment.Instance.Checkpoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }
        animationController.PlayAnim(AnimList.WALK);
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= GameEnvironment.Instance.Checkpoints.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(GameEnvironment.Instance.Checkpoints[currentIndex].transform.position);
        }

        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, animationController, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animationController.PlayAnim(AnimList.IDLE);
        base.Exit();
    }
}