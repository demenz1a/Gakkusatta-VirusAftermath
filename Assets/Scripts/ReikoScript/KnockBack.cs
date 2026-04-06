using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float _knockBackForce;
    [SerializeField] private float _knockBackTimerMax;

    private float _knockBackTimer;

    private Rigidbody2D _rb;

    public bool IsGettingKnockedBack {get; private set;}

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _knockBackTimer -= Time.deltaTime;
        if (_knockBackTimer < 0)
        {
            StopKnockBackMovement();
        }
    }

    public void GetKnockedBack(Transform damageSource)
    {
        IsGettingKnockedBack = true;
        _knockBackTimer = _knockBackTimerMax;
        Vector2 difference = (transform.position - damageSource.position).normalized * _knockBackForce;
        _rb.AddForce(difference,ForceMode2D.Impulse);
    }
    public void StopKnockBackMovement()
    {
        _rb.linearVelocity = Vector2.zero;
        IsGettingKnockedBack = false;
    }


}

