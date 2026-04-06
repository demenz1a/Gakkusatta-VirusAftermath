using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Collections.AllocatorManager;

public class Shot : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private AudioClip rejectedhit;
    [SerializeField] int _damageAmount;
    //[SerializeField] BossEntity mascheroni;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out BossEntity mascheroni))
        {
            // mascheroni.TakeDamageBoss(_damageAmount);
            audioSource.PlayOneShot(rejectedhit);
        }
        if (collision.CompareTag("Block"))
        {
            collision.GetComponent<Blocks>()?.OnHit();
        }
        if (collision.transform.TryGetComponent(out MobEntity mobentity))
        {
            mobentity.TakeDamage(_damageAmount);
        }
        if (collision.CompareTag("Figures"))
        {
            Debug.Log("Rejected");
            Transform bossTransform = GameObject.FindGameObjectWithTag("Boss")?.transform;
            collision.GetComponent<Figures>()?.OnHit(bossTransform);
        }
    }

    public void DestroyYourself()
    {
        Destroy(gameObject);
        Cursor.visible = true;
    }

}
