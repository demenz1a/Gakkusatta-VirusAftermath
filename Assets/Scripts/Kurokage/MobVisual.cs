using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class MobVisual : MonoBehaviour
{
    [SerializeField] private EnemyAI1 _mobAI;
    [SerializeField] private MobEntity _enemyEntity;

    private Animator _animator;

    private const string IS_RUNNING = "IsRunning";
    private const string TAKEHIT = "TakeHit";
    private const string IS_DIE = "IsDie";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";
    private const string ATTACK = "Attack";

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _mobAI.OnEnemyAttack += _mobAI_OnEnemyAttack;
        _enemyEntity.OnTakeHit += _mobAIEntity_OnTakeHit;
        _enemyEntity.OnDeath += _mobAIEntity_OnDeath;
    }

    private void _mobAIEntity_OnDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = 2;
    }

    private void _mobAIEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TAKEHIT);
    }

    private void Update()
    {
        _animator.SetBool(IS_RUNNING, _mobAI.IsRunning);
    }

    private void _mobAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ATTACK);
    }

}
