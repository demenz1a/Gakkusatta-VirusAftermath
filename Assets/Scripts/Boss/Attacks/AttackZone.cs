using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public int _damageAmount = 20;
    [SerializeField] private Collider2D _collider;

    private bool canDamage = false;
    private Animator _animator;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _damageClip;
    [SerializeField] private AudioClip _spawnClip;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    public void StartZone()
    {
        
        _animator.Play("Intro");
    }

    public void AttackColliderTurnoff()
    {
        _collider.enabled = false;
        _audioSource.PlayOneShot(_damageClip);
    }
    public void SelfDestruct()
    {
        gameObject.SetActive(false);
    }

    private void AttackColliderTurnon()
    {
        _collider.enabled = true;
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
}

