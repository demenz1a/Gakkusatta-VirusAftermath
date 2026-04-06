using UnityEngine;

public class HealZone : MonoBehaviour
{
    public int _healAmount = 5;  
    private float healTimer = 1f;

    public float lifeTime = 5f;    

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Reiko reiko))
        {
            Scene1Manager.Instance.ReikoHP += _healAmount;
            Scene1Manager.Instance.ReikoHP = Mathf.Clamp(Scene1Manager.Instance.ReikoHP, 0, reiko.maxHealth);
        }

        if (collision.transform.TryGetComponent(out Minori minori))
        {
            Scene1Manager.Instance.MinoriHP += _healAmount;
            Scene1Manager.Instance.MinoriHP = Mathf.Clamp(Scene1Manager.Instance.MinoriHP, 0, minori.maxHealth);
        }
    }
}