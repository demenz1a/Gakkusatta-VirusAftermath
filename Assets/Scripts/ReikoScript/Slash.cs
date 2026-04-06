using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Slash : MonoBehaviour
{
    public int _damageAmount = 3;
    [SerializeField] private Collider2D collider2D;
    private float _timer;
    private float _delay = 0.5f;
    private bool _isActive = false;

    private void Start()
    {
        _timer = _delay;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 1f)
        {
            ActiveAttack();
            collider2D.enabled = true;
        }

        if (_timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
    public bool IsAttack()
    {
        return _isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out MobEntity mobentity))
        {
            mobentity.TakeDamage(_damageAmount);
        }
    }

    private void ActiveAttack()
    {
        _isActive = true;
    }
}
