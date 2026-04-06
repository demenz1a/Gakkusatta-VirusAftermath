using UnityEngine;

public class DAMAGE : MonoBehaviour
{
    [SerializeField] private int _damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Reiko reiko))
        {
            reiko.TakeDamageWithKB(gameObject, _damageAmount);
        }

        if (collision.transform.TryGetComponent(out Minori minori))
        {
            minori.TakeDamageWithKB(transform, _damageAmount);
        }
    }
}
