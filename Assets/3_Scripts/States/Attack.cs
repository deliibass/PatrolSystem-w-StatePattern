using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    float rotationSpeed = 25.0f;
    AudioSource shoot;

    public Attack(GameObject _npc, NavMeshAgent _agent, AnimationController _animationController, Transform _player)
        : base(_npc, _agent, _animationController, _player)
    {
        name = STATE.ATTACK;
        shoot = _npc.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        animationController.PlayAnim(AnimList.SHOOT);
        agent.isStopped = true;
        shoot.Play(14000);
        base.Enter();
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        if (!CanAttackPlayer())
        {
            nextState = new Idle(npc, agent, animationController, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animationController.PlayAnim(AnimList.IDLE);
        shoot.Stop();
        base.Exit();
    }
}