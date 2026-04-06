using UnityEngine;

public class MinoriVisual : MonoBehaviour
{
    [SerializeField] private Minori _minori;
    [SerializeField] private PolygonCollider2D _polygonCollider2D;

    private Animator _animator;
    public SpriteRenderer spriteRenderer;

    private const string ISWALKING = "IsWalking";
    private const string ISHIT = "IsHit";
    private const string ISHEAL = "IsHeal";
    private const string ISDEAD = "IsDead";
    private const string ISATTACK = "IsAttack";
    private const string ISAIMING = "IsAiming";


    public bool IsFlipped => spriteRenderer.flipX;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _minori.OnTakeHit += _minori_OnTakeHit;
        _minori.OnDead += _minori_OnDead;
        _minori.OnSkill += _minori_OnHeal;
        _minori.onAimShot += _minori_OnAim;
        _minori.onGunShot += _minori_OnShot;
    }

    private void _minori_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ISHIT);
    }

    private void _minori_OnDead(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ISDEAD);
    }

    private void _minori_OnHeal(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ISHEAL);
    }

    private void _minori_OnAim(object sender, System.EventArgs e)
    {
        _animator.SetBool(ISAIMING,true);
    }

    private void _minori_OnShot(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(ISATTACK);
        _animator.SetBool(ISAIMING, false);
    }

    private void Update()
    {
        _animator.SetBool(ISWALKING, Minori._instance.IsRunning());
        //_animator.SetTrigger(ISDOGDE, Minori._instance.IsHeal());
    }

    public void AdjustPlayerFacingDirection(Vector2 dir)
    {
        if (dir.x < -0.01f)
            spriteRenderer.flipX = true;
        else if (dir.x > 0.01f)
            spriteRenderer.flipX = false;
    }

    public void SetAiming(bool state)
    {
        _animator.SetBool(ISAIMING, state);
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
