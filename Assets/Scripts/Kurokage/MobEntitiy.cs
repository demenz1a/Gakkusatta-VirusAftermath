using System;
using UnityEngine;
using UnityEngine.Audio;

//[RequireComponent(typeof(PolygonCollider2D))]
//[RequireComponent(typeof(MobAI1))]
public class MobEntity : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip death;
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    public static MobEntity _instance;
    private KnockBack _knockBack;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damageAmount;

    private int _currentHealth;

    [SerializeField] private KnockbackFeedback knockbackFeedback;
    [SerializeField] private PolygonCollider2D _polygonCollider2D;
    [SerializeField] private PolygonCollider2D _polygonCollider2DAttack;
    private EnemyAI1 _mobAI;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _mobAI = GetComponent<EnemyAI1>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    public void TakeDamageWithKB(GameObject reiko, int damage)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - damage);
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        Debug.Log(_currentHealth);
        if (knockbackFeedback != null)
            knockbackFeedback.PlayFeedback(reiko);

        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Debug.Log("Kurokage died!");
            CounterManager.Instance.AddKill();
            _polygonCollider2D.enabled = false;

            _mobAI.SetDeathState();

            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
    public void DestroyYourself()
    {
        Destroy(gameObject);
    }

    public void DeactiveYourself()
    {
        _polygonCollider2DAttack.enabled = false ;
    }

    public void ActivateCollider()
    {
        _polygonCollider2DAttack.enabled = true;
    }
    private void PlayHit()
    {
        audioSource.PlayOneShot(hit);
    }

    private void PlayDeath()
    {
        audioSource.PlayOneShot(death);
    }
}
