using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    #region FIELDS
    
    [Header("Fields")]
    [SerializeField] private AnimationController animationController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    
    private Animator _anim;
    private State _currentState;
    
    #endregion

    #region VARIABLES

    [Header("Variables")]
    [SerializeField] private float visDistance = 10.0f;
    [SerializeField] private float visAngle = 50.0f;
    [SerializeField] private float shootDistance = 7.0f;

    #endregion

    #region UNITY FUNCTIONS

    void Start()
    {
        _currentState = new Idle(gameObject, agent, animationController, player);
        _currentState.SetVariables(visDistance, visAngle, shootDistance);
    }
    
    void Update()
    {
        _currentState = _currentState.Process();
    }

    #endregion
}
