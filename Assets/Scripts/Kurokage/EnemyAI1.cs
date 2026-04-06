using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI1 : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private State startingState = State.Idle;

    [Header("Chasing")]
    [SerializeField] private float chasingDistance = 4f;       
    [SerializeField] private float stopChasingDistance = 6f;   
    [SerializeField] private float chasingSpeedMultiplier = 2f;

    [Header("Attacking")]
    [SerializeField] private float attackingDistance = 2f;
    [SerializeField] private float attackRate = 2f;

    private float _nextAttackTime = 0f;

    private NavMeshAgent _navMeshAgent;
    private State _currentState;

    private float _roamingSpeed;
    private float _chasingSpeed;

    private float _nextCheckDirectionTime = 0f;
    private readonly float _checkDirectionDuration = 0.1f;
    private Vector3 _lastPosition;

    public event EventHandler OnEnemyAttack;

    private enum State
    {
        Idle,
        Chasing,
        Attacking,
        Death
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _currentState = startingState;

        _roamingSpeed = _navMeshAgent.speed;
        _chasingSpeed = _navMeshAgent.speed * chasingSpeedMultiplier;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();
    }

    public void SetDeathState()
    {
        _navMeshAgent.ResetPath();
        _currentState = State.Death;
    }

    private void StateHandler()
    {
        switch (_currentState)
        {
            case State.Idle:
                CheckCurrentState();
                break;

            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;

            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;

            case State.Death:
                _navMeshAgent.ResetPath();
                break;
        }
    }

    private void ChasingTarget()
    {
        Transform player = GetActiveTarget();
        if (player == null) return;

        _navMeshAgent.SetDestination(player.position);
    }

    private void CheckCurrentState()
    {
        Transform player = GetActiveTarget();
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        State newState = _currentState;

        if (_currentState != State.Death)
        {
            if (distanceToPlayer <= attackingDistance)
            {
                newState = State.Attacking;
            }
            else if (distanceToPlayer <= chasingDistance)
            {
                newState = State.Chasing;
            }
            else if (_currentState == State.Chasing && distanceToPlayer <= stopChasingDistance)
            {
                newState = State.Chasing;
            }
            else
            {
                newState = State.Idle;
            }
        }

        if (newState != _currentState)
        {
            SwitchState(newState);
        }
    }

    private void SwitchState(State newState)
    {
        switch (newState)
        {
            case State.Idle:
                _navMeshAgent.ResetPath();
                _navMeshAgent.speed = _roamingSpeed;
                _navMeshAgent.isStopped = true;
                break;

            case State.Chasing:
                _navMeshAgent.speed = _chasingSpeed;
                _navMeshAgent.isStopped = false;
                break;

            case State.Attacking:
                _navMeshAgent.ResetPath();
                _navMeshAgent.isStopped = true;
                break;
        }

        _currentState = newState;
    }


    private Transform GetActiveTarget()
    {
        return CharacterSwitch.instance.CurrentCharacter.transform;
    }

    private void AttackingTarget()
    {
        if (Time.time > _nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            _nextAttackTime = Time.time + attackRate;
        }
    }

    private void MovementDirectionHandler()
    {
        if (Time.time > _nextCheckDirectionTime)
        {
            if (IsRunning)
            {
                ChangeFacingDirection(_lastPosition, transform.position);
            }
            else if (_currentState == State.Attacking)
            {
                ChangeFacingDirection(transform.position, CharacterSwitch.instance.CurrentCharacter.transform.position);
            }

            _lastPosition = transform.position;
            _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        transform.rotation = sourcePosition.x > targetPosition.x
            ? Quaternion.Euler(0, -180, 0)
            : Quaternion.Euler(0, 0, 0);
    }

    public bool IsRunning => _navMeshAgent.velocity != Vector3.zero;
}

