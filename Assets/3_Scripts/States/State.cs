using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    #region FIELDS

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected AnimationController animationController;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    #endregion

    #region VARIABLES

    private float _visDist = 10.0f;
    private float _visAngle = 50.0f;
    private float _shootDist = 7.0f;

    #endregion

    #region CONSTRUCTOR

    public State(GameObject _npc, NavMeshAgent _agent, AnimationController _animationController, Transform _player)
    {
        this.npc = _npc;
        this.agent = _agent;
        this.animationController = _animationController;
        this.player = _player;
        
        stage = EVENT.ENTER;
    }

    #endregion
    
    #region VIRTUAL FUNCTIONS

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    #endregion
    
    #region CONTROL FUNCTIONS

    public void SetVariables(float visDist = 10.0f, float visAngle = 50.0f, float shootDist = 7.0f)
    {
        _visDist = visDist;
        _visAngle = visAngle;
        _shootDist = shootDist;
    }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < _visDist && angle < _visAngle)
        {
            return true;
        }
        return false;
    }
    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < _shootDist)
        {
            return true;
        }
        return false;
    }

    #endregion
}