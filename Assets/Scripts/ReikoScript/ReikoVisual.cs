using UnityEngine;

public class ReikoVisual : MonoBehaviour
{
    [SerializeField] private Reiko _reiko;
    [SerializeField] private PolygonCollider2D _polygonCollider2D;

    private Animator _animator;
    public SpriteRenderer spriteRenderer;

    private const string ISWALKING = "IsWalking";
    private const string ISHIT = "IsHit";
    private const string ISDOGDE = "IsDogde";
    private const string ISDEAD = "IsDead";


    public bool IsFlipped => spriteRenderer.flipX;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _reiko.OnTakeHit += _reiko_OnTakeHit;
        _reiko.OnDead += _reiko_OnDead;
    }

    private void _reiko_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ISHIT);
    }

    private void _reiko_OnDead(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ISDEAD);
    }

    private void Update()
    {
        _animator.SetBool(ISWALKING, Reiko._instance.IsRunning());
        _animator.SetBool(ISDOGDE, Reiko._instance.IsDogde());
    }

    public void AdjustPlayerFacingDirection(Vector2 dir)
    {
        if (dir.x < -0.01f)
            spriteRenderer.flipX = true;
        else if (dir.x > 0.01f)
            spriteRenderer.flipX = false;
    }




    public void AttackColliderTurnoff()
    {
        _polygonCollider2D.enabled = false;
    }

    private void AttackColliderTurnon()
    {
        _polygonCollider2D.enabled = true;
    }

}
