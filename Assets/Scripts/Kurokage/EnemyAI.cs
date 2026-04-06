using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State _startingState;
    private Transform _transformCharacter1;
    private Transform _transformCharacter2;

    public event Action<GameObject> OnKurokageDied;

    [Header("Charge Settings")]
    [SerializeField] private float _chargeCooldown = 1f;  
    [SerializeField] private float _chargeDistance = 6f;   
    [SerializeField] private float _chargeSpeed = 6f;      
    [SerializeField] private float _overshootDistance = 2f;
    [SerializeField] private PolygonCollider2D polygonCollider2D;

    private Vector3 _chargeTarget;
    private float _nextChargeTime = 0f;

    public NavMeshAgent _navMeshAgent;
    private State _currentState;
    private Vector3 _lastPosition;
    

    private float _nextCheckDirectionTime = 0f;
    private float _checkDirectionDuration = 0.1f;

    public event EventHandler OnEnemyAttack;

    // public float GetRoamingAnimationSpeed() { return _navMeshAgent.speed / _roamingSpeed; }

    private enum State
    {
        Idle,
        Charging,
        Death
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _currentState = _startingState;
        //_transformCharacter1 = Reiko.Instance.transform;
    }

    private void Update()
    {
        if (_currentState == State.Death) return;

        switch (_currentState)
        {
            case State.Idle:
            case State.Charging:
                DoCharge();
                break;
        }

        MovementDirectionHandler();
    }

    public void SetDeathState()
    {
        polygonCollider2D.enabled = false;
        _navMeshAgent.ResetPath();
        _currentState = State.Death;
        
    }

    private void DoCharge()
    {
        if (!_navMeshAgent.hasPath || Vector3.Distance(transform.position, _chargeTarget) < 0.5f)
        {
            if (Time.time >= _nextChargeTime)
            {
                StartCharge();
            }
        }
    }

    private void StartCharge()
    {
        Vector3 dirToPlayer = (GetActiveTarget().position - transform.position).normalized;
        _chargeTarget = GetActiveTarget().position + dirToPlayer * _overshootDistance;

        _navMeshAgent.speed = _chargeSpeed;
        _navMeshAgent.SetDestination(_chargeTarget);

        _currentState = State.Charging;

        _nextChargeTime = Time.time + _chargeCooldown;
    }

    private void MovementDirectionHandler()
    {
        if (Time.time > _nextCheckDirectionTime)
        {
            //if (IsRunning)
            //{
             //   ChangeFacingDirection(_lastPosition, transform.position);
            //}

 //           _lastPosition = transform.position;
   //         _nextCheckDirectionTime = Time.time + _checkDirectionDuration;
        }
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private Transform GetActiveTarget()
    {
        return CharacterSwitch.instance.CurrentCharacter.transform;
    }
}

