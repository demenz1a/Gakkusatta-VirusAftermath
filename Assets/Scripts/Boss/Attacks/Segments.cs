using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Segments : MonoBehaviour
{
    public int _damageAmount = 3;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    private float _timer;
    private float _delay = 2.5f;
    private bool _isActive = false;

    void Start()
    {
        _timer = _delay;
    }

    void Update()
    {
        _timer -= Time.deltaTime;

        if ( _timer <= 1f )
        {
            ActiveAttack();
            _boxCollider2D.enabled = true;
        }

        if ( _timer <= 0f )
        {
            gameObject.SetActive(false);
        }
    }
    public bool IsAttack()
    {
        return _isActive;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Reiko reiko))
        {
            reiko.TakeDamage(_damageAmount);
        }

        if (collision.transform.TryGetComponent(out Minori minori))
        {
            minori.TakeDamage(_damageAmount);
        }
    }

    private void ActiveAttack()
    {
        _isActive = true;
    }
}
